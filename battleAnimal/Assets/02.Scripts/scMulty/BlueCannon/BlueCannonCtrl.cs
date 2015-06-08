using UnityEngine;
using System.Collections;

public class BlueCannonCtrl : MonoBehaviour {
	private BlueCannonFire _fireCtrl;
	public bool isAttack;
	public GameObject targetObj;
	private BlueCannon_OutterCtrl _outterCtrl;
	private BlueCannonState _state;
	
	private Transform tr;
	
	
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		isAttack = false;
		_fireCtrl = GetComponent<BlueCannonFire> ();
		_outterCtrl = GetComponentInChildren<BlueCannon_OutterCtrl> ();
		_state = GetComponent<BlueCannonState> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttack&&(!_state.isDie)) {
			if (targetObj != null) {
				if(targetObj.tag=="Player"&&targetObj.GetComponent<PlayerHealthState>().isDie==true){
					_outterCtrl.targetDie(targetObj.name);
				}else if(targetObj.tag=="MINION"&&targetObj.GetComponent<minionCtrl>().isDie==true){
					_outterCtrl.targetDie(targetObj.name);
				}
				_fireCtrl.Fire (targetObj.name);
			}
		}
	}
}