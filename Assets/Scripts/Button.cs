using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
		public int ButtonState = 0;
		public int NOptions = 2;
		
		//timing variables for non-toggling buttons
		public int t = 0;
		public int tMax = 20;
		
		public bool buttonLit = true;
		public bool blinking = false;
		public bool magnetDetectionEnabled = true;

		public GameObject childOn;
		public GameObject childOff;
		
		//audio variables
		public AudioSource audio; 
		public AudioClip sndBeep;
		
		private Behaviour h;
		

		public delegate void PressAction (int pressedButtonState);
		public event PressAction OnPressed;

		// Use this for initialization
		void Start ()
		{
				h = (Behaviour)GetComponent ("Halo");
				h.enabled = false;
		
				childOn.SetActive (true);
				childOff.SetActive (false);

				CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
				// Disable screen dimming:
				Screen.sleepTimeout = SleepTimeout.NeverSleep;	

		}
	
		// Update is called once per frame
		void Update ()
		{
			if(blinking){
				t ++;
				if(t >= tMax * 2){
					t = 0;
					buttonLit = !buttonLit;
					setLights ();
				}
			}
			if(MastermindController.GameStarted){
				Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
				RaycastHit hit;
				
				if (collider.Raycast (ray, out hit, 10.0f)) {
						h.enabled = true;
						//(gameObject.GetComponent("Halo")).enabled = true;
						//GetComponent (Halo).enabled = true;
						//(gameObject.GetComponent("Halo") as Behaviour).enabled = true; 
						if (!magnetDetectionEnabled) return;
						if (CardboardMagnetSensor.CheckIfWasClicked()) {
							Press ();
							Handheld.Vibrate();
							Debug.Log("MAGNET STATE CHANGE!");  
							CardboardMagnetSensor.ResetClick();
					}



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
			
				//timing variables
				if (t > 0 && !blinking) {
						t --;
						if (t <= 0) {
								buttonLit = false;
								setLights ();
						}
				}
			}
		}
	
		public void Press ()
		{
			if(MastermindController.GameStarted){
				if(blinking) {
					blinking = false;
					Debug.Log ("Done Blinking");
				}
				ButtonState = (ButtonState + 1) % NOptions;
				Debug.Log ("Ray hit! Button is now in state " + ButtonState);
		
				if (OnPressed != null)
						OnPressed (ButtonState);
			
				audio.PlayOneShot (sndBeep);
				if (NOptions == 2) {
						
						buttonLit = ButtonState != 0;
						
				} else if (NOptions == 1) {
						buttonLit = true;
						
						t = tMax;
				}
		
				setLights ();
			}
		}
	
		public void setLights ()
		{
				childOn.SetActive (buttonLit);
				childOff.SetActive (!buttonLit);
		}
}