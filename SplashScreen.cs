using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour 
{
	public static bool countFadeOut = false;
	float timeToChangeScreen;

	// Use this for initialization
	void Start () 
	{
		Menu.numLevel = -1;
		timeToChangeScreen = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		print (Menu.numLevel);
		if (countFadeOut == true) {
			timeToChangeScreen += Time.deltaTime;
		}
		if (timeToChangeScreen > 2) {
			FadeInOut.fadeOut = true;
		}
		if (timeToChangeScreen > 3) {
			Application.LoadLevel("Menu");
		}
	}
}
