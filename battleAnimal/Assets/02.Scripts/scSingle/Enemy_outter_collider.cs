using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_outter_collider : MonoBehaviour {
	public MoveCtrl _ctrl;
	public List<GameObject> targets;
	public List<string> targetString;
	public string targetName;
	public bool isRun;
	
	// Use this for initialization
	void Start () {
		_ctrl = GetComponentInParent<MoveCtrl> ();
		targets = new List<GameObject>();
		targetString = new List<string> ();
		
		isRun = false;

		StartCoroutine (refreshList ());
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void targetDie(Transform a){
		removeOne (a.gameObject);
	}
	
	public void changeTarget(){
		refreshList ();
		if(targets.Count<=0){
			isRun=false;
			targetName = null;
			_ctrl.targetObj = null;
		}else if(targets.Count>=2){
			int i;
			for(i=0;i<targets.Count;i++){
				if(targets[i].tag !="Player")
					break;
			}
			targetName = targets[i].name;
			_ctrl.targetObj = targets[i];
			_ctrl.isAttackE = true;
		}else{
			targetName = targets[targets.Count-1].name;
			_ctrl.targetObj = targets[targets.Count-1];
			_ctrl.isAttackE = true;
		}	
	}
	
	void OnTriggerEnter(Collider coll){
						if (coll.gameObject.name != "touchCollider") {
								if (coll.tag == "Player") {
										string parentName = coll.gameObject.transform.parent.name;
										if (parentName [0] == 'R') {
												addEnemy (coll.gameObject);							
										}
								} else if (coll.tag == "MINION") {			
										if (coll.name [0] == 'r') {
												addEnemy (coll.gameObject);	
										}
								} else if (coll.tag == "BUILDING") {		
										if (coll.name [0] == 'r') {
												addEnemy (coll.gameObject);	
										}
								} else if (coll.tag == "RED_CANNON") {
										addEnemy (coll.gameObject);	
								}
						}
	}
	
	void OnTriggerExit(Collider coll){
		removeOne (coll.gameObject);
	}
	
	public void addEnemy(GameObject _zn){
		if (targetString.BinarySearch (_zn.name) <0) {
			targets.Add (_zn);
			targetString.Add (_zn.name);
			if (isRun == false) {
				targetName = _zn.name;
				_ctrl.targetObj = _zn;
				_ctrl.traceKey = true;
				isRun = true;
			}
		}
	}
	
	public void removeOne(GameObject go){
		targets.Remove (go);
		targetString.Remove (go.name);
		//	if (go.name == targetName) {
		_ctrl.moveKey = true;
		_ctrl.targetObj = null;
		changeTarget ();
		//		}
	}
	
	public void removeAll(){
		targets.Clear ();
		targetString.Clear ();
		targetName = null;
		isRun= false;
		if(_ctrl!=null)
			_ctrl.targetObj = null;
	}
	
	public IEnumerator refreshList(){
		while (true) {
			yield return new WaitForSeconds(1.0f);
			for (int i=0; i<targets.Count; i++) {
				if (targets [i] == null){
					if(i==0)
						targetName = null;
					targets.Remove (targets [i]);
					targetString.Remove(targets[i].name);
				}
			}
		}
	}
}