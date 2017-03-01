using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour {

	public float speed;
	Vector2 dest = Vector2.zero;
	Vector2 vUp = new Vector2(5.4f,2.5f);
	Vector2 vRight = new Vector2(5.4f,-2.5f);
	Vector3 target;

	public GameObject Char;
	private bool gameOver = false;
	private bool restart = false;


	void Start() {
		dest = transform.position;
		Debug.Log("wee");
	}

	void FixedUpdate() {
//			if (Input.GetKey(KeyCode.UpArrow))
//				dest = (Vector2)transform.position + vUp;
//			if (Input.GetKey(KeyCode.RightArrow))
//				dest = (Vector2)transform.position + vRight;
//			if (Input.GetKey(KeyCode.DownArrow))
//				dest = (Vector2)transform.position - vUp;
//			if (Input.GetKey(KeyCode.LeftArrow))
//				dest = (Vector2)transform.position - vRight;

		if(Input.GetMouseButtonDown(0)){
			//Debug.Log("click");

			if (restart)
			{
				Application.LoadLevel (Application.loadedLevel);
			}

			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
			dest.x = target.x;
			dest.y = target.y;

			if ( target.x < Char.transform.position.x && target.y > Char.transform.position.y && valid(-vRight))
			{
				//LeanTween.move( Char, Char.transform.position + new Vector3(-5.4f, 2.5f, 0f), 0.2f);//.setEase(LeanTweenType.easeInQuad);
				dest = (Vector2)transform.position - vRight;
			}
			if ( target.x > Char.transform.position.x && target.y > Char.transform.position.y && valid(vUp))
			{
				//LeanTween.move( Char, Char.transform.position + new Vector3(5.4f, 2.5f, 0f), 0.2f);//.setEase(LeanTweenType.easeInQuad);
				dest = (Vector2)transform.position + vUp;
			}
			if ( target.x < Char.transform.position.x && target.y < Char.transform.position.y && valid(-vUp))
			{
				//LeanTween.move( Char, Char.transform.position + new Vector3(-5.4f, -2.5f, 0f), 0.2f);//.setEase(LeanTweenType.easeInQuad);
				dest = (Vector2)transform.position - vUp;
			}
			if ( target.x > Char.transform.position.x && target.y < Char.transform.position.y && valid(vRight))
			{
				//LeanTween.move( Char, Char.transform.position + new Vector3(5.4f, -2.5f, 0f), 0.2f);//.setEase(LeanTweenType.easeInQuad);
				dest = (Vector2)transform.position + vRight;
			}
		}

		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed*Time.deltaTime*50);
		GetComponent<Rigidbody2D>().MovePosition(p);

	}

	bool valid(Vector2 dir) {
		// Cast Line from 'next to Pac-Man' to 'Pac-Man'
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D>());
	}


}
