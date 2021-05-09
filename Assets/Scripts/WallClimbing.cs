using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class WallClimbing : MonoBehaviour
{
   
    public Vector2 rayStartDist = new Vector2(0f, -0.5f);
    public float rayXOffset = 0.51f;
    public float rayDistance = 0.01f;

    private Movement _movement;
    private Rigidbody2D _rb;
    private float _prevGravScale;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody2D>();
        _prevGravScale = _rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        rayStartDist.x = (float) _movement.FacingDirection * rayXOffset;
        Vector2 upperRayStartDist = new Vector2(rayStartDist.x, Mathf.Abs(rayStartDist.y));
        Vector3 lowerRayStart = (rayStartDist + (Vector2)transform.position);
        Vector3 upperRayStart = (upperRayStartDist + (Vector2)transform.position);

        Vector3 RayDir = new Vector3((float)_movement.FacingDirection, 0 ,0).normalized;

        RaycastHit2D lowerHit = Physics2D.Raycast(lowerRayStart, RayDir, rayDistance, 1 << LayerMask.NameToLayer("Wall Climb"));
        RaycastHit2D upperHit = Physics2D.Raycast(upperRayStart, RayDir, rayDistance, 1 << LayerMask.NameToLayer("Wall Climb"));
        
        // Does the ray intersect any objects excluding the player layer
        if ((lowerHit.collider != null && lowerHit.collider.gameObject != gameObject) || (upperHit.collider != null && upperHit.collider.gameObject != gameObject))
        {
            _rb.gravityScale = 0f;
            _movement.wallClimbing = true;
            
            Debug.DrawRay(lowerRayStart, RayDir * rayDistance, Color.green);
            Debug.DrawRay(upperRayStart, RayDir * rayDistance, Color.green);
        }
        else
        {
            _rb.gravityScale = _prevGravScale;
            _movement.wallClimbing = false;
            Debug.DrawRay(lowerRayStart, RayDir * rayDistance, Color.white);
            Debug.DrawRay(upperRayStart, RayDir * rayDistance, Color.white);
        }
    }
}
