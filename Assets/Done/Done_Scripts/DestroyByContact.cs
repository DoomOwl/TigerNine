using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int asteroidScore;
	private GameController gameController;
	private MastermindController mastermindController;

	void Start () 
	{
		GameObject mastermindControllerObject = GameObject.FindGameObjectWithTag ("Mastermind");
		mastermindController = mastermindControllerObject.GetComponent <MastermindController> ();

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	/* void Update ()
	{
			if (mastermindController.GameEnd()) {
				Destroy (gameObject);
			}
		}
	*/

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary") // || other.tag == "Enemy")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
			mastermindController.GameEnd();
	
		}

		gameController.AddScore(asteroidScore);
		Debug.Log ("Asteroid Destroyed!");
		Destroy (other.gameObject);
		Destroy (gameObject);

	}
}