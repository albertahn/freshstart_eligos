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
		durationTime = 5.0f;
	}

	public void setPosition(string firedby, Vector3 _pos){
		_destPos = _pos;
		_destPos.y += 2.0f;
		firedbyname = firedby;
		birth = Time.time;
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
	
	void OnTriggerEnter(Collider coll){
		if (target != null) {
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