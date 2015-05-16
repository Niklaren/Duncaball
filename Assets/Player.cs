using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float pitch, yaw, roll;

	// Use this for initialization
	void Start () {
		
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
		float deltaY = Input.GetAxis ("Mouse Y");
		pitch += deltaX;
		yaw += deltaY;
		roll += 0;

		WrapAngle(pitch);
		WrapAngle(yaw);
		WrapAngle(roll);

		transform.localEulerAngles = new Vector3(0, pitch, yaw);
		//transform.rotation.x = (transform.rotation.x + deltaX);
		//transform.localRotation = Quaternion.Euler (pitch, yaw, roll);
	}

	public static void WrapAngle(float angle)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
	}

	void Move(){
		ControlledMovement ();
		UncontrolledMovement ();
	}
	
	void ControlledMovement(){
		
	}
	
	void UncontrolledMovement(){
		// assume rigid body sort of does this already
	}
	
	void Jump(){
		Debug.Log ("jump");
	}
	
	void Slide(){
		
	}
	
	void ThrowBall(){
		
	}
	
	void Tackle(){
		
	}
}
