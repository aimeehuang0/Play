using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R1T0 : MonoBehaviour {

	public GameObject Box1;
	public GameObject trig1;
	public float speed = 20f;
	Vector2 dest = Vector2.zero;
	Vector2 vUp = new Vector2(5.4f,2.5f);
	Vector2 vRight = new Vector2(5.4f,-2.5f);

	void Start(){
		Box1 = GameObject.Find("MoveBox3");
		//dest = transform.position;
		//Debug.Log(dest);
	}

	void OnTriggerEnter2D(Collider2D co) {
//		if (co.name == "pacman")
//			Destroy(gameObject);

		Vector2 p = Vector2.MoveTowards(Box1.transform.position, (Vector2)Box1.transform.position - vUp, speed);
		Box1.GetComponent<Rigidbody2D>().MovePosition(p);

		trig1.SetActive(true);
		gameObject.SetActive(false);
		//GetComponent<Rigidbody2D>().MovePosition(p);
	}
}
