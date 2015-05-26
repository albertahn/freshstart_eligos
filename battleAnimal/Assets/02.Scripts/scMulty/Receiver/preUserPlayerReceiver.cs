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
	private SpawnPlayer _spawnPlayer;

	GameObject Rteam,Bteam;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
		_spawnPlayer = GetComponent<SpawnPlayer> ();
		Rteam = GameObject.Find ("RedTeam");
		Bteam = GameObject.Find ("BlueTeam");
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			GameObject player = (GameObject)Resources.Load(_char);
			GameObject b = (GameObject)Instantiate(player,spawnPos,Quaternion.identity);
			b.name=id;
			if(team =="red"){
				b.transform.parent = Rteam.transform;
			}else{
				b.transform.parent = Bteam.transform;
			}

			switch_=false;
		}
	}
	public void receive(string data){
		string[] temp2 = data.Split('=');
		sender = temp2[0];
		list = temp2[1].Split('_');

		if(ClientState.id==sender){
			for(int i=0;i<list.Length-2;i++)
			{
				temp3 = list[i].Split(':');
				id =temp3[0];		
				pos = temp3[1].Split(',');		
				spawnPos = new Vector3(float.Parse(pos[0]),
				                       float.Parse(pos[1]),
				                       float.Parse(pos[2]));	
				_char = temp3[2];		
				team = temp3[3];
				switch_ = true;
			}
		}
	}
}
