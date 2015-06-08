using UnityEngine;
using System.Collections;

public class respawnReceiver : MonoBehaviour {
	private bool switch_;
	private Respawn _respawn;
	private string _id;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
		_respawn = GetComponent<Respawn> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit ());
			switch_=false;
		}	
	}
	public void receive(string data){
		_id = data;
		switch_ = true;
	}
	
	private IEnumerator doit(){
		_respawn.Set (_id);
		yield return null;
	}
}