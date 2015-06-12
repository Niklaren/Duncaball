using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Check Collisions
	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Ball") {
			// give the team who was in possesion 1 point

			// begin reset
		}
	}
}
