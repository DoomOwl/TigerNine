using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
		public int ButtonState = 0;
		public int NOptions = 2;
		
		//timing variables for non-toggling buttons
		public int t = 0;
		public int tMax = 20;
		
		public bool buttonLit = false;
		
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
		void Update() {
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
			RaycastHit hit;
			Behaviour h = (Behaviour)GetComponent ("Halo");
			
			
			
			if (collider.Raycast (ray, out hit, 10.0f)) { //&& hit.transform.tag == "button") {
				//print ("I'm looking at " + hit.transform.name);
				h.enabled = true;
				//(gameObject.GetComponent("Halo")).enabled = true;
				//GetComponent (Halo).enabled = true;
				//(gameObject.GetComponent("Halo") as Behaviour).enabled = true; 
				if (Input.GetButtonDown("Fire1")) {
					Press ();
					
				}	
				
				for (var i = 0; i < Input.touchCount; ++i) {
					if (Input.GetTouch (i).phase == TouchPhase.Began) {
						Press ();
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
			
			//timing variables
			if(t > 0){
				t --;
				Debug.Log (t);
				if(t <= 0){
					buttonLit = false;
					setLights ();
				}
			}
		}
	
	public void Press()
	{
		
		
		ButtonState = (ButtonState + 1) % NOptions;
		Debug.Log ("Ray hit! Button is now in state " + ButtonState);
		
		if(OnPressed != null) OnPressed (ButtonState);
		
		if(NOptions == 2){
			buttonLit = ButtonState != 0;
		} else if (NOptions == 1){
			buttonLit = true;
			t = tMax;
		}
		
		setLights ();
	}
	
	public void setLights(){
		childOn.SetActive(buttonLit);
		childOff.SetActive(!buttonLit);
	}
}