using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	public int ButtonState = 0;
	public int NOptions = 2;
	public bool AllowInteraction = false;

		//timing variables for non-toggling buttons
	public int t = 0;
	public int tMax = 20;

	public bool buttonLit = true;
	public bool blinking = false;
	public bool magnetDetectionEnabled = true;

	public GameObject childOn;
	public GameObject childOff;

	//scripts
	public PlayerController playerController;
	public radioScript radio;

		//audio variables
	public AudioSource audio; 
	public AudioClip sndBeep;

	public Material materialDIF;
	public Material materialILL;

	private Behaviour h;


	public delegate void PressAction (int pressedButtonState);
	public event PressAction OnPressed;

		// Use this for initialization
	void Start ()
	{
		h = (Behaviour)GetComponent ("Halo");
		h.enabled = false;
		
		childOn.SetActive (false);

		if (gameObject.tag != "Radio") {
			childOff.SetActive (true);
		}

#if UNITY_ANDROID
		CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
			// Disable screen dimming:
		Screen.sleepTimeout = SleepTimeout.NeverSleep;	
#endif
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

		/*if (AllowInteraction == false && gameObject.tag == "Radio") {
			childOff.SetActive (false);
		}

		 if (AllowInteraction == true && gameObject.tag == "Radio") {
			childOff.SetActive (true);
		} */

		if (AllowInteraction == false && gameObject.tag != "BTN") {
			childOff.renderer.material = materialDIF;
		}

		if (AllowInteraction == true && gameObject.tag != "BTN") {
			childOff.renderer.material = materialILL;
		}

		if(AllowInteraction){
			/*if (gameObject.tag != "BTN") {
				childOff.renderer.material = materialILL;
			}*/

			Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			RaycastHit hit;

			if (collider.Raycast (ray, out hit, 10.0f)) {
				h.enabled = true;
						//(gameObject.GetComponent("Halo")).enabled = true;
						//GetComponent (Halo).enabled = true;
						//(gameObject.GetComponent("Halo") as Behaviour).enabled = true; 
#if UNITY_ANDROID
				if (!magnetDetectionEnabled) return;
				if (CardboardMagnetSensor.CheckIfWasClicked()) {
					Press ();
					Handheld.Vibrate();
					if (gameObject.tag == "FireButton") {
						playerController.ShotFire();
					}
					if (gameObject.tag == "MoveLeft") {
						playerController.MoveLeft();
					}
					if (gameObject.tag == "MoveRight") {
						playerController.MoveRight();
					}
					if (gameObject.tag == "Radio") {
						radio.StationChange();
					}
					Debug.Log("MAGNET STATE CHANGE!");  
					CardboardMagnetSensor.ResetClick();
				}
#endif
				if (Input.GetButtonDown ("Fire1")) {
					Press ();
					#if UNITY_ANDROID
					Handheld.Vibrate();
					#endif
					if (gameObject.tag == "FireButton") {
						playerController.ShotFire();
					}
					if (gameObject.tag == "MoveLeft") {
						playerController.MoveLeft();
					}
					if (gameObject.tag == "MoveRight") {
						playerController.MoveRight();
					}
					if (gameObject.tag == "Radio") {
						radio.StationChange();
					}
				}
			
			
		/*	#if UNITY_ANDROID
				for (var i = 0; i < Input.touchCount; ++i) {
					if (Input.GetTouch (i).phase == TouchPhase.Began) {
						Press ();
						Handheld.Vibrate ();
					}
				} 
#endif */
			} else {
						//Behaviour h = (Behaviour)GetComponent("Halo");
				h.enabled = false;

				//childOff.renderer.material = materialDIF;
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
		if(AllowInteraction) {
			if(blinking) {
				blinking = false;
				Debug.Log ("Done Blinking");
			}

			ButtonState = (ButtonState + 1) % NOptions;
			//Debug.Log ("Ray hit! Button is now in state " + ButtonState);

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