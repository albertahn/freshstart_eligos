using UnityEngine;
using System.Collections;

public class preUserMininonReceiver : MonoBehaviour {
	private bool switch_;

	string[] list;
	string sender;
	string[] pos;
	string id;
	string[] temp3;
	Vector3 spawnPos;

	private SpawnMinion _spawnMinion;

	
	// Use this for initialization
	void Start () {
		_spawnMinion = GetComponent<SpawnMinion> ();
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			//StartCoroutine(doit ());			
			switch_=false;
		}	
	}
	public void receive(string data){
		string[] temp = data.Split ('=');
		sender = temp[0];
		list = temp[1].Split('_');

		if(ClientState.id==sender){			
			switch_ = true;
		}
	}

	/*private IEnumerator doit(){
		for(int i=0;i<list.Length-2;i++)
		{
			temp3 = list[i].Split(':');
			id =temp3[0];
			pos = temp3[1].Split(',');
			spawnPos = new Vector3(float.Parse(pos[0]),
			                       float.Parse(pos[1]),
			                       float.Parse(pos[2]));
			if(id[0]=='r'){
				_spawnMinion.REDsetSpawn(id,spawnPos);
			}else if(id[0]=='b'){
				_spawnMinion.BLUEsetSpawn(id,spawnPos);
			}
		}
		yield return null;
	}*/
}
