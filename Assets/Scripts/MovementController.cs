using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Animator))]

public class MovementController : MonoBehaviour
{
    public bool isJumping;
    public float jumpSpeed = 8f;
    public float jumpDurationThreshold = 0.25f;
    public float speed = 14f;
    public float accel = 6f;
    private float jumpDuration;
    private float rayCastLengthCheck = 0.05f;
    private float width;
    private float height;
    private Vector2 input;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //Grabs the width and height of the sprite and adds some padding.
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
    }

    public bool PlayerIsGrounded()
    {
        //casts 3 rays under the player sprite to make sure the base of the sprite is covered, and gives the sprite realistic boundaries.
        bool groundCheck1 = Physics2D.Raycast(new Vector2
            (transform.position.x, transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        bool groundCheck2 = Physics2D.Raycast(new Vector2
            (transform.position.x + (width - 0.2f),
            transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        bool groundCheck3 = Physics2D.Raycast(new Vector2
            (transform.position.x - (width - 0.2f),
            transform.position.y - height), -Vector2.up, rayCastLengthCheck);

        //Returns whether the raycast is being triggered or not.
        if (groundCheck1 || groundCheck2 || groundCheck3)
        {
            return true;
        }
        else
        {
            return false;
        }   
    }

    private void Update()
    {
        //Gets these values from unity.
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Jump");

        //Determines which way the sprite should be facing according to the x axis.
        if (input.x > 0f)
        {
            sr.flipX = false;
        }
        else if (input.x < 0f)
        {
            sr.flipX = true;
        }

        //as long as jump is held time.deltaTime will increment jump duration.
        if (input.y >=1f)
        {
            jumpDuration += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpDuration = 0f;
        }

        //Checks to see if the player is grounded or jumping.
        if (PlayerIsGrounded() && isJumping == false)
        {
            if (input.y > 0f)
            {
                isJumping = true;
            }
        }

        //If the player holds jump longer than the threshold the input is set to 0.
        if (jumpDuration > jumpDurationThreshold) input.y = 0f;
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

        //
        if (isJumping && jumpDuration < jumpDurationThreshold)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
}
