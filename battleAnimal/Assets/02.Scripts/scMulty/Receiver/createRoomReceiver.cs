using UnityEngine;
using System.Collections;

public class createRoomReceiver : MonoBehaviour {
	public bool switch_;	
	public SpawnPlayer _spawnPlayer;
	
	// Use this for initialization
	void Awake () {		
		_spawnPlayer = GetComponent<SpawnPlayer> ();
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
	}

	private IEnumerator doit(){
		_spawnPlayer.CreatePlayer();				
		switch_=false;
		yield return null;
	}
}