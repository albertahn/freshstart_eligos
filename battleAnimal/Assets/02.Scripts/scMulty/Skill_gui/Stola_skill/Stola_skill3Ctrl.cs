using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stola_skill3Ctrl : MonoBehaviour{
	public float birth;	
	private float durationTime;
	private string firedByName;
	public int damage;
	public List<Collider> targetList;
	
	// Use this for initialization
	void Start () {
		damage = playerStat.skill3_damage;
		birth = Time.time;
		dotTimeStart = Time.time;
		durationTime = 5.0f;
		dotTime = 1.0f;
		StartCoroutine (damageTargets());
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
	
	private float dotTime;
	private float dotTimeStart;
	
	
	void OnTriggerEnter(Collider coll){
		if (targetList.Contains (coll) == false) {
			if (coll.gameObject.tag == "MINION") {
				string hitParentName = coll.transform.parent.name;
				string firedparentName = GameObject.Find (firedByName).transform.parent.name;
				
				if ((ClientState.team == "red" && coll.name [0] == 'b') ||
				    (ClientState.team == "blue" && coll.name [0] == 'r')) {
					//Debug.Log("skill first hit min");
					if (coll.gameObject.name [0] == 'r')
						targetList.Add (coll);
					else if (coll.gameObject.name [0] == 'b') {
						Debug.Log ("hey blue minion!!");
						targetList.Add (coll);
					}
					//Destroy (this.gameObject);
				}
			} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider" && coll.name != firedByName) {
				
				string hitParentName = coll.transform.parent.name;
				string firedparentName = GameObject.Find (firedByName).transform.parent.name;
				
				if (hitParentName != firedparentName && hitParentName != firedByName) {
					targetList.Add (coll);
					//Destroy (this.gameObject);
				}//if
			}
		}
	}
	
	void OnTriggerExit(Collider coll){		
		if (targetList.Contains (coll) == true) {
			targetList.Remove(coll);
		}
	}
	
	IEnumerator damageTargets(){
		while (true) {
			yield return new WaitForSeconds(dotTime);
			for(int i=0;i<targetList.Count;i++)
			{
				if(targetList[i]!=null){
					if (targetList[i].gameObject.tag == "MINION") {
						string hitParentName = targetList[i].transform.parent.name;
						string firedparentName = GameObject.Find (firedByName).transform.parent.name;
						
						if ((ClientState.team == "red" && targetList[i].name [0] == 'b') ||
						    (ClientState.team == "blue" && targetList[i].name [0] == 'r')) {
							//Debug.Log("skill first hit min");
							if (targetList[i].gameObject.name [0] == 'r')
								targetList[i].gameObject.GetComponent<minion_state> ().Heated ("skill", gameObject, damage);
							else if (targetList[i].gameObject.name [0] == 'b'){
								Debug.Log("hey blue minion!!");
								targetList[i].gameObject.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, damage);
							}
							//Destroy (this.gameObject);
						}
					} else if (targetList[i].gameObject.tag == "Player" && targetList[i].name != "touchCollider" && targetList[i].name != firedByName) {
						
						string hitParentName = targetList[i].transform.parent.name;
						string firedparentName = GameObject.Find (firedByName).transform.parent.name;
						
						if (hitParentName != firedparentName && hitParentName != firedByName) {
							Debug.Log ("hit target = " + targetList[i].name);
							
							targetList[i].gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedByName, this.gameObject);
							//Destroy (this.gameObject);
						}//if
					}
				}
			}
		}
	}
}
