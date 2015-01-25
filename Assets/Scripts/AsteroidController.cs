using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount = 6;
	public float spawnWait = 1f;
	public float startWait = 1f;
	public float waveWait = 4f;
	private float minRange = 0.2f;
	private float maxRange = 1.2f;
	
	private bool gameOver;
	private bool restart;
	private int score;



	void Start() {
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {

				for (int i = 0; i <hazardCount; i++) {
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					hazard.GetComponent<Transform>().localScale = new  Vector3 (Random.Range (minRange, maxRange), Random.Range (minRange, maxRange), Random.Range (minRange, maxRange)); 	
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				Debug.Log (hazardCount + " " + minRange + " " + maxRange);
				}
			yield return new WaitForSeconds (waveWait);
			hazardCount += hazardCount;
			maxRange += 0.5f;
		}
	}
}