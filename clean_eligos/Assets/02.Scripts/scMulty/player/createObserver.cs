using UnityEngine;
using System.Collections;

public class createObserver : MonoBehaviour {
	public bool switch_;
	private GameObject player;
	private GameObject a;
	private Vector3 pos;
	private CameraTouch _cameraTouch;
	private preUserPlayerReceiver _preUserPlayerReceiver;
	
	// Use this for initialization
	void Awake () {
		switch_ = false;
		pos = new Vector3 (83.8f,132.6f,17.1f);
		_preUserPlayerReceiver = GetComponent<preUserPlayerReceiver> ();
		////_cameraTouch = GameObject.Find ("CameraWrap").GetComponent<CameraTouch>();
		////_cameraTouch.transform.position = pos;
		//_cameraTouch.setPlayer ();
		//_cameraTouch.focusCamPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			//StartCoroutine(doit ());
			//SocketStarter.Socket.Emit ("preuserREQ", "ob");
			player = (GameObject)Resources.Load("Observer");
			a = (GameObject)Instantiate(player,pos,Quaternion.identity);
			a.name="ob";
			string data = starterInfo.info;
			_preUserPlayerReceiver.receive (data);
			switch_=false;
		}	
	}
	
	public void receive(){
		while(true){
			switch_ = true;
			if(switch_==true)break;
		}
	}
}
