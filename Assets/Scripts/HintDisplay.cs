using UnityEngine;
using System.Collections;

public class HintDisplay : MonoBehaviour
{
	public MastermindController Controller;

	public SpriteRenderer[] Icons;

	private TextMesh textComponent;
		// Use this for initialization
	void Start ()
	{
		textComponent = GetComponent<TextMesh> ();
		Controller.OnValidate += UpdateHint;
		Controller.OnStateValid += UpdateSystemIcons;
		textComponent.text = "";
		for(int i=0;i<Icons.Length;i++){
			Icons[i].color = Color.green;
		}
	}
	
		// Update is called once per frame
	void Update ()
	{
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
