﻿using UnityEngine;
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
	public AudioClip sndINTRO;
	public AudioClip sndWrong;
	public AudioClip sndRight;
	
	//ambient audio files
	public AudioClip[] sndConfVoices; // Computer voices played at each stage completion
	public AudioClip[] sndConfBG; // Background sounds played at each stage completion
	public AudioSource[] sndConfLoops; // Loops played after each stage completion
	
	public AudioSource Radio;
	public AudioClip sndRadioChatter;
	
	public SceneFadeInOut Fader;
	
	//cinematic timers
	public CinematicState CurrentState;
	public float StateTimer;
	public CinematicState[] States;
	private int _stateIndex = 0;
	
	//screen shake variables
	private Vector2 Origin;

	public Light[] roomLights;
	
	//particles
	public GameObject SmokeParticles;
	public GameObject Engine;

	//Game Objects
	public GameObject Missiles;
	
	public HintDisplay UI;

	// Use this for initialization
	void Start ()
	{
		Debug.Log(States.Length);
		foreach(var s in States) {
			Debug.Log(s.State);
		}
		CurrentState = States[_stateIndex];
		// Setting origin for screen shake
		Origin = new Vector2(transform.position.x,transform.position.y);

		VerifyButton.OnPressed += VerifyState;
		RandomizeValidState ();

		sndWrong = ImportSound("sounds/error_beep");
		sndRight = ImportSound("sounds/confirmation");
		sndINTRO = ImportSound ("sounds/oneshot_intro_FULLCUTSCENE");

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

			//Object States
		SmokeParticles.SetActive(false);
		Engine.SetActive(true);

		Missiles.SetActive(false);
	}
	
	AudioClip ImportSound(string name) {
		return Resources.Load (name) as AudioClip;
	}

	void AdvanceState() {
		if(_stateIndex < States.Length) {
			CurrentState = States[++_stateIndex];
			StateTimer = 0;
			if(CurrentState.State == State.Gameplay) {
				VerifyButton.AllowInteraction = true;
				foreach(Button b in OtherButtons)
					b.AllowInteraction = true;
			}
		} else {
			Debug.LogWarning("Attempted to advance state, but no further states to advance to!");
		}
	}

	// Update is called once per frame
	void Update ()
	{
		//keyboard controls for debugging
		if(Input.GetKeyUp ("n")) {
			StateValid ();
		}

		StateTimer += Time.deltaTime;
		
		switch(CurrentState.State) {
			case State.Introduction:
				for(int i=0;i<OtherButtons.Length;i++) {
					OtherButtons[i].buttonLit = true; // EVAL: Does this need to be done every frame?
					OtherButtons[i].setLights ();
				}
				break;
			case State.BreakingDown:
				//screen shake
				Vector3 newPos = new Vector3(
					Origin.x+Random.Range (-0.01F,0.01F),
					Origin.y+Random.Range (-0.01F,0.01F),
					transform.position.z
					);
				transform.position = newPos;

				for(int i=0;i<roomLights.Length;i++){
					roomLights[i].enabled = Random.Range (0,10) <= 2;
				}
				for(int i=0;i<OtherButtons.Length;i++){
					OtherButtons[i].buttonLit = Random.Range (0,10) <= 2;
					OtherButtons[i].setLights ();
				}
				SmokeParticles.SetActive(true);
				Engine.SetActive(false);
				
				if(StateTimer >= CurrentState.Duration) {
					SmokeParticles.SetActive(false);
					
					for(int i=0;i<roomLights.Length;i++){
						roomLights[i].enabled = false;
					}
					VerifyButton.buttonLit = false;
					VerifyButton.setLights ();
					for(int i=0;i<OtherButtons.Length;i++){
						OtherButtons[i].buttonLit = false;
						OtherButtons[i].setLights ();
					}
					
					transform.position = new Vector3(Origin.x,Origin.y,transform.position.z);
				}
				break;
			case State.Silence:
				if(StateTimer >= CurrentState.Duration) {
					VerifyButton.blinking = true;
				}
				break;
		}

		if(StateTimer >= CurrentState.Duration && CurrentState.Duration > 0) {
			AdvanceState();
		}
	}

	void VerifyState (int pressedButtonState)
	{
		if(CurrentState.State != State.Gameplay) return;

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
		if(OnValidate != null) OnValidate (NLow, NHigh);
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

		if(NInactive == 0){
				//turn on the lights
			for(int i=0;i<roomLights.Length;i++){
				roomLights[i].enabled = true;
			}
			for(int i=0;i<UI.Icons.Length;i++){
				UI.Icons[i].enabled = true;
			}
		} else {
			if(NInactive <= UI.Icons.Length) UI.Icons[NInactive-1].color = Color.green;
		}

		if(NInactive == 1){
			Radio.clip = sndRadioChatter;
			Radio.Play();
		}

		if(NInactive == 2){
			Engine.SetActive(true);
		}

		if (NInactive == 3) {
			Missiles.SetActive (true);
			Missiles.animation.Play("missilePrep");
		}

			//I'd rather have the missiles launch when you press the middle button
		if (NInactive == 4) {
			Missiles.animation.Play("missilesLaunch");
		}

			//increase number of inactive buttons and re-randomize new state
		if (NInactive < OtherButtons.Length)
		NInactive++;
		else {
			Debug.Log ("You Win!");
			Fader.transform.position = transform.position;
			Fader.sceneEnding = true;
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
