using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	
	public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.autoBraking = false;

		GoToNextPoint ();
	}

	void GoToNextPoint() {
		if (points.Length == 0)
			return;
		agent.destination = points [destPoint].position;

		destPoint = (destPoint + 1) % points.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance < 0.5f)
			GoToNextPoint();
	}
}
