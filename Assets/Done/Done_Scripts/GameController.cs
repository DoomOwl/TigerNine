using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	
	private bool gameOver;
	private bool restart;
	private int score;
	private int asteroidScore;
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		scoreText.enabled = true;
		asteroidScore = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
		//UpdateScore ();
		//Debug.Log (score + " " + asteroidScore);

		if (restart)
		{
			if (Input.anyKey)
			{
				Application.LoadLevel (Application.loadedLevel);
				score = 0;
				asteroidScore = 0;
				//something that resets all the buttons
			}
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				//Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Vector3 spawnPosition =  transform.TransformPoint(Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), spawnValues.z);
				//transform.localPosition = Vector3(0, 0, 0);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				//Debug.Log (spawnPosition);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				UpdateScore();
				scoreText.enabled = true;
				restartText.text = "Click for Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int asteroidScore)
	{
		score += asteroidScore;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		score = 15000 - Time.frameCount;
		scoreText.text = "Score: " + score + " + " + asteroidScore + " asteroid bonus" ;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}