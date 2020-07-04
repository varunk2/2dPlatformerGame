using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public BoxCollider2D playerCollider;     // Creating a BoxCollider2D variable
    private float p_sizeX, p_sizeY;          // Variables for storing X and Y coordinates of Box Collider.

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
        // Run animation code.
        float speed = Input.GetAxisRaw("Horizontal");
        playerAnimator.SetFloat("Speed", Mathf.Abs(speed)); // Mathf.Abs() is used for converting negative values to positive values.

        Vector3 scale = transform.localScale;
        if (speed < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;

        // Crouch animation code.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerAnimator.SetBool("isCrouch", true);
            playerCollider.size = new Vector2(x: p_sizeX, 2.24f);   // Resizing BoxCollider in crouch
            Debug.Log("Current BoxCollider2D Size while crouching: " + GetComponent<BoxCollider2D>().size);
        }
        else {
            playerAnimator.SetBool("isCrouch", false);
            playerCollider.size = new Vector2(x: p_sizeX, y: p_sizeY);   // Resetting the BoxCollider in idle
            Debug.Log("Current BoxCollider2D Size while standing: " + GetComponent<BoxCollider2D>().size);
        }


    }
}
