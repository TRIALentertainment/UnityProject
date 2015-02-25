using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour 
{
	static float timeToFadeIn;
	static float timeToFadeOut;

	public static bool fadeIn = false;
	public static bool fadeOut = false;

	public float timeToFade;

	public Texture fade;

	// Use this for initialization
	void Start () 
	{
		fadeIn = true;
		fadeOut = false;
		timeToFadeIn = 0;
		timeToFadeOut = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fadeIn == true) 
			timeToFadeIn += Time.deltaTime;

		if (fadeOut == true) 
			timeToFadeOut += Time.deltaTime;
		//print ("Fade In: "+fadeIn+" --- Fade Out: "+fadeOut+" --- Time To Fade In: "+timeToFadeIn+" --- Time To Fade Out: "+timeToFadeOut);
	}

	void OnGUI () 
	{
		if (fadeIn == true) 
		{
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, 1 -timeToFadeIn/timeToFade);
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),fade);
		}else if (fadeOut == true) 
		{
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, timeToFadeOut/timeToFade);
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),fade);
		}

		if (GUI.color.a <= 0) 
		{
			fadeIn = false;
			if (Menu.numLevel == -1) 
				SplashScreen.countFadeOut = true;
		}else if (fadeOut && GUI.color.a >= timeToFade) 
		{
			fadeOut = false;
			timeToFadeIn = 0;
			timeToFadeOut = 0;

			if (Menu.numLevel > 0 && Player.passedLevel == false) 
			{
				Application.LoadLevel("Job"+(Menu.numLevel +1));
				Player.passedLevel = true;
			}
		}
	}
}
