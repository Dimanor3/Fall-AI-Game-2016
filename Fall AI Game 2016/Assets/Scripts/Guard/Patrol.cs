using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	
	public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;
	// Target's transform agent needs to move
	[SerializeField] private Transform target;
	// Agent's rigidbody
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float rotationSpeed;
	//[SerializeField] private float rotationTimer;
	private bool detected;
	private int timer;
	private int maxTimer;

	//private Vector2 rotation;
	private Vector3 direction;
	private Vector3 playerPosition;
	private float angle;

	void Awake () {
		// Initialize agents rigidbody
		rb = GetComponent<Rigidbody> ();
		agent = GetComponent<NavMeshAgent> ();
	}


	// Use this for initialization
	void Start () {
		
		agent.autoBraking = false;
		//GoToNextPoint ();
	}


	
	// Update is called once per frame
	void Update () {
		if (detected) {
			//agent.Stop();
			Pursue ();
			//This is the 2d stuff
			//angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)+90;
			//Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			//transform.rotation = Quaternion.Slerp (q, transform.rotation, rotationSpeed * Time.deltaTime);

		}
		else if (!detected) {
			if (agent.remainingDistance < 0.5f)
				GoToNextPoint ();
		}
	}

	void Pursue() {
		playerPosition = GameObject.FindWithTag ("Player").transform.position;
		//print (playerPosition);
		direction =  playerPosition - transform.position;
		//print ("Direction: " + direction);
		Quaternion rotation = Quaternion.LookRotation (new Vector3(direction.x, 0, direction.z), Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
		agent.destination = playerPosition;
	}

	void GoToNextPoint() {
		if (points.Length == 0)
			return;
		agent.destination = points [destPoint].position;

		destPoint = (destPoint + 1) % points.Length;
	}

	void FixedUpdate() {
		timer--;
		if(timer <= 0 && detected) {
			gameObject.GetComponentInChildren <Shoot> ().shoot ();
			timer = maxTimer;
		}
	}
		
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == ("Player")) {
			detected = true;
			print ("DETECTED");
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.GetComponent<Collider>().CompareTag ("Player")) {
			detected = false;
			print ("UNDETECTED");
		}
	}
}
