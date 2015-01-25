using UnityEngine;
using System.Collections;

public class fleetWarpDelay : MonoBehaviour
{

		private int launchDelay = 385;
		private int launchRand = 50;
		private bool notLaunched = true;
		private float lifetime = 5f;

		// Use this for initialization
		void Start ()
		{
				notLaunched = true;
				//audio.Play();
				//Destroy (gameObject, lifetime);
		}
	
		// Update is called once per frame
		void Update ()
		{
				int frameTime = Time.frameCount / 6;
				//Debug.Log (frameTime);
				if (frameTime > launchDelay && notLaunched == true) {
						int launch = Random.Range (1, launchRand);
						if (launch == 1) {
								animation.Play ("warpNew");
								audio.Play ();
								notLaunched = false;
								Destroy (gameObject, lifetime);
								return;
								//Debug.Log (launch);
						}
				}
		}


}
