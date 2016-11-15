using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[SerializeField] private float firingSpeed;		// Bullets movement speed

	[SerializeField] private int damage;			// Damage dealt

	[SerializeField] private Rigidbody2D rb;		// Bullet's rigidbody

	void Start () {
		// Initialize all necessary variables
		firingSpeed = 500f;
		damage = 10;
	}

	// Update is called once per frame
	void Update () {
		rb.AddForce (transform.up * firingSpeed);		// 	Move bullet
	}

	void OnCollisionEnter2D (Collision2D col) {
		// If hit player deal damage
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent <PlayerController> ().dealDamage (damage);
		}

		Destroy (gameObject);   				// Turn off gameObject
	}
}
