using UnityEngine;
using System.Collections;

public class moveMinionReceiver : MonoBehaviour {
	private bool switch_;
	private string id;
	private Vector3 currPos;
	private Vector3 destPos;

	private minionCtrl _ctrl;
	private blueMinionCtrl _bctrl;

	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			GameObject a = GameObject.Find (id);
			a.transform.position = currPos;

			if (a != null) {
				if(a.name[0]=='r'){
					_ctrl = a.GetComponent<minionCtrl>();
					_ctrl.dest = destPos;
					_ctrl.move ();
				}else{
					_bctrl = a.GetComponent<blueMinionCtrl>();
					_bctrl.dest = destPos;
					_bctrl.move ();
				}
			}

			switch_=false;
		}	
	}
	public void receive(string data){
		string[] temp = data.Split (':');
		string[] posTemp;
		Debug.Log ("move minon data2 = " + data);
		while (switch_) {}
		id = temp [0];
		posTemp = temp [1].Split (',');
		currPos = new Vector3(float.Parse(posTemp[0]),
		                      float.Parse(posTemp[1]),
		                      float.Parse(posTemp[2]));
		posTemp = temp [2].Split (',');
		destPos = new Vector3(float.Parse(posTemp[0]),
		                      float.Parse(posTemp[1]),
		                      float.Parse(posTemp[2]));
		switch_ = true;
	}
}
