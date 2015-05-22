using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody rb;

	enum Team{A, B};
	Team team;

	public GameObject ball;
	Vector3 ballOffset;

	public float pitch, yaw, roll;
	//public Camera camera;

	public float jump_speed = 6.6f;
	public float run_speed = 0.5f;
	
	private float back_speed;
	private float side_speed;
	
	private float ground_threshhold;
	private bool grounded;
	private float distance_to_ground;
	private Vector3 velocity;

	
	
	public GameObject grab_zone;
	bool has_ball;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		//Cursor.lockState = true;
		ballOffset = new Vector3(0.0f, 1.25f, 0.0f);

	    ground_threshhold = 0.1f;
		back_speed = 0.25f * run_speed;
		side_speed = 0.75f * run_speed;
		rb = GetComponent<Rigidbody>();
		distance_to_ground = GetComponent<Collider>().bounds.extents.y + ground_threshhold;

		foreach (Transform child in transform){
			if (child.name == "grab zone"){
				grab_zone = child.gameObject;
			}
		}
		ball = GameObject.FindWithTag ("ball");
	}
	
	// Update is called once per frame
	void Update () {
		Move ();

		//print(rb.velocity.magnitude);

		Look ();

		if(Input.GetKeyDown ("e"))
			GrabBall ();
		if (has_ball)
			CarryBall ();
		if (Input.GetMouseButtonDown (0))
			ThrowBall ();
		if(Input.GetMouseButtonDown (1))
			CarryBall ();
	}

	void CheckGrounded()
	{
		grounded = Physics.Raycast(transform.position, -transform.up, distance_to_ground);
	}
	
	void Look(){
		float deltaX = Input.GetAxis("Mouse X");
		//float deltaY = Input.GetAxis ("Mouse Y");
		pitch += deltaX * 4.0f;
		//yaw -= deltaY;
		roll += 0;

		WrapAngle(pitch);
		WrapAngle(yaw);
		WrapAngle(roll);

		transform.localEulerAngles = new Vector3(0, pitch, 0);
		//transform.rotation.x = (transform.rotation.x + deltaX);
		//transform.localRotation = Quaternion.Euler (pitch, yaw, roll);
	}

	void Move(){
		CheckGrounded();
		if(grounded)
		{
			if(rb.velocity.magnitude < 100)
			ControlledMovement ();
		}
		
		UncontrolledMovement ();
	}
	
	void ControlledMovement(){
		velocity = Vector3.zero;
		if(grounded)
		{
			if (Input.GetKey ("w"))
				MoveForwards (run_speed);
			if (Input.GetKey ("s"))
				MoveForwards (-back_speed);
			if (Input.GetKey ("a"))
				MoveLeft (side_speed);
			if (Input.GetKey ("d"))
				MoveRight (side_speed);
			
			rb.velocity = velocity;
			if(Input.GetKeyDown("space"))
				Jump();
		}
		else
		{
			if (Input.GetKey ("w"))
				MoveForwards (run_speed*0.01f);
			if (Input.GetKey ("s"))
				MoveForwards (-back_speed*0.01f);
			if (Input.GetKey ("a"))
				MoveLeft (side_speed*0.01f);
			if (Input.GetKey ("d"))
				MoveRight (side_speed*0.01f);
			
			rb.velocity += velocity;
			
		}
	}
	
	void UncontrolledMovement(){
		// assume rigid body sort of does this already
	}
	
	void Jump(){
		Debug.Log ("jump");
		Vector3 jump_strength = (transform.up * jump_speed);
		//Debug.Log (jump_strength);
		rb.AddForce(jump_strength);
	}
	
	void Slide(){
		
	}

	void GrabBall(){
		Debug.Log ("grab try");
		Collider[] colliders_in_zone = Physics.OverlapSphere (transform.position, 1.5f);
		int i = 0;
		while (i < colliders_in_zone.Length) {
			if (colliders_in_zone[i].tag == "ball"){
				Debug.Log("grab success");
				PickupBall();
			}
			i++;
		}
	}

	void PickupBall(){
		has_ball = true;
		ball.GetComponent<Rigidbody> ().angularVelocity.Equals (new Vector3 (0, 0, 0));
		//ball.GetComponent<Transform>().
	}

	void CarryBall(){
		Debug.Log ("carry");
		ball.transform.position = transform.position + ballOffset;
	}

	void ThrowBall(){
		Debug.Log ("throw try");
		if (has_ball) {
			has_ball = false;
			Debug.Log("throw success");
			Vector3 throw_direction = grab_zone.GetComponent<Transform> ().forward;
			float throw_force = 100.0f;
			ball.GetComponent<Rigidbody> ().AddForce (throw_direction * throw_force);
		}
	}
	
	void Tackle(){
		
	}


	public static void WrapAngle(float angle)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
	}

	void MoveForwards(float f)
	{
	velocity += transform.forward * f;
		//rb.AddForce(transform.forward * f);
	}

	void MoveLeft(float f)
	{
	velocity -= transform.right * f;
		//rb.AddForce(transform.right * -f);
	}

	void MoveRight(float f)
	{
	velocity += transform.right * f;
	}
}



