using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlueCannon_OutterCtrl : MonoBehaviour {
	public BlueCannonCtrl _ctrl;
	private string targetName;
	
	public List<GameObject> enemyList;
	
	private bool isRun;
	
	// Use this for initialization
	void Start () {
		isRun = false;
		_ctrl = GetComponentInParent<BlueCannonCtrl> ();
		enemyList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Player") {
			string parentName = coll.gameObject.transform.parent.name;
			if (parentName [0] == 'R') {
				enemyList.Add (coll.gameObject);
				
				if(isRun==false){
					targetName = coll.name;
					_ctrl.targetObj = coll.gameObject;
					_ctrl.isAttack = true;
					isRun=true;
				}
			}
		} else if (coll.tag == "MINION") {
			if (coll.name [0] == 'r') {
				enemyList.Add (coll.gameObject);
				Debug.Log ("list num = "+enemyList.Count);
				
				if(isRun==false){
					targetName = coll.name;
					_ctrl.targetObj = coll.gameObject;
					_ctrl.isAttack = true;
					isRun=true;
				}
			}
		} else if (coll.tag == "BUILDING") {		
			if (coll.name [0] == 'r') {
				enemyList.Add (coll.gameObject);
				
				if(isRun==false){
					targetName = coll.name;
					_ctrl.targetObj = coll.gameObject;
					_ctrl.isAttack = true;
					isRun=true;
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
		Debug.Log ("list num = "+enemyList.Count);
		if(enemyList.Count<=0){
			isRun=false;
		}else{
			targetName = enemyList[enemyList.Count-1].name;
			_ctrl.targetObj = enemyList[enemyList.Count-1];
			_ctrl.isAttack = true;
		}	
	}
}