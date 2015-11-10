using UnityEngine;
using System.Collections;

public class Furfur_skill3Ctrl : MonoBehaviour{
	public float birth;	
	private float durationTime;
	private int damage;
	
	public Vector3 targetPos;
	public string firedByName;
	
	// Use this for initialization
	void Start () {
		birth = Time.time;
		durationTime = 3.0f;
		damage = playerStat.skill3_damage;
	}
	
	public void setTarget(string firedby){
		firedByName = firedby;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birth) > durationTime) {
			Destroy(this.gameObject);
		}
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
			}
		} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider"&&coll.name!=firedByName) {
			
			string hitParentName = coll.transform.parent.name;
			string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
			if (hitParentName != firedparentName && hitParentName != firedByName) {
					coll.gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedByName, this.gameObject,damage);
					onTriggerEmitter(coll.gameObject.name,3);
			}//if
		} else if (coll.gameObject.tag == "FLOOR") {
		
		}
		}
	}
	
	
	private void onTriggerEmitter(string enemy,int order){
		if (ClientState.isMulty) {
						string data = ClientState.id + ":" + ClientState.character + ":" + "third" + ":" + enemy + ":" + order.ToString () + ":" + damage.ToString ();
						SocketStarter.Socket.Emit ("SkillDamageREQ", data);
				}
	}
	
}