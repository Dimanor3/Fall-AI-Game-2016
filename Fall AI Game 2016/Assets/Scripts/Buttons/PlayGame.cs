using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour {
	public void PlayGameButton () {
		SceneManager.LoadScene(1);
	}
}
