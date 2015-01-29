using UnityEngine;
using System.Collections;

public class HintDisplay : MonoBehaviour
{
	public MastermindController Controller;

	public SpriteRenderer[] Icons;

	public int currentSceneFrames;

	public int frameCountAtStart;

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

		frameCountAtStart = Time.frameCount;
		currentSceneFrames = -frameCountAtStart;
		}
	}
	
		// Update is called once per frame
	void Update ()
	{
		currentSceneFrames = Time.frameCount -frameCountAtStart;
		//Debug.Log (currentSceneFrames);

		if (currentSceneFrames > 1200 && currentSceneFrames < 1400) {
			textComponent.text = "LAUNCH READY";
			textComponent.fontSize = 70;
		}else if(currentSceneFrames > 1400 && currentSceneFrames < 1500) {
			textComponent.text = "3";
			textComponent.fontSize = 150;
		}else if(currentSceneFrames > 1500 && currentSceneFrames < 1600) {
			textComponent.text = "2";
		}else if(currentSceneFrames > 1600 && currentSceneFrames < 1800) {
			textComponent.text = "1";
		}else if(currentSceneFrames > 1800 && currentSceneFrames < 2000) {
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
