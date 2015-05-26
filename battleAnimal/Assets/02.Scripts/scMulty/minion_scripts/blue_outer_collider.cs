using UnityEngine;
using System.Collections;

public class blue_outer_collider : MonoBehaviour {
	public blueMinionCtrl _ctrl;

	// Use this for initialization
	void Start () {
		_ctrl = GetComponentInParent<blueMinionCtrl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
				if (coll.tag == "Player") {
						string parentName = coll.gameObject.transform.parent.name;
						if (parentName [0] == 'R') {
								_ctrl.targetObj = coll.gameObject;
								_ctrl.playerTr = coll.transform;
								_ctrl.traceKey = true;
						}
				} else if (coll.tag == "MINION") {			
						if (coll.name [0] == 'r') {
								_ctrl.targetObj = coll.gameObject;
								_ctrl.playerTr = coll.transform;
								_ctrl.traceKey = true;
						}
				} else if (coll.tag == "BUILDING") {		
						if (coll.name [0] == 'r') {
								_ctrl.targetObj = coll.gameObject;
								_ctrl.playerTr = coll.transform;
								_ctrl.traceKey = true;
						}

				} else if (coll.tag == "RED_CANNON") {
						_ctrl.targetObj = coll.gameObject;
						_ctrl.playerTr = coll.transform;
						_ctrl.traceKey = true;
				}
		}
}