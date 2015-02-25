using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cutscenes : MonoBehaviour 
{
	public static bool inCutscene 			= false; // Play or stop cutscenes
	
	bool waitTime 							= false;
	bool verifyAudio						= false;
	
	public Vector3[] positionCamera 		= new Vector3[100];
	
	public Quaternion[] rotationCamera 		= new Quaternion[100];
	
	float timeToChangeText					= 0f;
	float countTime 						= 0f;
	
	// Control the time of the camera...
	float timeToMoveCamera 					= 0f; // ... in position
	float timeToRotateCamera				= 0f; // ... in rotation
	
	int positionCutscene					= 0; // Control the cutscenes
	int positionCameraNum					= 0; // Control the camera
	
	public GameObject borderCutscene;
	public GameObject player;
	
	public AudioClip[] audioCutscene		= new AudioClip[2]; // All audio clip archieves
	
	public Text cutscenes;

	// Use this for initialization
	void Start () 
	{
		cutscenes = cutscenes.GetComponent<Text>();
		borderCutscene = GameObject.Find ("/Canvas/Cutscene");
		player = GameObject.Find ("/Hercules");
		inCutscene = false;
		if (SaveLoad.newGame == true) 
		{
			positionCutscene = 0;
			positionCameraNum = 0;
		}
		positionCamera[0] = new Vector3(0,0,0);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (positionCameraNum == 0) 
			timeToMoveCamera = 1f;
		if (positionCameraNum == 0) 
			timeToRotateCamera = 1f;
		if (positionCameraNum == 1) 
			timeToMoveCamera = Time.deltaTime;
		if (positionCameraNum == 0) 
			timeToRotateCamera = Time.deltaTime;
		
		// Check if is playing a cutscene
		if (inCutscene == true) 
		{
			ControlCamera (positionCamera[positionCameraNum], rotationCamera[positionCameraNum]);
			borderCutscene.SetActive(true);
			
			// If value of current cutscene is less than max value...
			if (positionCutscene == 0 || positionCutscene == 1) 
			{
				// ... Assign a new audio clip referring to value...
				player.audio.clip = audioCutscene[positionCutscene];
				player.audio.Play ();
				waitTime = true;
			}
			// ... And then assign a respective subtitle text of this audio clip.
			cutscenes.text = GameTexts.textCutscenes[positionCutscene];	
		}else 
			borderCutscene.SetActive(false);
		
		if (waitTime == true) 
			countTime += Time.deltaTime;
		
		if (countTime > 0f) {
			verifyAudio = true;
			waitTime = false;
			countTime = 0f;
		}
		// If audio is playing now...
		if (verifyAudio == true)
		{ 
			inCutscene = true;
			
			// ... start count time.
			timeToChangeText += Time.deltaTime; 		
			
			// If time is more than length of current audio clip...
			if (timeToChangeText > audioCutscene[positionCutscene].length) 
			{ 
				// ... make play next clip adding +1 to current cutscene...
				positionCutscene ++;  
				
				// And return zero to time.
				timeToChangeText = 0;	
				
				verifyAudio = false;				
			}
		}else 
		{
			cutscenes.text = "";
			inCutscene = false;
		}
	}

	void ControlCamera (Vector3 posToMove, Quaternion posToRotate) 
	{
		transform.position = Vector3.Lerp (transform.position, posToMove, timeToMoveCamera);
		transform.rotation = Quaternion.Lerp (transform.rotation, posToRotate, timeToRotateCamera);
		
		if (transform.position == posToMove) 
			positionCameraNum ++;
	}
}
