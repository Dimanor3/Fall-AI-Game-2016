using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (transform.position.x, -5, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, -5, transform.position.z);
	}
}
