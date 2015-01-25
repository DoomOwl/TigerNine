using UnityEngine;
using System.Collections;

public class fleetWarpDelay : MonoBehaviour
{

		private float launchDelay = 41f;
		private float launchRand = 2f;
		private bool notLaunched = true;
		private float lifetime = 5f;

		public float time = 0;
		
		private int frameOffset = 0;

		// Use this for initialization
		void Start ()
		{
				notLaunched = true;
				launchDelay += Random.Range(0, launchRand);
				Debug.Log(launchDelay);
				//audio.Play();
				//Destroy (gameObject, lifetime);
				frameOffset = Random.Range (0,600);
		}
	
		// Update is called once per frame
		void Update ()
		{
				time += Time.deltaTime;
				if (time > launchDelay && notLaunched) {
					animation.Play ("warpNew");
					audio.Play ();
					notLaunched = false;
					Destroy (gameObject, lifetime);
				} else {
					//subtle flying animations
					int theta = (Time.frameCount / 6 + frameOffset)/5;
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
