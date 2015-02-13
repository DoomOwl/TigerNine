using UnityEngine;
using System.Collections;

public class LevelResetButton : MonoBehaviour {
	public Button activeButton;
	// Use this for initialization
	void Start () {
		activeButton.OnPressed += ResetLevel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ResetLevel (int pressedButtonState) {
		Debug.Log ("Reset");
		Application.Quit();
	}
}
