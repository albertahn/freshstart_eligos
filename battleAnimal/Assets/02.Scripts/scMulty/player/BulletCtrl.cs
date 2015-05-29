using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {
	private float speed;
	public float birth;
	private float durationTime;
	public GameObject target;
	private Transform tr;
	public string firedbyname;
	public int damage;
	private TrailRenderer _trail;

	// Use this for initialization
	void Awake () {
		tr = GetComponent<Transform> ();
		_trail = GetComponent<TrailRenderer> ();
		//target = null;
		damage = playerStat.damage;
		speed = 20.0f;
		//rigidbody.AddForce (transform.forward * speed);
		birth = Time.time;
		durationTime = 5.0f;
	}

//set target
	public void setTarget(string firedby, string _name){
		target = GameObject.Find(_name);
		firedbyname = firedby;

		//Debug.Log ("fired by: "+ firedbyname);
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 targetPosition=target.transform.position;
			targetPosition.y = 51.0f;
			if (targetPosition != tr.position) {
				float step = speed* Time.deltaTime;
				tr.position = Vector3.MoveTowards(tr.position, targetPosition, step);
			}
		}

		if ((Time.time - birth) > durationTime) {
			birth = Time.time;
			StartCoroutine (PushObjectPool ());
		}
	}

	void OnTriggerEnter(Collider coll){
		if (target != null) {
			if(target.name==coll.name){
				if(target.tag=="MINION"){
					if(target.name[0]=='r')
						target.GetComponent<minion_state>().Heated(firedbyname, gameObject,playerStat.damage);
					else
						target.GetComponent<blue_minion_state>().Heated(firedbyname, gameObject,playerStat.damage);
					StartCoroutine (PushObjectPool ());
				}else if(target.tag=="Player"){
					target.GetComponent<PlayerHealthState>().Heated(firedbyname, gameObject,playerStat.damage);
					StartCoroutine (PushObjectPool ());
				}else if(target.tag=="RED_CANNON"){
					target.GetComponent<RedCannonState>().Heated(firedbyname, gameObject,playerStat.damage);
					StartCoroutine (PushObjectPool ());
				}else if(target.tag=="BLUE_CANNON"){
					target.GetComponent<BlueCannonState>().Heated("minion", gameObject,playerStat.damage);
					StartCoroutine (PushObjectPool ());
				}
			}
		}
	}

	IEnumerator PushObjectPool()
	{
		yield return new WaitForSeconds(0.1f);
		_trail.enabled = false;
		gameObject.SetActive (false);
	}
}
