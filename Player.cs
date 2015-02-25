using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	// UnityScript : @script RequireComponent(Rigidbody);

	public static bool died				= false;
	public static bool droppedWeapon 	= false;
	public static bool attacked			= false;
	
	public static float numSubAnimation	= 0f;
	
	public static bool passedLevel 		= false;
	public static bool paused 			= false;
	public static bool walking 			= false;
	public static bool running 			= false;
	public static bool pressToGetItem	= false;
	public static bool jumped 			= false;
	public static bool sighting			= false;
	public static bool inStairs 		= false;
	
	bool canWalk  						= false;
	bool showTip						= false;
	
	public GameObject[] objectsToPickUp		= new GameObject[1];
	
	public GameObject playerDeath;
	public GameObject hUD;
	private GameObject objectCollided;
	public GameObject spine;

	public Quaternion rotPlayer;

	public Vector3 positionPlayer;
	
	public Camera mainCamera;

	public float timeToDestroyTips				= 0f;
	
	public int life 					= 100;
	public int timesDead 						= 0;
	public int collideWithItem 				= 0;
	
	public AudioClip[] audioCutscene			= new AudioClip[2];
	public AudioClip[] audioPlayer				= new AudioClip[2];

	Ray ray;

	RaycastHit rayCollision;

	public GUISkin personalizedSkin;

	public RectTransform lifeHUD;

	Inventory inventoryScript;

	Menu menuScript;

	// Use this for initialization
	void Start () 
	{
		// Make load pre values if is a new game
		if (SaveLoad.newGame == true) 
		{
			showTip = true;
			life = 100;
		}else 
			transform.position = positionPlayer;
		
		mainCamera = Camera.main;
		passedLevel = false;
		paused = false;
		died = false;
		inventoryScript = GetComponent<Inventory>();
		menuScript = GameObject.Find ("/Canvas/Scripts").GetComponent<Menu>();
		lifeHUD = GameObject.Find ("/Canvas/HUD/Life Bar").GetComponent<RectTransform>();
		rotPlayer = transform.rotation;
	}
	
	void WaitForAttack () 
	{
		if (Inventory.currentWeapon == -1) 
		{
			if (Random.value > 0.5f) 
				numSubAnimation = 0.5f;
			else 
				numSubAnimation = 0f;
		}
		
		// UnityScript : yield WaitForSeconds = 37f;
		
		attacked = false;
		droppedWeapon = false;
	}

	// Use this for physics situations
	void FixedUpdate () 
	{
		if (Missions.quickTime == false) 
		{
			// Walk if are colliding with terain
			if (Input.GetAxis ("Vertical") != 0 && canWalk == true) 
			{
				Quaternion rotationForward = Quaternion.FromToRotation (transform.forward, mainCamera.transform.forward) * transform.rotation;
				transform.rotation = new Quaternion (transform.rotation.x, Quaternion.Lerp (transform.rotation, rotationForward, Time.deltaTime).eulerAngles.y, transform.rotation.z, transform.rotation.w);
			}
			
			// Rotate if horizontal axis are different of zero
			if (Input.GetAxis ("Horizontal") != 0) 
				transform.Rotate(0, Input.GetAxis ("Horizontal") * 100 * Time.deltaTime, 0);
			
			// Jump
			if (Input.GetKeyDown(KeyCode.Space) && jumped == false && canWalk == true) 
			{ 
				GetComponent<Animator>().applyRootMotion = false; // Disable ApplyRootMotion for be able to jump
				canWalk = false; // Disable to can't walk in air
				jumped = true;
				//	rigidbody.AddForce (transform.up * 500, ForceMode.Impulse); // Apply impulse to player
			}
		}
	}

	// Update is called once per frame
	void Update () {
		// Call important functions
		RayCast ();
		
		// HUD
		if (Cutscenes.inCutscene == false) 
			hUD.SetActive(!paused);
		else 
			hUD.SetActive(!Cutscenes.inCutscene);
		
		lifeHUD.sizeDelta = new Vector2 (life*2,20);
		
		// If player fall, returns to +2 up the terrain
		if (transform.position.y < -450) {
			transform.position = new Vector3 (transform.position.x, 2, transform.position.z);
		}

		// Get current position
		positionPlayer = transform.position;
		
		// Die
		if (life <= 0) 
		{
			Destroy (gameObject);
			timesDead ++;
			died = true;
			hUD.SetActive(false);
			Instantiate (playerDeath, transform.position, transform.rotation); // Use ragdoll
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}else 
		{
			Screen.showCursor = paused;
			Screen.lockCursor = !paused;
		}
		
		// Enable/Disable tips for auxiliating the player
		if (showTip == true) 
			timeToDestroyTips += Time.deltaTime;
		
		if (timeToDestroyTips > 8) 
			showTip = false;
		
		// Input controls
		if (Input.GetMouseButton (1)) 
		{ // Sight
			spine.transform.eulerAngles = new Vector3 (mainCamera.transform.eulerAngles.x, 0, 0);
			sighting = true;
		}else 
			sighting = false;
		
		if (Input.GetMouseButtonDown (0)) 
		{ // Attack
			if (attacked == false) 
			{
				attacked = true;
				
				if (Inventory.currentWeapon == 8)
					droppedWeapon = true;
				
				WaitForAttack ();
			}
		}
		
		if (Input.GetKeyDown (KeyCode.G) && inventoryScript.weaponSet != null) 
		{ // Drop weapon
			droppedWeapon = true;
			WaitForAttack ();
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))  // Pause
			if (Menu.openOptions == false && SaveLoad.saveOrLoad == false)
				Pause(paused);
		
		if (Input.GetKeyDown (KeyCode.E) && collideWithItem > 0) // Pick up item
			pressToGetItem = true;
		
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) 
		{
			walking = true;
			if (Input.GetKey(KeyCode.LeftShift)) 
			{ // Run
				ConfigCamera.maxDistance = 4;
				running = true;
			}else 
			{
				ConfigCamera.maxDistance = 3;
				running = false;
			}
		}else
			walking = false;
	}

	void PickUpItem () 
	{
		Destroy (objectCollided);
		Inventory.currentWeapon = 8;
		inventoryScript.weapons[Inventory.currentWeapon] = objectsToPickUp[collideWithItem -1];
		inventoryScript.SetWeapon ();
		collideWithItem = 0;
		pressToGetItem = false;
	}
	
	void RayCast () 
	{
		ray = mainCamera.ScreenPointToRay (new Vector3(Screen.width/2, Screen.height/2, 0));
		if (Physics.Raycast (ray, out rayCollision, 30)) 
		{
			if (rayCollision.collider.tag == "Job3 - Part 1" && Missions.quickTime == false) 
			{
				if (Input.GetMouseButton(0)) 
					Missions.quickTime = true;
			}
		}
		
		// Can walk if colliding with ground, else can't
		if (Physics.Raycast(transform.position, -transform.up, GetComponent<CapsuleCollider>().height / 2 + 1.14f)) 
		{
			GetComponent<Animator>().applyRootMotion = true;
			jumped = false;
			canWalk = true;
			if (rayCollision.collider.tag == "Stair") 
				inStairs = true;
			else
				inStairs = false;
		}else 
		{
			GetComponent<Animator>().applyRootMotion = false; // Disable ApplyRootMotion for be able to jump
			canWalk = false; // Disable to can't walk in air
			/*if (jumped == false && inStairs == false) {
			rigidbody.freezeRotation = false;
		}else if (jumped == true || inStairs == true) {
			rigidbody.freezeRotation = true;
		}*/
		}
	}
	
	public void Pause(bool type) 
	{
		if (paused == false) 
		{
			Time.timeScale = 0;
			menuScript.pauseMenu.SetActive (true);
			paused = true;
		}else 
		{
			Time.timeScale = 1;
			menuScript.pauseMenu.SetActive (false);
			paused = false;
		}
	}
	
	void OnCollisionEnter (Collision collision) 
	{
		if (collision.gameObject.tag == "Stair") 
			inStairs = true;
		
		if (collision.gameObject.tag == "Enemy - Lion") 
			life -= 5;
	}
	
	void OnTriggerEnter (Collider other) 
	{
		// Pass level
		if (other.gameObject.tag == "Launcher" && passedLevel == false) 
			FadeInOut.fadeOut = true;
		
		// Possibilite to pick up items
		if (other.gameObject.tag == "Items - Rock") 
		{
			collideWithItem = 1;
			objectCollided = other.gameObject;
		}else 
			collideWithItem = 0;
		
	}
	
	void OnGUI () 
	{
		GUI.skin = personalizedSkin;
		
		if (collideWithItem > 0) {
			GUI.Box (new Rect(Screen.width/2-55, Screen.height-60, 110, 22),"");
			GUI.Label (new Rect(Screen.width/2-55, Screen.height-60, 110, 22),"Press E to pick up");
		}
		
		if (showTip == true) {
			GUI.Box (new Rect(0, 0, 200, 140),"");
			GUI.Label (new Rect(6, 3, 200, 22),"Press W, S, A, D to walk");
			GUI.Label (new Rect(6, 25, 200, 22),"Press LSHIFT to run");
			GUI.Label (new Rect(6, 47, 200, 22),"Press E to special actions");
			GUI.Label (new Rect(6, 69, 200, 22),"Press LMB to attack");
			GUI.Label (new Rect(6, 91, 200, 22),"Press RMB to sight");
			GUI.Label (new Rect(6, 113, 200, 22),"Use MOUSE to look");
		}
	}
}
