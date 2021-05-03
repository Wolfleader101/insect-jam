using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hovering : MonoBehaviour
{
    [SerializeField] private float hoverRate = 10f;
    public float HoverRate => hoverRate;

    [SerializeField] [Range(0, 1)] private float hoverRange = 0.25f;
    public float HoverRange => hoverRange;

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
    void Update()
    {
        // TODO
        /*
         * Make lower height decrease when going down
         * Make Upper height decrease when going down
         * make lower height increase when going up
         *
         * Fix overall hovering system
         * 
         */
        if (_input.y < 0)
        {
            upperHeight += _input.y * (_speed * Time.deltaTime);
        }
        // EVENTUALLY USE THIS TO CHANGE LOWER HEIGHT
        //lowerHeight += _input.y * (_speed * Time.deltaTime);
        transform.position = ClampHeight((Vector3.up * Mathf.Cos(Time.time * hoverRate) * ClampRange(hoverRange)) + transform.position);
    }
    
    
    private float ClampRange(float value)
    {
        if (transform.position.y > upperHeight)
            upperHeight = transform.position.y;
        if (transform.position.y < lowerHeight)
            lowerHeight = transform.position.y - lowerHeight;
        if (upperHeight < lowerHeight)
            upperHeight = lowerHeight + 0.1f;
        if (lowerHeight > upperHeight)
            lowerHeight = upperHeight + 0.1f;
       
        value = ((upperHeight + lowerHeight) / 2) - 0.25f;
        value *= hoverRange;
        value *= 0.01f;
 
        return value;
    }

    private Vector3 ClampHeight(Vector3 value)
    {
        if (value.y < lowerHeight)
            value.y = lowerHeight;
        if (value.y > upperHeight)
            value.y = upperHeight;
        return value;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _input = ctx.ReadValue<Vector2>();
    }
}
