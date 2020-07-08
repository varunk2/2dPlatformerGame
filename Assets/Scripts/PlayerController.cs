using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private BoxCollider2D playerCollider;    // Creating a BoxCollider2D variable
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb2d;

    private float playerColliderSizeX, playerColliderSizeY;     // Variables for storing X and Y coordinates of Box Collider.
    private float playerColliderYAxis = 2.24f;
    public float jump;
    private float speed = 5.0f;//, jump = 6.0f;


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
        if (vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Force);
            playerAnimator.SetBool("Jump", true);
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
        }
    }

    private void MoveCharacter(float h_Movement, float vertical)
    {
        // Mover character horizontally
        Vector3 position = transform.position;
        position.x += h_Movement * speed * Time.deltaTime;
        transform.position = position;
    }
}
