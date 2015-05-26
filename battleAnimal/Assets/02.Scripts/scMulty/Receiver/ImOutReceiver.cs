using UnityEngine;
using System.Collections;

public class ImOutReceiver : MonoBehaviour {
	private bool switch_;
	private string outID;

	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			GameObject a = GameObject.Find(outID);
			GameObject.Destroy(a);			
			switch_=false;
		}	
	}
	public void receive(string data){
		outID = data;
		switch_ = true;
	}
}