using UnityEngine;
using System.Collections;

public class MastermindController : MonoBehaviour
{
		public Button VerifyButton;
		public Button[] OtherButtons;

		public bool IsStateValid = false;
		public int[] ValidState;
		public int NLow = 0;
		public int NHigh = 0;

		// Use this for initialization
		void Start ()
		{
				VerifyButton.OnPressed += VerifyState;

				ValidState = new int[OtherButtons.Length];
				for (int i = 0; i < OtherButtons.Length; ++i)
						ValidState [i] = 1;
		}
	
		// Update is called once per frame
		void Update ()
		{
		}

		void VerifyState ()
		{
				Debug.Log ("Verifying state");
				NLow = NHigh = 0;
				for (int i = 0; i < OtherButtons.Length; ++i) {
						var curButton = OtherButtons [i];
						if (curButton.ButtonState < ValidState [i])
								++NLow;
						else if (curButton.ButtonState > ValidState [i])
								++NHigh;
				}
				Debug.Log ("NLow: " + NLow + " | NHigh: " + NHigh);
				IsStateValid = NLow == 0 && NHigh == 0;
				if (IsStateValid)
						Debug.Log ("State is valid!");
		}
}
