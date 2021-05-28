using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {

	float topSpeed = 16.0f;
	float acceleration = 4.0f;

	Rigidbody rb = null;


	// Start is called before the first frame update
	void Start () {

		rb = GetComponent<Rigidbody>();

		if (rb) Debug.Log("Got the rigidbody.");
		else Debug.Log("No rigidbody.");
	}

	// Update is called once per frame
	void Update () {

		rb.velocity = rb.velocity.z < topSpeed ? Vector3.forward * (rb.velocity.z + acceleration * Time.deltaTime) : Vector3.forward * topSpeed;
	}
}
