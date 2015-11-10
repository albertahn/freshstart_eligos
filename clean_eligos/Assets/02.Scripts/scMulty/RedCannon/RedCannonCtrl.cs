using UnityEngine;
using System.Collections;

public class RedCannonCtrl : MonoBehaviour {
	private RedCannonFire _fireCtrl;
	public bool isAttack;
	public GameObject targetObj;
	private RedCannon_OutterCtrl _outterCtrl;
	private RedCannonState _state;
	
	private Transform tr;
	
	
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		isAttack = false;
		_fireCtrl = GetComponent<RedCannonFire> ();
		_outterCtrl = GetComponentInChildren<RedCannon_OutterCtrl> ();
		_state = GetComponent<RedCannonState> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttack&&(!_state.isDie)) {
			if (targetObj != null) {
				if(targetObj.tag=="Player"&&targetObj.GetComponent<PlayerHealthState>().isDie==true){
					_outterCtrl.targetDie(targetObj.name);
				}else if(targetObj.tag=="MINION"&&targetObj.GetComponent<blueMinionCtrl>().isDie==true){
					_outterCtrl.targetDie(targetObj.name);
				}else if(targetObj.tag=="DIE"){
					_outterCtrl.targetDie(targetObj.name);
				}else
					_fireCtrl.Fire (targetObj.name);
			}
		}
	}
}
