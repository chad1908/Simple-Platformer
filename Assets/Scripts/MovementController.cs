using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Animator))]

public class MovementController : MonoBehaviour
{
    public float speed = 14f;
    public float accel = 6f;
    private Vector2 input;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Gets these values from unity
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Jump");

        //Determines which way the sprite should be facing according to the x axis
        if (input.x > 0f)
        {
            sr.flipX = false;
        }
        else if (input.x < 0f)
        {
            sr.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        //This will be used to hold ground and air velocity values.
        var acceleration = accel;
        var xVelocity = 0f;

        //Sets velocity to 0 if there is no input to prevent the player from sliding.
        if (input.x == 0)
        {
            xVelocity = 0f;
        }
        else
        {
            xVelocity = rb.velocity.x;
        }
        //Adds acceleration to the player so they can move.
        rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
        //Controls the player velocity.
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

}
