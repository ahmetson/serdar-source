using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void UpdateUpdate () {
		Vector3 cameraPosition = Camera.main.transform.position;
		cameraPosition.z = cameraPosition.z * -1;
		transform.LookAt (cameraPosition);
	}
}
