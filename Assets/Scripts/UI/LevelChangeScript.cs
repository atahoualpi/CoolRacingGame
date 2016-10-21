using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChangeScript : MonoBehaviour {

    public void LoadLevelButton(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
