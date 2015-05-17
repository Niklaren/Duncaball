using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float pitch, yaw, roll;
	public Camera camera;

	public float jump_speed = 6.6f;
	public float run_speed = 0.5f;
	private float back_speed;
	private float side_speed;

	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		back_speed = 0.25f * run_speed;
		side_speed = 0.75f * run_speed;
		rb = GetComponent<Rigidbody>();


	}
	
	// Update is called once per frame
	void Update () {
		Move ();

		if(Input.GetKeyDown("space"))
		   Jump();

		Look ();
		
	}

	void Look(){
		float deltaX = Input.GetAxis("Mouse X");
		//float deltaY = Input.GetAxis ("Mouse Y");
		pitch += deltaX * 2.0f;
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
		ControlledMovement ();
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
		transform.position += (f * transform.forward);
	}

	void MoveLeft(float f)
	{
		transform.position -= (f * transform.right);
	}

	void MoveRight(float f)
	{
		transform.position += (f * transform.right);
	}

	void Strafe(float s)
	{
		transform.position += (s * transform.right);
	}
}
