using UnityEngine;
using System.Collections;

public class HintDisplay : MonoBehaviour
{
	public MastermindController Controller;

	public SpriteRenderer[] Icons;

	//public int currentSceneFrames;
	//public int frameCountAtStart;
	public float StateTimer = 0f;

	private TextMesh textComponent;
		// Use this for initialization
	void Start ()
	{
		textComponent = GetComponent<TextMesh> ();
		Controller.OnValidate += UpdateHint;
		Controller.OnStateValid += UpdateSystemIcons;
		textComponent.text = "AUTOPILOT";
		textComponent.fontSize = 100;
		for(int i=0;i<Icons.Length;i++){
			Icons[i].color = Color.green;
		StateTimer = 0;
		//frameCountAtStart = Time.frameCount;
		//currentSceneFrames = -frameCountAtStart;
		}
	}
	
		// Update is called once per frame
	void Update ()
	{
		//currentSceneFrames = Time.frameCount -frameCountAtStart;
		StateTimer += Time.deltaTime;
		//Debug.Log (StateTimer);

		if (StateTimer > 30 && StateTimer < 42) {
			textComponent.text = "LAUNCH READY";
			textComponent.fontSize = 70;
		}else if(StateTimer > 42 && StateTimer < 45) {
			textComponent.text = "3";
			textComponent.fontSize = 150;
		}else if(StateTimer > 45 && StateTimer < 48) {
			textComponent.text = "2";
		}else if(StateTimer > 48 && StateTimer < 51) {
			textComponent.text = "1";
		}else if(StateTimer > 51 && StateTimer < 55) {
			textComponent.text = "";
		}

		switch(Controller.CurrentState.SequenceName) {
			case CinematicState.Sequence.BreakingDown:
				for(int i=0;i<Icons.Length;i++){
					Icons[i].enabled = Random.Range (0,2) == 0;
					Icons[i].color = Random.Range (0,2) == 0 ? Color.green : Color.red;
				}
				break;
			case CinematicState.Sequence.Silence:
				for(int i=0;i<Icons.Length;i++){
					Icons[i].enabled = false;
					Icons[i].color = Color.red;
				}
				break;
		}
	}

	void UpdateHint (int nLow, int nHigh)
	{
		textComponent.text = "-" + nHigh + " +" + nLow;
	}

	void UpdateSystemIcons (int nSolved)
	{
		if(nSolved == 0) {
			foreach(var icon in Icons) {
				icon.enabled = true;
			}
		} else if(nSolved <= Icons.Length) {
			Icons[nSolved-1].color = Color.green;
		}
	}
}
