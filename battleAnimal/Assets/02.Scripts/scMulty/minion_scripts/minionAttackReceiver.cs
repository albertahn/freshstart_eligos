using UnityEngine;
using System.Collections;

public class minionAttackReceiver : MonoBehaviour {
	public string from, to;
	private bool switch_;

	public void receive(string data){
		string[] temp = data.Split (':');

		while (switch_) {}

		from = temp[0];
		to = temp [1];

		switch_ = true;
	}

	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (switch_) {
			//Debug.Log ("from "+from+" to "+to);
			GameObject go = GameObject.Find(from);
			if(go!=null)
				go.GetComponent<mFireCtrl>().Fire(to);
			switch_ = false;
		}	
	}
}
