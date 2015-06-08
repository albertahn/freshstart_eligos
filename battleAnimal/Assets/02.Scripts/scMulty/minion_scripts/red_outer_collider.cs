using UnityEngine;
using System.Collections;

public class red_outer_collider : MonoBehaviour {
	public minionCtrl _ctrl;
	
	// Use this for initialization
	void Start () {
		_ctrl = GetComponentInParent<minionCtrl> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.name != "touchCollider") {
			if (coll.tag == "Player") {
				Debug.Log("coll.tag = "+coll.tag);
				Debug.Log("coll.name = "+coll.name);
				string parentName = coll.gameObject.transform.parent.name;
						if (parentName [0] == 'B') {
										_ctrl.targetObj = coll.gameObject;
										_ctrl.playerTr = coll.transform;
										_ctrl.traceKey = true;
								}
			} else if (coll.tag == "MINION") {
								if (coll.name [0] == 'b') {
										_ctrl.targetObj = coll.gameObject;
										_ctrl.playerTr = coll.transform;
										_ctrl.traceKey = true;
								}
			} else if (coll.tag == "BUILDING") {
								if (coll.name [0] == 'b') {
										_ctrl.targetObj = coll.gameObject;
										_ctrl.playerTr = coll.transform;
										_ctrl.traceKey = true;
								}
			} else if (coll.tag == "BLUE_CANNON") {
								_ctrl.targetObj = coll.gameObject;
								_ctrl.playerTr = coll.transform;
								_ctrl.traceKey = true;
			}
		}
	}
}