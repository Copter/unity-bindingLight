using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public float gameScore = 0;
	public float comboCount = 0;
	public float maxCombo = 0;
	public float comboTimer = 0;
	public float rawScore = 0;
	public float gameTime = 0;
	public float multiCount = 0;
	public float multiTimer = 0;

	public float comboBlink;
	public float comboDuration;
	float bestScoreFade = 50;
	public bool timeReset;
	int quoteRand = 0;
	Color defaultColor;
	float colorFade;
	float fadeTextTimer;

	public AudioClip[] comboSFX = new AudioClip[23];
	public float comboBeforeSound = 0;
	AudioSource audioSource;

	static public bool showHint = false;

	// Use this for initialization
	void Start () {
		/*PlayerPrefs.SetFloat ("record", 0);
		PlayerPrefs.SetFloat ("record raw", 0);
		PlayerPrefs.SetFloat ("record combo", 0);
		PlayerPrefs.SetFloat ("record time", 0);*/	// For resetting best score.
		defaultColor = GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().color;
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(GameClock ());
	}
	
	// Update is called once per frame
	void Update () {

		GameObject.Find ("Score Canvas/Score Text").GetComponent<Text> ().text = Mathf.Floor (gameScore).ToString ();
		if (gameScore == 0) {
			if (GameStartScript.gameStart) {
				if (!GameEndScript.gameEnd)
					GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = Mathf.Floor (PlayerPrefs.GetFloat ("record", 0)).ToString ();
				GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().color = defaultColor;
				GameObject.Find ("Score Canvas/Score Text").GetComponent<Text> ().color = defaultColor;
				GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().fontSize = 48;
				if(showHint && !GameEndScript.gameEnd)
					GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = "Burn green squares with the beam";
				else
					GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = "";
				colorFade = 50f/255f;
			} else {
				colorFade = (Mathf.Abs(50f - fadeTextTimer) + 50f)/255f;
				GameObject.Find ("Score Canvas/Score Text").GetComponent<Text> ().text = Mathf.Floor (PlayerPrefs.GetFloat ("record", 0)).ToString ();
				GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().fontSize = 48;
				GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = "Hold with two fingers to start";
			}
			GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().color = new Color (colorFade, colorFade, colorFade, 1);
			if (fadeTextTimer < 100)
				fadeTextTimer++;
			else
				fadeTextTimer = 0;
		} else {
			GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().fontSize = 96;
			if (gameScore > PlayerPrefs.GetFloat ("record", 0)) {
				if(bestScoreFade < 100f)
					bestScoreFade = bestScoreFade + .5f;
				GameObject.Find ("Score Canvas/Score Text").GetComponent<Text> ().color = new Color (bestScoreFade / 255f, bestScoreFade / 255f, bestScoreFade / 255f, 1);
			} else {
				GameObject.Find ("Score Canvas/Score Text").GetComponent<Text> ().color = defaultColor;	// Assuming Combo Text and Score Text have the same color.
			}

			if (comboCount < 5) {
				GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = "";
				GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "";
				quoteRand = Random.Range (0, 2);
			}
			if (comboCount >= 5) {
				GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = comboCount + "x";

				comboDuration = comboTimer + comboBlink;
				float textBrightness = (50 + (Mathf.Min (comboCount, 100f) *(0.5f) )) / 255f;
				GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().color = new Color (textBrightness, textBrightness, textBrightness, 1);
				GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().color = new Color (textBrightness, textBrightness, textBrightness, comboDuration / 50f);

				if (comboCount >= 5) {
					if (quoteRand == 0)
						GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Cool!";
					if (quoteRand == 1)
						GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Nice!";
				}
				if (comboCount >= 10) {
					if (quoteRand == 0)
						GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Great!";
					if (quoteRand == 1)
						GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Super!";
				}
				if (comboCount >= 20) {
					if (quoteRand == 0)
						GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Excellent!";
					if (quoteRand == 1)
						GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Awesome!";
				}
				if (comboCount >= 50) {
					if (quoteRand == 0)
						GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Marvelous!";
					if (quoteRand == 1)
						GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Incredible!";
				}
				if (comboCount >= 100) {
					GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Unstoppable!!";
				}
			}
			if (multiTimer > 9) {
				float textBrightness = 100 / 255f;
				GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().color = new Color (textBrightness, textBrightness, textBrightness, multiTimer /5f);
				if (multiCount >= 2)
					GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Double!";
				if (multiCount >= 3)
					GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Triple!";
				if (multiCount >= 4)
					GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Quadruple!";
				if (multiCount >= 5)
					GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = "Five in a row!!";
				if (multiCount >= 6)
					GameObject.Find ("Score Canvas/Quote Text").GetComponent<Text> ().text = multiCount + " in a row!!";
			}
		}

		if (Time.timeScale > 0) {
			if (comboTimer > 0) {
				comboTimer--;
				comboBlink = 20;
			} else {
				if (comboCount >= 5) {
					if (comboBlink == 20 || comboBlink == 12 || comboBlink == 4)
						GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = "";
					if (comboBlink == 16 || comboBlink == 8)
						GameObject.Find ("Score Canvas/Combo Text").GetComponent<Text> ().text = comboCount + "x";
				}
				if(comboBlink == 0)
					comboCount = 0;
				if(comboBlink > 0)
					comboBlink--;
			}
			if (multiTimer > 0) {
				multiTimer--;
			}
			if (multiTimer <= 0) {
				multiCount = 0;
			}
		}
		if(maxCombo < comboCount)
			maxCombo = comboCount;

		if (comboCount != comboBeforeSound) {
			if (comboCount > comboBeforeSound) 
				PlayComboSFX (Mathf.FloorToInt (comboCount));
			comboBeforeSound = comboCount;
		}
	}

	IEnumerator GameClock (){
		
		if (gameScore == 0 && !timeReset) {
			timeReset = true;
			gameTime = 0;
		}
		yield return new WaitForSeconds (1);
		if(Time.timeScale > 0 && GameStartScript.gameStart){
			gameTime++;
		}


		StartCoroutine(GameClock ());
	}

	void PlayComboSFX (int comboNumber){
		
		float playVolume = PlayerPrefs.GetFloat ("SFXVolume", 1f);
		if(comboNumber<21)
			audioSource.PlayOneShot(comboSFX[comboNumber], playVolume);
		else if(comboNumber>=21 && comboNumber < 100)
			audioSource.PlayOneShot(comboSFX[21], playVolume);
		else if(comboNumber>=100)
			audioSource.PlayOneShot(comboSFX[22], playVolume);
	}
}
