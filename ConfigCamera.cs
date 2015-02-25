using UnityEngine;
using System.Collections;

public class ConfigCamera : MonoBehaviour 
{
	static float rotationCamX;
	static float rotationCamY;
	public static float maxDistance;
	
	float limitLook 					= 0f;
	public float limitCameraInXAxisPositive	= 50f;
	public float limitCameraInXAxisNegative	= 20f;
	float distancePlayer 				= 0f;
	
	Vector3 followPlayer;


	public GameObject player;
	public GameObject cameraPoint;

	RaycastHit rayCollision;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("/Hercules");
		cameraPoint = GameObject.Find ("/Camera Point");
		maxDistance = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{
		limitLook = transform.eulerAngles.x;
		
		// Make Camera follow the player
		if (Player.died == false) 
		{
			cameraPoint.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y +1.01f, player.transform.position.z);
			distancePlayer = Vector3.Distance(transform.position, player.transform.position);
			transform.LookAt (cameraPoint.transform);
			
			if (distancePlayer > maxDistance || distancePlayer < -maxDistance) 
				transform.position = Vector3.Lerp(transform.position, transform.position+transform.forward*maxDistance, Time.deltaTime);
		}
		
		// Create a camera system collision to
		
		if (Physics.Raycast (transform.position, transform.forward, out rayCollision)) 
		{
			if (rayCollision.collider.tag != "Player" && rayCollision.collider.tag != "Items - Rock" && rayCollision.collider.tag != "Map") 
				transform.position = Vector3.Lerp(transform.position, rayCollision.point+transform.forward*2, Time.deltaTime);
		}
		
		if (Physics.Raycast (transform.position, transform.right, out rayCollision,1)) 
		{
			if (rayCollision.collider.tag != "Player" && rayCollision.collider.tag != "Items - Rock") 
				transform.position = Vector3.Lerp(transform.position, rayCollision.point+transform.right*-2, Time.deltaTime);
		}
		
		if (Physics.Raycast (transform.position, -transform.right, out rayCollision,1)) 
		{
			if (rayCollision.collider.tag != "Player" && rayCollision.collider.tag != "Items - Rock") 
				transform.position = Vector3.Lerp(transform.position, rayCollision.point+transform.right*2, Time.deltaTime);
		}
		
		rotationCamX = Input.GetAxis("Mouse X") * 90 * Time.deltaTime;
		rotationCamY = -Input.GetAxis("Mouse Y") * 90 * Time.deltaTime;
		transform.RotateAround (player.transform.position, transform.up, rotationCamX);
		
		if (limitLook >= 200) 
			limitLook = limitLook -360;
		
		if (limitLook > -limitCameraInXAxisNegative && limitLook < limitCameraInXAxisPositive) 
			transform.RotateAround (player.transform.position, transform.right, rotationCamY);
		else 
		{
			if (limitLook > limitCameraInXAxisPositive && rotationCamY < 0) 
				transform.RotateAround (player.transform.position, transform.right, rotationCamY);
			
			if (limitLook < -limitCameraInXAxisNegative && rotationCamY > 0) 
				transform.RotateAround (player.transform.position, transform.right, rotationCamY);
		}
	}
}
