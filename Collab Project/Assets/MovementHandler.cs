using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float jumpForce = 10f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         float x = Input.GetAxisRaw("Horizontal");
    
         Vector2 moveDir = new Vector2(x, 0).normalized;
    
         rb.velocity = new Vector2(moveDir.x * moveSpeed * Time.deltaTime, rb.velocity.y);
    
         if (Input.GetKeyDown(KeyCode.UpArrow))
         {
             if (IsGrounded())
             {
                 rb.velocity = new Vector2(rb.velocity.x, jumpForce);
             }
         }
         
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Ground"));
        return raycastHit.collider != null;
    }
}
