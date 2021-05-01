﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour
{
    public float speed = 4;
    public float jumpHeight = 5;
    public BoxCollider2D floor;

    private Rigidbody2D rb;

    private float moveDirection;
    private bool canJump = false;
    public bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && canJump)
        {
            moveDirection = 1;
        } else if (Input.GetKey(KeyCode.LeftArrow) && canJump)
        {
            moveDirection = -1;
        }
        else
        {
            moveDirection = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            canJump = false;
        }
    }

    private void FixedUpdate()
    {
        // rb.AddRelativeForce(new Vector2(moveDirection * speed, rb.velocity.y));
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
        if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerOneController>().grounded)
        { 
            canJump = true;
        }
        
        if (grounded)
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }
    }
}
