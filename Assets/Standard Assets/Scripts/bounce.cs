using UnityEngine;
using System.Collections;

public class bounce : MonoBehaviour {
	Rigidbody r;
	public float strength;
	
	void OnCollisionEnter (Collision col)
	{
	print ("col");
		//r = col.gameObject.GetComponent<Rigidbody>();
		//r.AddForce(transform.up * strength);
		col.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * strength);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
