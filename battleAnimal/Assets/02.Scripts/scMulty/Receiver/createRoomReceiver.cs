using UnityEngine;
using System.Collections;

public class createRoomReceiver : MonoBehaviour {
	private bool switch_;	
	private SpawnPlayer _spawnPlayer;
	
	// Use this for initialization
	void Start () {		
		_spawnPlayer = GetComponent<SpawnPlayer> ();
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(_spawnPlayer.CreatePlayer());			
			switch_=false;
		}	
	}
	public void receive(){
		
		switch_ = true;
	}
}
