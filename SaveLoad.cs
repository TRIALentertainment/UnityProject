using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour 
{
	public static bool newGame		= true;
	public static bool saveOrLoad 	= false;

	public GameObject fadeScreen;
	
	bool save 						= false;
	bool load 						= false;
	bool[] haveData 				= new bool[10];
	
	string slotX;
	
	public GUISkin personalizedSkin;
	
	Player playerScript;

	// Use this for initialization
	void Start () {
		playerScript = GameObject.Find ("/Hercules").GetComponent<Player>();
		fadeScreen = GameObject.Find ("/Canvas/Fade Screen");
		fadeScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SaveLoadNewGame (int num) {
		if (num == 0) {
			save = true;
			load = false;
		}
		
		if (num == 1) {
			load = true;
			save = false;
		}
	}
	
	void OnGUI () {
		GUI.skin = personalizedSkin;
		
		if (saveOrLoad == true) {
			if (GUI.Button (new Rect(Screen.width - 170, Screen.height - 40, 160, 30), "Return")) {
				Menu.backButtonWasPressed = true;
			}
			for (int i = 0; i < 10; i++) {
				if (GUI.Button (new Rect(0, Screen.height/2 - Screen.height/4 - 25 + (i * 35), Screen.width,35), "Slot "+(i+1))) {
					if (save == true) {
						SlotN (i);
						Save (i);
						save = false;
						playerScript.Pause (true);
						saveOrLoad = false;
					}
					
					if (load == true) {
						SlotN (i);
						Load (i);
						load = false;
					}
				}
				if (GUI.Button (new Rect(0, Screen.height/2 - Screen.height/4 - 25 + (i * 35), Screen.width, 35), "Slot "+(i+1))) {
					
				}
			}
		}
	}
	
	void Save (int num) {
		if (haveData[num] == false) {
			PlayerPrefs.SetInt(slotX+"CurrentLevel", Menu.numLevel);
			PlayerPrefs.SetInt(slotX+"Life", playerScript.life);
			PlayerPrefs.SetFloat(slotX+"PositionPlayerX", playerScript.positionPlayer.x);
			PlayerPrefs.SetFloat(slotX+"PositionPlayerY", playerScript.positionPlayer.y);
			PlayerPrefs.SetFloat(slotX+"PositionPlayerZ", playerScript.positionPlayer.z);
			haveData[num] = true;
		}
	}
	
	void Load (int num) {
		if (haveData[num] == true) {
			Menu.numLevel = PlayerPrefs.GetInt(slotX+"CurrentLevel");
			playerScript.life = PlayerPrefs.GetInt(slotX+"Life");
			playerScript.positionPlayer.x = PlayerPrefs.GetFloat(slotX+"PositionPlayerX");
			playerScript.positionPlayer.y = PlayerPrefs.GetFloat(slotX+"PositionPlayerY");
			playerScript.positionPlayer.z = PlayerPrefs.GetFloat(slotX+"PositionPlayerZ");
			fadeScreen.SetActive (true);
			Application.LoadLevel ("Job"+(Menu.numLevel));
		}
	}
	
	void SlotN (int slot) {
		slotX = "Slot"+slot;
		print ("Num Slot: "+slot+" --- Name Slot: "+slotX);
	}
}
