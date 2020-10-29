using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour {

	public GameObject redObj;
	public GameObject bluObj;

	void Update () {

		transform.position = (redObj.transform.position + bluObj.transform.position)/2;

		transform.right = -(redObj.transform.position - bluObj.transform.position);

		transform.localScale = new Vector3(Vector3.Distance(redObj.transform.position, bluObj.transform.position)/20, .3f);

		//float angleBetween = Vector2.Angle (redObj.transform.position, bluObj.transform.position);
		//transform.eulerAngles = new Vector3 (0, 0, angleBetween);
		//Vector2 vectorBetween = redObj.transform.position - bluObj.transform.position;

		/*
		float deltaX = redObj.transform.position.x + bluObj.transform.position.x;
		float deltaY = redObj.transform.position.y + bluObj.transform.position.y;
		float midpointX = deltaX/2;
		float midpointY = deltaY/2;
		transform.position = new Vector3 (midpointX, midpointY);
		transform.eulerAngles = new Vector3 (0, 0, Mathf.Tan(deltaY/deltaX));
		*/
	}
}
