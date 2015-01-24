using UnityEngine;
using System.Collections;

public class GlowActivate : MonoBehaviour {
		
	//public Behaviour h = (Behaviour)GetComponent ("Halo");
	//GameObject switch_0 
	public GameObject childOn;
	public GameObject childOff;
		
		void Awake() {
			childOn.SetActive (false);
			childOff.SetActive (true);
		}
		
		void Update() {
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
			RaycastHit hit;
			Behaviour h = (Behaviour)GetComponent ("Halo");
			

				
			if (collider.Raycast (ray, out hit, 10.0f)) { //&& hit.transform.tag == "button") {
						print ("I'm looking at " + hit.transform.name);
						h.enabled = true;
						//(gameObject.GetComponent("Halo")).enabled = true;
						//GetComponent (Halo).enabled = true;
						//(gameObject.GetComponent("Halo") as Behaviour).enabled = true; 
					if (Input.GetButtonDown("Fire1")) {
						childOn.SetActive(!childOn.activeSelf);
						childOff.SetActive(!childOff.activeSelf);
						}	

						for (var i = 0; i < Input.touchCount; ++i) {
								if (Input.GetTouch (i).phase == TouchPhase.Began) {
									//childOn.SetActive(!childOn.activeSelf);
									//childOff.SetActive(!childOff.activeSelf);
									Handheld.Vibrate ();
								} 
						} 
				}

					else {
						//Behaviour h = (Behaviour)GetComponent("Halo");
						h.enabled = false;
						//print("I'm looking at nothing!");
						//deactivate all halos
				}
		}
	}