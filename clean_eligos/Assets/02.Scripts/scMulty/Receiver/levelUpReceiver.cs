using UnityEngine;
using System.Collections;

public class levelUpReceiver : MonoBehaviour {
	private bool switch_;
	private string id;
	private int hp;
	private Level_up_evolve _levelUpEvolve;
	
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
		id = temp [0];
		hp = int.Parse (temp[1]);
		switch_ = true;
	}
	
	private IEnumerator doit(){
		_levelUpEvolve = GameObject.Find (id).GetComponent<Level_up_evolve>();

		_levelUpEvolve.processSync (hp);
		yield return null;
	}
}