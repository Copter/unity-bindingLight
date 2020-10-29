using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFollow : MonoBehaviour {

	public Camera mainCam;
	public GameObject redObj;
	public GameObject bluObj;

	Vector2 originPosRed;
	Vector2 swipedPosRed;
	Vector2 objPosRed;

	Vector2 originPosBlu;
	Vector2 swipedPosBlu;
	Vector2 objPosBlu;

	int anotherTouch;

	void Update()
	{
		//int tapCount = Input.touchCount;
		for ( int i = 0 ; i < Input.touchCount ; i++ ) {

			Touch eachTouch = Input.GetTouch(i);

			if (eachTouch.fingerId == 0) {

				Touch firstTouch = eachTouch; 

				switch (firstTouch.phase) {

				case TouchPhase.Began:

					Vector3 startPos = mainCam.ScreenToWorldPoint (firstTouch.position);
					float deltaRed = Vector3.Distance (startPos, redObj.transform.position);
					float deltaBlu = Vector3.Distance (startPos, bluObj.transform.position);

					if (deltaRed < deltaBlu) {
						anotherTouch = 0;
					} 
					else {
						anotherTouch = 1;
					}

					if (anotherTouch == 0) {
						originPosRed = mainCam.ScreenToWorldPoint (firstTouch.position);
						objPosRed = redObj.transform.position;
					}
					else {
						originPosBlu = mainCam.ScreenToWorldPoint(firstTouch.position);
						objPosBlu = bluObj.transform.position;
					}
					break;

				case TouchPhase.Moved:
					if (anotherTouch == 0) {
						swipedPosRed = mainCam.ScreenToWorldPoint (firstTouch.position);
						Vector2 deltaPos = swipedPosRed - originPosRed;

						Vector2 newPosCheckRed = objPosRed + deltaPos;
						if (newPosCheckRed.x > -8.4f && newPosCheckRed.x < 8.4f && newPosCheckRed.y > -4.5f && newPosCheckRed.y < 4.5f)
							redObj.transform.position = objPosRed + deltaPos;
					}
					else {
						swipedPosBlu = mainCam.ScreenToWorldPoint (firstTouch.position);
						Vector2 deltaPos = swipedPosBlu - originPosBlu;

						Vector2 newPosCheckBlu = objPosBlu + deltaPos;
						if (newPosCheckBlu.x > -8.4f && newPosCheckBlu.x < 8.4f && newPosCheckBlu.y > -4.5f && newPosCheckBlu.y < 4.5f)
							bluObj.transform.position = objPosBlu + deltaPos;
					}
					break;

				}
			}

			if (eachTouch.fingerId == 1) {

				Touch secondTouch = eachTouch;

				switch (secondTouch.phase) {

				case TouchPhase.Began:
					if (anotherTouch == 0) {
						originPosBlu = mainCam.ScreenToWorldPoint(secondTouch.position);
						objPosBlu = bluObj.transform.position;
					}
					else {
						originPosRed = mainCam.ScreenToWorldPoint (secondTouch.position);
						objPosRed = redObj.transform.position;
					}
					break;

				case TouchPhase.Moved:
					if (anotherTouch == 0) {
						swipedPosBlu = mainCam.ScreenToWorldPoint (secondTouch.position);
						Vector2 deltaPos = swipedPosBlu - originPosBlu;

						Vector2 newPosCheckBlu = objPosBlu + deltaPos;
						if (newPosCheckBlu.x > -8.4f && newPosCheckBlu.x < 8.4f && newPosCheckBlu.y > -4.5f && newPosCheckBlu.y < 4.5f)
							bluObj.transform.position = objPosBlu + deltaPos;
					}
					else {
						swipedPosRed = mainCam.ScreenToWorldPoint (secondTouch.position);
						Vector2 deltaPos = swipedPosRed - originPosRed;

						Vector2 newPosCheckRed = objPosRed + deltaPos;
						if (newPosCheckRed.x > -8.4f && newPosCheckRed.x < 8.4f && newPosCheckRed.y > -4.5f && newPosCheckRed.y < 4.5f)
							redObj.transform.position = objPosRed + deltaPos;
					}
					break;
				}
			}
		}
	}
	/*
	void controlBlue(Touch eachTouch){
		Touch firstTouch = eachTouch; 

		switch (firstTouch.phase) {

		case TouchPhase.Began:
			originPosRed = mainCam.ScreenToWorldPoint(firstTouch.position);
			objPosRed = bluObj.transform.position;
			break;

		case TouchPhase.Moved:
			swipedPosRed = mainCam.ScreenToWorldPoint (firstTouch.position);
			Vector2 deltaPos = swipedPosRed - originPosRed;
			bluObj.transform.position = objPosRed + deltaPos;
			break;

		case TouchPhase.Ended:
			break;
		}
	}

	void controlRed(Touch eachTouch){
		Touch secondTouch = eachTouch;

		switch (secondTouch.phase) {

		case TouchPhase.Began:
			originPosBlu = mainCam.ScreenToWorldPoint(secondTouch.position);
			objPosBlu = redObj.transform.position;
			break;

		case TouchPhase.Moved:
			swipedPosBlu = mainCam.ScreenToWorldPoint (secondTouch.position);
			Vector2 deltaPos = swipedPosBlu - originPosBlu;
			redObj.transform.position = objPosBlu + deltaPos;
			break;

		case TouchPhase.Ended:
			break;
		}
	}
	*/
}
