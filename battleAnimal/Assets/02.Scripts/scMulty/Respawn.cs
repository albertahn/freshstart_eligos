using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	private float respawnTime;
	private float birth;
	private bool _switch;
	private GameObject player;
	private PlayerHealthState _playerState;

	public CameraTouch _CameraTouch;
	public GameObject cameraman; 

	// Use this for initialization
	void Start () {
		respawnTime = 3.0f;
		birth = 0;
		_switch = false;

		cameraman = GameObject.Find ("CameraWrap");
		
		_CameraTouch = cameraman.GetComponent<CameraTouch>();

	}

	public void setPlayer(){		
		player = GameObject.Find (ClientState.id);
		_playerState = player.GetComponent<PlayerHealthState> ();
	}

	public void Set(){
		if (ClientState.level <= 2) {
				
		} else if (ClientState.level <= 4) {
			respawnTime = 4.0f;
		} else {
			respawnTime = 5.0f;
		}
		birth = Time.time;
		_switch = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (_switch && (Time.time - birth > respawnTime)) {
			_playerState.isDie = false;
			player.collider.enabled = true;
			_playerState.hp =playerStat.maxHp;
			if(ClientState.team=="red")
				player.transform.position = new Vector3( 25.0f,50.0f,25.0f);
			else
				player.transform.position = new Vector3(70.0f,50.0f,70.0f);

			_switch = false;

			_CameraTouch.focusCamPlayer = true;

			Debug.Log ("respawns");

		}
	
	}
}
