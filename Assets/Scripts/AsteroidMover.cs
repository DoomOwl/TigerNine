using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour {

	public float speed;
	public float minRange;
	public float maxRange;

	void Start () {
		speed = Random.Range (minRange, maxRange);
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		GetComponent<Rigidbody> ().velocity = transform.right * speed / 2;
		GetComponent<Rigidbody> ().velocity = transform.up * speed / 2;
	}
}