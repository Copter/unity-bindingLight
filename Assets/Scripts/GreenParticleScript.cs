using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenParticleScript : MonoBehaviour {

	float fading = .5f;
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, fading);
		fading -= 0.016f;
		if (fading < 0)
			Destroy (gameObject);
	}
}
