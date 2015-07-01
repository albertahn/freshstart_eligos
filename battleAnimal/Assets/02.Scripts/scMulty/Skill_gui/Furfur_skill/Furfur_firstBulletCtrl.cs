using UnityEngine;
using System.Collections;

public class Furfur_firstBulletCtrl : MonoBehaviour {
	private float speed;
	public float birth;
	private float durationTime;
	public Vector3 targetPos;
	public Transform tr;
	public string firedByName;
	public int damage;
	private TrailRenderer _trail;
	public GameObject damageEffectObject;
	
	// Use this for initialization
	void Awake () {
		tr = GetComponent<Transform> ();
		_trail = GetComponent<TrailRenderer> ();
		//target = null;
		damage = playerStat.skill1_damage;
		speed = 20.0f;
		rigidbody.AddForce (transform.up * 1000.0f);
		birth = Time.time;
		durationTime = 5.0f;
	}
	
	//set target
	public void setTarget(string firedby, Vector3 _pos){
		targetPos = _pos;
		firedByName = firedby;
		birth = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance( targetPos,transform.position)>1.0f) {
			float step = speed* Time.deltaTime;
			tr.position = Vector3.MoveTowards(tr.position, targetPos, step);
		}
		
		if ((Time.time - birth) > durationTime) {
			//birth = Time.time;
			Destroy(this.gameObject);
		}
	}


	void OnTriggerEnter(Collider coll){
		Debug.Log ("coll.gameObject.tag = "+coll.gameObject.tag);
		if (coll.gameObject.tag == "MINION") {
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
			if ((ClientState.team == "red" && coll.name [0] == 'b') ||
			    (ClientState.team == "blue" && coll.name [0] == 'r')) {
				//Debug.Log("skill first hit min");
				
				damageEffectFunc (this.transform.position);
				
				if (coll.gameObject.name [0] == 'r')
					coll.gameObject.GetComponent<minion_state> ().Heated ("skill", gameObject, damage);
				else if (coll.gameObject.name [0] == 'b')
					coll.gameObject.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, damage);
				Destroy (this.gameObject);
			}
		} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider"&&coll.name!=firedByName) {
			
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
			if (hitParentName != firedparentName && hitParentName != firedByName) {
				Debug.Log ("hit target = " + coll.name);
				
				coll.gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedByName, this.gameObject);
				Destroy (this.gameObject);
			}//if
		} else if (coll.gameObject.tag == "FLOOR") {
			Debug.Log ("coll.gameObject.name = "+coll.gameObject.name);
			damageEffectFunc (this.transform.position);
			Destroy (this.gameObject);
		}
	}
	
	void damageEffectFunc(Vector3 _pos){
		GameObject a = (GameObject)Instantiate(damageEffectObject);
		_pos.y += 3.0f;
		a.transform.position = _pos;
	}
}