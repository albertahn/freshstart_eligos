using UnityEngine;
using System.Collections;

public class attackCannonReceiver : MonoBehaviour {
	private bool switch_;
	private string name;
	private int hp;
	private RedCannonState redTargetScript;
	private BlueCannonState blueTargetScript;
	
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
		name = temp [0];

		hp = int.Parse(temp [1]);
		
		switch_ = true;
	}
	
	private IEnumerator doit(){
		if (name [0] == 'r') {
			redTargetScript = GameObject.Find (name).GetComponent<RedCannonState> ();
			redTargetScript.HeatedSync (hp);
		} else if (name [0] == 'b') {
			blueTargetScript = GameObject.Find (name).GetComponent<BlueCannonState> ();
			blueTargetScript.HeatedSync (hp);

		}
		yield return null;
	}
}
