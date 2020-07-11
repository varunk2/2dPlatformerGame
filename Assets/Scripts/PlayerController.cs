using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ScoreController scoreController;
    //public float jump;

    private Animator playerAnimator;
    private BoxCollider2D playerCollider;    // Creating a BoxCollider2D variable
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb2d;

    private float playerColliderSizeX, playerColliderSizeY;     // Variables for storing X and Y coordinates of Box Collider.
    private float playerColliderYAxis = 2.24f;
    private float speed = 5.0f, jump = 7.0f;
    private bool onGround = true;    

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
    }

    public void PickUpKey()
    {
        scoreController.IncreaseScore(10);
    }

    void Update()
    {
        // Run animation.
        float h_Movement = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");

        playerRun(h_Movement, vertical);
        playerCrouch();
        playerJump(vertical);
    }    

    private void playerRun(float h_Movement, float vertical)
    {
        MoveCharacter(h_Movement, vertical);
        playerAnimator.SetFloat("Speed", Mathf.Abs(h_Movement)); // Mathf.Abs() is used for converting negative values to positive values.

        Vector3 scale = transform.localScale;
        if (h_Movement < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (h_Movement > 0)
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
            if (onGround)
            {
                rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                Debug.Log("Player jumped.");
            }
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
