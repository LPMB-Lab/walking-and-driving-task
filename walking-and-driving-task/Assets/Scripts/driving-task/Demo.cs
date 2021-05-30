using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour {

	[SerializeField]
	private GameObject leftCar = null;
	[SerializeField]
	private GameObject rightCar = null;
	[SerializeField]
	private Transform leftCarStartLocation = null;
	[SerializeField]
	private Transform rightCarStartLocation = null;
	[SerializeField]
	private Transform startLocation = null;
	// [SerializeField]
	// private BoxCollider trigger = null;
	[SerializeField]
	private Transform apertureDropdown = null;

	private float topSpeed = 16.0f;
	private float acceleration = 4.0f;
	private float[] apertures = new float[] { 0.8f, 1.0f, 1.2f, 1.4f };
	private float aperture = 1.0f;

	private Rigidbody rb = null;

	private bool simulationStarted = false;


	// Start is called before the first frame update
	void Start () {

		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (simulationStarted) {

			rb.velocity = rb.velocity.z < topSpeed ? Vector3.forward * (rb.velocity.z + acceleration * Time.deltaTime) : Vector3.forward * topSpeed;

			leftCar.transform.Translate(Vector3.forward * topSpeed * Time.deltaTime);
			rightCar.transform.Translate(Vector3.forward * topSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter (Collider other) {
		leftCar.SetActive(false);
		rightCar.SetActive(false);
	}

	public void StartCars () {

		leftCar.SetActive(true);
		leftCar.transform.position = leftCarStartLocation.transform.position + Vector3.forward * (1 - aperture) * topSpeed;
		rightCar.SetActive(true);
		rightCar.transform.position = rightCarStartLocation.transform.position - Vector3.forward * (1 - aperture) * topSpeed;
		simulationStarted = true;
	}

	public void Reset () {

		simulationStarted = false;

		rb.velocity = Vector3.zero;

		transform.position = startLocation.position;
		leftCar.transform.position = leftCarStartLocation.position;
		rightCar.transform.position = rightCarStartLocation.position;
	}

	public void ReadApertureValue () {
		aperture = apertures[apertureDropdown.GetComponent<Dropdown>().value];
		Debug.Log("Set aperture to " + aperture);
	}
}
