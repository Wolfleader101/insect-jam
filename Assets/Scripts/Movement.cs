using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public float Speed => speed;

    [SerializeField] private bool flyingType = false;
    public bool FlyingType => flyingType;

    [SerializeField] private float jumpHeight = 5f;
    public float JumpHeight => jumpHeight;

    [SerializeField] private BoxCollider2D floor;
    public BoxCollider2D Floor => floor;

    [SerializeField] private bool grounded = false;
    public bool Grounded => grounded;


    private Rigidbody2D _rb;
    private Vector2 _input;
    private bool _canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _canJump = true;
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (flyingType)
        {
            _rb.velocity = new Vector2(_input.x * speed, _input.y * speed);
            return;
        }

        if (_input.y > 0 && _canJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
            _canJump = false;
        }

        _rb.velocity = new Vector2(_input.x * speed, _rb.velocity.y);
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