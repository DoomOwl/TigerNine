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
	
	public Text scoreTextL;
	public Text restartTextL;
	public Text gameOverTextL;
	public Text winTextL;

	public Text scoreTextR;
	public Text restartTextR;
	public Text gameOverTextR;
	public Text winTextR;
	
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
		restartTextL.text = "";
		restartTextR.text = "";
		gameOverTextL.text = "";
		gameOverTextR.text = "";
		score = 0;
		scoreTextL.enabled = false; 
		scoreTextR.enabled = false; 
		winTextL.enabled = false;
		winTextR.enabled = false;
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
				Application.Quit ();
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
			//	break;

		}
	}
	
	public void AddScore (int asteroidScore)
	{
		totalAsteroidScore += asteroidScore;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		score = 50000 - framesThisScene; 
		//asteroidScore = ?
		scoreTextL.text = "Score: " + score + " w/ asteroid bonus: " + totalAsteroidScore + "" ;
		scoreTextR.text = "Score: " + score + " w/ asteroid bonus: " + totalAsteroidScore + "" ;
	}

	public void GameWin()
	{
			scoreTextL.enabled = true;
			winTextL.enabled = true;
			gameOverTextL.enabled = false;
			scoreTextR.enabled = true;
			winTextR.enabled = true;
			gameOverTextR.enabled = false;
			UpdateScore ();
			StopCoroutine (SpawnWaves ());
	}

	public void LoadCredits()
	{
		if (winTextL.enabled == true) {
			Application.LoadLevel (2);
			Debug.Log ("Loading loading loading!");
		}
	}
		
	public void GameOver ()
	{
		Handheld.Vibrate();
		gameOverTextL.text = "Game Over!";
		gameOverTextR.text = "Game Over!";
		gameOver = true;
		scoreTextL.enabled = false;
		scoreTextR.enabled = false;
		winTextL.enabled = false;
		winTextR.enabled = false;
		StopCoroutine (SpawnWaves());
		
		UpdateScore();
		Debug.Log ("Game is Over");
	
	//don't show score unless you win
		scoreTextL.enabled = false;
		scoreTextR.enabled = false;
		restartTextL.text = "Click to Exit";
		restartTextR.text = "Click to Exit";
		restart = true;
		
	}
}