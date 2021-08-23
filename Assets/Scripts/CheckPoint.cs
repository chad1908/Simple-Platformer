using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private gameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("pos set");
            gm.lastCheckPointPos = transform.position;
        }
    }
}
