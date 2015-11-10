using UnityEngine;
using System.Collections;

public class preUserPlayerReceiver : MonoBehaviour {
	private bool switch_;
	
	string[] list;
	string sender;
	string[] pos;
	string id;
	string[] temp3;
	Vector3 spawnPos;
	string _char;
	string team;
	private Vector3 RspawnPoint, BspawnPoint;
	
	GameObject Rteam,Bteam;
	private minimap _minimap;
	
	// Use this for initialization
	void Start () {
		_minimap = GameObject.Find ("minimapWrapper").GetComponent<minimap> ();

		switch_ = false;
		Rteam = GameObject.Find ("RedTeam");
		Bteam = GameObject.Find ("BlueTeam");

		RspawnPoint = GameObject.Find ("RedTeam/spawnPoint").transform.position;
		BspawnPoint = GameObject.Find ("BlueTeam/spawnPoint").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit ());
			switch_=false;
		}
	}
	public void receive(string data){
		string[] temp = data.Split ('_');
	Debug.Log("preUserPlayerReceiver = "+data);
		for (int i=0; i<temp.Length-1; i++) {
			string[] temp1 = temp [i].Split (':');
			if (temp1 [0] != ClientState.id) {
				id = temp1[0];
				_char = temp1[1];		
				team = temp1[2];

				switch_ = true;
			}
		}
	}
	
	
	private IEnumerator doit(){
		GameObject player = (GameObject)Resources.Load(_char);
		if(team =="red"){
			spawnPos = RspawnPoint;
		}else{
			spawnPos = BspawnPoint;
		}
		GameObject b = (GameObject)Instantiate(player,spawnPos,Quaternion.identity);
		b.name=id;
		
		int i = GameState.idx;
		GameState.name [i] = id;
		GameState.team [i] = team;
		GameState.idx++;

		_minimap.setOtherPlayer (b.transform);

		if(team =="red"){
			b.transform.parent = Rteam.transform;
		}else{
			b.transform.parent = Bteam.transform;
		}
		yield return null;
	}
}
