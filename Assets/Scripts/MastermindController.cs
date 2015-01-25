using UnityEngine;
using System.Collections;

public class MastermindController : MonoBehaviour
{
		public delegate void StateValidAction (int nSolved);
		public event StateValidAction OnStateValid;

		public delegate void ValidateAction (int nLow,int nHigh);
		public event ValidateAction OnValidate;

		public Button VerifyButton;
		public Button[] OtherButtons;
		public bool IsStateValid = false;
		public int[] ValidState;
		public int NLow = 0;
		public int NHigh = 0;
		public int NInactive = 0; //number of inactive buttons in the valid state
		
		//audio files
		public AudioSource audio;
		public AudioClip sndWrong;
		public AudioClip sndRight;
		
		//ambient audio files
		public AudioClip[] sndConfVoices;
		public AudioClip[] sndConfBG;
		public AudioSource[] sndConfLoops;

		// Use this for initialization
		void Start ()
		{
				VerifyButton.OnPressed += VerifyState;
				RandomizeValidState ();
				
				sndWrong = ImportSound("sounds/error_beep");
				sndRight = ImportSound("sounds/confirmation");
				
				//computerized voice at end of each round
				sndConfVoices = new AudioClip[8];
				sndConfVoices[0] = ImportSound ("ambient/oneshot_1_auxiliary_power_online_comptuer_voice");
				sndConfVoices[1] = ImportSound ("ambient/oneshot_2_computer_initialized_computer_voice");
				sndConfVoices[2] = ImportSound ("ambient/oneshot_3_comm_link_online_computer_voice");
				sndConfVoices[3] = ImportSound ("ambient/oneshot_4_main_reactor_online_computer_voice");
				sndConfVoices[4] = ImportSound ("ambient/oneshot_5_weapons_online_computer_voice");
				sndConfVoices[5] = ImportSound ("ambient/oneshot_6_navigation_system_configured_computer_voice");
				sndConfVoices[6] = ImportSound ("ambient/oneshot_7_engines_online_computer_voice");
				sndConfVoices[7] = ImportSound ("ambient/oneshot_8_warp_drive_charged_computer_voice");
				
				//secondary (optional) audio at the end of each round
				sndConfBG = new AudioClip[8];
				sndConfBG[0] = null;
				sndConfBG[1] = ImportSound ("ambient/oneshot_2_computer_initalized");
				sndConfBG[2] = ImportSound ("ambient/oneshot_3_comm_link_enabled");
				sndConfBG[3] = null;
				sndConfBG[4] = ImportSound ("ambient/oneshot_5_weapons_primed");
				sndConfBG[5] = null;
				sndConfBG[6] = null;
				sndConfBG[7] = ImportSound ("ambient/oneshot_8_engage_hyperspace");
				
				AudioSource[] aSources = GetComponents<AudioSource>();
				sndConfLoops = new AudioSource[8];
				sndConfLoops[0] = aSources[0];
				sndConfLoops[1] = null;
				sndConfLoops[2] = null;
				sndConfLoops[3] = aSources[1];
				sndConfLoops[4] = null;
				sndConfLoops[5] = null;
				sndConfLoops[6] = aSources[2];
				sndConfLoops[7] = null;
			
		}
		
		AudioClip ImportSound(string name){
			return Resources.Load (name) as AudioClip;
		}
	
		// Update is called once per frame
		void Update ()
		{
			//keyboard controls for debugging
			if(Input.GetKeyUp ("n")){
				StateValid ();
			}
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
				OnValidate (NLow, NHigh);
				IsStateValid = NLow == 0 && NHigh == 0;
				if (IsStateValid) {
					audio.PlayOneShot (sndRight,.5F);
					StateValid ();
					if (OnStateValid != null)
								OnStateValid (NInactive);
				} else {
					audio.PlayOneShot (sndWrong);
				}
		}

		void StateValid ()
		{
				Debug.Log ("State is valid!");
				audio.PlayOneShot(sndConfVoices[NInactive]);
				if(sndConfBG[NInactive] != null){
					audio.PlayOneShot(sndConfBG[NInactive]);
				}
				if(sndConfLoops[NInactive] != null){
					sndConfLoops[NInactive].Play ();
				}
				
				//increase number of inactive buttons and re-randomize new state
				if (NInactive < OtherButtons.Length)
						NInactive++;
				else {
					Debug.Log ("You Win!");
					audio.PlayOneShot(sndConfVoices[7]);
				}

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
				ResetButtons ();
		}
		
		void ResetButtons()
		{
			for(int i=0;i<OtherButtons.Length;i++){
				OtherButtons[i].ButtonState = 0;
				OtherButtons[i].buttonLit = false;
				OtherButtons[i].setLights ();
			}
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
