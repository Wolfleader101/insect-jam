using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    public Vector2 rayStartDist = new Vector2(0, 0.51f);
    public float rayDistance = 4f;
    public float curveAmount = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        Vector3 rayStart = (rayStartDist + (Vector2)transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        
        Vector3 RayDir = (mouseWorldPos - rayStart).normalized;
        
        RaycastHit2D hit = Physics2D.Raycast(rayStart, RayDir, rayDistance);
        
  
      
        
        // Does the ray intersect any objects excluding the player layer
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            Debug.Log($"Htting: {hit.collider.gameObject.name}");
            
            Debug.DrawRay(rayStart, RayDir * rayDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayStart, RayDir * rayDistance, Color.white);
        }
    }
}