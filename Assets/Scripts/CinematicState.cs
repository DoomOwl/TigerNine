using UnityEngine;
using System.Collections;

[System.Serializable]
public class CinematicState {
	public enum Sequence {
		Introduction, BreakingDown, Silence, Gameplay
	}

	public Sequence SequenceName;
	public float Duration;
}
