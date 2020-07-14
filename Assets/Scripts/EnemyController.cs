using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnimator;
    private float enemySpeed = 0.31f;

    public float horizontal;

    private void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        enemyWalk(horizontal, enemySpeed);
    }

    private void enemyWalk(float horizontal, float enemySpeed)
    {
        Vector3 position = transform.position;
        position.x += horizontal * enemySpeed * Time.deltaTime;
        transform.position = position;

        enemyAnimator.SetFloat("enemySpeed", Mathf.Abs(enemySpeed));
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;

        if (scale.x > 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if(scale.x < 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
        horizontal = -horizontal;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if(playerController.GetHealth() == 0) playerController.KillPlayer();
            else playerController.DecreasePlayerHealth();

        }
    }
}
