using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
	public static int currentWeapon 	= -1;
	public static bool droppedWeapon	= false;
	
	public GameObject weaponSet;
	GameObject spawnPosition;
	GameObject hand;
	
	public GameObject[] weapons = new GameObject[10];

	public Sprite[] iconWeapons = new Sprite[11];

	public Image weaponsHUD;

	public void SetWeapon () {
		if (currentWeapon != -1) {
			Destroy (weaponSet);
			weaponSet = Instantiate(weapons[currentWeapon], spawnPosition.transform.position, spawnPosition.transform.rotation) as GameObject;
			weaponSet.transform.parent = hand.transform;
			weaponSet.rigidbody.useGravity = false;
			weaponSet.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			weaponSet.rigidbody.isKinematic = true;
			weaponSet.rigidbody.mass = 1;
			weaponSet.rigidbody.interpolation = RigidbodyInterpolation.None;
			
			if (currentWeapon == 0 || currentWeapon == 1) 
				weaponSet.GetComponent<BoxCollider>().enabled = false;
			
			if (currentWeapon == 8) {
				weaponSet.GetComponent<CapsuleCollider>().enabled = false;
				weaponSet.GetComponent<SphereCollider>().enabled = false;
			}
		}else 
			Destroy (weaponSet);

		weaponsHUD = GameObject.Find ("/Canvas/HUD/Weapon HUD").GetComponent<Image>();
	}
	
	void DropWeapon () {
		if (weaponSet != null && Player.droppedWeapon == true) {
			Destroy (weaponSet);
			Instantiate(weapons[currentWeapon], spawnPosition.transform.position, spawnPosition.transform.rotation);
			currentWeapon = -1;
		}
	}

	// Use this for initialization
	void Start () {
		spawnPosition = GameObject.Find("/Hercules/python/hips/spine/spine-1/chest/chest-1/clavicle_R/deltoid_R/upper_arm_R/forearm_R/hand_R/PositionWeapon");
		hand = GameObject.Find("/Hercules/python/hips/spine/spine-1/chest/chest-1/clavicle_R/deltoid_R/upper_arm_R/forearm_R/hand_R");
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.attacked == true && currentWeapon == 0) {
			Instantiate(weapons[currentWeapon +9], spawnPosition.transform.position, spawnPosition.transform.rotation);
			Player.attacked = false;
		}
		
		if (weaponSet != null) weaponSet.rigidbody.position = Vector3.zero;
		
		DropWeapon ();
		
		weaponsHUD.sprite = iconWeapons[currentWeapon +1];
		
		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			currentWeapon = -1;
			SetWeapon ();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			currentWeapon = 0;
			SetWeapon ();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			currentWeapon = 1;
			SetWeapon ();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha9)) {
			currentWeapon = 8;
			SetWeapon ();
		}
	}
}
