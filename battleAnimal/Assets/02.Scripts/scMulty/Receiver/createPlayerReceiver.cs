using UnityEngine;
using System.Collections;

public class createPlayerReceiver : MonoBehaviour {
	private bool switch_;
	string[] temp2;
	string _char;
	string team;
	string addId;
	private SpawnPlayer _spawnPlayer;
	private preUserPlayerReceiver _preUserPlayerReceiver;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
		_spawnPlayer = GetComponent<SpawnPlayer> ();
		_preUserPlayerReceiver = GetComponent<preUserPlayerReceiver> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit ());
			switch_=false;
		}
	}
	public void receive(string data){
		temp2 = data.Split(':');
		addId = temp2[0];//접속한 유저의 아이디
		_char = temp2[1];
		team = temp2[2];
		
		while (switch_) {}
		switch_ = true;
	}
	private IEnumerator doit(){
		string data = starterInfo.info;

		_spawnPlayer.setSpawn(addId,_char,team);
		_preUserPlayerReceiver.receive (data);

		int i = GameState.idx;
		GameState.name [i] = addId;
		GameState.team [i] = team;
		GameState.idx++;
		
		if(ClientState.id==addId){
			SocketStarter.Socket.Emit ("preuserREQ", addId);
		}
		yield return null;
	}//end do it
}
