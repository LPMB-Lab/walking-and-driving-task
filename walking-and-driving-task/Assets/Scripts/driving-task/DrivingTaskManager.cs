using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrivingTaskManager : MonoBehaviour {

	[SerializeField]
	private GameObject overlord = null;
	private SimulationHandler sh = null;
	[SerializeField]
	private Transform collisionCentre = null;
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
	[SerializeField]
	private Transform trigger = null;
	[SerializeField]
	private Transform apertureDropdown = null;
	[SerializeField]
	private Transform disappearanceTimeDropdown = null;

	private float topSpeed = 16.0f;
	private float acceleration = 4.0f;
	private float[] apertures = new float[] { 0.8f, 1.0f, 1.2f, 1.4f };
	private float aperture = 0.8f;
	private float[] disappearanceTimes = new float[] { 0.8f, 1.0f, 1.2f, 1.4f };
	private float disappearanceTime = 0.8f;
	private const float CAR_WIDTH = 2.0f;
	private Vector3 CAR_OFFSET = new Vector3 (0, 0, -2.5f);
	private Vector3 LEFT_CAR_OFFSET = new Vector3 (-4.6f, 0, 0);
	private Vector3 RIGHT_CAR_OFFSET = new Vector3 (4.2f, 0, 5.0f);

	private Rigidbody rb = null;

	private bool simulationStarted = false;


	// Start is called before the first frame update
	void Start () {

		rb = GetComponent<Rigidbody>();
		sh = overlord.GetComponent<SimulationHandler>();
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

		leftCarStartLocation.position = collisionCentre.position + LEFT_CAR_OFFSET - leftCarStartLocation.forward * (topSpeed * 9.0f + (aperture - 1) * CAR_WIDTH * 8);
		rightCarStartLocation.position = collisionCentre.position + RIGHT_CAR_OFFSET - rightCarStartLocation.forward * (topSpeed * 9.0f + (aperture - 1) * CAR_WIDTH * 8);
		trigger.position = collisionCentre.position + CAR_OFFSET - Vector3.forward * (topSpeed * disappearanceTime);
		leftCar.SetActive(true);
		rightCar.SetActive(true);
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

	public void ReadDisappearanceTime () {
		disappearanceTime = disappearanceTimes[disappearanceTimeDropdown.GetComponent<Dropdown>().value];
		Debug.Log("Set disappearance time to " + disappearanceTime);
	}
}
