using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenieScript : MonoBehaviour {

	public float hitPoints = 1;
	string followWho = "Red";
	float startTime;
	/*
	bool gameEnd;
	int zoomBoost = 0;
	int zoomScale = 100;
	int circlePause = 0;
	*/
	public GameObject greenParticle;
	public GameObject scoreWaveObj;
	Vector3 trailPosition;
	int particleClock = 0;

	public GameObject spawnGreenParticle;

	//int waitForRestart = 0; // TEMPORARY

	void Start () {

		startTime = Time.timeSinceLevelLoad;
		trailPosition = gameObject.transform.position;

		if (Random.Range (0, 2) == 0)
			followWho = "Red";
		else
			followWho = "Blue";

		GameObject emitted = Instantiate (spawnGreenParticle);
		emitted.transform.position = new Vector3 (transform.position.x + 3, transform.position.y + 3, transform.position.z);
		emitted.GetComponent<Rigidbody2D> ().velocity = new  Vector2 (-8, -8);
		emitted = Instantiate (spawnGreenParticle);
		emitted.transform.position = new Vector3 (transform.position.x + 3, transform.position.y - 3, transform.position.z);
		emitted.GetComponent<Rigidbody2D> ().velocity = new  Vector2 (-8, +8);
		emitted = Instantiate (spawnGreenParticle);
		emitted.transform.position = new Vector3 (transform.position.x - 3, transform.position.y + 3, transform.position.z);
		emitted.GetComponent<Rigidbody2D> ().velocity = new  Vector2 (+8, -8);
		emitted = Instantiate (spawnGreenParticle);
		emitted.transform.position = new Vector3 (transform.position.x - 3, transform.position.y - 3, transform.position.z);
		emitted.GetComponent<Rigidbody2D> ().velocity = new  Vector2 (+8, +8);
		
	}

	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, GameObject.Find(followWho).transform.position, Time.deltaTime);

		particleClock++;
		if(particleClock >= 10 && Time.timeScale > 0){
			GameObject emitted = Instantiate (greenParticle);
			emitted.transform.position = trailPosition;
			trailPosition = gameObject.transform.position;
			particleClock = 0;
		}

		Image hpFill = transform.Find("Canvas/Fill").GetComponent<Image>();
		hpFill.fillAmount = hitPoints;

		if (hitPoints <= 0) {

			ScoreScript ScoreScr = GameObject.Find("Score Canvas").GetComponent<ScoreScript>();
			ScoreScr.gameScore += 1;
			ScoreScr.rawScore += 1;
			ScoreScr.comboCount += 1;
			ScoreScr.comboTimer = 30;
			ScoreScr.multiCount += 1;
			ScoreScr.multiTimer = 5;
			if(ScoreScr.comboCount >= 5)
				ScoreScr.gameScore += Mathf.Min(ScoreScr.comboCount, 100) / 100;
			if(ScoreScr.multiCount >= 2)
				ScoreScr.gameScore += 1;

			Instantiate (scoreWaveObj);

			for (int i = 0; i < 3; i++) {
				GameObject emitted = Instantiate (greenParticle);
				emitted.transform.position = new Vector3 (transform.position.x + Random.Range (-.5f, .5f), transform.position.y + Random.Range (-.5f, .5f), transform.position.z);
				emitted.GetComponent<Rigidbody2D> ().velocity = Vector2.MoveTowards (transform.position, transform.position, -Time.deltaTime * Random.Range (.1f, .2f));
				//emitted.GetComponent<Rigidbody2D> ().velocity = Vector2.MoveTowards (emitted.transform.position, transform.position, -Time.deltaTime * Random.Range (.1f, .2f));
			}

			Destroy (gameObject);
		}
			/*
		if (gameEnd) {
			GameObject.Find ("End Canvas/Mask").GetComponent<RectTransform> ().position = gameObject.transform.position;

			GameEndScript EndScr = GameObject.Find ("EventSystem").GetComponent<GameEndScript> ();
			EndScr.OnGameEnd ();
		}*/
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.name == "Beam") {
			float dmg = 0.03f / GameObject.Find ("Beam").transform.localScale.x;
			hitPoints -= dmg;
		}
	}

	/*
	public void OnDestroy ()
	{
		for (int i = 0; i < 3; i++) {
			GameObject emitted = Instantiate (greenParticle);
			emitted.transform.position = new Vector3 (transform.position.x + Random.Range (-.5f, .5f), transform.position.y + Random.Range (-.5f, .5f), transform.position.z);
			emitted.GetComponent<Rigidbody2D> ().velocity = Vector2.MoveTowards (emitted.transform.position, transform.position, -Time.deltaTime * Random.Range (.1f, .2f));
		}
	}
	*/

	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			if (hitPoints > 0.05f) {
				/*
				GameObject[] greens = GameObject.FindGameObjectsWithTag ("Green");

				foreach (GameObject green in greens)
					Destroy (green);
				*/

				//ScoreScr.gameScore = 0;

				GameObject.Find ("End Canvas/Mask").GetComponent<RectTransform> ().position = gameObject.transform.position;

				GameEndScript EndScr = GameObject.Find ("EventSystem").GetComponent<GameEndScript> ();
				EndScr.OnGameEnd ();
			} else {
				hitPoints = 0;
			}
		}
	}

	void OnBecameInvisible() {
		if(Time.timeSinceLevelLoad - startTime > 5){
			Destroy (gameObject);
		}
	}
}
