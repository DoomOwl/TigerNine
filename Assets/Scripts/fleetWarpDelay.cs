using UnityEngine;
using System.Collections;

public class fleetWarpDelay : MonoBehaviour
{

		private int launchDelay = 385;
		private int launchRand = 50;
		private bool notLaunched = true;
		private float lifetime = 5f;
		
		private int frameOffset = 0;

		// Use this for initialization
		void Start ()
		{
				notLaunched = true;
				//audio.Play();
				//Destroy (gameObject, lifetime);
				frameOffset = Random.Range (0,600);
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
				} else {
					//subtle flying animations
					int theta = (frameTime+frameOffset)/5;
					Vector3 newPos = new Vector3(transform.position.x+Mathf.Sin(theta)/600,
			                             transform.position.y+Mathf.Sin(theta*2)/600,
			                             transform.position.z);
			    	Quaternion newRot = new Quaternion(
			    		transform.rotation.x,
			    		transform.rotation.y,
						transform.rotation.z+Mathf.Sin(theta)/600,
						transform.rotation.w);
			
					transform.position = newPos;
					transform.rotation = newRot;
				}
		}


}
