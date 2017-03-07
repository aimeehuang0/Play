using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R3T4 : MonoBehaviour {

	public GameObject Box1;
	public float speed = 20f;
	Vector2 dest = Vector2.zero;
	Vector2 vUp = new Vector2(5.4f,2.5f);
	Vector2 vRight = new Vector2(5.4f,-2.5f); //-32.2f,22.2f
	Vector2 boxPos = new Vector2(-26.8f,19.7f);

	void Start(){
		Box1 = GameObject.Find("MoveBox8");
		Debug.Log(Box1.transform.position);
	}

	void OnTriggerEnter2D(Collider2D co) {
//		if (co.name == "pacman")
//			Destroy(gameObject);
		Debug.Log(Box1.transform.position);
//		Vector2 p = Vector2.MoveTowards(Box1.transform.position, dest, speed);

		Vector2 p = Vector2.MoveTowards(Box1.transform.position, (Vector2)boxPos, speed);
		Box1.GetComponent<Rigidbody2D>().MovePosition(p);

		gameObject.SetActive(false);
		Application.LoadLevel (Application.loadedLevel);
	}
}
