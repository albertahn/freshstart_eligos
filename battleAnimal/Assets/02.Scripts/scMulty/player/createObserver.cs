using UnityEngine;
using System.Collections;

public class createObserver : MonoBehaviour {
	private bool switch_;
	private GameObject player;
	private GameObject a;
	private Vector3 pos;
	private CameraTouch _cameraTouch;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
		pos = new Vector3 (83.8f,132.6f,17.1f);
		_cameraTouch = GameObject.Find ("CameraWrap").GetComponent<CameraTouch>();
		_cameraTouch.transform.position = pos;
		//_cameraTouch.setPlayer ();
		//_cameraTouch.focusCamPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			SocketStarter.Socket.Emit ("preuserREQ", "ob");
			player = (GameObject)Resources.Load("Observer");
			a = (GameObject)Instantiate(player,pos,Quaternion.identity);
			a.name="ob";
			
			switch_=false;
		}	
	}
	public void receive(){		
		while (switch_) {}
		
		switch_ = true;
	}
}
