using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPtcEmitScript : MonoBehaviour {

	public GameObject Particle;
	public GameObject Shard;
	int emitT = 0;

	void Update () {
		if (emitT >= 3) {
			if (!GameEndScript.gameEnd) 
			{
				GameObject emitted = Instantiate (Particle);
				emitted.transform.position = transform.position;
			}
			emitT = 0;
		} else {
			emitT++;
		}
	}

	public void Shatter(){
		for(int i = 0; i < 20; i++){
			GameObject emitted = Instantiate (Shard);
			emitted.transform.position = new Vector3 (	transform.position.x + Random.Range (-.5f, .5f), 
				transform.position.y + Random.Range (-.5f, .5f), 
				transform.position.z);
		}
	}
}
