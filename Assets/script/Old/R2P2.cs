using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R2P2 : MonoBehaviour {

	public GameObject camera;
	public GameObject player;
	public Vector3 cameraTarget;
	public Vector2 playerTarget;

	void Start(){
		camera = GameObject.FindWithTag("MainCamera");
		//player = GameObject.FindWithTag("Player");
	}

	void OnTriggerEnter2D(Collider2D co) {

		if(co.name == "Char")
		{

			camera.GetComponent<CameraFollow>().target = cameraTarget;

			Debug.Log(player.name);
			player.GetComponent<Rigidbody2D>().transform.position = playerTarget;
		}
	}
}
