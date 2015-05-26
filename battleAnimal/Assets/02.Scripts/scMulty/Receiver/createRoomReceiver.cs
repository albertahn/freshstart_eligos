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
			StartCoroutine(doit ());			
			switch_=false;
		}	
	}
	public void receive(){
		
		switch_ = true;
	}

	private IEnumerator doit(){
		StartCoroutine(_spawnPlayer.CreatePlayer());	
		yield return null;
	}
}
