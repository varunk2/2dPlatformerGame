using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public static LevelManager Instance { get => instance; set => instance = value; }

    public string levelName;

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GetLevelStatus(levelName) == LevelStatus.Locked) {
            SetLevelStatus(levelName, LevelStatus.Unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string level){
        /*LevelStatus levelStatus = (LevelStatus) PlayerPrefs.GetInt(level, 0);
        return levelStatus;*/

        return (LevelStatus)PlayerPrefs.GetInt(level, 0); ;
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus){
        PlayerPrefs.SetInt(level, (int)levelStatus);
    }
}