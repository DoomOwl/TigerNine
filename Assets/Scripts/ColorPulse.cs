using UnityEngine;
using System.Collections;

public class ColorPulse : MonoBehaviour {
	public Material material1;
	public Material material2;
	public float duration = 2.0F;

	void Start() {
		renderer.material = material1;
	}
	void Update() {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		renderer.material.Lerp(material1, material2, lerp);
	}
}