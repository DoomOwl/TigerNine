using UnityEngine;
using System.Collections;

public class HintDisplay : MonoBehaviour
{
		public MastermindController Controller;

		private TextMesh textComponent;
		// Use this for initialization
		void Start ()
		{
				textComponent = GetComponent<TextMesh> ();
				Controller.OnValidate += UpdateDisplay;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void UpdateDisplay (int nLow, int nHigh)
		{
				textComponent.text = "-" + Controller.NHigh + " +" + Controller.NLow;
				Debug.Log (textComponent.text);
		}
}
