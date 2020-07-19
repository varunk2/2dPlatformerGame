using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button button;
    public string levelName;
    private LevelStatus levelStatus;

    public void Awake(){
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        levelStatus = LevelManager.Instance.GetLevelStatus(levelName);
        switch(levelStatus){
            case LevelStatus.Locked:
                Debug.Log("Can't play this level till you unlock it");
                break;
            case LevelStatus.Unlocked:
                SceneManager.LoadScene(levelName);
                break;
            case LevelStatus.Completed:
                break;
        }
    }
}