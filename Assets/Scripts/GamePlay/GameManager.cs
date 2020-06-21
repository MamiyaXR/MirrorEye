using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Rendering.PostProcessing;

public enum GameState : int
{
    Loading,
    InMenu,
    Gaming,
    Pause
}

public enum Direct : int
{
    Left = -1,
    Right = 1
}

public class GameManager : MonoBehaviour
{
    public LayerMask canBeFlipped;
    public ArrayList viewDatas;
    public bool isLSFliped;
    public float lsProgress;
    private float maxFlipDepth = 0f;
    private ArrayList flipObjects;
    //**************************************************************
    //
    //          单例模式构建
    //
    //**************************************************************
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    private void Awake()
    {
        _instance = this;
        gameData = GameData;
        viewDatas = new ArrayList();
        flipObjects = new ArrayList();
        maxFlipDepth = GameData.nFlipDepth;
        isLSFliped = false;
    }
    private void Start()
    {
        ReturnMenu();
    }
    private void Update()
    {
        GetFlipObjects();
        if (Input.GetButtonDown("Flip"))
            Flip(true);
        else if (Input.GetButtonUp("Flip"))
            Flip(false);
    }
    //**************************************************************
    //
    //          游戏状态机
    //
    //**************************************************************
    private GameState gameState;
    public GameState GameState { get => gameState; set => gameState = value; }
    public void ChangeGameState(string scene)
    {
        switch (scene)
        {
            case "LoadScene":
                GameState = GameState.Loading;
                break;
            case "MenuScene":
                GameState = GameState.InMenu;
                break;
            case "Level01":
            case "Level02":
            case "Level03":
            case "Level04":
            case "Level05":
                GameState = GameState.Gaming;
                break;
            default:
                break;
        }
    }

