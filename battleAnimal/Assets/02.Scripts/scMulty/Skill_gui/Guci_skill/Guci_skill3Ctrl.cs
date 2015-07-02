using UnityEngine;
using System.Collections;

public class Guci_skill3Ctrl : MonoBehaviour {
	public float birth;
	private float durationTime;

	public Transform tr;
	public string firedByName;
	public int damage;
	
	// Use this for initialization
	void Awake () {
		tr = GetComponent<Transform> ();
		//target = null;
		damage = playerStat.skill3_damage;

		birth = Time.time;
		durationTime = 5.0f;
	}

	public void setOwner(string firedby){
		firedByName = firedby;
		StartCoroutine (destroySkill ());
	}

	IEnumerator destroySkill(){
		yield return new WaitForSeconds (durationTime);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "MINION") {			
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
			if ((ClientState.team == "red" && coll.name [0] == 'b') ||
			    (ClientState.team == "blue" && coll.name [0] == 'r')) {
				//Debug.Log("skill first hit min");

				if (coll.gameObject.name [0] == 'r')
					coll.gameObject.GetComponent<minion_state> ().Heated ("skill", gameObject, damage);
				else if (coll.gameObject.name [0] == 'b')
					coll.gameObject.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, damage);
				//Destroy (this.gameObject);
			}
		} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider"&&coll.name!=firedByName) {
			
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
			if (hitParentName != firedparentName && hitParentName != firedByName) {
				Debug.Log ("hit target = " + coll.name);
				
				coll.gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedByName, this.gameObject);
				//Destroy (this.gameObject);
			}//if
		}
	}
}