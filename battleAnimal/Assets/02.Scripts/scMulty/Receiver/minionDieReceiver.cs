using UnityEngine;
using System.Collections;

public class minionDieReceiver : MonoBehaviour {	
	private bool switch_;
	private string name;
	
	public void receive(string data){
		while (switch_) {}
		name = data;
		switch_ = true;
	}
	// Use this for initialization
	void Start () {		
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (switch_) {
			StartCoroutine(doit ());
			switch_ = false;
		}
	}
	
	private IEnumerator doit(){
		if (GameObject.Find (name) != null) {
			if (name [0] == 'r')
				GameObject.Find (name).GetComponent<minion_state> ().minionDie ();
			else
				GameObject.Find (name).GetComponent<blue_minion_state> ().minionDie ();
		}
		yield return null;
	}
}
