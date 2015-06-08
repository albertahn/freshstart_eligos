using UnityEngine;
using System.Collections;

public class createMinionReceiver : MonoBehaviour {
	private bool switch_;
	private string redID,blueID;

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
		redID = temp [0];
		blueID = temp [1];
		
		switch_ = true;
	}
	
	private IEnumerator doit(){
		_spawnMinion.REDsetSpawn (redID);
		_spawnMinion.BLUEsetSpawn (blueID);
		yield return null;
	}
}