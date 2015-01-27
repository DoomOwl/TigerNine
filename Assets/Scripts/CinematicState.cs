using UnityEngine;
using System.Collections;

[System.Serializable]
public class CinematicState {
	public State State;
	public float Duration;
}

public enum State {
	Introduction, BreakingDown, Silence, Gameplay
}
