using UnityEngine;
using System.Collections;

public class fleetWarpDelay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int frameTime = Time.frameCount / 6;
		//Debug.Log (frameTime);
		if (frameTime > 10) {
			int launch = Random.Range(1,15);
			if (launch == 1) {
				animation.Play("warpNew");
				//Debug.Log (launch);
			}
		}
	}
}
