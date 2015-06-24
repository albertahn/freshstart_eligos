using UnityEngine;
using System.Collections;

public class playerHpSyncReceiver : MonoBehaviour {
	private bool switch_;
	private string name;
	private int hp;

	private PlayerHealthState _playerHealthState;

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
		_playerHealthState = GameObject.Find (name).GetComponent<PlayerHealthState> ();

		_playerHealthState.HeatedSync(hp);
		yield return null;
	}
}
