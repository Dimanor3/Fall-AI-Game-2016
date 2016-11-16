using UnityEngine;
using System.Collections;

public class soundMade : MonoBehaviour {
	private GameObject[] guardList;

	private float distance, 				// The distance between the guard and the sound made
				  distSoundTravel, 			// The distance the sound will travel
				  rateOfObjectDecay,		// The amount of decay that an object that is in the way causes
				  guardBuffer,				// Amount of decay caused by guards
				  wallBuffer,				// Amount of decay caused by walls
				  doorBuffer;				// Amount of decay caused by doors

	private RaycastHit[] hit;

	private Vector3 rand;

	void Awake () {
		// Instantiate a list of all guards
		guardList = GameObject.FindGameObjectsWithTag ("Guard");
	}

	void Start () {
		// Instantiate all necessary variables
		rateOfObjectDecay = 2f;
		guardBuffer = 2f;
		wallBuffer = 10f;
		doorBuffer = 10f;
	}

	/// <summary>
	/// Makes the sound.
	/// </summary>
	/// <param name="soundLevel">The amount of sound said object makes.</param>
	/// <param name="obj">Object that the sound is being emitted from.</param>
	/// <returns>Returns a randomized Vector2 position to the guard for pathing.</returns>
	public Vector3 makeSound (float soundLevel, GameObject obj) {
		foreach (GameObject g in guardList) {
			// Distance between the sounds original location and the guard.
			distance = Vector3.Distance (obj.transform.position, g.transform.position);

			// List of all objects hit by a raycast.
			// In theory the last object to be hit by the raycast is the guard.
			hit = Physics.RaycastAll (obj.transform.position, g.transform.position, distance);

			distSoundTravel = soundLevel;

			foreach (RaycastHit h in hit) {
				if (h.collider.gameObject.CompareTag ("Guard")) {
					distSoundTravel -= rateOfObjectDecay * guardBuffer;
				} 

				if (h.collider.gameObject.CompareTag ("Wall")) {
					distSoundTravel -= rateOfObjectDecay * wallBuffer;
				}

				if (h.collider.gameObject.CompareTag ("Door")) {
					distSoundTravel -= rateOfObjectDecay * doorBuffer;
				}
			}

			if (distSoundTravel <= 0) {
				distSoundTravel = 0;
			}

			rand = Random.insideUnitCircle * 3;
			print ("Distance: " + distance + " sound travel distance " + distSoundTravel);
			if (distance <= distSoundTravel) {
				print ("Testing random point: " + (obj.transform.position + rand));
			}
		}

		return Vector3.zero;
	}
}
