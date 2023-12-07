using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MovementHandler : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 7f;
    public float slimeTrailCooldown = 10f;
    public float slimeTrailDuration = 5f;
    public float slimeSpeedMultiplier;

    private float fasterSpeed;
    private float originalSpeed;
    private Tilemap slimeTrail,groundTilemap;
    public Tile slimeTile,slimeTileRotated;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    
    
    private bool _slimeTrailActive;
    

    private void Awake()
    {
        originalSpeed = speed;
        fasterSpeed = speed * slimeSpeedMultiplier;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _slimeTrailActive = false;
        slimeTrail = GameObject.Find("Slime").GetComponent<Tilemap>();
        groundTilemap = GameObject.Find("GroundTilemap").GetComponent<Tilemap>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        if (Input.GetButtonDown("Fire1") && _isGrounded)
        {
            SwitchGravity();
            
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            if (!_slimeTrailActive)
            {
                StartCoroutine(DoSlimeTrail());
                _slimeTrailActive = true;
                StartCoroutine(DoSlimeTrailCooldown());
            }
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SlimeTrail"))
        {
            speed = fasterSpeed;
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SlimeTrail"))
        {
            speed = originalSpeed;
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
        _isGrounded = false;
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

    private IEnumerator DoSlimeTrail()
    {
        float time = 0f;
        while(time < slimeTrailDuration)
        {
            //need to offset the tile row by 1 to get the correct tile
            if(jumpForce > 0 && groundTilemap.HasTile(groundTilemap.WorldToCell(transform.position + new Vector3(0f, -0.5f, 0f))))
                slimeTrail.SetTile(slimeTrail.WorldToCell(transform.position + new Vector3(0f, -0.5f, 0f)), slimeTile);
            else if(groundTilemap.HasTile(groundTilemap.WorldToCell(transform.position + new Vector3(0f, 0.5f, 0f))))
                slimeTrail.SetTile(slimeTrail.WorldToCell(transform.position + new Vector3(0f, 0.5f, 0f)), slimeTileRotated);
            time += Time.deltaTime;
            yield return null;
        }
        slimeTrail.ClearAllTiles();
        speed = originalSpeed;
    }
    
    private IEnumerator DoSlimeTrailCooldown()
    {
        float time = 0f;
        while(time < slimeTrailCooldown)
        {
            time += Time.deltaTime;
            yield return null;
        }
        _slimeTrailActive = false;
    }
    
    
    
}