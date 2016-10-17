using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public void ExitGame () {
        Application.Quit ();
        Time.timeScale = 1f;
    }

    public void MainMenuButton () {
        SceneManager.LoadScene (0);
        Time.timeScale = 1f;
    }

    public void PlayGameButton () {
        SceneManager.LoadScene (1);
        Time.timeScale = 1f;
    }

    public void PlayTestButton () {
        SceneManager.LoadScene (2);
        Time.timeScale = 1f;
    }
}
