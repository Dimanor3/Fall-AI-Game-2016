using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	[SerializeField] private SFXManager sfxMan; // Get access to the SFXManager

	private GameObject[] credits, nonCredits;	// Gets access to all GameObjects that either have the credits or nonCredits tag

	void Start () {
		// Instantiate the sfxMan to an object containing the SFXManager
		sfxMan = FindObjectOfType<SFXManager> ();

		// Instantiate both credits and nonCredits
		credits = GameObject.FindGameObjectsWithTag ("Credits");
		nonCredits = GameObject.FindGameObjectsWithTag ("NonCredits");

		hideCredits (credits);
	}

	/// <summary>
	/// Exits the game.
	/// </summary>
    public void ExitGame () {
		playButtonClick ();

        Application.Quit ();
    }

	/// <summary>
	/// Button that loads the main menu and hides the credits.
	/// </summary>
    public void MainMenuButton () {
		playButtonClick ();

		hideCredits (credits);
		showNonCredits (nonCredits);

		SceneManager.LoadScene (0);
        Time.timeScale = 1f;
    }

	/// <summary>
	/// Loads the main menu and hides the credits.
	/// </summary>
	public void MainMenuButton2 () {
		playButtonClick ();
		
		hideCredits (credits);
		showNonCredits (nonCredits);
	}

	/// <summary>
	/// Loads the main game.
	/// </summary>
	public void PlayGameButton () {
		playButtonClick ();

		SceneManager.LoadScene (1);
        Time.timeScale = 1f;
    }

	/// <summary>
	/// Loads the test scene.
	/// </summary>
	public void PlayTestButton () {
		playButtonClick ();

		SceneManager.LoadScene (2);
        Time.timeScale = 1f;
    }

	// Display the Credits
	/// <summary>
	/// Shows the credits and hides the main menu.
	/// </summary>
	public void CreditsButton () {
		playButtonClick ();
		
		hideNonCredits (nonCredits);
		showCredits (credits);
	}

	/// <summary>
	/// Plays the button click sound.
	/// </summary>
    private void playButtonClick () {
		if (!sfxMan.ButtonClick.isPlaying) {
			sfxMan.ButtonClick.Play ();
		}
    }

	/// <summary>
	/// Hides the credits.
	/// </summary>
	/// <param name="gO">Gets a list of Gameobjects that have the credits tag.</param>
	private void hideCredits (GameObject[] gO) {
		foreach (GameObject g in gO) {
			g.SetActive (false);
		}
	}

	/// <summary>
	/// Shows the credits.
	/// </summary>
	/// <param name="gO">Gets a list of Gameobjects that have the credits tag.</param>
	private void showCredits (GameObject[] gO) {
		foreach (GameObject g in gO) {
			g.SetActive (true);
		}
	}

	/// <summary>
	/// Hides the non credits.
	/// </summary>
	/// <param name="gO">Gets a list of Gameobjects that have the non credits tag.</param>
	private void hideNonCredits (GameObject[] gO) {
		foreach (GameObject g in gO) {
			g.SetActive (false);
		}
	}

	/// <summary>
	/// Shows the non credits.
	/// </summary>
	/// <param name="gO">Gets a list of Gameobjects that have the non credits tag.</param>
	private void showNonCredits (GameObject[] gO) {
		foreach (GameObject g in gO) {
			g.SetActive (true);
		}
	}
}
