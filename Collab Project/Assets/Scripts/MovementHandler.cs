using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementHandler : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;

    private Animator anim;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("jumping", true);
        }
        else
        {
            anim.SetBool("jumping", false);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            SwitchGravity();
            
        }
    }
    
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalInput * speed, _rigidbody2D.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
    
    private void SwitchGravity()
    {
        _rigidbody2D.gravityScale *= -1;
        jumpForce *= -1;
        StartCoroutine(Rotate());
    }
    
    private IEnumerator Rotate()
    {
        float time = 0f;
        while (time < 1f)
        {
            transform.Rotate(0f, 0f, 180f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }
    
}
