using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpawn : MonoBehaviour {

	public GameObject greenStraight;
	public int sequenceTimer = 0;
	public int sequenceChoose = 0;
	static public int swarmTrigger = 0;
	int patternCount = 9;	// Add this number up whenever a new pattern is added.	//	//	//
	public int patternsInPlay;
	public int targetPlayerNo = 0;

	public GameObject[] patternObjects = new GameObject[99];	// Add the number in Unity *Inspector* up whenever a new pattern that needs an object is added.	//	//	//

	void Start () {
	}

	float orbitClock = 0;

	void FixedUpdate () {
		if (swarmTrigger == 1) {
			if(sequenceChoose == 0)	// For debugging specific pattern
				sequenceChoose = Random.Range (1, Mathf.Min( Mathf.Max(LevelFill.publicPlayerLevel, 2), patternCount) + 1);
			swarmTrigger = 0;
		}

		patternsInPlay = Mathf.Min(LevelFill.publicPlayerLevel, patternCount);

		if (sequenceChoose == 1) {
			if (sequenceTimer == 0) {
				GameObject firstCreated = Instantiate (patternObjects [3]);
				firstCreated.transform.position = new Vector2 (-12, 0);
				firstCreated.name = "Triple Size";
			}
			GameObject thatPattern = GameObject.Find ("Triple Size");
			//if (GameObject.Find ("BigGreenFollow")) {
			if (sequenceTimer < 870) {
				string followWho;
				float dstToR = Vector2.Distance (thatPattern.transform.position, GameObject.Find ("Red").transform.position);
				float dstToB = Vector2.Distance (thatPattern.transform.position, GameObject.Find ("Blue").transform.position);
				if (dstToR > dstToB)
					followWho = "Blue";
				else
					followWho = "Red";
				thatPattern.transform.position = Vector3.MoveTowards (thatPattern.transform.position, GameObject.Find (followWho).transform.position, Time.deltaTime);
			} else {
				if (GameObject.Find ("Triple Size")) {
					foreach (Transform child in thatPattern.transform) {
						//transform.position = Vector3.MoveTowards (thatPattern.transform.position, Vector3.zero, -Time.deltaTime * 1.5f);
						if (child.name != "Triple Size")
							child.GetComponent<Rigidbody2D> ().velocity = Vector3.right * Time.timeScale * 1.2f;
							//child.GetComponent<Rigidbody2D> ().velocity = Vector3.MoveTowards (thatPattern.transform.position, Vector3.zero, -Mathf.Sqrt(Time.deltaTime));
						//child.GetComponent<Rigidbody2D>().velocity = Vector3.MoveTowards (thatPattern.transform.position, Vector3.zero, -Mathf.Sqrt(Time.deltaTime));
						//print("for each "+child);
					}
				
					thatPattern.transform.DetachChildren ();
					Destroy (GameObject.Find ("Triple Size"));
				}
			}
			//}

			//if (sequenceTimer == 871) {	}
			PatternClock (900);
		}

		if (sequenceChoose == 2) {
			if (sequenceTimer == 0) {
				GameObject firstCreated = Instantiate (patternObjects[0]);
				firstCreated.name = "GreenFishPattern";
				firstCreated.transform.position = new Vector2 (10, 0);
				firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.left * 2f;
			}
			if (GameObject.Find ("GreenFishPattern")) {
				GameObject thatPattern = GameObject.Find ("GreenFishPattern");
				float accelerate = 1f;
				if (sequenceTimer >= 40 && sequenceTimer < 60)
					accelerate = accelerate * 0.2f;
				if (sequenceTimer == 60)
					accelerate = 1;

				if (sequenceTimer >= 100 && sequenceTimer < 120)
					accelerate = accelerate * 0.2f;
				if (sequenceTimer == 120)
					accelerate = 1;

				if (sequenceTimer >= 160 && sequenceTimer < 180)
					accelerate = accelerate * 0.2f;
				if (sequenceTimer == 180)
					accelerate = 1;

				if (sequenceTimer >= 220 && sequenceTimer < 240)
					accelerate = accelerate * 0.2f;
				if (sequenceTimer == 240)
					accelerate = 1;

				if (sequenceTimer >= 280 && sequenceTimer < 300)
					accelerate = accelerate * 0.2f;
				if (sequenceTimer == 300)
					accelerate = 1;

				if (sequenceTimer >= 340 && sequenceTimer < 360)
					accelerate = accelerate * 0.2f;
				if (sequenceTimer == 360)
					accelerate = 1;

				if (sequenceTimer >= 400 && sequenceTimer < 420)
					accelerate = accelerate * 0.2f;
				if (sequenceTimer == 420)
					accelerate = 1;

				thatPattern.GetComponent<Rigidbody2D> ().velocity = Vector2.left * 3f * accelerate;

				if (sequenceTimer == 480)
					Destroy (thatPattern);
			}
			PatternClock (480);
		}

		if (sequenceChoose == 3) {
			if (sequenceTimer == 0) {
				GameObject firstCreated = Instantiate (greenStraight);
				firstCreated.name = "GreenFanSpread";
				firstCreated.transform.position = new Vector2 (0, 6);
				firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.down * 2 /* Time.deltaTime*/;
			}
			if(GameObject.Find ("GreenFanSpread")){
				GameObject greenFanSpread = GameObject.Find ("GreenFanSpread");
				if (sequenceTimer == 50) {
					greenFanSpread.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				}
				if (sequenceTimer >= 75 && sequenceTimer < 85) {
					GameObject createdObj = Instantiate (greenStraight);
					createdObj.transform.position = greenFanSpread.transform.position;
					createdObj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-4f, 4f), -2);
				}
				if (sequenceTimer == 85) {
					greenFanSpread.GetComponent<Rigidbody2D> ().velocity = Vector2.down * 2 /* Time.deltaTime*/;
				}
			}
			PatternClock (90);
		}

		if (sequenceChoose == 4) {
			if (sequenceTimer == 0) {
				GameObject firstCreated = Instantiate (patternObjects [1]);
				firstCreated.name = "ZigZagPattern";
				firstCreated.transform.position = new Vector2 (10, 0);
				firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.left * 1.5f;
			}
			if (sequenceTimer >= 900)
				Destroy (GameObject.Find("ZigZagPattern"));
			PatternClock (900);
		}

		if (sequenceChoose == 5) {
			if (sequenceTimer < 120 && sequenceTimer % 24 == 0) {
				GameObject firstCreated = Instantiate (greenStraight);
				firstCreated.name = "GreenRightCharge";
				firstCreated.transform.position = new Vector2 (10, GameObject.Find ("Red").transform.position.y);
				firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.left * 1.2f;
			}
			if (sequenceTimer < 120 && sequenceTimer % 24 == 12) {
				GameObject firstCreated = Instantiate (greenStraight);
				firstCreated.name = "GreenRightCharge";
				firstCreated.transform.position = new Vector2 (10, GameObject.Find ("Blue").transform.position.y);
				firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.left * 1.2f;
			}
			PatternClock (150);
		}

		if (sequenceChoose == 6) {
			float orbX, orbY;
			if (sequenceTimer == 0) {
				GameObject firstCreated = Instantiate (patternObjects [2]);
				firstCreated.name = "OrbitPattern";
				firstCreated.transform.position = new Vector2 (12, -.5f);
				firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.left * 1.5f;

				if (GameObject.Find ("Orbit Small")) {
					GameObject orbitSmall = GameObject.Find ("Orbit Small");
					orbitSmall.transform.position = new Vector2 (firstCreated.transform.position.x + 3.5f, firstCreated.transform.position.y);
				}
			}
			if (sequenceTimer > 0) {
				if (GameObject.Find ("Orbit Small") && GameObject.Find ("Orbit Big")) {
					GameObject orbitSmall = GameObject.Find ("Orbit Small");
					GameObject orbitBig = GameObject.Find ("Orbit Big");

					orbitClock += (Mathf.PI/180f) * Time.timeScale;

					orbX = Mathf.Cos(orbitClock);
					orbY = Mathf.Sin(orbitClock);

					orbitSmall.transform.position = new Vector2 (orbX * 3 + orbitBig.transform.position.x + 0.5f, orbY * 3 + orbitBig.transform.position.y);

					//print ("Orbit!");
				}
			}
			if (sequenceTimer >= 900)
				Destroy (GameObject.Find ("OrbitPattern"));
			PatternClock (900);
		}

		if (sequenceChoose == 7) {
			int PatternDuration = 60;
			if (sequenceTimer < PatternDuration && sequenceTimer % 3 == 0) {
				GameObject firstCreated = Instantiate (greenStraight);
				firstCreated.name = "GreenLeftSway";
				firstCreated.transform.position = new Vector2 (-10, 0);
				Vector3	launchDir;
				if(targetPlayerNo == 0)
					launchDir = GameObject.Find("Red").transform.position - new Vector3 (-10, 0, 0);
				else
					launchDir = GameObject.Find("Blue").transform.position - new Vector3 (-10, 0, 0);
				Vector3 launchVelo;
				float LaunchSway;
				LaunchSway = launchDir.y; //+ ((sequenceTimer - (PatternDuration/2)) / 20f);
				if(launchDir.x >= launchDir.y)
					launchVelo = new Vector3(launchDir.x/Mathf.Abs(launchDir.x), LaunchSway/Mathf.Abs(launchDir.x));
				else
					launchVelo = new Vector3(launchDir.x/Mathf.Abs(launchDir.y), LaunchSway/Mathf.Abs(launchDir.y));
				firstCreated.GetComponent<Rigidbody2D> ().velocity = launchVelo * (1 + (sequenceTimer/PatternDuration)) * 1.2f;
			}
			PatternClock (PatternDuration);
		}

		if (sequenceChoose == 8) {
			if (sequenceTimer == 0) {
				GameObject firstCreated = Instantiate (greenStraight);
				firstCreated.name = "GreenLeftBranch";
				firstCreated.transform.position = new Vector2 (-10, 0);
				firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.right * 1.5f;
			}
			if (GameObject.Find ("GreenLeftBranch")) {
				GameObject BigBranch = GameObject.Find ("GreenLeftBranch");
				if (sequenceTimer > 60 && sequenceTimer < 540 && sequenceTimer % 60 == 0) {
					GameObject firstCreated = Instantiate (greenStraight);
					firstCreated.name = "GreenRightTwigs";
					firstCreated.transform.position = BigBranch.transform.position;
					firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.up * 1.5f;
				}
				if (sequenceTimer > 60 && sequenceTimer < 540 && sequenceTimer % 60 == 30) {
					GameObject firstCreated = Instantiate (greenStraight);
					firstCreated.name = "GreenRightTwigs";
					firstCreated.transform.position = BigBranch.transform.position;
					firstCreated.GetComponent<Rigidbody2D> ().velocity = Vector2.down * 1.5f;
				}
			}
			PatternClock (750);
		}

		if (sequenceChoose == 9) {
			if (sequenceTimer < 200 && sequenceTimer % 20 == 0) {
				int loopNo = sequenceTimer / 20;
				GameObject firstCreated = Instantiate (greenStraight);
				firstCreated.name = "GreenSnakePart " + loopNo;
				//print ("Created " + loopNo + "!");
				firstCreated.transform.position = new Vector2 (10, -3f);
				firstCreated.GetComponent<Rigidbody2D> ().velocity = (Vector2.left + Vector2.up) * 1.2f;
			}
			if (sequenceTimer >= 200 && sequenceTimer < 200 + 200 && sequenceTimer % 20 == 0) {
				int loopNo = (sequenceTimer - 200) / 20;
				if (GameObject.Find ("GreenSnakePart " + loopNo)) {
					GameObject.Find ("GreenSnakePart " + loopNo).GetComponent<Rigidbody2D> ().velocity = (Vector2.left + Vector2.down) * 1.2f;
					//print ("Found " + loopNo + "!");
				}
			}
			if (sequenceTimer >= 400 && sequenceTimer < 400 + 200 && sequenceTimer % 20 == 0) {
				int loopNo = (sequenceTimer - 400) / 20;
				if (GameObject.Find ("GreenSnakePart " + loopNo)) {
					GameObject.Find ("GreenSnakePart " + loopNo).GetComponent<Rigidbody2D> ().velocity = (Vector2.left + Vector2.up) * 1.2f;
					//print ("Found " + loopNo + "!");
				}
			}
			if (sequenceTimer >= 600 && sequenceTimer < 600 + 200 && sequenceTimer % 20 == 0) {
				int loopNo = (sequenceTimer - 600) / 20;
				if (GameObject.Find ("GreenSnakePart " + loopNo)) {
					GameObject.Find ("GreenSnakePart " + loopNo).GetComponent<Rigidbody2D> ().velocity = (Vector2.left + Vector2.down) * 1.2f;
					//print ("Found " + loopNo + "!");
				}
			}
			PatternClock (800);
		}

		/*
		if (sequenceChoose == 9001) {
		if (sequenceTimer < 300 && sequenceTimer % 20 == 0) {
			GameObject firstCreated = Instantiate (greenStraight);
			firstCreated.name = "GreenRightCharge";
			firstCreated.transform.position = new Vector2 (-10, 0);
			Vector3	launchDir;
			if(targetPlayerNo == 0)
				launchDir = GameObject.Find("Red").transform.position - new Vector3 (-10, 0, 0);
			else
				launchDir = GameObject.Find("Blue").transform.position - new Vector3 (-10, 0, 0);
			Vector3 launchVelo;
			if(launchDir.x >= launchDir.y)
				launchVelo = new Vector3(launchDir.x/Mathf.Abs(launchDir.x), launchDir.y/Mathf.Abs(launchDir.x));
			else
				launchVelo = new Vector3(launchDir.x/Mathf.Abs(launchDir.y), launchDir.y/Mathf.Abs(launchDir.y));
			firstCreated.GetComponent<Rigidbody2D> ().velocity = launchVelo * (1 + (sequenceTimer/300f));
		}
		PatternClock (330);
		}
		*/

	}
		


	void PatternClock(int duration){
		if (sequenceTimer < duration) {
			if (Time.timeScale > 0)
				sequenceTimer++;
		} else {
			sequenceTimer = 0;
			sequenceChoose = 0;
			targetPlayerNo = Random.Range (0, 2);
		}
	}

}
