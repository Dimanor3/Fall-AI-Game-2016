using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	[SerializeField] private float firingSpeed;		// Bullets movement speed

	[SerializeField] private int damage;			// Damage dealt

	[SerializeField] private Rigidbody rb;			// Bullet's rigidbody

	void Awake () {
		rb = gameObject.GetComponent <Rigidbody> ();
	}

	void Start () {
		// Initialize all necessary variables
		firingSpeed = 500f;
		damage = 10;
	}

	void FixedUpdate () {
		rb.AddForce (transform.forward * firingSpeed);		// Move Bullet
	}

	void OnTriggerEnter (Collider col) {
		if (!col.isTrigger) {
			// If hit player deal damage
			if (col.gameObject.tag == "Player") {
				col.gameObject.GetComponent <PlayerController> ().dealDamage (damage);
			}

			Destroy (gameObject);   				// Turn off gameObject
		}
	}
}
