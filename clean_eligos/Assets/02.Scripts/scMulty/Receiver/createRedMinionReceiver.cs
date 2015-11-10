using UnityEngine;
using System.Collections;

public class createRedMinionReceiver : MonoBehaviour {
	private bool switch_;
	private string id;
	private string[] pos;
	private SpawnMinion _spawnMinion;
	
	// Use this for initialization
	void Start () {
		_spawnMinion = GetComponent<SpawnMinion> ();
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

		id = temp[0];//접속한 유저의 아이디
		pos = temp[1].Split(',');

		while (switch_) {}
		
		switch_ = true;
	}

	private IEnumerator doit(){
//		_spawnMinion.REDsetSpawn(id);	
		yield return null;
	}
}