using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    public GameObject playerDeathParticles;
    private SpriteRenderer spriteRenderer;

    //Caches a reference to the sprite renderer when the script starts.
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check to make sure the colliding object is the player
        if (collision.transform.tag == "Player")
        {
            //Destroy the colliding object
            Destroy(collision.gameObject);

            //sets the player jump to 0 on collision to prevent a bug that allowed the player to jump on hazards. (this simply allows time for the gameObject to be destroyed)
            //GameObject player = GameObject.Find("Player");
            //MovementController movementController = player.GetComponent<MovementController>();
            //movementController.jump = 0;
            //Debug.Log("jump set to 0");

            //instantiate the death particles at the point of collision.
            //Instantiate(playerDeathParticles, collision.contacts[0].point, Quaternion.identity);

            Instantiate(playerDeathParticles, transform.position, transform.rotation);

            gameManager.GMinstance.restartLevel(1.25f);
        }
    }
}
