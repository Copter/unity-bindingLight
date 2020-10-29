using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueShardScript : MonoBehaviour {

	float fading = 1;
	Color defaultColor;
	float shardSpeed;
	float shardSpin;

	void Start () {
		defaultColor = gameObject.GetComponent<SpriteRenderer> ().color;
		shardSpeed = Random.Range (-0.1f, -0.005f);
		shardSpin = Random.Range (-30f, 30f);
	}

	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Blue").transform.position, shardSpeed);
		transform.Rotate(transform.forward*shardSpin);

		gameObject.GetComponent<SpriteRenderer> ().color = new Color (defaultColor.r, defaultColor.b, defaultColor.g, fading);
		gameObject.transform.localScale = new Vector3 (.1f * fading, .1f * fading, 1);
		fading -= 0.02f;
		if (fading < 0)
			Destroy (gameObject);
	}
}
