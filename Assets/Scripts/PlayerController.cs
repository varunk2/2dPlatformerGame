using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ScoreController scoreController;
    public HealthController healthController;
    public GameOverController gameOverController;

    private Animator playerAnimator;
    private BoxCollider2D playerCollider;    // Creating a BoxCollider2D variable
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb2d;
    

    private float playerColliderSizeX, playerColliderSizeY;     // Variables for storing X and Y coordinates of Box Collider.
    private float playerColliderYAxis = 2.24f;
    private float speed = 5.0f, jump = 7.0f;
    private bool onGround = true;
    private bool isDead;

    private void Awake()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        // Initializing and assigning variables
        playerColliderSizeX = playerCollider.size.x;
        playerColliderSizeY = playerCollider.size.y;        
        isDead = false;
    }

    public void KillPlayer()
    {
        isDead = true;
        playerAnimator.SetBool("isDead", isDead);
        //Debug.Log("isDead: " + isDead);
        //yield return new WaitForSeconds(5);
        gameOverController.PlayerDied();
        this.enabled = false;
        //ReloadLevel();
    }

    public int GetHealth()
    {
        return healthController.GetHealth();
    }

    public void DecreasePlayerHealth()
    {
        healthController.DecreaseHealth(10);
    }

    public void PickUpKey()
    {
        scoreController.IncreaseScore(10);
    }

    void Update()
    {
        // Run animation.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");

        playerRun(horizontal, vertical);
        playerCrouch();
        playerJump(vertical);
    }    

    private void playerRun(float horizontal, float vertical)
    {
        // Don't move the horizontally player when it is in crouch position.
        if(playerAnimator.GetBool("isCrouch") == false) MoveCharacter(horizontal, vertical);

        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal)); // Mathf.Abs() is used for converting negative values to positive values.

        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    private void playerCrouch()
    {
        // Crouch animation.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerAnimator.SetBool("isCrouch", true);
            playerCollider.size = new Vector2(x: playerColliderSizeX, y: playerColliderYAxis);   // Resizing BoxCollider in crouch
        }
        else
        {
            playerAnimator.SetBool("isCrouch", false);
            playerCollider.size = new Vector2(x: playerColliderSizeX, y: playerColliderSizeY);   // Resetting the BoxCollider in idle
        }
    }

    // Crouch animation.
    private void playerJump(float vertical)
    {
        jumpForce();

        if (vertical > 0)
        {
            playerAnimator.SetBool("Jump", true);
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
        }
    }

    // Here we are applying force on keydown so the player gets a lift-off
    private void jumpForce() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround) rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }

    }

    private void MoveCharacter(float h_Movement, float vertical)
    {
        // Mover player horizontally
        Vector3 position = transform.position;
        position.x += h_Movement * speed * Time.deltaTime;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        // Check if the other object has Tilemap Renderer attached to it.
        if (otherObject.gameObject.GetComponent<TilemapRenderer>())
        {
            TilemapRenderer otherObjectTilemapRender = otherObject.gameObject.GetComponent<TilemapRenderer>();

            if (otherObjectTilemapRender.sortingOrder != spriteRender.sortingOrder)
            {
                onGround = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D otherObject)
    {
        // Check if the other object has Tilemap Renderer attached to it.
        if (otherObject.gameObject.GetComponent<TilemapRenderer>())
        {
            TilemapRenderer otherObjectTilemapRender = otherObject.gameObject.GetComponent<TilemapRenderer>();

            if (otherObjectTilemapRender.sortingOrder != spriteRender.sortingOrder)
            {
                onGround = false;
            }
        }
        
    }
}
