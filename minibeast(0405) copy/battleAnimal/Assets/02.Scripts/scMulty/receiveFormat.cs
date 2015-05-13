using UnityEngine;
using System.Collections;

public class receiveFormat : MonoBehaviour {
	private bool switch_;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			
			switch_=false;
		}	
	}
	public void receive(string data){
		string[] temp = data.Split (':');
		
		while (switch_) {}
		
		switch_ = true;
	}
}
