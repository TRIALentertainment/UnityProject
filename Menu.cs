using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public static string textResolution;
	public static string textLanguage;
	public static string textQuality;
	public static string textMode;
	
	public static bool openOptions			= false;
	public static bool showFadeTexture		= false;
	public static bool restartingGame 		= false;
	public static bool backButtonWasPressed = false;
	
	public static int numLevel 				= 0;

	public GameObject mainMenu;
	public GameObject startMenu;
	public GameObject pauseMenu;
	public GameObject deathMenu;
	public GameObject optionsMenu;
	public GameObject fadeScreen;

	public Texture fadeTexture;
	
	int levelResolution = 1;
	int levelLanguage;
	int levelQuality;
	int levelMode;
	int width;
	int height;
	
	Player playerScript;

	// Use this for initialization
	void Start () 
	{
		playerScript = GameObject.Find ("Hercules").GetComponent<Player>();
		fadeScreen = GameObject.Find ("/Canvas/Fade Screen");
		textResolution = Screen.width+"x"+Screen.height;
		textLanguage = GameTexts.nameLanguage[levelLanguage];
		textQuality = QualitySettings.names [QualitySettings.GetQualityLevel ()];
		levelQuality = QualitySettings.GetQualityLevel ();
		levelLanguage = GameTexts.language;
		
		if (Screen.fullScreen == true) 
		{
			levelMode = 1;
			textMode = GameTexts.words[1];
		}else 
		{
			levelMode = 0;
			textMode = GameTexts.words[0];
		}
		
		openOptions = false;
		
		if (restartingGame == false) 
			numLevel ++;
		//textMode = GameTexts.words[levelMode];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (backButtonWasPressed == true) 
		{
			ReturnSaveLoad ();
			backButtonWasPressed = false;
		}

		if (numLevel > 0) 
			deathMenu.SetActive(Player.died);
	}
	
	public void Menus (int numMenu) 
	{
		if (numMenu == 0) 
		{
			startMenu.SetActive (true);
			mainMenu.SetActive (false);
		}

		if (numMenu == 1) 
		{
			if (openOptions == false) 
			{
				optionsMenu.SetActive(true);

				if (numLevel == 0) 
					mainMenu.SetActive(false);

				if (numLevel >= 1) 
					pauseMenu.SetActive(false);

				openOptions = true;
			}else if (openOptions == true) 
			{
				optionsMenu.SetActive(false);

				if (numLevel == 0) 
					mainMenu.SetActive(true);

				if (numLevel >= 1) 
					pauseMenu.SetActive(true);

				openOptions = false;
			}
		}
		if (numMenu == 2) 
			Application.Quit ();

		if (numMenu == 3) 
		{
			restartingGame = true;
			Application.LoadLevel ("Job"+(numLevel));
		}
	}

	public void ReturnSaveLoad () 
	{	
		if (numLevel == 0) 
			startMenu.SetActive (true);

		if (numLevel >= 1) 
			pauseMenu.SetActive (true);

		SaveLoad.saveOrLoad = false;
	}

	public void SubMenu1 (int subMenu) 
	{
		if (subMenu == 0) 
		{
			SaveLoad.newGame = true;
			showFadeTexture = true;			
			fadeScreen.SetActive (true);
			Application.LoadLevel ("Job"+(numLevel +1));
		}

		if (subMenu == 1) 
		{
			if (numLevel == 0) 
				startMenu.SetActive (false);

			if (numLevel >= 1) 
				pauseMenu.SetActive (false);

			SaveLoad.saveOrLoad = true;
		}
	}

	public void PauseMenu (int numMenu) 
	{
		if (numMenu == 0) 
			playerScript.Pause(true);
		if (numMenu == 2) 
		{
			Application.LoadLevel ("Menu");
			numLevel = 0;
		}
	}

	public void ResolutionLevel (string addOrSub) 
	{
		if (levelResolution == 0) 
		{
			width = 640;
			height = 480;
		}else if (levelResolution == 1) 
		{
			width = 800;
			height = 600;
		}else if (levelResolution == 2) 
		{
			width = 1024;
			height = 728;
		}else if (levelResolution == 3) 
		{
			width = 1280;
			height = 728;
		}else if (levelResolution == 4) 
		{
			width = 1366;
			height = 768;
		}

		if (levelResolution >= 0 && levelResolution <= 5) 
		{
			if (addOrSub == "+1" && levelResolution < 4) 
				levelResolution ++;

			else if (addOrSub == "-1" && levelResolution > 0) 
				levelResolution --;
		}

		if (Screen.fullScreen == true) 
			Screen.SetResolution(width, height, true);
		else 
			Screen.SetResolution(width, height, false);

		textResolution = width+"x"+height;
	}

	public void QualityLevelX (string addOrSub) 
	{
		if (levelQuality >= 0 && levelQuality <= 7) 
		{
			if (addOrSub == "+1" && levelQuality < 6) 
				levelQuality ++;

			else if (addOrSub == "-1" && levelQuality > 1) 
				levelQuality --;
		}

		if (levelQuality != 0) 
		{
			QualitySettings.SetQualityLevel(levelQuality -1);
			textQuality = QualitySettings.names[levelQuality -1];
		}
	}

	public void ExibitionMode (string addOrSub) 
	{
		if (levelMode >= 0 && levelMode <= 2) 
		{
			if (addOrSub == "+1" && levelMode < 1) 
				levelMode ++;

			else if (addOrSub == "-1" && levelMode > 0) 
				levelMode --;
		}

		if (levelMode == 1) 
			Screen.SetResolution(Screen.width, Screen.height, true);
		else 
			Screen.SetResolution(Screen.width, Screen.height, false);

		textMode = GameTexts.words[levelMode];
	}

	public void ChooseLanguage (string addOrSub) 
	{
		if (levelLanguage >= 0 && levelLanguage <= 2) 
		{
			if (addOrSub == "+1" && levelLanguage < 2) 
				levelLanguage ++;

			else if (addOrSub == "-1" && levelLanguage > 0) 
				levelLanguage --;
		}

		GameTexts.language = levelLanguage;
		textLanguage = GameTexts.nameLanguage[levelLanguage];
	}
}
