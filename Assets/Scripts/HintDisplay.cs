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
				Controller.OnValidate += UpdateDisplay;
				textComponent.text = "";
				for(int i=0;i<Icons.Length;i++){
					Icons[i].color = Color.green;
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
			if(Controller.Introduction){
			
			}else if(Controller.BreakingDown){
				for(int i=0;i<Icons.Length;i++){
					Icons[i].enabled = Random.Range (0,2) == 0;
					Icons[i].color = Random.Range (0,2) == 0 ? Color.green : Color.red;
				}
			} else if (Controller.Silence){
				for(int i=0;i<Icons.Length;i++){
					Icons[i].enabled = false;
					Icons[i].color = Color.red;
				}
			}
		}

		void UpdateDisplay (int nLow, int nHigh)
		{
				textComponent.text = "-" + Controller.NHigh + " +" + Controller.NLow;
				Debug.Log (textComponent.text);
		}
}
