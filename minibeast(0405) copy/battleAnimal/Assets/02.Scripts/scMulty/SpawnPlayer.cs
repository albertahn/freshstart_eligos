using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
	public GameObject player;
	public bool spawnSwitch;//socket.on에서 instantiate를 하면 에러가 나서 업데이트 문에서 쓰기위한 꼼수
	Vector3 spawnPos;
	private string ClientID;//접속한 사람의 id	
	private string team;

	public GameObject Rteam, Bteam;
	private Respawn _respawn;
	private expBar _exp;
	private UI_skill_manager _ui_skill_manager;

	private skill1Plus _skill1Plus;
	private skill2Plus _skill2Plus;
	private skill3Plus _skill3Plus;

	public IEnumerator CreatePlayer(){
		spawnSwitch = false;
		string data;
		if(ClientState.team=="red"){
			GameObject.Find ("CameraWrap").transform.position= new Vector3(26.0f,73.67f,4.21f);
			data = ClientState.id+":25.0,50,25:"+ClientState.character+":"+ClientState.team;
			//접속한 유저의 아이디와 생성할 위치를 서버에 전송
		}else{
			GameObject.Find ("CameraWrap").transform.position= new Vector3(72.0f,73.67f,43.21f);
			data = ClientState.id+":70.0,50,70:"+ClientState.character+":"+ClientState.team;
		}
		SocketStarter.Socket.Emit("createPlayerREQ",data);
		yield return null;
	}
	
	private string id; //접속한 유저의 아이디
	public string character;

	// Use this for initialization
	void Start () {
		_respawn = GetComponent<Respawn> ();
		//_gui = GameObject.Find ("UIManager").GetComponent<DogSkill_GUI>();
		_exp = GameObject.Find ("ExpBarParent").GetComponent<expBar>();
		_ui_skill_manager = GameObject.Find ("UIManager").GetComponent<UI_skill_manager> ();

		_skill1Plus = GameObject.Find ("skill1+").GetComponent<skill1Plus> ();
		_skill2Plus = GameObject.Find ("skill2+").GetComponent<skill2Plus> ();
		_skill3Plus = GameObject.Find ("skill3+").GetComponent<skill3Plus> ();

		ClientID = ClientState.id;
		Rteam = GameObject.Find ("RedTeam");
		Bteam = GameObject.Find ("BlueTeam");

		PlayerPrefs.SetString("evolved", "false");
		//StartCoroutine (CreatePlayer());
		spawnSwitch = false;
	}

	public void setSpawn(string _id,Vector3 pos,string _char,string _team){
		id = _id;
		spawnPos = pos;
		character = _char;
		team = _team;

		spawnSwitch=true;
	}
	
	//a.transform.parent = rms.transform;
	// Update is called once per frame
	void Update () {
		if (spawnSwitch) {
			GameObject a;
			player = (GameObject)Resources.Load(character);
			a = (GameObject)Instantiate(player,spawnPos,Quaternion.identity);
			a.name=id;
			if(team =="red"){
				a.transform.parent = Rteam.transform;
			}else{
				a.transform.parent = Bteam.transform;
			}
			//a.GetComponentInChildren<HP_Bar>().target = a.transform;
			_respawn.setPlayer();
			_ui_skill_manager.setPlayer();
			//_gui.setPlayer();
			_exp.setPlayer();			
			_skill1Plus.setPlayer();
			_skill2Plus.setPlayer();
			_skill3Plus.setPlayer();

			spawnSwitch=false;
		}
	}
}