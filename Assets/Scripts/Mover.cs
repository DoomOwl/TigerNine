using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	public float minRange;
	public float maxRange;

	void Start () {
		speed = Random.Range (minRange, maxRange);
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}