using UnityEngine;
using System.Collections;

public class RedCannonCtrl : MonoBehaviour {
	private RedCannonFire _fireCtrl;
	public bool isAttack;
	public GameObject targetObj;
	private RedCannon_OutterCtrl _outterCtrl;
	
	private Transform tr;
	
	
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		isAttack = false;
		_fireCtrl = GetComponent<RedCannonFire> ();
		_outterCtrl = GetComponentInChildren<RedCannon_OutterCtrl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttack) {
			if (targetObj != null) {
				if(targetObj.tag=="Player"&&targetObj.GetComponent<PlayerHealthState>().isDie==true){
					_outterCtrl.targetDie(targetObj.name);
				}else if(targetObj.tag=="MINION"&&targetObj.GetComponent<blueMinionCtrl>().isDie==true){
					_outterCtrl.targetDie(targetObj.name);
				}
				_fireCtrl.Fire (targetObj.name);
			}
		}
	}
}
