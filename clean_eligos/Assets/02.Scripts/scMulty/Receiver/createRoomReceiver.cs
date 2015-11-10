using UnityEngine;
using System.Collections;

public class createRoomReceiver : MonoBehaviour {
	public bool switch_;	
	public SpawnPlayer _spawnPlayer;
	private createPlayerReceiver _createPlayerReceiver;
	private Vector3 RspawnPoint, BspawnPoint;

	// Use this for initialization
	void Awake () {
		_spawnPlayer = GetComponent<SpawnPlayer> ();
		_createPlayerReceiver = GetComponent<createPlayerReceiver> ();
		RspawnPoint = GameObject.Find ("RedTeam/spawnPoint").transform.position;
		BspawnPoint = GameObject.Find ("BlueTeam/spawnPoint").transform.position;
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit ());
		}	
	}
	public void receive(){
		switch_ = true;
		while(true){
			if(switch_==false)break;
		}
	}

	private IEnumerator doit(){
		string data = starterInfo.info;
		string data2="";
		string[] temp = data.Split ('_');
		string id, _char, team;
		for (int i=0; i<temp.Length-1; i++) {
			string[] temp1 = temp [i].Split (':');
			if (temp1 [0] == ClientState.id) {
				id = temp1[0];
				_char = temp1[1];		
				team = temp1[2];
				
				data2 += id+":"+_char+":"+team;
				ClientState.character = _char;
			}
		}
		_createPlayerReceiver.receive (data2);
		switch_=false;
		yield return null;
	}
}