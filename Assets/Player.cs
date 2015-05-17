using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float pitch, yaw, roll;
	public Camera camera;

	public float jump_speed = 6.6f;
	public float run_speed = 0.5f;
	
	private float back_speed;
	private float side_speed;
	
	private float ground_threshhold;
	private bool grounded;
	private float distance_to_ground;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
	    ground_threshhold = 0.1f;
		back_speed = 0.25f * run_speed;
		side_speed = 0.75f * run_speed;
		rb = GetComponent<Rigidbody>();
		distance_to_ground = GetComponent<Collider>().bounds.extents.y + ground_threshhold;

	}
	
	// Update is called once per frame
	void Update () {
		Move ();

		print(rb.velocity.magnitude);

		Look ();
		
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
			if(rb.velocity.magnitude < 30)
			ControlledMovement ();
		}
		UncontrolledMovement ();
	}
	
	void ControlledMovement(){
		if (Input.GetKey ("w"))
			MoveForwards (run_speed);
		if (Input.GetKey ("s"))
			MoveForwards (-back_speed);
		if (Input.GetKey ("a"))
			MoveLeft (side_speed);
		if (Input.GetKey ("d"))
			MoveRight (side_speed);
		if(Input.GetKeyDown("space"))
			Jump();
	}
	
	void UncontrolledMovement(){
		// assume rigid body sort of does this already
	}
	
	void Jump(){
		Debug.Log ("jump");
		Vector3 jump_strength = (transform.up * jump_speed);
		Debug.Log (jump_strength);
		rb.AddForce(jump_strength);
	}
	
	void Slide(){
		
	}
	
	void ThrowBall(){
		
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
		rb.AddForce(transform.forward * f);
	}

	void MoveLeft(float f)
	{
		rb.AddForce(transform.right * -f);
	}

	void MoveRight(float f)
	{
		rb.AddForce(transform.right * f);
	}
}
