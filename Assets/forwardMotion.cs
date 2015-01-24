using UnityEngine;
using System.Collections;

public class forwardMotion : MonoBehaviour {

	public int speed = 1;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
		//transform.localPosition += transform.forward * speed * Time.deltaTime;
	}
}
