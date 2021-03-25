using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 5f;
    public float jumpForce = 5f;
    public float distance;
    bool isGrounded;
    bool looksToRight = true;
   public LayerMask whatIsGround;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Start()
    {

    }

    void Update()
    {
        Move();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Move()
    {
        isGrounded = Physics2D.Raycast(transform.position, -Vector2.up, distance, whatIsGround);

        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (moveX > 0 && !looksToRight)
            Flip();

        if (moveX < 0 && looksToRight)
            Flip();
    }

    private void Flip()
    {
        float scaleX = transform.localScale.x;
        transform.localScale = new Vector3(-1 * scaleX, 1, 1);
        looksToRight = !looksToRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -Vector2.up * distance);
    }
}
