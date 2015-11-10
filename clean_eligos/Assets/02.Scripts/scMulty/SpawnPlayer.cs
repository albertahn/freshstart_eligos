using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
	public GameObject player;
	private string ClientID;//접속한 사람의 id	

	public GameObject Rteam, Bteam;
	private Respawn _respawn;
	private expBar _exp;
	private UI_skill_manager _ui_skill_manager;
	private CameraTouch _cameraTouch;
	private FollowCam _followCam;

	private skill1Plus _skill1Plus;
	private skill2Plus _skill2Plus;
	private skill3Plus _skill3Plus;
	//private UIhpbar _uihpbar;

	private Vector3 RspawnPoint, BspawnPoint;

	private minimap _minimap;

	public void CreatePlayer(){
		string data;
		if(ClientState.team=="red"){
			GameObject.Find ("CameraWrap").transform.position= new Vector3(26.0f,73.67f,4.21f);
			data = ClientState.id
				+":"+RspawnPoint.x+","+RspawnPoint.y+","+RspawnPoint.z+":"
				+ClientState.character+":"+ClientState.team;
			//접속한 유저의 아이디와 생성할 위치를 서버에 전송
		}else{
			GameObject.Find ("CameraWrap").transform.position= new Vector3(72.0f,73.67f,43.21f);
			data = ClientState.id
				+":"+BspawnPoint.x+","+BspawnPoint.y+","+BspawnPoint.z+":"
				+ClientState.character+":"+ClientState.team;
		}		
		//SocketStarter.Socket.Emit("createPlayerREQ",data);
	}

	// Use this for initialization
	void Start () {

		_respawn = GetComponent<Respawn> ();
		//_gui = GameObject.Find ("UIManager").GetComponent<DogSkill_GUI>();
		_exp = GameObject.Find ("ExpBarParent").GetComponent<expBar>();
		_ui_skill_manager = GameObject.Find ("UIManager").GetComponent<UI_skill_manager> ();
		_cameraTouch = GameObject.Find ("CameraWrap").GetComponent<CameraTouch>();
		_followCam = GameObject.Find ("CameraWrap").GetComponent<FollowCam>();

		_skill1Plus = GameObject.Find ("skill1+").GetComponent<skill1Plus> ();
		_skill2Plus = GameObject.Find ("skill2+").GetComponent<skill2Plus> ();
		_skill3Plus = GameObject.Find ("skill3+").GetComponent<skill3Plus> ();
//		_uihpbar = GameObject.Find("HpBarParent").GetComponent<UIhpbar> ();

		_minimap = GameObject.Find ("minimapWrapper").GetComponent<minimap> ();

		ClientID = ClientState.id;
		Rteam = GameObject.Find ("RedTeam");
		Bteam = GameObject.Find ("BlueTeam");

		RspawnPoint = GameObject.Find ("RedTeam/spawnPoint").transform.position;
		BspawnPoint = GameObject.Find ("BlueTeam/spawnPoint").transform.position;

		PlayerPrefs.SetString("evolved", "false");
//		StartCoroutine (CreatePlayer());
	}

	public void setSpawn(string _id,string _char,string _team){
		GameObject a;
		player = (GameObject)Resources.Load(_char);
		if(_team =="red"){
			a = (GameObject)Instantiate(player,RspawnPoint,Quaternion.identity);
			a.transform.parent = Rteam.transform;
		}else{

			a = (GameObject)Instantiate(player,BspawnPoint,Quaternion.identity);
			a.transform.parent = Bteam.transform;
		}
		a.name=_id;
		if (_id != "Enemy") {
			a.GetComponent<MoveCtrl>().DieOutterCollider();
		}

		//a.GetComponentInChildren<HP_Bar>().target = a.transform;
		if (_id == ClientState.id) {

						_respawn.setPlayer ();
						_ui_skill_manager.setPlayer ();
						//_gui.setPlayer();
						_exp.setPlayer ();			
						_skill1Plus.setPlayer ();
						_skill2Plus.setPlayer ();
						_skill3Plus.setPlayer ();
						_cameraTouch.setPlayer ();
						//_uihpbar.setPlayer ();

						_followCam.setTarget (a.transform);
						_minimap.setPlayer (a.transform);


		} else {

			_minimap.setOtherPlayer(a.transform);

		}
	}
	
	//a.transform.parent = rms.transform;
	// Update is called once per frame
	void Update () {

	}
}