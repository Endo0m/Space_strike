using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpForce = 600.0f;
    public Transform groundCheck;
    public Transform jumpCheck;
    public LayerMask groundLayer;
    private GameObject player;
    public float checkingRadius = 0.2f;
    public float shootingRange = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private EnemyShooter enemyShooter;
    private float groundCheckRadius = 0.2f;
    private float stoppingDistance = 10f;
    private float lastJumpTime;
    private const float jumpCooldown = 2.0f; // 2 секунды между прыжками


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyShooter = GetComponentInChildren<EnemyShooter>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void MoveAndJump()
    {
        if (player != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            float horizontalMove = 0f;
            Vector2 targetPosition = new Vector2(player.transform.position.x, transform.position.y);
            float distanceToPlayer = Vector2.Distance(rb.position, player.transform.position);

            if (distanceToPlayer > stoppingDistance)
            {
                horizontalMove = (targetPosition.x > transform.position.x) ? 1 : -1;
            }
            else if (distanceToPlayer <= shootingRange)
            {
                horizontalMove = (targetPosition.x < transform.position.x) ? 1 : -1;
            }

            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

            if (isGrounded && ShouldJump() && Time.time - lastJumpTime >= jumpCooldown)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                lastJumpTime = Time.time; 
            }
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
    }



    void Update()
    {
        if (player != null)
        {
            if (!isGrounded)
            {
                rb.gravityScale = 30f;
            }
            else
            {
                rb.gravityScale = 1f;
            }

            if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(.2f, .2f, .2f);
                enemyShooter.transform.localScale = new Vector3(-1f, -1f, 1f);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-.2f, .2f, .2f);
                enemyShooter.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            MoveAndJump();
            CheckShoot();
            AimAtPlayer();
        }
    }

    private void FixedUpdate()
    {
        Animate();
        
    }
    public void AimAtPlayer()
    {
        if (player != null)
        {
            Vector3 difference = player.transform.position - enemyShooter.transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            enemyShooter.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 180);
        }
    }


    bool ShouldJump()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        // Увеличьте дальность проверки, если нужно
        float checkDistance = 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(jumpCheck.position, direction, checkDistance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    void CheckShoot()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.GetComponent<Transform>().position) <= shootingRange)
            {
                enemyShooter.Shoot();
            }
        }
    }
    void Animate()
    {
        animator.SetFloat("vSpeed", Mathf.Abs(rb.velocity.y));
        animator.SetBool("isJumping", !isGrounded);
    }
}