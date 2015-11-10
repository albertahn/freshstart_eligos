using UnityEngine;
using System.Collections;

public class Furfur_beShotEffectCtrl : MonoBehaviour{
	public float birth;	
	private float durationTime;

	// Use this for initialization
	void Start () {
		birth = Time.time;
		durationTime = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birth) > durationTime) {
			Destroy(this.gameObject);
		}
	}
}
