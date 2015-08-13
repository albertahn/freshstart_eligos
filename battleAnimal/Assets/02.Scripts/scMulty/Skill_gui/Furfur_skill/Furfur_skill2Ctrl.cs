using UnityEngine;
using System.Collections;

public class Furfur_skill2Ctrl : MonoBehaviour{
	public float birth;	
	private float durationTime;

	// Use this for initialization
	void Start () {
		birth = Time.time;
		durationTime = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birth) > durationTime) {
			Destroy(this.gameObject);
		}
	}
}
