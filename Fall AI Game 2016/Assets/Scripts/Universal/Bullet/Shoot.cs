using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	[SerializeField] private GameObject bullet;		// GameObject of the bullet

	// Spawn bullet
	public void shoot () {
		Instantiate (bullet, transform.position, transform.rotation);
	}
}
