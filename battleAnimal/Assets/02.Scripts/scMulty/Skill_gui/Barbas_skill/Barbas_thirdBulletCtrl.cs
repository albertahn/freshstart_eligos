using UnityEngine;
using System.Collections;

public class Barbas_thirdBulletCtrl : MonoBehaviour {
	private float speed;
	public float birth;
	private float durationTime;
	public GameObject target;
	private Transform tr;
	public string firedbyname;
	public int damage;
	private TrailRenderer _trail;
	private Vector3 _destPos;
	
	// Use this for initialization
	void Awake () {
		tr = GetComponent<Transform> ();
		_trail = GetComponent<TrailRenderer> ();
		//target = null;
		damage = playerStat.damage;
		speed = 30.0f;
		//rigidbody.AddForce (transform.forward * speed);
		birth = Time.time;
		durationTime = 2.0f;
	}

	public void setPosition(string firedby,Vector3 _oriPos ,Vector3 _dest){
		_destPos = _dest;
		_destPos.y += 2.0f;
		firedbyname = firedby;
		birth = Time.time;

		_destPos = calculateFarPos (_oriPos,_destPos);
		_destPos.y = 55.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (tr.position, _destPos) > 1.0f) {
			float step = speed * Time.deltaTime;
			tr.position = Vector3.MoveTowards (tr.position, _destPos, step);
		} else {
			Destroy (this.gameObject);
		}

		
		if ((Time.time - birth) > durationTime) {
			Destroy(this.gameObject);
		}
	}

	Vector3 calculateFarPos(Vector3 _near,Vector3 _far){
		float distance = 1000.0f;
		Vector3 tempPos = new Vector3(_far.x-_near.x,_far.y-_near.y,_far.z-_near.z);
		tempPos=tempPos.normalized;
		
		Vector3 ret;
		ret = new Vector3 (tempPos.x*distance,tempPos.y*distance,tempPos.z*distance);
		return ret;
	}

	void OnTriggerEnter(Collider coll){
		Debug.Log ("skill3 !!! coll.gameObject.tag = "+coll.gameObject.tag);
		if (coll.gameObject.tag == "MINION") {
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedbyname).transform.parent.name;
			
			if ((ClientState.team == "red" && coll.name [0] == 'b') ||
			    (ClientState.team == "blue" && coll.name [0] == 'r')) {
				//Debug.Log("skill first hit min");
				if (coll.gameObject.name [0] == 'r')
					coll.gameObject.GetComponent<minion_state> ().Heated ("skill", gameObject, damage);
				else if (coll.gameObject.name [0] == 'b')
					coll.gameObject.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, damage);
				//Destroy (this.gameObject);
			}
		} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider"&&coll.name!=firedbyname) {
			
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedbyname).transform.parent.name;
			
			if (hitParentName != firedparentName && hitParentName != firedbyname) {
				Debug.Log ("hit target = " + coll.name);
				
				coll.gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedbyname, this.gameObject);
				//Destroy (this.gameObject);
			}//if
		} else if (coll.gameObject.tag == "FLOOR") {
			Debug.Log ("coll.gameObject.name = "+coll.gameObject.name);
			//Destroy (this.gameObject);
		}
	}
}