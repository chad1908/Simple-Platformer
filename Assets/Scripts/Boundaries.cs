using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //simply sets the player jump to 0 when collising with a boundary. (this is used to keep the player in the game scene)
            GameObject player = GameObject.Find("Player");
            MovementController movementController = player.GetComponent<MovementController>();
            movementController.jump = 0;
            Debug.Log("jump set to 0");

            StartCoroutine(resetJump(2));
            Debug.Log("jump set to 14");           
        }
    }

    private IEnumerator resetJump(float waitTime = 2f)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject player = GameObject.Find("Player");
        MovementController movementController = player.GetComponent<MovementController>();
        movementController.jump = 14f;
        Debug.Log("jump set to 14");
    }
}
