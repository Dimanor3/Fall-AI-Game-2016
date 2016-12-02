using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	[SerializeField] private SFXManager sfxMan;		// Get access to the SFXManager

	[SerializeField] private GameObject bullet;     // GameObject of the bullet

	[SerializeField] private soundMade soundMaker;		// Used to make sounds that guards can hear
	private float soundLevel;							// Amount of sound made by door

	void Awake () {
		// Instantiate the sfxMan to an object containing the SFXManager
		sfxMan = FindObjectOfType<SFXManager> ();
		soundMaker = FindObjectOfType<soundMade> ();
	}

	void Start () {
		soundLevel = 200f;
	}

	/// <summary>
	/// Spawn bullet.
	/// </summary>
	public void shoot () {
		sfxMan.GunShot.Play ();
		soundMaker.makeSound (soundLevel, bullet);
		Instantiate (bullet, transform.position, transform.rotation);
	}
}
