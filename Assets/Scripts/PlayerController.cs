using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;

    private void Awake()
    {
        Debug.Log("Player Controller Awake.");
    }

    //Collision Detection
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collision " + collision.gameObject.name);
    //}

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        playerAnimator.SetFloat("Speed", Mathf.Abs(speed)); // Mathf.Abs() is used for converting negative values to positive values.

        Vector3 scale = transform.localScale;

        if (speed < 0) scale.x = -1f * Mathf.Abs(scale.x);
        else if (speed > 0) scale.x = Mathf.Abs(scale.x);

        //scale.x = (speed > 0) ? -1f * Mathf.Abs(scale.x) : Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}
