using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour {

	public GameObject scoreText;
	public GameObject recordText;
	public GameObject rawScoreText;
	public GameObject maxComboText;
	public GameObject timeText;

	//float recordScore;
	float fadeTextTimer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		ScoreScript ScoreScr = GameObject.Find ("Score Canvas").GetComponent<ScoreScript> ();

		scoreText.GetComponent<Text>().text = Mathf.Floor (ScoreScr.gameScore).ToString ();

		float recordScore = Mathf.Floor (PlayerPrefs.GetFloat ("record", 0));
		if (GameEndScript.newRecord) {
			recordText.GetComponent<Text> ().text = "";
			float colorFade = (255f - Mathf.Abs(100f - fadeTextTimer))/255f;
			scoreText.GetComponent<Text> ().color = new Color (colorFade, colorFade, colorFade, 1);
		} else {
			recordText.GetComponent<Text> ().text = recordScore.ToString ();
			scoreText.GetComponent<Text> ().color = Color.white;
		}

		rawScoreText.GetComponent<Text>().text = Mathf.Floor (ScoreScr.rawScore).ToString ();
		//float recordRawScore = Mathf.Floor (PlayerPrefs.GetFloat ("record raw", 0));
		if (GameEndScript.newRecordRaw) {
			float colorFade = (255f - Mathf.Abs(100f - fadeTextTimer))/255f;
			rawScoreText.GetComponent<Text> ().color = new Color (colorFade, colorFade, colorFade, 1);
		} else {
			rawScoreText.GetComponent<Text> ().color = Color.white;
		}

		maxComboText.GetComponent<Text>().text = Mathf.Floor (ScoreScr.maxCombo)+"x";
		//float recordComboScore = Mathf.Floor (PlayerPrefs.GetFloat ("record combo", 0));
		if (GameEndScript.newRecordCombo) {
			float colorFade = (255f - Mathf.Abs(100f - fadeTextTimer))/255f;
			maxComboText.GetComponent<Text> ().color = new Color (colorFade, colorFade, colorFade, 1);
		} else {
			maxComboText.GetComponent<Text> ().color = Color.white;
		}

		float gameTimeInSecs = Mathf.Floor (ScoreScr.gameTime);
		float gameTimeM = Mathf.Floor (gameTimeInSecs / 60f);
		float gameTimeS = gameTimeInSecs - (gameTimeM*60);

		timeText.GetComponent<Text> ().text = gameTimeM + ":" + gameTimeS;
		if(gameTimeS < 10)
			timeText.GetComponent<Text> ().text = gameTimeM + ":0" + gameTimeS;
		//float recordTime = Mathf.Floor (PlayerPrefs.GetFloat ("record time", 0));
		if (GameEndScript.newRecordTime) {
			float colorFade = (255f - Mathf.Abs(100f - fadeTextTimer))/255f;
			timeText.GetComponent<Text> ().color = new Color (colorFade, colorFade, colorFade, 1);
		} else {
			timeText.GetComponent<Text> ().color = Color.white;
		}

		if (fadeTextTimer < 200)
			fadeTextTimer += 4;
		else
			fadeTextTimer = 0;
	}
	/*
			float recordScore = PlayerPrefs.GetFloat ("record", 0);
			if(ScoreScr.gameScore > recordScore)
				PlayerPrefs.SetFloat ("record", ScoreScr.gameScore);
			float recordRaw = PlayerPrefs.GetFloat ("record raw", 0);
			if(ScoreScr.rawScore > recordRaw)
				PlayerPrefs.SetFloat ("record raw", ScoreScr.rawScore);
			float recordCombo = PlayerPrefs.GetFloat ("record combo", 0);
			if(ScoreScr.maxCombo > recordCombo)
				PlayerPrefs.SetFloat ("record combo", ScoreScr.maxCombo);
			float recordTime = PlayerPrefs.GetFloat ("record time", 0);
			if(ScoreScr.gameTime > recordTime)
				PlayerPrefs.SetFloat ("record time", ScoreScr.gameTime);
	*/
}
