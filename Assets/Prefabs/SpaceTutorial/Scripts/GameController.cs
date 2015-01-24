using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount = 6;
	public float spawnWait = 1f;
	public float startWait = 1f;
	public float waveWait = 4f;
	private float minRange = 0.2f;
	private float maxRange = 1.2f;
	
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;



	void Start() {
		/*gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore (); */
		StartCoroutine (SpawnWaves ());
	}

	void Update () {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {

				for (int i = 0; i <hazardCount; i++) {
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					hazard.GetComponent<Transform>().localScale = new  Vector3 (Random.Range (minRange, maxRange), Random.Range (minRange, maxRange), Random.Range (minRange, maxRange)); 	
					var instance = Instantiate (hazard, Vector3.zero, spawnRotation);
					//instance.transform.position += spawnPosition;
					yield return new WaitForSeconds (spawnWait);
				Debug.Log (hazardCount + " " + minRange + " " + maxRange);
				}
			yield return new WaitForSeconds (waveWait);
			hazardCount += hazardCount;
			maxRange += 0.5f;

		/*	if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
		} */
	}
}
	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}
	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}
	public void GameOver () {
		gameOverText.text = "GAME OVER!";
		gameOver = true;

	}

}
