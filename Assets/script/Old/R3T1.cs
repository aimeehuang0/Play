using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R3T1 : MonoBehaviour {

	public GameObject Box1;
	public float speed = 20f;
	Vector2 dest = Vector2.zero;
	Vector2 vUp = new Vector2(5.4f,2.5f);
	Vector2 boxPos = new Vector2(-32.1f,11.9f);

	void Start(){
		Box1 = GameObject.Find("MoveBox3");
		//dest = transform.position;
		//Debug.Log(dest);
	}

	void OnTriggerEnter2D(Collider2D co) {
		//-26.7f, 14.4f
		// 5.4f,-2.5f

		Vector2 p = Vector2.MoveTowards(Box1.transform.position, (Vector2)boxPos, speed);
		Box1.GetComponent<Rigidbody2D>().MovePosition(p);

		gameObject.SetActive(false);
		//GetComponent<Rigidbody2D>().MovePosition(p);
	}
}
