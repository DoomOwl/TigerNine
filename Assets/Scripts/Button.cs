using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
		public int ButtonState = 0;
		public int NOptions = 2;
		
		public GameObject childOn;
		public GameObject childOff;

		public delegate void PressAction (int pressedButtonState);
		public event PressAction OnPressed;

		// Use this for initialization
		void Start ()
		{
				childOn.SetActive (false);
				childOff.SetActive (true);
		}
	
		// Update is called once per frame
		void Update ()
		{
				Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
				RaycastHit hit;
				Behaviour h = (Behaviour)GetComponent ("Halo");

				if (collider.Raycast (ray, out hit, 10.0f)) {
						h.enabled = true;
						//(gameObject.GetComponent("Halo")).enabled = true;
						//GetComponent (Halo).enabled = true;
						//(gameObject.GetComponent("Halo") as Behaviour).enabled = true; 
						if (Input.GetButtonDown ("Fire1"))
								Press ();
				
						for (var i = 0; i < Input.touchCount; ++i) {
								if (Input.GetTouch (i).phase == TouchPhase.Began) {
										Press ();
										Handheld.Vibrate ();
								} 
						} 
				} else {
						//Behaviour h = (Behaviour)GetComponent("Halo");
						h.enabled = false;
						//print("I'm looking at nothing!");
						//deactivate all halos
				}
		}
	
		public void Press ()
		{
		
		
				ButtonState = (ButtonState + 1) % NOptions;
				Debug.Log ("Ray hit! Button is now in state " + ButtonState);
				if (OnPressed != null)
						OnPressed (ButtonState);
		
				childOn.SetActive (ButtonState != 0);
				childOff.SetActive (ButtonState == 0);
		
		}
}