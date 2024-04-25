using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 12f;
    public float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 8f;

    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip hitEnemySFX;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    public bool isJumping;

    public int coinCount;
    [SerializeField] AudioClip pickupSFX;

    public bool isBoost = false;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }


    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();       

        /*if (Mathf.Abs(myRigidbody.velocity.y) > 0 && isJumping)
        {
            myAnimator.SetBool("isJumping", true);
            myAnimator.SetBool("isRunning", false);
        }*/

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("JumpThroughPlatform"))
            {
            isJumping = false;
            }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }

    void OnJump(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))
        && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        { return; }

        if (value.isPressed)
        {
            GetComponent<AudioSource>().PlayOneShot(jumpSFX);
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            isJumping = true;            
        }

    }

    void Run()
    {
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0;
            myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
            myAnimator.SetBool("isJumping", false);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            myAnimator.enabled = true;            
            return;
        }

        if (!isJumping)
        {
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0f;

            if (Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                myAnimator.SetBool("isClimbing", true);
                myAnimator.SetFloat("climbSpeed", 1f);
                myAnimator.SetBool("isJumping", false);
            }

            if (myRigidbody.velocity.y == 0 && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))
            && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myAnimator.SetBool("isClimbing", true);
                myAnimator.SetFloat("climbSpeed", 0f);
                myAnimator.SetBool("isJumping", false);
            }
        }

        else if (isJumping)
        {
            myRigidbody.gravityScale = gravityScaleAtStart;

            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                myAnimator.SetBool("isClimbing", true);
            }

            else
            {
                myAnimator.SetBool("isClimbing", false);
            }

            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && myRigidbody.velocity.y <= 0)
            {
                isJumping = false;
                myRigidbody.gravityScale = 0f;
            }
        }

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("isClimbing", false);
            myAnimator.SetBool("isJumping", false);
        }

    }

}
