using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour {

	// Target's transform agent needs to move
	[SerializeField] private Transform target;
	// Agent's rigidbody
	[SerializeField] private Rigidbody2D rb;
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
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		rotationSpeed = 0f;
		timer = 50;
		maxTimer = 50;
	}

	// Update is called once per frame
	void Update () {
		if (detected) {
			playerPosition = GameObject.FindWithTag ("Player").transform.position;
			direction = playerPosition - transform.position;
			angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)+90;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (q, transform.rotation, rotationSpeed * Time.deltaTime);
		}
	}
	
	void FixedUpdate(){
		timer--;
		if(timer <= 0 && detected) {
			gameObject.GetComponentInChildren <Shoot> ().shoot ();
			timer = maxTimer;
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Player")) {
			detected = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.CompareTag ("Player")) {
			detected = false;
		}
	}
}
