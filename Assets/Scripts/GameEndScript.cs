using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScript : MonoBehaviour {

	static public bool gameEnd = false;
	public bool restartGame = false;

	int zoomBoost = 0;
	int zoomScale = 100;
	int circlePause = 0;

	public GameObject greenParticle;
	Vector3 trailPosition;

	//int waitForRestart = 0; // TEMPORARY
	float resultSlide = 1;
	public float waitBeforeResult = 30;	
	Vector3 resultOPos;
	bool shatteredOnce = false;

	static public bool newRecord = false;
	static public bool newRecordRaw = false;
	static public bool newRecordCombo = false;
	static public bool newRecordTime = false;

	bool gainXpOnce = false;

	public void OnGameEnd () {
		gameEnd = true;
	}

	public void Start(){
		resultOPos = GameObject.Find ("Result Canvas/Result Panel").GetComponent<RectTransform> ().position;
	}

	public void Update () {

		if (gameEnd) {
			Time.timeScale = 0;

			GameObject.Find ("End Canvas/Mask").GetComponent<RectTransform> ().sizeDelta = new Vector2 (zoomScale, zoomScale);
			GameObject.Find ("End Canvas/Mask/BaW Image").GetComponent<RectTransform> ().position = Vector3.zero;

			ScoreScript ScoreScr = GameObject.Find ("Score Canvas").GetComponent<ScoreScript> ();
			float recordScore = PlayerPrefs.GetFloat ("record", 0);
			if (ScoreScr.gameScore > recordScore) {
				PlayerPrefs.SetFloat ("record", ScoreScr.gameScore);
				newRecord = true;
			}
			float recordRaw = PlayerPrefs.GetFloat ("record raw", 0);
			if (ScoreScr.rawScore > recordRaw) {
				PlayerPrefs.SetFloat ("record raw", ScoreScr.rawScore);
				newRecordRaw = true;
			}
			float recordCombo = PlayerPrefs.GetFloat ("record combo", 0);
			if (ScoreScr.maxCombo > recordCombo) {
				PlayerPrefs.SetFloat ("record combo", ScoreScr.maxCombo);
				newRecordCombo = true;
			}
			float recordTime = PlayerPrefs.GetFloat ("record time", 0);
			if (ScoreScr.gameTime > recordTime) {
				PlayerPrefs.SetFloat ("record time", ScoreScr.gameTime);
				newRecordTime = true;
			}

			if (!gainXpOnce) {				
				float newXP = PlayerPrefs.GetFloat ("xp", 0) + ScoreScr.gameScore;
				PlayerPrefs.SetFloat ("xp", newXP);
				gainXpOnce = true;
			}

			ScoreScr.comboCount = 0;
			ScoreScr.comboTimer = 0;
			ScoreScr.multiCount = 0;
			ScoreScr.multiTimer = 0;
			ScoreScr.comboBlink = 0;

			GameObject.Find ("EventSystem").GetComponent<SwarmSpawn> ().sequenceTimer = 0;
			GameObject.Find ("EventSystem").GetComponent<SwarmSpawn> ().sequenceChoose = 0;

			GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = "";
			GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "";

			circlePause++;

			if (zoomScale < 2000) 
			{
				zoomScale = zoomScale + zoomBoost;
				if (circlePause > 30)
					zoomBoost += 2;
			} 
			else
			{
				GameObject.Find ("End Canvas/Mask").GetComponent<RectTransform> ().sizeDelta = Vector3.zero;
				//GameObject.Find ("End Canvas/Mask/BaW Image").GetComponent<RectTransform> ().position = Vector3.zero;

				GameObject.Find ("Result Canvas/Result Panel").GetComponent<RectTransform> ().position = new Vector3(resultOPos.x,(resultOPos.y * resultSlide)+.8f,resultOPos.z);
				if (waitBeforeResult <= 0 && resultSlide > 0)
					resultSlide = (resultSlide * 0.9f);
				if(waitBeforeResult > 0)
					waitBeforeResult--;

				GameObject.Find ("Red").GetComponent<SpriteRenderer> ().enabled = false;
				GameObject.Find ("Blue").GetComponent<SpriteRenderer> ().enabled = false;
				GameObject.Find ("Beam").GetComponent<SpriteRenderer> ().enabled = false;
				if (!shatteredOnce) {
					PlayerPtcEmitScript shatterScriptRed = GameObject.Find ("Red").GetComponent<PlayerPtcEmitScript> ();
					shatterScriptRed.Shatter ();
					PlayerPtcEmitScript shatterScriptBlue = GameObject.Find ("Blue").GetComponent<PlayerPtcEmitScript> ();
					shatterScriptBlue.Shatter ();
					shatteredOnce = true;
				}

				GameObject[] greens = GameObject.FindGameObjectsWithTag ("Green");
				foreach (GameObject green in greens) {
					for (int i = 0; i < 3; i++) {
						GameObject emitted = Instantiate (greenParticle);
						emitted.transform.position = new Vector3 (green.transform.position.x + Random.Range (-.5f, .5f),
							green.transform.position.y + Random.Range (-.5f, .5f),
							green.transform.position.z);
						emitted.GetComponent<Rigidbody2D> ().velocity = Vector2.MoveTowards (emitted.transform.position, green.transform.position, -Time.deltaTime * Random.Range (.1f, .2f));
					}
					Destroy (green);
				}
			}


			if (restartGame) {
				/*
				waitForRestart++;
				if (waitForRestart >= 30) {
					float reverseZoom = 2000 - ((waitForRestart - 30) * 66.67f);
					GameObject.Find ("End Canvas/Mask").GetComponent<RectTransform> ().sizeDelta = new Vector2 (reverseZoom, reverseZoom);
					GameObject.Find ("End Canvas/Mask/BaW Image").GetComponent<RectTransform> ().position = Vector3.zero;
				}
				if (waitForRestart >= 60) {
				*/

				GameObject[] greens = GameObject.FindGameObjectsWithTag ("Green");
				foreach (GameObject green in greens) {
					for (int i = 0; i < 3; i++) {
						GameObject emitted = Instantiate (greenParticle);
						emitted.transform.position = new Vector3 (green.transform.position.x + Random.Range (-.5f, .5f),
							green.transform.position.y + Random.Range (-.5f, .5f),
							green.transform.position.z);
						emitted.GetComponent<Rigidbody2D> ().velocity = Vector2.MoveTowards (emitted.transform.position, green.transform.position, -Time.deltaTime * Random.Range (.1f, .2f));
					}
					Destroy (green);
				}

				GameObject.Find ("Red").GetComponent<SpriteRenderer> ().enabled = true;
				GameObject.Find ("Blue").GetComponent<SpriteRenderer> ().enabled = true;
				GameObject.Find ("Beam").GetComponent<SpriteRenderer> ().enabled = true;

				GameObject.Find ("End Canvas/Mask").GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
				GameObject.Find ("End Canvas/Mask/BaW Image").GetComponent<RectTransform> ().position = Vector3.zero;

				PauseScript PauseScr = GameObject.Find ("Pause Button").GetComponent<PauseScript> ();
				PauseScr.doubleTap = 0;

				SpawnerScript SpawnScr = GameObject.Find ("EventSystem").GetComponent<SpawnerScript> ();
				SpawnScr.extraGreen = 0;
				SpawnScr.nextLevelIn = 30;
				SpawnScr.swarmTimer = 0;

				if(ScoreScr.gameScore == 0)
					ScoreScript.showHint=true;
				else
					ScoreScript.showHint=false;

				ScoreScr.gameScore = 0;
				ScoreScr.maxCombo = 0;
				ScoreScr.rawScore = 0;
				ScoreScr.gameTime = 0;
				ScoreScr.timeReset = false;

				newRecord = false;
				newRecordRaw = false;
				newRecordCombo = false;
				newRecordTime = false;

				resultSlide = 0.001f;
				waitBeforeResult = 30;	
				restartGame = false;
				gameEnd = false;
				zoomBoost = 0;
				zoomScale = 100;
				circlePause = 0;
				shatteredOnce = false;

				gainXpOnce = false;

				Time.timeScale = 1;
				//waitForRestart = 0;
				//}
			}
		}		


		if (waitBeforeResult > 0) {
			GameObject.Find ("Result Canvas/Result Panel").GetComponent<RectTransform> ().position = new Vector3(resultOPos.x, resultOPos.y * resultSlide ,resultOPos.z);
			if (resultSlide < 1)
				resultSlide = resultSlide*9f;
			if (resultSlide > 1)
				resultSlide = 1;
		}


	}
}

