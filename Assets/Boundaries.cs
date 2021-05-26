using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
            MovementController movementController = player.GetComponent<MovementController>();
            movementController.jump = 0;
            Debug.Log("jump set to 0");
        }
    }
}
