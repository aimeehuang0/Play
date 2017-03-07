using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float cameraSpeed = 0.1f ; //the speed of the camera movement
	public Vector3 target;

	void Start () {
		target = new Vector3(0f,0f,-10f);
	}

	void LateUpdate () {
		transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime * cameraSpeed);
	}
}
