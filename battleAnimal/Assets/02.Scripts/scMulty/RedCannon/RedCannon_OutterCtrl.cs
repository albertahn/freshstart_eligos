using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedCannon_OutterCtrl : MonoBehaviour {
	public RedCannonCtrl _ctrl;
	private string targetName;
	private RedCannonState _state;
	
	public List<GameObject> enemyList;
	
	public bool isRun;
	
	// Use this for initialization
	void Start () {
		isRun = false;
		_ctrl = GetComponentInParent<RedCannonCtrl> ();
		enemyList = new List<GameObject> ();
		_state = GetComponentInParent<RedCannonState> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider coll){
		if (!_state.isDie) {
			if (coll.tag == "Player") {
				string parentName = coll.gameObject.transform.parent.name;
				if (parentName [0] == 'B') {
					enemyList.Add (coll.gameObject);
					
					if (isRun == false) {
						targetName = coll.name;
						_ctrl.targetObj = coll.gameObject;
						_ctrl.isAttack = true;
						isRun = true;
					}
				}
			} else if (coll.tag == "MINION") {
				if (coll.name [0] == 'b') {
					enemyList.Add (coll.gameObject);
					
					if (isRun == false) {
						targetName = coll.name;
						_ctrl.targetObj = coll.gameObject;
						_ctrl.isAttack = true;
						isRun = true;
					}
				}
			} else if (coll.tag == "BUILDING") {		
				if (coll.name [0] == 'b') {
					enemyList.Add (coll.gameObject);
					
					if (isRun == false) {
						targetName = coll.name;
						_ctrl.targetObj = coll.gameObject;
						_ctrl.isAttack = true;
						isRun = true;
					}
				}
			}
		}
	}
	
	void OnTriggerExit(Collider coll){
		if (coll.name == targetName) {
			enemyList.Remove (coll.gameObject);
			_ctrl.isAttack = false;
			
			changeTarget ();
		} else {
			enemyList.Remove (coll.gameObject);
			
		}
	}
	public void targetDie(string a){
		enemyList.Remove (GameObject.Find(a));
		if (a == targetName) {
			_ctrl.isAttack=false;
			changeTarget();
		}
	}
	
	public void changeTarget(){
		if(enemyList.Count<=0){
			isRun=false;
			targetName = null;
		}else{
			targetName = enemyList[enemyList.Count-1].name;
			_ctrl.targetObj = enemyList[enemyList.Count-1];
			_ctrl.isAttack = true;
		}	
	}
}