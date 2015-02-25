using UnityEngine;
using System.Collections;

public class GlobalAnimation : MonoBehaviour 
{
	public Animator animatorPlayer;
	public Animator[] animatorEnemy = new Animator[12];
	float count = 0f;
	float num = 0f;

	// Use this for initialization
	void Start () 
	{
		animatorPlayer = animatorPlayer.GetComponent<Animator>();
		animatorEnemy[Menu.numLevel -1] = GameObject.FindWithTag ("Enemy").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (count < num && count != num) 
			count += 1f;
		
		if (count > num && count != num)
			count -= 1f;
		
		if (Player.died == false) 
		{
			animatorPlayer.SetBool ("Walk", Player.walking);
			animatorPlayer.SetBool ("Run", Player.running);
			animatorPlayer.SetInteger ("Weapon", Inventory.currentWeapon);
			animatorPlayer.SetBool ("PickUp", Player.pressToGetItem);
			animatorPlayer.SetBool ("Jump", Player.jumped);
			animatorPlayer.SetBool ("Attack", Player.attacked);
			animatorPlayer.SetBool ("Sight", Player.sighting);
			
			if (Inventory.currentWeapon == -1) 
				num = 0f;
			
			if (Inventory.currentWeapon == 1) 
				num = 5f;
			
			if (Inventory.currentWeapon == 8) 
				num = 1;
			
			animatorPlayer.SetFloat ("Animation", count);
			animatorPlayer.SetFloat ("SubAnimation", Player.numSubAnimation);
		}
		
		if (Enemy.died == false) 
		{
			animatorEnemy[Menu.numLevel -1].SetBool ("Walk", Enemy.walking);
			animatorEnemy[Menu.numLevel -1].SetBool ("Run", Enemy.running);
		}
	}
}
