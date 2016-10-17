using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public void ExitGame () {
        Application.Quit ();
    }

    public void MainMenuButton () {
        SceneManager.LoadScene (0);
    }

    public void PlayGameButton () {
        SceneManager.LoadScene (1);
    }

    public void PlayTestButton () {
        SceneManager.LoadScene (2);
    }
}
