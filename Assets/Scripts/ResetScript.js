#pragma strict


function Update () {
	#if UNITY_ANDROID
	if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
		Application.LoadLevel(0);
		Handheld.Vibrate();
		}
	#endif
	if (Input.GetKeyDown(KeyCode.R)) {
		Application.LoadLevel(0);
	}
}