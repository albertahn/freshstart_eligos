using UnityEngine;
using System.Collections;

public class createMinionReceiver : MonoBehaviour {
	private bool switch_;
	private string redID,blueID;
	private int redLine,blueLine;
	
	private SpawnMinion _spawnMinion;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
		_spawnMinion = GetComponent<SpawnMinion> ();
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
		string[] temp2;
		temp2 = temp [0].Split('_');
		redID = temp2 [0];
		redLine = int.Parse(temp2 [1]);
		
		temp2 = temp [1].Split ('_');
		blueID = temp2 [0];
		blueLine = int.Parse(temp2 [1]);
		switch_ = true;
	}
	
	private IEnumerator doit(){
		_spawnMinion.REDsetSpawn (redID,redLine);
		_spawnMinion.BLUEsetSpawn (blueID,blueLine);
		yield return null;
	}
}