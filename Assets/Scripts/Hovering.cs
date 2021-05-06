using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hovering : MonoBehaviour
{
    [SerializeField] private float hoverRate = 10f;
    public float HoverRate => hoverRate;
    

    [SerializeField] private float upperHeight = 2f;
    public float UpperHeight => upperHeight;

    [SerializeField] float lowerHeight = 1.2f;
    public float LowerHeight => lowerHeight;

    private Vector2 _input;
    private float _speed;
    


    // Start is called before the first frame update
    void Start()
    {
        _speed = GetComponent<Movement>().Speed;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.localPosition = new Vector3 (transform.localPosition.x, 
                                        Mathf.Lerp(lowerHeight, +upperHeight, (Mathf.Sin(Time.time * hoverRate) + 1) / 2f), 
                                          transform.position.z);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _input = ctx.ReadValue<Vector2>();
    }
}
