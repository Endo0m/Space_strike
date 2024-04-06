using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float horizontalInput;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private bool isGrounded = false;
    private float groundCheckDistance = 0.5f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Transform upperBody; // Ссылка на Transform объекта, который необходимо повернуть
    private Animator animator;
    private Rigidbody2D rb;
    private void Awake()
    {
      rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        CheckGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

        //Rotate body
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-.2f, .2f, .2f); // Поворот влево
        }
        else
        {
            transform.localScale = new Vector3(.2f, .2f, .2f); // Поворот вправо
        }
        //Rotate
       
    }


    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("vSpeed", rb.velocity.y);
    }

    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        animator.SetBool("isJumping", !isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);

    }
   
}