using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[SerializeField] private float firingSpeed = 500f;	// Bullets movement speed

	[SerializeField] private int damage = 10;			// Damage dealt

	[SerializeField] private Rigidbody2D rb;			// Bullet's rigidbody
	
	// Update is called once per frame
	void Update () {
		rb.AddForce (transform.up * firingSpeed);		// 	Move bullet
	}

	void OnCollisionEnter2D (Collision2D col) {
		// If hit player deal damage
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent <PlayerController> ().dealDamage (damage);
		}

        gameObject.SetActive (false);   				// Turn off gameObject
	}
}
