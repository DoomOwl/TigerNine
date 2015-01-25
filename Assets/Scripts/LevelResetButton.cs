using UnityEngine;
using System.Collections;

public class LevelResetButton : Button {

	// Use this for initialization
	void Start () {
		this.OnPressed += ResetLevel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ResetLevel (int pressedButtonState) {
		Debug.Log ("Reset");
		Application.LoadLevel(Application.loadedLevel);
		
	}
}
