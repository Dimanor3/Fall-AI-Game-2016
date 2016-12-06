using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour {
	[SerializeField] private lockedDoor unLock;		// Gives us access to the lockedDoor script so we can unlock it.

	void OnTriggerEnter (Collider col) {
		if (col.CompareTag ("Player")) {
			unLock.Locked = false;

			gameObject.SetActive (false);
		}
	}
}
