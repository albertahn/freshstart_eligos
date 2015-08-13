using UnityEngine;
using System.Collections;

public class Barbas_skill1Ctrl : MonoBehaviour {
	public float birth;
	private float durationTime;
	
	public Transform tr;
	public string firedByName;
	public int damage;
	
	// Use this for initialization
	void Awake () {
		tr = GetComponent<Transform> ();
		//target = null;
		damage = playerStat.skill1_damage;
		
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
		if(ClientState.isMaster){
			if (coll.gameObject.tag == "MINION") {			
				string hitParentName = coll.transform.parent.name;
				string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
				if ((ClientState.team == "red" && coll.name [0] == 'b') ||
			    (ClientState.team == "blue" && coll.name [0] == 'r')) {
				//Debug.Log("skill first hit min");
				
				if (coll.gameObject.name [0] == 'r'){
						coll.gameObject.GetComponent<minion_state> ().Heated ("skill", gameObject, damage);
					onTriggerEmitter(coll.gameObject.name,1);
				}else if (coll.gameObject.name [0] == 'b'){
						coll.gameObject.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, damage);
					onTriggerEmitter(coll.gameObject.name,2);
					}
				//Destroy (this.gameObject);
				}
			} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider"&&coll.name!=firedByName) {
			
				string hitParentName = coll.transform.parent.name;
				string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
				if (hitParentName != firedparentName && hitParentName != firedByName) {
				Debug.Log ("hit target = " + coll.name);
				
					coll.gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedByName, this.gameObject,damage);
					onTriggerEmitter(coll.gameObject.name,3);
				//Destroy (this.gameObject);
				}//if
			}
		}
	}
	private void onTriggerEmitter(string enemy,int order){
		string data = ClientState.id + ":"+ClientState.character+":"+"first"+":"+enemy+":"+order.ToString()+":"+damage.ToString() ;
		SocketStarter.Socket.Emit ("SkillDamageREQ", data);
	}
}