#pragma strict
var guiObject : GUITexture;

var fadeTime = 2.0;
//var nextscene= "prototype";
var material1 : Material;
var material2 : Material;
var duration = 1.0;


enum Fade {In, Out}

// Fade in the GUITexture, wait a couple of seconds, then fade it out

function Start () {
    renderer.material = material1;

    guiObject.color.a = 0;

    yield WaitForSeconds(0.5);

    yield FadeGUITexture(guiObject, fadeTime, Fade.In);

    yield WaitForSeconds(0.25);

    yield FadeGUITexture(guiObject, fadeTime, Fade.Out);
	
	yield WaitForSeconds(0.25);
	
	yield FadeGUITexture(guiObject, fadeTime, Fade.In);

    yield WaitForSeconds(0.25);

    yield FadeGUITexture(guiObject, fadeTime, Fade.Out);
	
	yield WaitForSeconds(0.25);
	
	
	
    
    //Application.LoadLevel("prototype");
}

function Update () {
	var lerp = Mathf.PingPong(Time.time, duration) / duration;
	renderer.material.Lerp(material1, material2, lerp);

	
	if(Input.anyKey) {
		//yield FadeGUITexture(guiObject, fadeTime, Fade.Out);
		Application.LoadLevel(1);	
		Debug.Log("A key or mouse click has been detected");
	}

 }

function FadeGUITexture (guiObject : GUITexture, timer : float, fadeType : Fade) {

    var start = fadeType == Fade.In? 0.0 : 1.0;

    var end = fadeType == Fade.In? 1.0 : 0.0;

    var i = 0.0;

    var step = 1.0/timer;

    

    while (i < 1.0) {

        i += step * Time.deltaTime;

        guiObject.color.a = Mathf.Lerp(start, end, i)*.5;

        yield;

    }

}

