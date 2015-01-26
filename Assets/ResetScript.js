#pragma strict


function Update () {
	if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
		Application.LoadLevel(0);
		Handheld.Vibrate();
		}
}