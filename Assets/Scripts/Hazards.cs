﻿using System.Collections;
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
            //instantiate the death particles at the point of collision.
            Instantiate(playerDeathParticles, collision.contacts[0].point, Quaternion.identity);

            //Destroy the colliding object
            Destroy(collision.gameObject);
        }
    }
}
