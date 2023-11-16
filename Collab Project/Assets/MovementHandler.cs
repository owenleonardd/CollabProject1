using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Vector2 _currVector;
    private bool _canSwitch;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currVector = new Vector2(0, -1);
        _canSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canSwitch)
        {
            _canSwitch = IsGrounded();
        }
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
        }

        rb.AddForce(new Vector2(-rb.velocity.x * 0.5f, 0));
        
        if (Input.GetButtonDown("Fire1") && _canSwitch)
        {
            SwitchGravity();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), 0) * speed);
    }

    private void SwitchGravity()
    {
        rb.gravityScale*= -1;
        _currVector *= -1;
        jumpForce *= -1;
        _canSwitch = false;
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, _currVector, 1, LayerMask.GetMask("Ground"));
    }
    
}
