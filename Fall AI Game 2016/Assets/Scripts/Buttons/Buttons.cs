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

	// Exit game button
    public void ExitGame () {
		playButtonClick ();

        Application.Quit ();
    }

	// Load to main menu button
    public void MainMenuButton () {
		playButtonClick ();

		hideCredits (credits);
		showNonCredits (nonCredits);

		SceneManager.LoadScene (0);
        Time.timeScale = 1f;
    }

	public void MainMenuButton2 () {
		playButtonClick ();
		
		hideCredits (credits);
		showNonCredits (nonCredits);
	}

	// Load to play game button
	public void PlayGameButton () {
		playButtonClick ();

		SceneManager.LoadScene (1);
        Time.timeScale = 1f;
    }

	// Load to play test button
	public void PlayTestButton () {
		playButtonClick ();

		SceneManager.LoadScene (2);
        Time.timeScale = 1f;
    }

	// Display the Credits
	public void CreditsButton () {
		playButtonClick ();
		
		hideNonCredits (nonCredits);
		showCredits (credits);
	}

	// Play the buttonclick sound effect sound
    private void playButtonClick () {
		if (!sfxMan.ButtonClick.isPlaying) {
			sfxMan.ButtonClick.Play ();
		}
    }

	// Hide Credits
	private void hideCredits (GameObject[] gO) {
		foreach (GameObject g in gO) {
			g.SetActive (false);
		}
	}

	// Show Credits
	private void showCredits (GameObject[] gO) {
		foreach (GameObject g in gO) {
			g.SetActive (true);
		}
	}

	// Hide Non Credits
	private void hideNonCredits (GameObject[] gO) {
		foreach (GameObject g in gO) {
			g.SetActive (false);
		}
	}

	// Show Non Credits
	private void showNonCredits (GameObject[] gO) {
		foreach (GameObject g in gO) {
			g.SetActive (true);
		}
	}
}
