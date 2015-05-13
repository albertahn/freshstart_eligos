using UnityEngine;
using System.Collections;

public class Minion_health_reciever_socket : MonoBehaviour {

	
	// Use this for initialization
	private bool switch_;
	private string id;
	
	private string rec_hp;
	
	private Vector3 destPos;
	
	private minion_state minion_state;
	private blue_minion_state blue_minion_state;
	
	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(switch_){
			
			GameObject a = GameObject.Find (id);

			if(a !=null){

				if(id[0]== 'r'){
					minion_state = a.GetComponent<minion_state>();
					minion_state.hp = int.Parse(rec_hp);

				}else{
					blue_minion_state = a.GetComponent<blue_minion_state>();
					blue_minion_state.hp = int.Parse(rec_hp);

				}


				switch_=false;
			}

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
