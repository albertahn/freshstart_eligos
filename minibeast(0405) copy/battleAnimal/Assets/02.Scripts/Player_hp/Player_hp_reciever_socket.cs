using UnityEngine;
using System.Collections;

public class Player_hp_reciever_socket : MonoBehaviour {

	// Use this for initialization
	private bool switch_;
	private string id;

	private string rec_hp;

	private Vector3 destPos;
	
	private PlayerHealthState _playerhealthstate;

	
	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(switch_){

			GameObject a = GameObject.Find (id);

					_playerhealthstate = a.GetComponent<PlayerHealthState>();

			_playerhealthstate.hp = int.Parse(rec_hp);
					
			switch_=false;
		}	
	}//end update
	public void receive(string data){

		Debug.Log ("hp recieved sync");

		string[] temp = data.Split (':');
		string[] posTemp;
		

		id = temp [0];
		rec_hp = temp[1];
		switch_ = true;
	}
}
