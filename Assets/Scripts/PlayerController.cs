using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public BoxCollider2D playerCollider;     // Creating a BoxCollider2D variable
    private float p_sizeX, p_sizeY;          // Variables for storing X and Y coordinates of Box Collider.
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

        // Initializing and assigning variables
        playerCollider = GetComponent<BoxCollider2D>();
        p_sizeX = playerCollider.size.x;
        p_sizeY = playerCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Run animation.
        float h_Movement = Input.GetAxisRaw("Horizontal");
        PlayerAnimation(h_Movement);
        MoveCharacter(h_Movement);

        // Crouch animation.
        PlayerCrouch();

    }

    private void MoveCharacter(float h_Movement)
    {
        Vector3 position = transform.position;
        position.x += h_Movement * speed * Time.deltaTime;
        transform.position = position;
    }

    private void PlayerAnimation(float h_Movement)
    {
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

    private void PlayerCrouch()
    {
        // Crouch animation.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerAnimator.SetBool("isCrouch", true);
            playerCollider.size = new Vector2(x: p_sizeX, 2.24f);   // Resizing BoxCollider in crouch
        }
        else
        {
            playerAnimator.SetBool("isCrouch", false);
            playerCollider.size = new Vector2(x: p_sizeX, y: p_sizeY);   // Resetting the BoxCollider in idle
        }
    }
}
