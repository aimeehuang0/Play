using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanMove : MonoBehaviour {

	public float speed;
	Vector2 dest = Vector2.zero;
	Vector2 vUp = new Vector2(1f,1f);
	Vector2 vRight = new Vector2(1f,-1f);
	Vector3 target;

	public GameObject Char;


	void Start() {
		dest = transform.position;
		Debug.Log(dest);
	}

	void FixedUpdate() {
		// Move closer to Destination
		//Debug.Log(Time.deltaTime);

		if(Input.GetMouseButtonDown(0)){
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log(target);

			if ( target.x < Char.transform.position.x && target.y > Char.transform.position.y)
			{
				LeanTween.move( Char, Char.transform.position + new Vector3(-5.4f, 2.5f, 0f), 0.2f).setEase(LeanTweenType.easeInQuad);
			}
			if ( target.x > Char.transform.position.x && target.y > Char.transform.position.y)
			{
				LeanTween.move( Char, Char.transform.position + new Vector3(5.4f, 2.5f, 0f), 0.2f).setEase(LeanTweenType.easeInQuad);
			}
			if ( target.x < Char.transform.position.x && target.y < Char.transform.position.y)
			{
				LeanTween.move( Char, Char.transform.position + new Vector3(-5.4f, -2.5f, 0f), 0.2f).setEase(LeanTweenType.easeInQuad);
			}
			if ( target.x > Char.transform.position.x && target.y < Char.transform.position.y)
			{
				LeanTween.move( Char, Char.transform.position + new Vector3(5.4f, -2.5f, 0f), 0.2f).setEase(LeanTweenType.easeInQuad);
			}

			//Debug.Log("click");
			//LeanTween.move( Char, Char.transform.position + new Vector3(6f, -2.7f, 0f), 0.2f).setEase(LeanTweenType.easeInQuad);
		}

	}

	bool valid(Vector2 dir) {
		// Cast Line from 'next to Pac-Man' to 'Pac-Man'
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D>());
	}
}
