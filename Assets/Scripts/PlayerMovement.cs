using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] 
    private float runSpeed = 1;
    [SerializeField]
    private float jumpDistance = 1;
    [SerializeField]
    private float climbSpeed = 1;
    private float gravityScale;
    private Rigidbody2D ridgitBody;
    private bool isTouchingGround;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        ridgitBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScale = ridgitBody.gravityScale; 
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")) || GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Climbing"));
        animator.SetBool("isJumping", !isTouchingGround);
        Run();
        climbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            ridgitBody.velocity += new Vector2(0, jumpDistance);
        }
    }

    void Run() 
    {
        ridgitBody.velocity = new Vector2(moveInput.x * runSpeed, ridgitBody.velocity.y);

        // Flips the sprite according to movment direction
        if (ridgitBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (ridgitBody.velocity.x > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        bool isMoving = Mathf.Abs(ridgitBody.velocity.x) > 0;
        animator.SetBool("isRunning", isMoving && isTouchingGround);
    }
    void climbLadder()
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            ridgitBody.velocity = new Vector2(ridgitBody.velocity.x, moveInput.y * climbSpeed);
            ridgitBody.gravityScale = 0;
            bool isClimbing = Mathf.Abs(ridgitBody.velocity.y) > 0;
            animator.SetBool("isClimbing", isClimbing);
            bool isTouchingGround = GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"));
            animator.SetBool("isIdleClimbing", !isTouchingGround && !isClimbing);
        }
        else
        {
            ridgitBody.gravityScale = gravityScale;
            animator.SetBool("isClimbing", false);
            animator.SetBool("isIdleClimbing", false);
        }

    }
}
