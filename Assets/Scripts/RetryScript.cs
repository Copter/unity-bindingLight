using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryScript : MonoBehaviour {

	Vector3 Opos;
	float btnSlide = 1;

	void Start () {
		Opos = transform.position;
		transform.position += Vector3.right*4;
	}

	void Update () {
		GameEndScript EndScr = GameObject.Find ("EventSystem").GetComponent<GameEndScript> ();
		if (EndScr.waitBeforeResult == 0) {
			transform.position = new Vector3(Opos.x+4-(4*(1-btnSlide)), Opos.y, Opos.z);
			btnSlide = btnSlide * 0.9f;
		}
		else
		{
			transform.position = new Vector3(Opos.x+4, Opos.y, Opos.z);
		}
	}

	public void RetryBtnPressed (){
		if(GameEndScript.gameEnd)
		{
			GameEndScript EndScr = GameObject.Find ("EventSystem").GetComponent<GameEndScript> ();
			EndScr.restartGame = true;
		}
	}
}
