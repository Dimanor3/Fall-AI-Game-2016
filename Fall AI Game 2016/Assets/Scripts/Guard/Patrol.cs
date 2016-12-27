using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {

	Animator anim;
	public Transform[] points;
	private int destPoint = 0;
	private UnityEngine.AI.NavMeshAgent agent;
	// Target's transform agent needs to move
	[SerializeField] private Transform target;
	// Agent's rigidbody
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float rotationSpeed;
	//[SerializeField] private float rotationTimer;
	//public Vector3 newTransform;
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
	Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
	Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

	RaycastHit[] hit;
	Ray ray;

	void Awake () {
		// Initialize agents rigidbody
		rb = GetComponent<Rigidbody> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}


	// Use this for initialization
	void Start () {
		//transform.position = new Vector3 (transform.position.x, -5, transform.position.z);
		//anim = GetComponent<Animator> ();
		//anim.SetFloat ("Walk", 0.2f);
		maxTimer = timer = 25;
		transform.position = points [0].position;
		currPoint = 0;
	}


	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3 (transform.position.x, -5, transform.position.z);
		moveDirection = pointPosition - transform.position;
		// raycast work
		//ray = new Ray (transform.position, moveDirection);
		//hit = Physics.RaycastAll (ray, 10f);
		//Debug.DrawRay(transform.position, moveDirection, Color.green);
		if (detected) {
			Pursue ();
		} else {
			GoToNextPoint (moveDirection);
		}
	}

	/*void Detection() {
		RaycastHit hit;
		var angle = transform.rotation * startingAngle;
		var direction = angle * Vector3.forward;
		var pos = transform.position;
		for(var i = 0; i < 24; i++)
		{
			if(Physics.Raycast(pos, direction, out hit, 100))
			{
				Debug.DrawRay(transform.position, direction, Color.green);
				var enemy = hit.collider.CompareTag("Player");
				if (enemy) {
					print ("enemy seen");
				} else {
					print ("sight clear");
				}
			}
			direction = stepAngle * direction;
		}
	}*/

	void Pursue() {
		playerPosition = GameObject.FindWithTag ("Player").transform.position;
		//print (playerPosition);
		moveDirection =  playerPosition - transform.position;
		//print ("Direction: " + direction);
		Quaternion rotation = Quaternion.LookRotation (new Vector3(moveDirection.x, 0, moveDirection.z), Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
		agent.SetDestination (playerPosition);
		rb.velocity = moveDirection;
		//if (!detected) {
			//GoToNextPoint (moveDirection);
		//}
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
			//anim.SetFloat ("Walk", 0.2f);
		}
		rb.velocity = dir;
		// Radius of satisfaction
		if (Vector3.Distance (transform.position, pointPosition) <= radiusOfSatisfaction) {
			rb.velocity = Vector3.zero;
			//currPoint++;
			destPoint = (destPoint + 1) % points.Length;
		}

	}

	public void outsideMovement(Vector3 pos) {
		agent.Stop ();
		agent.destination = pos;

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
