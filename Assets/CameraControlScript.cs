using UnityEngine;
using System.Collections;

public class CameraControlScript : MonoBehaviour {

	public float yaw = 0F;
	public Vector3 cam_target;
	
	RaycastHit hit;

	void Start()
	{

	}
	
	void Update()
	{
		//float deltaX = Input.GetAxis("Mouse X");
		float deltaY = Input.GetAxis ("Mouse Y");
		//pitch += deltaX;
		yaw -= deltaY *1.5f;

		//WrapAngle(pitch);
		WrapAngle(ref yaw);
		//WrapAngle(roll);
			
		transform.localEulerAngles = new Vector3(yaw, 0, 0);
		cam_target = transform.position + transform.forward*10000;
	}
	
	public Vector3 Get_Target()
	{
		return cam_target;
	}
	
	void MoveForwards(float f)
	{
		transform.position += (f * transform.forward);
	}
	
	void Strafe(float s)
	{
		transform.position += (s * transform.right);
	}
	
	void ChangeHeight(float h)
	{
		transform.position += (h * Vector3.up);
	}
	/*
	void ChangeYaw(float y)
	{
		yaw += y;
		WrapAngle(ref yaw);
		transform.localEulerAngles = new Vector3(Pitch, Yaw, 0);
	}
	
	void ChangePitch(float p)
	{
		Pitch += p;
		WrapAngle(ref Pitch);
		transform.localEulerAngles = new Vector3(Pitch, Yaw, 0);
	}
	*/
	public static void WrapAngle(ref float angle)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
	}
	
} 
