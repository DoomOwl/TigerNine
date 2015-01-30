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

	private float minRange = 0.4f;
	private float maxRange = 1.2f;
	
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text winText;
	
	private bool gameOver;
	private bool restart;
	private int score;
	private int totalAsteroidScore;

	private int framesThisScene;
	private int framesAtStart;
	
	void Start ()
	{
		framesAtStart = Time.frameCount;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		scoreText.enabled = false; 
		winText.enabled = false;
		totalAsteroidScore = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
		framesThisScene = Time.frameCount - framesAtStart;
		//UpdateScore ();
		//Debug.Log (score + " " + asteroidScore);

		if (restart)
		{
			if (Input.anyKey)
			{
				Application.LoadLevel (Application.loadedLevel);
				//score = 0;
				//UpdateScore();
				totalAsteroidScore = 0;
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
				hazard.GetComponent<Transform>().localScale = new  Vector3 (Random.Range (minRange, maxRange), Random.Range (minRange, maxRange), Random.Range (minRange, maxRange)); 	
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
				Debug.Log ("Game is Over");

				//don't show score unless you win
				scoreText.enabled = false;
				restartText.text = "Click to Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int asteroidScore)
	{
		totalAsteroidScore += asteroidScore;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		score = 20000 - framesThisScene; 
		//asteroidScore = ?
		scoreText.text = "Score: " + score + " and an asteroid bonus of " + totalAsteroidScore + "" ;
	}

	public void GameWin()
	{
			scoreText.enabled = true;
			winText.enabled = true;
			gameOverText.enabled = false;
			UpdateScore ();
			StopCoroutine (SpawnWaves ());
	}

	public void LoadCredits()
	{
		if (winText.enabled == true) {
			Application.LoadLevel (2);
			Debug.Log ("Loading loading loading!");
		}
	}
		
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		scoreText.enabled = false;
		winText.enabled = false;
		StopCoroutine (SpawnWaves());}
}