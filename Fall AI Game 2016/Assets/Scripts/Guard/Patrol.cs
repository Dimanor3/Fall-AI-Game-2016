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
	[SerializeField] private float radiusOfSatisfaction = .25f;
	[SerializeField] private float maxSpeed = 10f;

	//private Vector2 rotation;
	private Vector3 moveDirection;
	private Vector3 playerPosition;
	private Vector3 pointPosition;
	private float angle;
	private int currPoint;
	public Vector3 velocity;

	RaycastHit[] hit;
	Ray ray;

	void Awake () {
		// Initialize agents rigidbody
		rb = GetComponent<Rigidbody> ();
	}


	// Use this for initialization
	void Start () {
		maxTimer = timer = 25;
		transform.position = points [0].position;
		currPoint = 0;
	}


	
	// Update is called once per frame
	void Update () {
		moveDirection = pointPosition - transform.position;
		// raycast work
		ray = new Ray (transform.position, moveDirection);
		hit = Physics.RaycastAll (ray, 10f);
		Debug.DrawRay(transform.position, moveDirection, Color.green);
		GoToNextPoint(moveDirection);
	}

	void Pursue() {
		playerPosition = GameObject.FindWithTag ("Player").transform.position;
		//print (playerPosition);
		moveDirection =  playerPosition - transform.position;
		//print ("Direction: " + direction);
		Quaternion rotation = Quaternion.LookRotation (new Vector3(moveDirection.x, 0, moveDirection.z), Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
		rb.velocity = moveDirection;

		//.speed += 1;
	}

	void GoToNextPoint(Vector3 dir) {
		if (points.Length == 0)
			return;
		pointPosition = points[destPoint].position;
		velocity = rb.velocity;
		Quaternion rotation = Quaternion.LookRotation (new Vector3(dir.x, 0, dir.z), Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
		if (dir.magnitude > maxSpeed) {
			dir.Normalize ();
			dir *= maxSpeed;
		}
		rb.velocity = dir;
		// Radius of satisfaction
		if (Vector3.Distance (transform.position, pointPosition) <= radiusOfSatisfaction) {
			rb.velocity = Vector3.zero;
			//currPoint++;
			destPoint = (destPoint + 1) % points.Length;
		}

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
