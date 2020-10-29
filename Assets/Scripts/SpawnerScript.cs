using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject greenies;
	public int extraGreen = 0;
	public int nextLevelIn = 30;
	public int swarmTimer = 0;

	void Start () {
		StartCoroutine(SpawnerClock ());
		StartCoroutine(DifficultyClock ());
	}

	void Update () {
		/*
		if(GameObject.FindGameObjectsWithTag("Green").Length == 0){
			for (int i = 0; i < 20; i++) {
				Spawn ();
			}
		}
		*/
	}

	IEnumerator SpawnerClock() {
		if (swarmTimer < 5) {
			for (int i = 0; i < 3 + extraGreen; i++) {
				Spawn ();
			}
			yield return new WaitForSeconds (3);
			for (int i = 0; i < 3 + extraGreen; i++) {
				Spawn ();
			}
			yield return new WaitForSeconds (2);
			if(GameStartScript.gameStart)
				swarmTimer++;
		} else {
			yield return new WaitForSeconds (2);
			SwarmSpawn.swarmTrigger = 1;
			yield return new WaitForSeconds (3);
			swarmTimer=0;
		}
		StartCoroutine(SpawnerClock ());
	}

	IEnumerator DifficultyClock() {
		if(GameStartScript.gameStart == true)
			nextLevelIn--;
		if (nextLevelIn <= 0 && extraGreen < 5) {
			extraGreen++;
			nextLevelIn = 60;
		}
		yield return new WaitForSeconds (1);
		StartCoroutine(DifficultyClock ());
	}

	void Spawn () {
		if(GameStartScript.gameStart && GameObject.FindGameObjectsWithTag("Green").Length < 99){
			GameObject spawned = Instantiate(greenies);
			int decidePos = Random.Range (0, 4);
			if (decidePos == 0)
				spawned.transform.position = new Vector3(-10, Random.Range (-6f, 6f));
			if (decidePos == 1)
				spawned.transform.position = new Vector3(10, Random.Range (-6f, 6f));
			if (decidePos == 2)
				spawned.transform.position = new Vector3(Random.Range (-10f, 10f) ,6);
			if (decidePos == 3)
				spawned.transform.position = new Vector3(Random.Range (-10f, 10f) ,-6);
		}
	}
}
