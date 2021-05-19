using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Animator))]

public class MovementController : MonoBehaviour
{
    public bool isJumping;
    public bool hasDash;
    public float jumpSpeed = 8f;
    public float jumpDurationThreshold = 0.25f;
    public float speed = 14f;
    public float accel = 6f;
    public float airAccel = 3f;
    public float jump = 14f;
    private float jumpDuration;
    private float rayCastLengthCheck = 0.05f;
    private float width;
    private float height;
    private Vector3 input;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public Animator animator;

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

    public bool isWallLeftOrRight()
    {
        bool wallOnLeft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        bool wallOnRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);

        if (wallOnLeft || wallOnRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isTouchingGroundOrWall()
    {
        if (PlayerIsGrounded() || isWallLeftOrRight())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //return an int based on whether the wall is to the left or right of the players position
    public int getWallDirection()
    {
        bool isWallLeft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        bool isWallRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);

        if (isWallLeft)
        {
            return -1;
        }
        else if (isWallRight)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void Update()
    {
        //Gets these values from unity.
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Jump");

        animator.SetFloat("Speed", Mathf.Abs(input.x));

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
        if (input.y >= 1f)
        {
            jumpDuration += Time.deltaTime;
            animator.SetBool("isJumping", true);
            Debug.Log("Anim1");
        }
        else
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
            Debug.Log("Anim2");
            jumpDuration = 0f;
        }

        //Checks to see if the player is grounded or jumping.
        if (PlayerIsGrounded() && isJumping == false)
        {
            if (input.y > 0f)
            {
                isJumping = true;
            }
            animator.SetBool("isOnWall", false);
            Debug.Log("Anim3");
        }

        //If the player holds jump longer than the threshold the input is set to 0.
        if (jumpDuration > jumpDurationThreshold) input.y = 0f;

        //When leftShit is pressed the player is moved 2 units in the direction of the input along the x axis.
        if (hasDash == false)
        {
           
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                float dashDistance = 2f;
                transform.position += input * dashDistance;
                Debug.Log("dash!");
            }
        }
    }

    private void FixedUpdate()
    {
        //This will be used to hold ground and air velocity values.
        //var acceleration = accel;
        var xVelocity = 0f;
        var yVelocity = 0f;

        var acceleration = 0f;
        if(PlayerIsGrounded())
        {
            acceleration = accel;
        }
        else
        {
            acceleration = airAccel;
        }

        //Normalise the xVelocity to 0 over time if there is no input to prevent the player from sliding.
        if (PlayerIsGrounded() && input.x == 0)
        {
            xVelocity = 0f;
        }
        else
        {
            xVelocity = rb.velocity.x;
        }

        if (isTouchingGroundOrWall() && input.y == 1)
        {
            yVelocity = jump;
        }
        else
        {
            yVelocity = rb.velocity.y;
        }

        //Adds acceleration to the player so they can move.
        rb.AddForce(new Vector3(((input.x * speed) - rb.velocity.x) * acceleration, 0));

        //Controls the player velocity.
        rb.velocity = new Vector3(xVelocity, yVelocity);

        if (isWallLeftOrRight() && !PlayerIsGrounded() && input.y == 1)
        {
            rb.velocity = new Vector2(-getWallDirection() * speed * 0.75f, rb.velocity.y);
            animator.SetBool("isOnWall", false);
            animator.SetBool("isJumping", true);
            Debug.Log("Anim4");
        }
        else if (!isWallLeftOrRight())
        {
            animator.SetBool("isOnWall", false);
            animator.SetBool("isJumping", true);
            Debug.Log("Anim5");
        }
        if (isWallLeftOrRight() && !PlayerIsGrounded())
        {
            animator.SetBool("isOnWall", true);
            Debug.Log("Anim6");
        }

        //
        if (isJumping && jumpDuration < jumpDurationThreshold)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
}
