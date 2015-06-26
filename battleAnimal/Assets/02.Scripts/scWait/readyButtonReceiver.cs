using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class readyButtonReceiver : MonoBehaviour {
	private bool switch_;
	private string _id;
	private string _team;

	//private int maxPple;
	public List<string> red,blue;
	
	// Use this for initialization
	void Start () {
	//	maxPple = 1;
		red.Clear ();
		blue.Clear ();
		switch_ = false;
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
		_id = temp [0];
		_team = temp [1];

		switch_ = true;
	}

	public void readySync(string _id,string _team){
		if (_team == "red") {
			if(red.BinarySearch(_id)==-1)
				red.Add(_id);
		} else if (_team == "blue") {
			if(blue.BinarySearch(_id)==-1)
				blue.Add(_id);
		}
	}
	
	private IEnumerator doit(){
		if (_team == "red") {
			if(red.BinarySearch(_id)==-1)
				red.Add(_id);
		} else if (_team == "blue") {
			if(blue.BinarySearch(_id)==-1)
				blue.Add(_id);
		}

		int redCount = red.Count;
		int blueCount = blue.Count;

		
		if (redCount == blueCount||redCount==1||blueCount==1) {
			waitSocketStarter.Socket.Emit("startCountREQ","h");
		}

		yield return null;
	}
}