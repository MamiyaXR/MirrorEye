using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float speed = 300.0f;

    public GameObject Platform;


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name + " ");
        if (collision.tag == "Unflipable" || collision.tag == "DeadLine")
            return;

        Platform.transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
