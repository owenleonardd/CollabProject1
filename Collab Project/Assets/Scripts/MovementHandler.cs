using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MovementHandler : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Tilemap slimeTrail;
    public Tile slimeTile;

    private Tile _slimeTileRotated;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private bool _slimeTrailActive;
    public float slimeMultiplier;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _slimeTrailActive = false;

    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            SwitchGravity();
            
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            _slimeTrailActive = !_slimeTrailActive;
        }
    }
    
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalInput * speed, _rigidbody2D.velocity.y);
        if (_slimeTrailActive && _isGrounded)
        {
            //need to offset the tile row by 1 to get the correct tile
            if(jumpForce > 0)
                slimeTrail.SetTile(slimeTrail.WorldToCell(transform.position + new Vector3(0f, -1f, 0f)), slimeTile);
            else
                slimeTrail.SetTile(slimeTrail.WorldToCell(transform.position + new Vector3(0f, 1f, 0f)), slimeTile);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SlimeTrail"))
        {
            speed *= slimeMultiplier;
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SlimeTrail"))
        {
            speed /= slimeMultiplier;
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
        // speed *= -1;
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
