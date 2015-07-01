using UnityEngine;
using System.Collections;

public class Furfur_skill3Ctrl : MonoBehaviour{
	public float birth;	
	private float durationTime;
	private int damage;

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

	void OnTriggerEnter(Collider coll){
		Debug.Log ("coll.gameObject.tag = "+coll.gameObject.tag);
		if (coll.gameObject.tag == "MINION") {
			if ((ClientState.team == "red" && coll.name [0] == 'b') ||
			    (ClientState.team == "blue" && coll.name [0] == 'r')) {				
				if (coll.gameObject.name [0] == 'r')
					coll.gameObject.GetComponent<minion_state> ().Heated ("skill", gameObject, damage);
				else if (coll.gameObject.name [0] == 'b')
					coll.gameObject.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, damage);
				Destroy (this.gameObject);
			}
		} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider"&&coll.name!=firedByName) {				
				coll.gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedByName, this.gameObject);
				Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "FLOOR") {
			Destroy (this.gameObject);
		}
	}
}