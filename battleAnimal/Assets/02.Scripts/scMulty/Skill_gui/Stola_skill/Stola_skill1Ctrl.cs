using UnityEngine;
using System.Collections;

public class Stola_skill1Ctrl : MonoBehaviour {
	private float speed;
	public float birth;
	private float durationTime;
	public GameObject target;
	private Transform tr;
	public string firedbyname;
	public int damage;
	private TrailRenderer _trail;
	private Vector3 _oriPos;
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
		durationTime = 0.5f;
	}
	
	public void setPosition(string firedby,Vector3 _ori ,Vector3 _dest){
		_oriPos = _ori;
		_destPos = _dest;
		firedbyname = firedby;
		birth = Time.time;		
		_destPos = calculateFarPos (_oriPos,_destPos);
		_destPos.y = 55.0f;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Vector3.Distance (tr.position, _destPos) > 1.0f) {
			float step = speed * Time.deltaTime;
			tr.position = Vector3.MoveTowards (tr.position, _destPos, step);
		} else {
			Destroy (this.gameObject);
		}*/
		float step = speed * Time.deltaTime;
		tr.position = Vector3.MoveTowards (tr.position, _destPos, step);
		
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
		if (target != null) {
			Debug.Log("target.tag = "+target.tag);
			if(target.name==coll.name){
				if(target.tag=="MINION"){
					if(target.name[0]=='r')
						target.GetComponent<minion_state>().Heated(firedbyname, gameObject,playerStat.damage);
					else
						target.GetComponent<blue_minion_state>().Heated(firedbyname, gameObject,playerStat.damage);
					Destroy(this.gameObject);
				}else if(target.tag=="Player"){
					target.GetComponent<PlayerHealthState>().Heated(firedbyname, gameObject,playerStat.damage);
					Destroy(this.gameObject);
				}else if(target.tag=="RED_CANNON"){
					target.GetComponent<RedCannonState>().Heated(firedbyname, gameObject,playerStat.damage);
					Destroy(this.gameObject);
				}else if(target.tag=="BLUE_CANNON"){
					target.GetComponent<BlueCannonState>().Heated("minion", gameObject,playerStat.damage);
					Destroy(this.gameObject);
				}else if(target.tag=="BLUE_CANNON"){
					target.GetComponent<BlueCannonState>().Heated("minion", gameObject,playerStat.damage);
					Destroy(this.gameObject);
				}else if(target.tag=="BUILDING"){
					target.GetComponent<MainFortress>().Heated(firedbyname, gameObject,playerStat.damage);
					Destroy(this.gameObject);
				}
				Destroy(this.gameObject);
			}
		}
	}
}