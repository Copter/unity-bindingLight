using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

	public Sprite pauseSpr;
	public Sprite pressedSpr;

	//bool intro = true;
	float pressedTime;
	float fadeTimer = 0.99f;
	public int doubleTap = 0;

	void Start () {
		gameObject.GetComponent<Image>().color = Color.clear;
	}

	void Update () {
		if (fadeTimer > 0) {
			gameObject.GetComponent<Image> ().color = new Color (1, 1, 1, fadeTimer);
			fadeTimer = fadeTimer - 0.03f;
		} else {
			if (doubleTap == 0) {
				gameObject.GetComponent<Image> ().color = Color.clear;
				gameObject.GetComponent<Image> ().sprite = pauseSpr;
			}
			if (doubleTap == 1 && Time.timeSinceLevelLoad - pressedTime > 3) {
				gameObject.GetComponent<Image> ().color = Color.clear;
				doubleTap = 0;
			}
		}
	}

	public void PauseBtnPressed (){
		switch (doubleTap) 
		{
		case 0:
			doubleTap = 1;
			gameObject.GetComponent<Image>().color = Color.white;
			pressedTime = Time.timeSinceLevelLoad;
			break;
		case 1:
			doubleTap = 2;
			Time.timeScale = 0;
			gameObject.GetComponent<Image>().sprite = pressedSpr;
			break;
		case 2:
			gameObject.GetComponent<Image>().sprite = pauseSpr;
			gameObject.GetComponent<Image>().color = Color.clear;
			Time.timeScale = 1;
			doubleTap = 0;
			break;
		}
	}
}
