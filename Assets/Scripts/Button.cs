using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
		public int ButtonState = 0;
		public int NOptions = 2;

		public delegate void PressAction (int pressedButtonState);
		public event PressAction OnPressed;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				/*if (Input.GetMouseButtonDown (0)) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;
						if (collider.Raycast (ray, out hit, 100.0f)) {
								Press ();
						}
				}*/
		}
		
		public void Press()
		{
			ButtonState = (ButtonState + 1) % NOptions;
			Debug.Log ("Ray hit! Button is now in state " + ButtonState);
			if(OnPressed != null) OnPressed (ButtonState);
		}
}