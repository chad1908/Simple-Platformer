using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashAbility : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
            MovementController movementController = player.GetComponent<MovementController>();
            movementController.hasDash = true;
            Debug.Log("pickup hit");
            Destroy(gameObject);
        }
    }
}
