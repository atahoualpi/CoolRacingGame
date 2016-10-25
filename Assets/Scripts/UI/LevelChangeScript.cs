using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChangeScript : MonoBehaviour {
    public int chooseScene;
    public void LoadLevelButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(chooseScene);
    }

    public void LoadAgainLevelButton(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

  
}
