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
    public float slimeSpeedMultiplier = 2f;

    private float fasterSpeed;
    private float originalSpeed;
    private Tilemap slimeTrail,groundTilemap;
    public Tile slimeTile,slimeTileRotated;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private bool _hasSwitchedGravity;
    private Animator anim;
    
    
    private bool _slimeTrailActive;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        originalSpeed = speed;
        fasterSpeed = speed * slimeSpeedMultiplier;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _slimeTrailActive = false;
        _isGrounded = true;
        _hasSwitchedGravity = false;
        slimeTrail = GameObject.Find("Slime").GetComponent<Tilemap>();
        groundTilemap = GameObject.Find("GroundTilemap").GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (anim.GetBool("dying")) {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }
    if (Input.GetKeyDown(KeyCode.Space)&& _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        if (Input.GetButtonDown("Fire1") && !_hasSwitchedGravity)
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
        
        if ((jumpForce*Input.GetAxisRaw("Horizontal")) > 0)
        {
            anim.SetBool("facingRight", true);
        }
        else if ((jumpForce*Input.GetAxisRaw("Horizontal")) < 0)
        {
            anim.SetBool("facingRight", false);
        }
    }
    
    private void FixedUpdate()
    {
        if(anim.GetBool("dying"))
            return;
        float horizontalInput = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalInput * speed, _rigidbody2D.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _hasSwitchedGravity = false;
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
        StartCoroutine(Rotate(180));
        _isGrounded = false;
        _hasSwitchedGravity = true;
    }
    
    private IEnumerator Rotate(float targetRotation)
    {
        float totalRotation = 0f;

        while (totalRotation < targetRotation)
        {
            float rotationAmount = 360f * Time.deltaTime;
            transform.Rotate(0f, 0f, rotationAmount);
            totalRotation += rotationAmount;

            yield return null;
        }
        //if else statement to check if the gravity is positive or negative, then sets rotation to one of two results to make up for slight inaccuracies
        if (_rigidbody2D.gravityScale < 0)
            transform.rotation = Quaternion.Euler(Vector3.forward*180f);
        else
            transform.rotation = Quaternion.identity;

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