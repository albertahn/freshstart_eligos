using UnityEngine;
using System.Collections;

public class playerAttackReceiver : MonoBehaviour {
	private bool switch_;
	private string attacker;
	private string target;
	private MoveCtrl _moveCtrl;
	private string character;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit ());
			switch_=false;
		}	
	}
	public void receive(string data){
		string[] temp = data.Split (':');
		while (switch_) {}
		attacker = temp [0];
		character = temp [1];
		target = temp [2];
		Debug.Log("attacker = "+attacker+" target = "+target);
		switch_ = true;
	}

	
	private IEnumerator doit(){
		GameObject a = GameObject.Find (attacker);
		
		if (a != null) {
			_moveCtrl = a.GetComponent<MoveCtrl>();
			_moveCtrl.attack(target);
		}	
		yield return null;
	}
}
