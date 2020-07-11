using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelOverController : MonoBehaviour
{
    public string newScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))   // Never ever use this.
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SceneManager.LoadScene(newScene);
            Debug.Log("Level finished by the player.");
        }
    }
}
