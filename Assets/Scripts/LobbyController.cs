using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button playButton;
    public GameObject levelSelection;
    
    //public Button levelSelectButton;

    private void Awake()
    {
        playButton.onClick.AddListener(SelectLevel);
        
        //levelSelectButton.onClick.AddListener(SelectLevel);
    }

    private void SelectLevel()
    {
        levelSelection.SetActive(true);        
    }

    /*private void PlayGame()
    {
        SceneManager.LoadScene(1);
    }*/
}
