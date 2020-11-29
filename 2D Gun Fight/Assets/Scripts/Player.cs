using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigbod;
    public PolygonCollider2D polyCollider;
    
    public int maxHealth = 200;
    public int currentHealth = 0;
    public HealthBar healthBar;
    public GameController gameController;

    //variables for movement based things
    public float moveSpeed = 3;
    public float jumpForce = 10f;
    public bool canJump, canFlip;
    public int amountOfJumps = 2;
    private int amountOfJumpsLeft;

    //variables for ground checking for jumping mechanics
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsHazard;
    public bool isGrounded;
    public bool isInAcid;
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
        polyCollider = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        amountOfJumpsLeft = amountOfJumps;
        canFlip = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
           gameController.restartLevel(); 
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
        AcidCheck(isInAcid);
    }

    private void AcidCheck(bool inAcid)
    {
        if(isInAcid)
        {
            TakeDamage(1);
        }
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
        isInAcid = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsHazard);
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

        if(Mathf.Abs(rigbod.velocity.x) >= 0.01f)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }

    private void Flip()
    {
        if(canFlip)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }   
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        gameController.healthCheck(currentHealth);
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rigbod.velocity.y);
    }

}
