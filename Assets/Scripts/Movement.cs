using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public float Speed => speed;
    
    [SerializeField] private float jumpHeight = 5f;
    public float JumpHeight => jumpHeight;

    private BoxCollider2D floor;
    public BoxCollider2D Floor => floor;
    
    [SerializeField] private bool grounded = false;
    public bool Grounded => grounded;
    
    private Rigidbody2D _rb;
    private float _moveDirection;
    private bool _canJump = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
