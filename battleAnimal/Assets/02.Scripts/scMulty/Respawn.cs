using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	private float respawnTime;
	private float birth;
	private bool _switch;
	private GameObject player;
	private PlayerHealthState _playerState;
	private Vector3 RspawnPos;
	private Vector3 BspawnPos;
	
	private string team;
	private int level;
	
	
	public CameraTouch _CameraTouch;
	public GameObject cameraman; 
	
	// Use this for initialization
	void Start () {
		respawnTime = 3.0f;
		birth = 0;
		_switch = false;
		
		cameraman = GameObject.Find ("CameraWrap");
		
		_CameraTouch = cameraman.GetComponent<CameraTouch>();
		RspawnPos = GameObject.Find ("RedTeam/spawnPoint").transform.position;
		BspawnPos = GameObject.Find ("BlueTeam/spawnPoint").transform.position;
	}
	
	public void setPlayer(){
		player = GameObject.Find (ClientState.id);
		_playerState = player.GetComponent<PlayerHealthState> ();
	}
	
	public void Set(string _id){
		if (ClientState.isMulty) {
			int i = GameState.search_by_name (_id);		
			team = GameState.team [i];
			player = GameObject.Find (_id);
			level = GameState.level [i];
		} else {
			team = ClientState.team;
			player = GameObject.Find (_id);
			level = ClientState.level;
		}
		
		_playerState = player.GetComponent<PlayerHealthState> ();
		
		if (level <= 2) {
			
		} else if (level <= 4) {
			respawnTime = 4.0f;
		} else {
			respawnTime = 5.0f;
		}
		
		birth = Time.time;
		_switch = true;
	}

	public void SetEnemy(){
		team = "blue";
		player = GameObject.Find ("Enemy");

		respawnTime = 4.0f;

		_playerState = player.GetComponent<PlayerHealthState> ();
		
		birth = Time.time;
		_switch = true;
	}

	// Update is called once per frame
	void Update () {
		if (_switch && (Time.time - birth > respawnTime)) {
			_playerState.isDie = false;
			player.collider.enabled = true;
			player.tag = "Player";
			player.transform.FindChild ("touchCollider").tag = "Player";
			_playerState.hp =_playerState.maxhp;
			if(team=="red")
				player.transform.position = RspawnPos;
			else
				player.transform.position = BspawnPos;
			_CameraTouch.focusCamPlayer = true;

			if((!ClientState.isMulty)&&player.name=="Enemy"){
				MoveCtrl moveCtrl = player.GetComponent<MoveCtrl>();
				moveCtrl.initiatePoints();
				moveCtrl.targetObj=null;
				moveCtrl.moveKey=true;
				StartCoroutine (moveCtrl.CheckMonsterState ());
			}
			_switch = false;
		}
	}
}