using UnityEngine;
using System.Collections;

public class MastermindController : MonoBehaviour
{
		public delegate void StateValidAction ();
		public event StateValidAction OnStateValid;

		public Button VerifyButton;
		public Button[] OtherButtons;
		public bool IsStateValid = false;
		public int[] ValidState;
		public int NLow = 0;
		public int NHigh = 0;
		public int NInactive = 0; //number of inactive buttons in the valid state

		// Use this for initialization
		void Start ()
		{
				VerifyButton.OnPressed += VerifyState;
				this.OnStateValid += StateValid;
				RandomizeValidState ();
		}
	
		// Update is called once per frame
		void Update ()
		{
		}

		void VerifyState (int pressedButtonState)
		{
				VerifyButton.ButtonState = 0;
				Debug.Log ("Verifying state:");
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
						OnStateValid ();
		}

		void StateValid ()
		{
				Debug.Log ("State is valid!");
				//increase number of inactive buttons and re-randomize new state
				if (NInactive < OtherButtons.Length)
						NInactive ++;
				else
						Debug.Log ("You Win!");

				RandomizeValidState ();
		}

		void RandomizeValidState ()
		{
				ValidState = new int[OtherButtons.Length];
			
				//populating new valid state
				for (int i = 0; i < OtherButtons.Length; ++i) {
						if (i < NInactive) {
								ValidState [i] = 0;
						} else {
								ValidState [i] = 1;
						}
				}

				//shuffling validstate array
				ShuffleArray (ValidState);
		}

		void ShuffleArray<T> (T[] arr)
		{
				for (int i = arr.Length - 1; i > 0; i--) {
						int r = Random.Range (0, i);
						T tmp = arr [i];
						arr [i] = arr [r];
						arr [r] = tmp;
				}
		}
}