    //**************************************************************
    //
    //          暂停/继续
    //
    //**************************************************************
    public void PauseGame()
    {
        if (Instance.GameState == GameState.Gaming)
        {
            Time.timeScale = 0;
            GameState = GameState.Pause;
            UIManager.Instance.PushPanel(UIPanelType.PausePanel);
        }
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        GameState = GameState.Gaming;
    }
    //**************************************************************
    //
    //          保存/加载
    //
    //**************************************************************
    private GameData gameData;
    public GameData GameData {
        get
        {
            if (gameData == null)
            {
                if (Load())
                    return gameData;

                gameData = new GameData();
                gameData.stageCurrent = 0;
                gameData.masterVolume = 5;
                gameData.bgmVolume = 5;
                gameData.seVolume = 5;
                gameData.videoValue = 0;
                gameData.isFirst = true;
                gameData.hasTelescope = false;
                gameData.keyNum = 0;
                gameData.continousCount = 0;
                string[] levelTypeNames = Enum.GetNames(typeof(LevelType));
                foreach (string name in levelTypeNames)
                    gameData.levelDataDict.Add(name, new LevelData(name, false, true));

                string[] uiTypeNames = Enum.GetNames(typeof(UIPanelType));
                foreach (string name in uiTypeNames)
                {
                    gameData.panelStateDict.Add(name, false);
                }
            }
            return gameData;
        }
    }
    public void Save()
    {
        if (gameData == null)
            return;

        string path = Application.dataPath + "/save" + "/savedata.json";
        if (!Directory.Exists(Application.dataPath + "/save"))
            Directory.CreateDirectory(Application.dataPath + "/save");
        string saveJsonStr = JsonMapper.ToJson(gameData);
        StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create));
        sw.Write(saveJsonStr);
        sw.Close();
        //AssetDatabase.Refresh();
    }
    public bool Load()
    {
        string path = Application.dataPath + "/save" + "/savedata.json";
        if (!Directory.Exists(Application.dataPath + "/save"))
            Directory.CreateDirectory(Application.dataPath + "/save");
        StreamReader sr = new StreamReader(new FileStream(path, FileMode.OpenOrCreate));
        string saveJsonStr = sr.ReadToEnd();
        if (saveJsonStr == "")
        {
            sr.Close();
            return false;
        }
        sr.Close();
        gameData = JsonMapper.ToObject<GameData>(saveJsonStr);
        //SetGame(save);
        return true;
    }
    //**************************************************************
    //
    //          场景切换
    //
    //**************************************************************
    private string nextScene;
    public string NextScene { get => nextScene; }
    public Scene activeGameScene;
    public Direct playerDirect;

    public string GetSceneName(LevelType type)//           根据关卡列表获取Scene名
    {
        return GameData.levelDataDict[Enum.GetName(typeof(LevelType), type)].sceneName;
    }
    public string GetSceneName()//                         根据保存文件获取Scene名
    {
        return GameData.levelDataDict[Enum.GetName(typeof(LevelType), GameData.stageCurrent)].sceneName;
    }
    private void ToLoadScene()
    {
        BreakFlip();
        AudioManager.Instance.StopAllAudio();
        StartCoroutine(AsyncToLoadScene());
    }
    private IEnumerator AsyncToLoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Scenes/LoadScene", LoadSceneMode.Additive);
        yield return StartCoroutine("WaitLoad", operation);
        ChangeGameState("LoadScene");
    }
    private IEnumerator WaitLoad(AsyncOperation operation)
    {
        while(!operation.isDone)
            yield return 0;
    }
    /*----------------------------------------------------------------------------------------*/

    public void ReturnMenu()//                              返回Menu
    {
        nextScene = "MenuScene";
        ToLoadScene();
    }
    public void StartGame(LevelType type)//                 选关专用
    {
        playerDirect = Direct.Right;
        GameData.continousCount = 0;
        GameData.stageCurrent = (int)type;
        nextScene = GetSceneName(type);
        ToLoadScene();
    }
    public void StartGame()//                               开始游戏
    {
        nextScene = GetSceneName();
        ToLoadScene();
    }
    public void NextStage(Direct direct)//                  下一关
    {
        int dir = (int)direct;

        if (dir == 1 && !GameData.levelDataDict[Enum.GetName(typeof(LevelType), GameData.stageCurrent)].isClear)
            GameData.levelDataDict[Enum.GetName(typeof(LevelType), GameData.stageCurrent)].isClear = true;

        if (GameData.stageCurrent + dir < 0)
            GameData.stageCurrent = GameData.levelDataDict.Count - 1;
        else if (GameData.stageCurrent + dir >= GameData.levelDataDict.Count)
            GameData.stageCurrent = 0;
        else
            GameData.stageCurrent += dir;

        if (direct != playerDirect)
            gameData.continousCount = 0;
        else
            gameData.continousCount += dir;
        playerDirect = direct;

        StartGame();
    }
    //**************************************************************
    //
    //          Camera控制
    //
    //**************************************************************
    private bool isFlipping = false;
    public bool IsFlipping { get => isFlipping; }
    public Vector3 saveOriginPoint;
    public void BreakFlip()
    {
        if (IsFlipping)
            Flip(false);
    }
    public void FreedomMove()
    {
        CameraMove cm = Camera.main.GetComponent<CameraMove>();
        if (cm != null)
            cm.FreedomMove();
    }
    private void GetFlipObjects()
    {
        if (IsFlipping)
            return;

        if (GameState == GameState.Gaming)
            maxFlipDepth = GameData.nFlipDepth;
        else
            maxFlipDepth = GameData.uiFlipDepth;

        if (flipObjects.Count != 0)
            flipObjects.Clear();

        foreach (ViewData viewData in viewDatas)
        {
            Collider2D[] colliders = viewData.GetObjects(canBeFlipped, maxFlipDepth, maxFlipDepth);
            foreach (Collider2D collider in colliders)
                if(!flipObjects.Contains(collider))
                    flipObjects.Add(collider);
        }
    }
    private void Flip(bool state)
    {
        if (IsFlipping == state)
            return;

        Camera cam = Camera.main;
        FindObjectOfType<RippleEffect>().Emit(cam.WorldToViewportPoint(new Vector3(cam.transform.position.x, cam.transform.position.y, 0)));

        Vector3 originPoint;
        if (!IsFlipping)
        {
            originPoint = new Vector3(cam.transform.position.x, cam.transform.position.y, maxFlipDepth);
            saveOriginPoint = originPoint;
        }
        else
        {
            originPoint = saveOriginPoint;
        }

        foreach (Collider2D collider in flipObjects)
        {
            if (collider == null)
                continue;

            Vector3 targetPos;
            FlipCallback cb = collider.GetComponent<FlipCallback>();
            if (collider.gameObject.layer == LayerMask.NameToLayer("FlipUI"))
            {
                targetPos = new Vector3(cam.transform.position.x, cam.transform.position.y, originPoint.z);
            } else
            {
                targetPos = new Vector3(originPoint.x * 2 - collider.transform.position.x,
                                            originPoint.y * 2 - collider.transform.position.y,
                                            originPoint.z * 2 - collider.transform.position.z);
            }
            
            collider.transform.RotateAround(originPoint, cam.transform.up, 180f);
            collider.transform.position = targetPos;

            if (cb == null)
            {
                FlipCallback[] cbs = collider.GetComponentsInChildren<FlipCallback>();
                if(cbs != null)
                    for (int i = 0; i < cbs.Length; i++)
                        cbs[i].OnFlipCallback();
            }
            else
            {
                cb.OnFlipCallback();
            }
        }
        //isFlipping = !isFlipping;
        isFlipping = state;
    }
    //**************************************************************
    //
    //          Player管理
    //
    //**************************************************************
    private GameObject _player;
    public GameObject Player
    {
        get
        {
            if(_player == null)
            {
                _player = Instantiate(Resources.Load<GameObject>(GameData.playerPath) as GameObject);
                DontDestroyOnLoad(_player);
            }
            return _player;
        }
    }
    public void SetPlayer(Vector3 position, Transform parent)
    {
        Player.transform.position = position;
        Player.transform.SetParent(parent);
    }
    public void SetPlayerCanMove(bool canMove)
    {
        _player.GetComponent<PlayerMove>().canMove = canMove;
    }
    //**************************************************************
    //
    //          全局光照管理
    //
    //**************************************************************
    private UnityEngine.Experimental.Rendering.Universal.Light2D _light;
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light
    {
        get
        {
            if(_light == null)
            {
                _light = Instantiate(Resources.Load<GameObject>(GameData.lightPath) as GameObject).GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                DontDestroyOnLoad(_light.gameObject);
                FixLight();
            }
            return _light;
        }
    }
    public void FixLight()
    {
        float count = GameData.levelDataDict.Count;
        float current = GameData.stageCurrent;
        if(GameData.panelStateDict[Enum.GetName(typeof(UIPanelType), UIPanelType.VideoPanel)])
            Light.intensity = Mathf.Clamp(current / (count - 2f), 0f, 1f);
        else
            Light.intensity = Mathf.Clamp(1f - current / (count - 2f), 0f, 1f);
    }
    //**************************************************************
    //
    //          画面后期处理
    //
    //**************************************************************
    private GameObject postProcessing;
    private PostProcessProfile profile;
    public PostProcessProfile Profile
    {
        get
        {
            if(profile == null)
            {
                postProcessing = Instantiate(Resources.Load<GameObject>(GameData.postProcessingPath) as GameObject);
                DontDestroyOnLoad(postProcessing);
                profile = postProcessing.GetComponent<PostProcessVolume>().profile;
            }
            return profile;
        }
    }
    public void FixBrightness()
    {
        Profile.GetSetting<ColorGrading>().brightness.value = (float)GameData.videoValue;
    }
    //**************************************************************
    //
    //          退出游戏销毁数据
    //
    //**************************************************************
    public void DestroyData()
    {
        Destroy(_player);
        Destroy(_light);
        Destroy(postProcessing);
        //Destroy(this);
    }
    //**************************************************************
    //
    //          Debug
    //
    //**************************************************************
    //private void OnGUI()
    //{
    //    GUILayout.TextField("IsFlipping = " + IsFlipping);
    //    GUILayout.TextField("continueCount = " + gameData.continousCount);
    //    foreach (Collider2D collider in flipObjects)
    //    {
    //        if (collider != null)
    //            GUILayout.TextField(collider.name);
    //    }
    //}
}
