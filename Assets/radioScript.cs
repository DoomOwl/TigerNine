using UnityEngine;
using System.Collections;

public class radioScript : MonoBehaviour {

	private string radioStation = "off";
	//public AudioSource Radio;

	public AudioSource Radio1;
	public AudioClip sndRadioChatter;

	public AudioSource Radio2;
	public AudioClip sndIPR;

	public AudioSource Radio3;
	public AudioClip MatMusic;

	public AudioSource Radio4;
	public AudioClip AlexMusic;

	// Use this for initialization
	void Start () {
		radioStation = "gameRadio";
	}
	
	// Update is called once per frame
	public void StationChange () {
			
			if(radioStation == "off"){
				radioStation = "gameRadio";
			}
			else if(radioStation == "gameRadio"){
				radioStation = "spaceNPR";
			}
			else if(radioStation == "spaceNPR"){
				radioStation = "AlexMusic";
			}
			else if(radioStation == "AlexMusic"){
				radioStation = "MatMusic";
			}
			else if(radioStation == "MatMusic"){
				radioStation = "off";
			}
			
			if(radioStation == "off"){
				Radio1.audio.volume = 0;
				Radio2.audio.volume = 0;
				Radio3.audio.volume = 0;
				Radio4.audio.volume = 0;
				Debug.Log ("off");
		}
			if(radioStation == "gameRadio"){
				Radio1.audio.volume = 1;
				Radio2.audio.volume = 0;
				Radio3.audio.volume = 0;
				Radio4.audio.volume = 0;
				Debug.Log ("Station01");
			}
			else if(radioStation == "spaceNPR"){
				Radio1.audio.volume = 0;
				Radio2.audio.volume = 1;
				Radio3.audio.volume = 0;
				Radio4.audio.volume = 0;
				Debug.Log ("Station02");
			}
			else if(radioStation == "AlexMusic"){
				Radio1.audio.volume = 0;
				Radio2.audio.volume = 0;
				Radio3.audio.volume = 1;
				Radio4.audio.volume = 0;
				Debug.Log ("Station03");
		}
			else if(radioStation == "MatMusic"){
				Radio1.audio.volume = 0;
				Radio2.audio.volume = 0;
				Radio3.audio.volume = 0;
				Radio4.audio.volume = 1;
				Debug.Log ("Station04");
			}
		}
}

