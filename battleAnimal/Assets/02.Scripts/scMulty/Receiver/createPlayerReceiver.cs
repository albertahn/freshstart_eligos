using UnityEngine;
using System.Collections;

public class createPlayerReceiver : MonoBehaviour {
	private bool switch_;
	string[] temp2;
	string[] pos;
	Vector3 spawnPos;
	string _char;
	string team;
	string addId;
	private SpawnPlayer _spawnPlayer;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
		_spawnPlayer = GetComponent<SpawnPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit ());
			switch_=false;
		}
	}
	public void receive(string data){		
		SocketOn.debugstring = data;
		temp2 = data.Split(':');
		addId = temp2[0];//접속한 유저의 아이디
		pos = temp2[1].Split(',');
		_char = temp2[2];
		team = temp2[3];
		spawnPos = new Vector3(float.Parse(pos[0]),
		                       float.Parse(pos[1]),
		                       float.Parse(pos[2]));

		while (switch_) {}
		
		switch_ = true;
	}
	private IEnumerator doit(){
		_spawnPlayer.setSpawn(addId,spawnPos,_char,team);
		
		if(ClientState.id==addId){
			SocketStarter.Socket.Emit ("preuserREQ", addId);
		}
		yield return null;
	}
}
