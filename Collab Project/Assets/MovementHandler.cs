using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Vector2 _currVector;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currVector = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized * speed);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce),ForceMode2D.Impulse);
        }

        rb.AddForce(new Vector2(-rb.velocity.x * 0.5f, 0));
        
        if (Input.GetButtonDown("Fire1") && IsGrounded())
        {
            SwitchGravity();
        }
    }

    private void SwitchGravity()
    {
        rb.gravityScale*= -1;
        _currVector *= -1;
        jumpForce*=-1;
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, _currVector, 1.1f, LayerMask.GetMask("Ground"));
    }
    
}
