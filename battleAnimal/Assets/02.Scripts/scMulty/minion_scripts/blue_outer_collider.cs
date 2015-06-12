using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class blue_outer_collider : MonoBehaviour {
	public blueMinionCtrl _ctrl;
	public List<GameObject> targets;
	public string targetName;
	public bool isRun;
	
	// Use this for initialization
	void Start () {
		_ctrl = GetComponentInParent<blueMinionCtrl> ();
		targets = new List<GameObject>();
		isRun = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void targetDie(Transform a){
		targets.Remove (a.gameObject);
		
		if (a.name == targetName) {
			_ctrl.moveKey = true;
			changeTarget();
		}
	}
	
	public void changeTarget(){
		if(targets.Count<=0){
			isRun=false;
		}else if(targets.Count>=2){
			int i;
			for(i=0;i<targets.Count;i++){
				if(targets[i].tag !="Player")
					break;
			}
			targetName = targets[i].name;
			_ctrl.targetObj = targets[i];
			_ctrl.isAttack = true;
		}else{
			targetName = targets[targets.Count-1].name;
			_ctrl.targetObj = targets[targets.Count-1];
			_ctrl.isAttack = true;
		}	
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.name != "touchCollider") {
			if (coll.tag == "Player") {
				string parentName = coll.gameObject.transform.parent.name;
				if (parentName [0] == 'R') {
					addEnemy(coll.gameObject);							
				}
			} else if (coll.tag == "MINION") {			
				if (coll.name [0] == 'r') {
					addEnemy(coll.gameObject);	
				}
			} else if (coll.tag == "BUILDING") {		
				if (coll.name [0] == 'r') {
					addEnemy(coll.gameObject);	
				}
			} else if (coll.tag == "RED_CANNON") {
				addEnemy(coll.gameObject);	
			}
		}
	}
	
	void OnTriggerExit(Collider coll){
		if (coll.name == targetName) {
			targets.Remove (coll.gameObject);
			_ctrl.isAttack = false;
			
			changeTarget ();
		} else {
			targets.Remove (coll.gameObject);
		}
	}
	
	public void addEnemy(GameObject _zn){
		targets.Add(_zn);
		if(isRun==false){
			targetName = _zn.name;
			_ctrl.targetObj = _zn;
			_ctrl.playerTr = _zn.transform;
			_ctrl.traceKey = true;
			isRun = true;
		}
	}
	public void removeAll(){
		Debug.Log ("dlelete!@#!@#!#$!#%$!");
		for(int i=0;i<targets.Count;i++){
			targets.Remove(targets[i]);
		}
	}
}
