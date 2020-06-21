using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    private static readonly string[] seList = { "btnDownSound", "btnMoveSound", "moveSound", "openMenuSound" };
    private static readonly string[] bgmList = { "menu_bgm", "level00_bgm", "level02_bgm_1", "level02_bgm_2", "level05_bgm" };

    [Header("Clips")]
    public AudioClip[] seClipList = new AudioClip[seList.Length];
    public AudioClip[] bgmClipList = new AudioClip[bgmList.Length];

    [Header("Mixer Group")]
    public AudioMixer audioMixer;
    public AudioMixerGroup bgmGroup;
    public AudioMixerGroup seGroup;

    private Dictionary<string, AudioSource> audioDict = new Dictionary<string, AudioSource>();

    private static AudioManager _instance;
    public static AudioManager Instance { get => _instance; }

    private void Awake()
    {
        _instance = this;
        for(int i = 0; i < seList.Length; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = seClipList[i];
            audioSource.outputAudioMixerGroup = seGroup;
            audioSource.playOnAwake = false;
            audioDict.Add(seList[i], audioSource);
        }
        for (int i = 0; i < bgmList.Length; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = bgmClipList[i];
            audioSource.outputAudioMixerGroup = bgmGroup;
            audioSource.playOnAwake = false;
            audioDict.Add(bgmList[i], audioSource);
        }
    }
    private void Start()
    {
        FixedMasterVolume();
        FixedBgmVolume();
        FixedSeVolume();
    }
    public bool Play(string audioStr)
    {
        if (audioDict[audioStr] == null)
            return false;
        audioDict[audioStr].Play();
        return true;
    }
    public bool Play(string audioStr, bool isLoop)
    {
        if (audioDict[audioStr] == null)
            return false;
        audioDict[audioStr].loop = isLoop;
        audioDict[audioStr].Play();
        return true;
    }
    public bool Play(string audioStr, bool isLoop, float delay)
    {
        if (audioDict[audioStr] == null)
            return false;
        audioDict[audioStr].loop = isLoop;
        audioDict[audioStr].PlayDelayed(delay);
        return true;
    }
    public void Stop(string bgmName)
    {
        if (audioDict[bgmName] == null)
            return;

        if (audioDict[bgmName].isPlaying)
            audioDict[bgmName].Stop();
    }
    public void StopAllAudio()
    {
        for(int i = 0; i < seList.Length; i++)
        {
            if (audioDict[seList[i]].isPlaying)
                audioDict[seList[i]].Stop();
        }
        for (int i = 0; i < bgmList.Length; i++)
        {
            if (audioDict[bgmList[i]].isPlaying)
                audioDict[bgmList[i]].Stop();
        }
    }
    public string GetBgmNameByLevelType(LevelType level)
    {
        int index = -1;
        switch(level)
        {
            case LevelType.Level01:
                index = 1;
                break;
            case LevelType.Level02:
                index = 1;
                break;
            case LevelType.Level03:
                if (GameManager.Instance.GameData.panelStateDict[Enum.GetName(typeof(UIPanelType), UIPanelType.AudioPanel)])
                    index = 2;
                else
                    index = 3;
                break;
            case LevelType.Level04:
                index = 1;
                break;
            case LevelType.Level05:
                index = 4;
                break;
            default:
                break;
        }
        if (index == -1)
            return "";

        return bgmList[index];
    }
    public bool FixedMasterVolume()
    {
        return audioMixer.SetFloat("masterVolume", (float)GameManager.Instance.GameData.masterVolume);
    }
    public bool FixedBgmVolume()
    {
        return audioMixer.SetFloat("bgmVolume", (float)GameManager.Instance.GameData.bgmVolume);
    }
    public bool FixedSeVolume()
    {
        return audioMixer.SetFloat("seVolume", (float)GameManager.Instance.GameData.seVolume);
    }
}
