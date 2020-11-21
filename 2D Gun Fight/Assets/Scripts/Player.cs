using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigbod;
    private BoxCollider2D boxCollider;
    
    public int maxHealth = 100;
    public int currentHealth = 0;
    public HealthBar healthBar;

    //variables for movement based things
    public float moveSpeed = 3;
    public float jumpForce = 10f;
    public bool canJump;
    public int amountOfJumps = 2;
    private int amountOfJumpsLeft;

    //variables for ground checking for jumping mechanics
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float groundCheckRadius;

    //variables for the direction checks of the player
    private float movementInputDirection;
    private bool isFacingRight = true; //character will spawn facing right

    //variable primarily for animation movements
    private Animator anim;
    private bool isRunning;

    private void Awake()
    {
        rigbod = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        amountOfJumpsLeft = amountOfJumps;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(20);
        }

        HandleMovement();
        CheckIfCanJump();
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    private void CheckIfCanJump() //to prevent infinite jumping
    {
        if (isGrounded && rigbod.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal"); //supplies the movement direction Left = negative, Right = positive

        if (Input.GetKey(KeyCode.W))
        {
            Jump();
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckMovementDirection() //Checks the direction of the movement as well as which way the character sprite is facing
    {
        if (isFacingRight && movementInputDirection < 0) //Facing Right but moving left =  flip
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0) //Facing Left but moving right = flip
        {
            Flip();
        }

        if(rigbod.velocity.x !=0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void HandleMovement()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rigbod.velocity = new Vector2(-moveSpeed, rigbod.velocity.y);
        }
        else
        {
            if(Input.GetKey(KeyCode.D))
            {
                rigbod.velocity = new Vector2(moveSpeed, rigbod.velocity.y);
            }
            else //No Keys Are Pressed
            {
                rigbod.velocity = new Vector2(0, rigbod.velocity.y);
            }
        }
    }

    private void Jump()
    {
        if(canJump)
        {
            rigbod.velocity = new Vector2(rigbod.velocity.x, jumpForce);
            amountOfJumpsLeft --;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rigbod.velocity.y);
    }
}
