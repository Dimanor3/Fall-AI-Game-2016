using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	[SerializeField] private SFXManager sfxMan;		// Get access to the SFXManager

	[SerializeField] private GameObject bullet;     // GameObject of the bullet

	void Start () {
		// Instantiate the sfxMan to an object containing the SFXManager
		sfxMan = FindObjectOfType<SFXManager> ();
	}

	/// <summary>
	/// Spawn bullet.
	/// </summary>
	public void shoot () {
		sfxMan.GunShot.Play ();
		Instantiate (bullet, transform.position, transform.rotation);
	}
}
