﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{ 
    [SerializeField] private float speed = 10f;
    public float Speed => speed;
    
    [SerializeField] private float jumpHeight = 5f;
    public float JumpHeight => jumpHeight;

    [SerializeField] private BoxCollider2D floor;
    public BoxCollider2D Floor => floor;
    
    [SerializeField] private bool grounded = false;
    public bool Grounded => grounded;
    
    
    private Rigidbody2D _rb;
    private float _moveDirection;
    private Vector2 _input;
    private bool _canJump = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _canJump = true;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        // rb.AddRelativeForce(new Vector2(moveDirection * speed, rb.velocity.y));

        if (_input.x > 0 && _canJump)
        {
            _moveDirection = 1;
        }
        else if (_input.x < 0 && _canJump)
        {
            _moveDirection = -1;
        }
        else
        {
            _moveDirection = 0;
        }

        if (_input.y > 0 && _canJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
            _canJump = false;
        }
        
        _rb.velocity = new Vector2(_moveDirection * speed, _rb.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
        if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<Movement>().grounded)
        { 
            _canJump = true;
        }
        
        if (grounded)
        {
            _canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }
    }
    
    public void Move(InputAction.CallbackContext ctx)
    {
        _input = ctx.ReadValue<Vector2>();
    }

}
