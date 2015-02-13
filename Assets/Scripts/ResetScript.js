#pragma strict


function Update () {
	/* #if UNITY_ANDROID
	if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
		Application.LoadLevel(0);
		Handheld.Vibrate();
		}
	#endif */
	if (Input.anyKey) {
		Application.Quit();
	}
}