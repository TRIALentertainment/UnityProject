using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour 
{
	float seconds = 0;

	public int minutes = 0;
	public int hours = 0;
	public int countSeconds = 0;

	public Transform pointSeconds;
	public Transform pointMinutes;
	public Transform pointHours;

	public Transform centerOfMass;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		seconds += Time.deltaTime;
		if (countSeconds < seconds) {
			countSeconds ++;
			pointSeconds.Rotate (0, 0, 6);
		}
		if (countSeconds > 59) {
			minutes ++;
			countSeconds = 0;
			seconds = 0;
		}
	}
}
