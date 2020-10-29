using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour {

	Vector3 titleOPos;
	public float titleSlide = 1;
	static public bool gameStart = false;

	public GameObject titleNeon;
	public int titleNeonClock;

	// Use this for initialization
	void Start () {
		//Time.timeScale = 0;
		titleOPos = transform.position;
		TitleDropStart ();
	}

	void TitleDropStart (){
		transform.position = new Vector3(titleOPos.x,((titleOPos.y -3.3f) * titleSlide) + 3.3f,titleOPos.z);
		if (titleSlide > 0.000001f) {
			titleSlide = titleSlide * 0.9f;
			TitleDropStart ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (titleNeonClock == 100) {
			GameObject emitted = Instantiate (titleNeon);
			emitted.transform.position = transform.position;
			emitted.GetComponent<Rigidbody2D> ().velocity = Vector2.left*.5f;

			GameObject emitted2 = Instantiate (titleNeon);
			emitted2.transform.position = transform.position;
			emitted2.GetComponent<Rigidbody2D> ().velocity = Vector2.right*.5f;

			titleNeonClock = 0;
		} else {
			titleNeonClock++;
		}

		if (Input.touchCount > 1) {
			gameStart = true;
			//Time.timeScale = 1;
		}
		if (gameStart) {
			transform.position = new Vector3 (titleOPos.x, ((titleOPos.y - 3.3f) * titleSlide) + 3.3f, titleOPos.z);
			if (titleSlide < 1)
				titleSlide = titleSlide * 1.9f;
		}
	}
}
