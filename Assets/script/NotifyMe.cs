using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyMe : MonoBehaviour {

	public float targetTime = 60.0f;
	public Text notifArea;
	// Use this for initialization
	void Start(){
		notifArea.text = "Every Step you take will have consequences";
	}

	// Update is called once per frame
	void Update () {
		targetTime -= Time.deltaTime;

		if (targetTime <= 0.0f)
		{
			timerEnded();
		}

	}

	public void Notification(string _notify){
		notifArea.text = _notify;
		targetTime = targetTime - targetTime + 3;
	}

	void timerEnded()
	{
		notifArea.text = "";
	}
}
