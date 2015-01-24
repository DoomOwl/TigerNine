using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
		public int ButtonState = 0;
		public int NOptions = 2;

		public delegate void PressAction (int);
		public event PressAction OnPressed;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetMouseButtonDown (0)) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;
						if (collider.Raycast (ray, out hit, 10.0f)) {
								Debug.DrawLine (ray.origin, hit.point);
								ButtonState = (ButtonState + 1) % NOptions;
								Debug.Log ("Ray hit! Button is now in state " + ButtonState);
								OnPressed ();
						}
				}
		}
}
