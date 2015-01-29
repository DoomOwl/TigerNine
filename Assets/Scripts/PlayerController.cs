using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform Ship;
	public Transform shotSpawn;

	public float fireRate = 0.15f;

	private float nextFire = 0.0f;

	/*
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin,boundary.xMax),
			0.0f,
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (GetComponent<Rigidbody>().velocity.z * -tilt/2, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	*/
	public void MoveLeft () {
		Ship.transform.Translate(Vector3.left * 0.3f, Space.Self);
		//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
		//Ship.GetComponent<Transform>.Translate (Vector3.left * Time.deltaTime, Space.World);
		}


	public void MoveRight () {
		Ship.transform.Translate(Vector3.right * 0.3f, Space.Self);
		//Ship.transform.localPosition = (Vector3.Lerp(transform.localPosition, Vector3.right * 0.5f, 1));
		//	Ship.transform.position = transform.position = Vector3.Lerp(3, transform.position, transform.position);
		//Vector3.Lerp(pointA, pointB, t)	
			//transform.Translate(Vector3(0,0,1));
		}

	public void ShotFire () {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			//GetComponent<AudioSource>().Play ();
		}
		/*
		if (Input.GetKeyDown (KeyCode.RightShift) || Input.GetKeyUp (KeyCode.LeftShift)) {
			tilt = tilt *4;
			speed = speed *1.5f;
		}
		if (Input.GetKeyUp (KeyCode.RightShift) || Input.GetKeyUp (KeyCode.LeftShift)) {
			tilt = tilt/4;
			speed = speed *0.6666667f;
		}
		*/
	}
}
