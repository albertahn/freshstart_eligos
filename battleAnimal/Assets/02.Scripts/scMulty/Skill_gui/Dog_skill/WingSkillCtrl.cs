using UnityEngine;
using System.Collections;

public class WingSkillCtrl : MonoBehaviour {
	
	public int damage;
	private float speed;
	public float birth;
	private float durationTime;
	public string firedByName;
	
	// Use this for initialization
	void Start () {
		damage = 50;
		speed = 1000.0f;
		rigidbody.AddForce (transform.forward * speed);
		birth = Time.time;
		durationTime = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birth) > durationTime)
			Destroy (this.gameObject);	
	}
	
	public void shotByname(string firedBy){
		firedByName = firedBy;	
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "MINION") {			
			
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
			if(( ClientState.team=="red" && coll.name[0]=='b')||
			   (ClientState.team=="blue" && coll.name[0]=='r')){		
				//Debug.Log("skill first hit min");
				
				if(coll.gameObject.name[0]=='r')
					coll.gameObject.GetComponent<minion_state>().Heated("skill", gameObject,damage);
				else if(coll.gameObject.name[0]=='b')
					coll.gameObject.GetComponent<blue_minion_state>().Heated("skill", gameObject,damage);
				Destroy (this.gameObject);
			}
		}else if(coll.gameObject.tag=="Player"&&coll.name!="touchCollider"){
			
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
			if( hitParentName != firedparentName&&hitParentName != firedByName){
				Debug.Log("hit target = "+coll.name);
				
				coll.gameObject.GetComponent<PlayerHealthState>().hitbySkill(firedByName, this.gameObject);
				Destroy (this.gameObject);
			}//if
			
		}//hit player		
	}//end colide	
}
