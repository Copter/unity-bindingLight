using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFill : MonoBehaviour {

	Image XPfill;
	float fillpercent;
	float oldXP, newXP;
	int gameEndWait = 0;

	int playerLevel;
	static public int publicPlayerLevel;
	int levelCap = 20;	// Add to extend maximum player level	//	//	//

	public GameObject LevelUpWave;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetFloat ("xp", 0);	// RESETS XPs
		oldXP = PlayerPrefs.GetFloat ("xp", 0);
		playerLevel = PlayerPrefs.GetInt ("plvl", 1);
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject thatScript = GameObject.Find ("Small Fill (4)");
		//AgilitySkillBar BarScript = thatScript.GetComponent<AgilitySkillBar> ();
		//fxp = (float)defBarScript.xp;
		//fmaxxp = (float)defBarScript.maxxp;
		//fillpercent = ((float)BarScript.xp/(float)BarScript.maxxp);

		newXP = PlayerPrefs.GetFloat ("xp", 0);
		if (GameEndScript.gameEnd) {
			if (gameEndWait < 180) {
				gameEndWait++;
			} 
			else {
				if (oldXP < (newXP - (2 * playerLevel)) ) {
					oldXP += 2 * playerLevel;
				} 
				else {
					oldXP = newXP;
				}
			}
		} 
		else {
			gameEndWait = 0;
		}
		float xpGoal = 100f * playerLevel * (playerLevel + 1) / 2;
		float thisLevelxp = oldXP - ( 100f * playerLevel * (playerLevel-1) / 2 );
		fillpercent = thisLevelxp/(100f * playerLevel);
		XPfill = GetComponent<Image>();
		XPfill.fillAmount = fillpercent;

		if (oldXP >= xpGoal && playerLevel < levelCap) {
			playerLevel++;
			ScoreScript ScoreScr = GameObject.Find ("Score Canvas").GetComponent<ScoreScript> ();
			if (ScoreScr.gameScore > 0) {
				Instantiate (LevelUpWave);
			}
		}

		GameObject levelTextObj = GameObject.Find ("Progress Level Text");
		levelTextObj.GetComponent<Text> ().text = "" + playerLevel;

		publicPlayerLevel = playerLevel;
	}
}
