using UnityEngine;
using System.Collections;

public class createBlueMinionReceiver : MonoBehaviour {
	private bool switch_;
	private string id;
	private string[] pos;
	private Vector3 spawnPos;
	private SpawnMinion _spawnMinion;
	
	// Use this for initialization
	void Start () {
		_spawnMinion = GetComponent<SpawnMinion> ();
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit());
			switch_=false;
		}	
	}
	public void receive(string data){
		string[] temp = data.Split (':');

		id = temp[0];//접속한 유저의 아이디
		pos = temp[1].Split(',');
		
		spawnPos = new Vector3(float.Parse(pos[0]),
		                       float.Parse(pos[1]),
		                       float.Parse(pos[2]));
		
		switch_ = true;
	}

	private IEnumerator doit(){
		_spawnMinion.BLUEsetSpawn(id,spawnPos);
		yield return null;
	}
}