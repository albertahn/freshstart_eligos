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

	private skill1Plus _skill1Plus;
	private skill2Plus _skill2Plus;
	private skill3Plus _skill3Plus;
	private UIhpbar _uihpbar;

	public IEnumerator CreatePlayer(){
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

	// Use this for initialization
	void Start () {
		_respawn = GetComponent<Respawn> ();
		//_gui = GameObject.Find ("UIManager").GetComponent<DogSkill_GUI>();
		_exp = GameObject.Find ("ExpBarParent").GetComponent<expBar>();
		_ui_skill_manager = GameObject.Find ("UIManager").GetComponent<UI_skill_manager> ();
		_cameraTouch = GameObject.Find ("CameraWrap").GetComponent<CameraTouch>();

		_skill1Plus = GameObject.Find ("skill1+").GetComponent<skill1Plus> ();
		_skill2Plus = GameObject.Find ("skill2+").GetComponent<skill2Plus> ();
		_skill3Plus = GameObject.Find ("skill3+").GetComponent<skill3Plus> ();
		_uihpbar = GameObject.Find("HpBarParent").GetComponent<UIhpbar> ();

		ClientID = ClientState.id;
		Rteam = GameObject.Find ("RedTeam");
		Bteam = GameObject.Find ("BlueTeam");

		PlayerPrefs.SetString("evolved", "false");
		//StartCoroutine (CreatePlayer());
	}

	public void setSpawn(string _id,Vector3 pos,string _char,string _team){
		GameObject a;
		player = (GameObject)Resources.Load(_char);
		a = (GameObject)Instantiate(player,pos,Quaternion.identity);
		a.name=_id;
		if(_team =="red"){
			a.transform.parent = Rteam.transform;
		}else{
			a.transform.parent = Bteam.transform;
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
						_uihpbar.setPlayer ();
				}
	}
	
	//a.transform.parent = rms.transform;
	// Update is called once per frame
	void Update () {

	}
}