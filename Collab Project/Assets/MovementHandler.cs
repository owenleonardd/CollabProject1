using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float switchCooldown = 1f;
    private Vector2 _currVector = new Vector2(0, -1);
    private bool _canSwitchGravity = true;

    private Rigidbody2D rb;
    private float _timeSinceLastSwitch = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastSwitch += Time.deltaTime;
        if (_timeSinceLastSwitch >= switchCooldown || IsGrounded())
        {
            _canSwitchGravity = true;
        }
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized * speed);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }

        rb.AddForce(new Vector2(-rb.velocity.x * 0.5f, 0));
        
        if (Input.GetButtonDown("Fire1") && _canSwitchGravity)
        {
            switchGravity();
        }
    }

    private void switchGravity()
    {
        rb.gravityScale*= -1;
        _currVector *= -1;
        jumpForce*=-1;
        _canSwitchGravity = false;
        _timeSinceLastSwitch = 0;
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, _currVector, 1.1f, LayerMask.GetMask("Ground"));
    }
    
}
