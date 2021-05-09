using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerDirection
{
    Left = -1,
    Right = 1
}
public class Movement : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInput = null;
    public InputActionAsset PlayerInput => playerInput;

    [SerializeField] private PlayerDirection facingDirection = PlayerDirection.Right;
    public PlayerDirection FacingDirection => facingDirection;

    private float speed = 10f;
    public float Speed => speed;

    [SerializeField] private bool flyingType = false;
    public bool FlyingType => flyingType;

    [SerializeField] private float jumpHeight = 5f;
    public float JumpHeight => jumpHeight;

    [SerializeField] private BoxCollider2D floor;
    public BoxCollider2D Floor => floor;

    [SerializeField] private bool grounded = false;
    public bool Grounded => grounded;

    [SerializeField] private Vector2 rayStartPos = new Vector2(0f, -0.51f);
    public Vector2 RayStartPos => rayStartPos;
    
    [SerializeField] private float rayDistance = 0.01f;
    public float RayDistance => rayDistance;

    public bool wallClimbing = false;


    private Rigidbody2D _rb;
    private PlayerInput _playerInput;
    private Vector2 _input;
    private bool _canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _canJump = true;
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions = playerInput;
    }

    private void Update()
    {
        if (_input.x < 0) facingDirection = PlayerDirection.Left;
        if (_input.x > 0) facingDirection = PlayerDirection.Right;
    }

    private void FixedUpdate()
    {
        _canJump = CheckCanJump();
        
        if (flyingType)
        {
            _rb.velocity = new Vector2(_input.x * speed, _input.y * speed);
            return;
        }

        if (wallClimbing)
        {
            _rb.velocity = new Vector2(0, _input.y * (speed * 0.75f)); // *0.75f to slightly decrease wall climbing speed
            return;
        }

        if (_input.y > 0 && _canJump)
        {
            _rb.velocity = new Vector2(0, jumpHeight);
            _canJump = false;
            return;
        }
        // Experimenting with not being able to air strafe - not sure if i like it
        //if (grounded || wallClimbing || _canJump) 
        
        // Allow for air strafing - can make some fun game mechanics
        _rb.velocity = new Vector2(_input.x * speed, _rb.velocity.y);

    }

    bool CheckCanJump()
    {
        Vector3 rayStart = (rayStartPos + (Vector2)transform.position);

        Vector3 RayDir = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, RayDir, rayDistance);
        
        // Does the ray intersect any objects excluding the player layer
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            Debug.DrawRay(rayStart, RayDir * rayDistance, Color.green);
            return true;
        }
        
        Debug.DrawRay(rayStart, RayDir * rayDistance, Color.white);
        return false;
    }
    
    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Floor"))
    //     {
    //         grounded = true;
    //     }
    //
    //     if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<Movement>().grounded)
    //     {
    //         _canJump = true;
    //     }
    //
    //     if (grounded)
    //     {
    //         _canJump = true;
    //     }
    // }
    //
    // private void OnCollisionExit2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Floor"))
    //     {
    //         grounded = false;
    //     }
    // }

    public void Move(InputAction.CallbackContext ctx)
    {
        _input = ctx.ReadValue<Vector2>();
    }
}