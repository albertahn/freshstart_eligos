using UnityEngine;
using System.Collections;

public class minionAttackReceiver : MonoBehaviour {
	public string from, to;
	private Vector3 pos;
	private bool switch_;
	
	public void receive(string data){
		string[] posTemp;
		string[] temp = data.Split (':');
		
		while (switch_) {}
		
		from = temp[0];
		to = temp [1];
		posTemp = temp [2].Split (',');
		pos = new Vector3 (float.Parse(posTemp [0]),
		                   float.Parse(posTemp [1]),
		                   float.Parse(posTemp [2]));
		switch_ = true;
	}
	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (switch_) {
			StartCoroutine (run ());		
			switch_ = false;
		}
	}
	
	IEnumerator run(){
		//Debug.Log ("from "+from+" to "+to);
		GameObject go = GameObject.Find(from);
		if(go!=null){
			go.transform.position = pos;
			if(go.name[0]=='r'){
				minionCtrl _ctrl = go.GetComponent<minionCtrl>();
				_ctrl.targetObj =  GameObject.Find(to);
				_ctrl.attack ();
			}else if(go.name[0]=='b'){
				blueMinionCtrl _ctrl = go.GetComponent<blueMinionCtrl>();
				_ctrl.targetObj =  GameObject.Find(to);
				_ctrl.attack ();
			}
		}
		yield return null;
	}
}
