using UnityEngine;
using System.Collections;

public class soundMade : MonoBehaviour {
	private GameObject[] guardList;

	void Start () {
		guardList = GameObject.FindGameObjectsWithTag ("Guard");
	}

	/// <summary>
	/// Makes the sound.
	/// </summary>
	/// <param name="soundLevel">The amount of sound said object makes.</param>
	/// <param name="obj">Object that the sound is being emitted from.</param>
	public void makeSound (float soundLevel, GameObject obj) {
		foreach (GameObject g in guardList) {
			
		}
	}
}
