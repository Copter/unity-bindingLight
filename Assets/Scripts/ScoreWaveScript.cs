using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreWaveScript : MonoBehaviour {

	float zoomScale = 0.1f;
	
	void Update () {
		if (zoomScale < 2) {
			transform.localScale = Vector3.one * zoomScale;
			Color defaultColor = gameObject.GetComponent<SpriteRenderer> ().color;
			/*gameObject.GetComponent<SpriteRenderer> ().color = new Color (	defaultColor.r,
																			defaultColor.b,
																			defaultColor.g, 
																			Mathf.Max(0.25f*(1 - zoomScale), 0));*/
			//if(Time.timeScale > 0)
				zoomScale = zoomScale+0.02f;
		} else
			Destroy (gameObject);
	}
}
