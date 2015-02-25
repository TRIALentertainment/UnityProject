using UnityEngine;
using System.Collections;

public class Missions : MonoBehaviour 
{
	public static bool quickTime 	= false;
	
	public Camera mainCamera;
	
	public int maxValueInQuickTime;

	int countPressedButton  = 0;
	int countButton  = 0;
	
	public GameObject world;

	GameObject spaceKeyIcon;

	Quaternion rotWorld;
	
	float timeToSubtration   	= 0f;
	float limitRotation		= 0f;
	float limitRotationWorld  = 20;
	
	public RectTransform barProgress;

	// Use this for initialization
	void Start () 
	{
		mainCamera = Camera.main;
		rotWorld = world.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Menu.numLevel == 1) 
		{
		}

		if (Menu.numLevel == 2) 
		{
		}

		if (Menu.numLevel == 3) 
		{
			if (countButton == 0 || countButton <= countPressedButton) 
			{
				Time.timeScale = 1;
			}

			if (countButton > countPressedButton) 
			{
			}
			
			countButton = countPressedButton;
			barProgress.sizeDelta = new Vector2 (countPressedButton*30,20);
			spaceKeyIcon.SetActive(quickTime);

			if (quickTime == true) 
			{
				timeToSubtration += Time.deltaTime;
				if (Input.GetKeyDown(KeyCode.Space)) 
				{
					countPressedButton ++;
				}

				if (timeToSubtration > 0.175f && (countPressedButton > 0 && countPressedButton <= maxValueInQuickTime +1)) 
				{
					countPressedButton --;
					timeToSubtration = 0;
				}

				if (countPressedButton > maxValueInQuickTime) 
				{
					quickTime = false;
					countPressedButton = 0;
				}
			}
		}

		if (Menu.numLevel == 4) 
		{
		}

		if (Menu.numLevel == 5) 
		{
		}

		if (Menu.numLevel == 6) 
		{
		}

		if (Menu.numLevel == 12) 
		{
			limitRotation = world.transform.rotation.x;
			world.transform.rotation = rotWorld;
			
			if (world.transform.rotation.x > 0)
				rotWorld.x -= 0.01f;
			
			if (world.transform.rotation.x < 0)
				rotWorld.x += 0.01f;
			
			if (limitRotation >= 270) 
				limitRotation = limitRotation -360;
			
			if ((limitRotation > -limitRotationWorld || limitRotation < -limitRotationWorld) && (limitRotation < limitRotationWorld || limitRotation > limitRotationWorld)) 
			{
				if (Input.GetKey (KeyCode.LeftArrow)) 
					rotWorld.x -= 0.01f;
				else 
					if (world.transform.rotation.x != 0)
						rotWorld.x += 0.01f;
				
				if (Input.GetKey (KeyCode.RightArrow)) 
					rotWorld.x += 0.01f;
				else
					if (world.transform.rotation.x != 0)
						rotWorld.x -= 0.01f;
			}
		}
	}
}