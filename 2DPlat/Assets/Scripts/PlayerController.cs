using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float movement = 0f;
    public float jumpSpeed = 10f;
    private Rigidbody2D rb;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private bool isFalling;
    //private bool hasAttacked;
    private Animator playerAnimation;
    public Vector3 respawnPoint;
    public LevelManager gameLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        playerAnimation = GetComponent<Animator> ();
        respawnPoint = transform.position;
        gameLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if objects on ground layer is within the radius from the ground checkpoint
        //Checks if object is touching the ground
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        //Checks if object is falling
        isFalling = false;

        //Checks if player has attacked
        //hasAttacked = false;

        //Checks if horizontal movement keys are being pressed to enable movement
        movement = Input.GetAxis("Horizontal");

        //If statement that creates horizontal movement
        if (movement > 0f) {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
            transform.localScale = new Vector2(3f, 3f);
        } else if (movement < 0f) {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
            transform.localScale = new Vector2(-3f, 3f);
        } else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //If statement to check if an object is falling
        if (rb.velocity.y < -0.1) {
            isFalling = true;
        }

        //If statement that creates jumping movement
        if (Input.GetButtonDown("Jump") && isTouchingGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        //If statement that creates attack
        
        if(Input.GetButtonDown("Fire1")) {
            Attack();
        }
        

        //Commands to control animations
        playerAnimation.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
        playerAnimation.SetBool("IsFalling", isFalling);
        //playerAnimation.SetBool("HasAttacked", hasAttacked);
    }

    void OnTriggerEnter2D (Collider2D other) {
        //Respawns player back to point upon falling
        if (other.tag == "FallDetector") {
            gameLevelManager.Respawn();
        }

        //Sets respawn point to most recent checkpoint
        if (other.tag == "Checkpoint") {
            respawnPoint = other.transform.position;
        }
    }

    void Attack() {
        //Play attack animation
        playerAnimation.SetTrigger("Attack1");
        //Detect enemies in range
        //Damage enemies in range
    }
}