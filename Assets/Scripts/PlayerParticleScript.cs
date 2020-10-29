using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleScript : MonoBehaviour {

	float fading = .5f;
	Color defaultColor;

	void Start () {
		defaultColor = gameObject.GetComponent<SpriteRenderer> ().color;
	}

	void Update () {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (defaultColor.r, defaultColor.b, defaultColor.g, fading);
			gameObject.transform.localScale = new Vector3 (.1f * fading, .1f * fading, 1);
			fading -= 0.02f;
			if (fading < 0)
				Destroy (gameObject);
	}
}
