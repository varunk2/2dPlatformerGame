using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button restartButton;

    private Scene currentScene;

    private void Awake()
    {
        restartButton.onClick.AddListener(ReloadLevel);
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void PlayerDied() {
        gameObject.SetActive(true);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
