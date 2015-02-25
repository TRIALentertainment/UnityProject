using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour 
{
	public static bool rockCollision;
	
	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("/Hercules");
	}

	// Use this for physics situations
	void FixedUpdate () 
	{
		if (Player.droppedWeapon == true && Inventory.currentWeapon == 8) 
		{
			rigidbody.AddForce (player.transform.up * 380, ForceMode.Force);
			rigidbody.AddForce (player.transform.forward * 300, ForceMode.Force);
		}
		
		if (Inventory.currentWeapon == 0) 
			rigidbody.velocity = player.transform.forward * 30;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter (Collision collision) 
	{
		if (collision.gameObject) 
		{
			rockCollision = true;
			audio.Play();
		}
	}
}
