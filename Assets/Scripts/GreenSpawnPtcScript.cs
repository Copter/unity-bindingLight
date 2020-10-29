using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpawnPtcScript : MonoBehaviour {

	int growSize;

	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.zero;
		StartCoroutine (TimeTillDelete ());
	}
	
	// Update is called once per frame
	void Update () {
		if (growSize < 10) {
			growSize++;
			transform.localEulerAngles = new Vector3 (0,0,-90 + (9*growSize));
			transform.localScale = new Vector3 (growSize*0.003f, growSize*0.003f, 0);
		}

		if(GameEndScript.gameEnd)
			Destroy (gameObject);
	}

	IEnumerator TimeTillDelete() {
		yield return new WaitForSeconds (0.33f);
		Destroy (gameObject);
	}
}
