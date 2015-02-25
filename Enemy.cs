using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour 
{
	//@script RequireComponent(Rigidbody);
	
	public static bool walking 		= true;
	public static bool running		= false;
	public static bool died			= false;
	
	public GameObject player;
	public GameObject rock;

	float rotateEnemy;

	Vector3 positionRock;
	Vector3 lookPosition;
	
	float timeStopped 				= 0f;
	float timeToChangeWay 			= 0f;
	float randomRotate 				= 0f;
	
	public int life 				= 100;
	public Sprite[] iconEnemy = new Sprite[12];
	public Image imageEnemyHUD;
	public RectTransform lifeHUD;

	// Use this for initialization
	void Start () 
	{
		life = 100;
		died = false;
		player = GameObject.Find ("/Hercules");
		lifeHUD = GameObject.Find ("/Canvas/HUD/Enemy Life Bar").GetComponent<RectTransform>();
		imageEnemyHUD = GameObject.Find ("/Canvas/HUD/Enemy Image").GetComponent<Image>();
	}

	// Use this for physics situations
	void FixedUpdate () {
		if (walking == true) 
		{
			//Rotate (randomRotate);
			if (Player.died == false && player.transform.position.x - transform.position.x < 10 && player.transform.position.y - transform.position.y < 10 && player.transform.position.z - transform.position.z < 10 && player.transform.position.x - transform.position.x > -10 && player.transform.position.y - transform.position.y > -10 && player.transform.position.z - transform.position.z > -10) 
			{
				transform.LookAt (lookPosition);

				if (player.transform.position.x - transform.position.x > 1 && player.transform.position.y - transform.position.y > 1 && player.transform.position.z - transform.position.z > 1 && player.transform.position.x - transform.position.x < -1 && player.transform.position.y - transform.position.y < -1 && player.transform.position.z - transform.position.z < -1)
					rigidbody.velocity = transform.forward * 150 * Time.deltaTime;

				running = true;
			}else 
			{
				rigidbody.velocity = transform.forward * 60 * Time.deltaTime;
				timeToChangeWay += Time.deltaTime;
				running = false;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		imageEnemyHUD.sprite = iconEnemy[Menu.numLevel];
		lifeHUD.sizeDelta = new Vector2 (life*2, 20);
		lookPosition.x = player.transform.position.x;
		lookPosition.y = 0;
		lookPosition.z = player.transform.position.z;
		
		if (Menu.numLevel == 1) 
		{
			if (Items.rockCollision == true && transform.position.x - rock.transform.position.x <= rock.GetComponent<AudioSource>().minDistance && transform.position.y - rock.transform.position.y <= rock.GetComponent<AudioSource>().minDistance && transform.position.z - rock.transform.position.z <= rock.GetComponent<AudioSource>().minDistance && transform.position.x - rock.transform.position.x >= -rock.GetComponent<AudioSource>().minDistance && transform.position.y - rock.transform.position.y >= -rock.GetComponent<AudioSource>().minDistance && transform.position.z - rock.transform.position.z >= -rock.GetComponent<AudioSource>().minDistance) 
			{
				positionRock.x = rock.transform.position.x;
				positionRock.y = rock.transform.position.y;
				positionRock.z = rock.transform.position.z;
				transform.LookAt(rock.transform);
				Lerp (positionRock);
			}else 
				Items.rockCollision = false;
		}
		
		if (timeToChangeWay > 10) 
		{
			walking = false;
			timeStopped += Time.deltaTime;
		}
		
		if (timeStopped > 3) 
		{
			randomRotate = Random.value * 100;
			walking = true;
			transform.Rotate(0,randomRotate,0);
			timeToChangeWay = 0;
			timeStopped = 0;
		}
		
		if (life <= 0) 
		{
			died = true;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Weapon" && Player.attacked == true) 
		{
			life -= 5;
		}
	}
	
	void Rotate (float valueRotate) {
		Quaternion quaternion;
		quaternion.y = valueRotate;
		rotateEnemy = Quaternion.Lerp (transform.rotation, quaternion, Time.deltaTime).eulerAngles.y;
		transform.rotation = new Quaternion (transform.rotation.eulerAngles.x, rotateEnemy, transform.rotation.eulerAngles.z, 0);
	}
	
	void Lerp (Vector3 position) {
		walking = true;
		transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);

		if (transform.position == position) 
			walking = false;
	}
}
