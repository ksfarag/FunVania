using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 horizontalMoveInput;
    [SerializeField]
    private float runSpeed = 1;
    [SerializeField]
    private float jumpDistance = 1;
    [SerializeField]
    private float climbSpeed = 1;
    private Vector2 deathFly = new Vector2(10f, 10f);
    private float gravityScale;
    private Rigidbody2D rigidBody;
    private bool isTouchingGround;
    private bool isTouchingHazard;
    private bool isTouchingLadder;
    private CapsuleCollider2D bodyColider;
    private BoxCollider2D feetColider;
    private Animator animator;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScale = rigidBody.gravityScale;
        bodyColider = GetComponent<CapsuleCollider2D>();
        feetColider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (feetColider != null)
        {
            isTouchingGround = feetColider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        }


        if (bodyColider != null && feetColider != null)
        {
            isTouchingHazard = bodyColider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")) || feetColider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"));
        }

        if (bodyColider != null)
        {
            isTouchingLadder = bodyColider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        }

        if (isAlive)
        {
            Run();
            climbLadder();
            Jump();
        }

        Die();
    }

    void OnMove(InputValue value)
    {
        horizontalMoveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (isTouchingGround || isTouchingLadder)
        {
            rigidBody.gravityScale = gravityScale;
            rigidBody.velocity += new Vector2(0, jumpDistance);
        }
    }

    private void Run() 
    {
        rigidBody.velocity = new Vector2(horizontalMoveInput.x * runSpeed, rigidBody.velocity.y);

        // Flips the sprite according to movment direction
        if (rigidBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rigidBody.velocity.x > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        bool isMoving = Mathf.Abs(rigidBody.velocity.x) > 0;
        animator.SetBool("isRunning", isMoving && isTouchingGround);
    }

    private void climbLadder()
    {
        if (isTouchingLadder)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, horizontalMoveInput.y * climbSpeed);
            rigidBody.gravityScale = 0;
            bool isClimbing = Mathf.Abs(rigidBody.velocity.y) > 0;
            animator.SetBool("isClimbing", isClimbing);
            //animator.SetBool("isIdleClimbing", !isTouchingLadder && !isClimbing);
        }
        else
        {
            rigidBody.gravityScale = gravityScale;
            animator.SetBool("isClimbing", false);
            animator.SetBool("isIdleClimbing", false);
        }

    }
    private void Jump()
    {
        animator.SetBool("isJumping", !isTouchingGround && !isTouchingLadder);
    }

    private void Die()
    {
        if ( isAlive && isTouchingHazard)
        {
            isAlive = false;
            animator.SetTrigger("Die");
            rigidBody.velocity = deathFly;
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitUntil(() => isTouchingGround);
        rigidBody.bodyType = RigidbodyType2D.Static;
        Destroy(bodyColider);
        Destroy(feetColider);
        //Shake camera
        //play death sound
    }
}
