using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeWallController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D enemyObject)
    {
        if (enemyObject.gameObject.GetComponent<EnemyController>() != null)
        {
            EnemyController enemyController = enemyObject.gameObject.GetComponent<EnemyController>();
            enemyController.Flip();
        }
    }
}
