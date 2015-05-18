using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//An element with the same key already exists in the dictionary에러를 피하려면 Socket.on을 모아놔야 한다.
public class SocketOn : MonoBehaviour {
	private SpawnPlayer _spawnPlayer;
	private SpawnMinion _spawnMinion;
	//private MoveCtrl _moveCtrl;
	private LobbyUI _lobbyUI;

	public string ClientID;
	private string addId;

	public Vector3 attackPos;
	private bool moveUserSwitch;

	private string outID;
	private bool outUserSwitch;
		
	private string attackID;
	private bool attackSwitch;
	private string attackTarget;
	private bool moveSyncSwitch;
	private bool loadlevelSwitch;

	private bool building_health_change;
	private string building_name;
	private int building_hp_int;


	private bool minion_health_change;
	private string minion_name;
	private int minion_hp_int;

	private string minionID;
	private Vector3 minionPos, minionTg;
	private bool minionSyncSwitch;
	// Use this for initialization

	public GameObject nmanager; // = GameObject.Find("NetworkManager");
	public Skill_socket_reciever skill_reciever;
	private minionAttackReceiver _mAttackReceiver;
	private minionDieReceiver _mDieReceiver;
	private movePlayerReceiver _movePlayerReceiver;
	private playerAttackReceiver _pAttackReceiver;
	private moveMinionReceiver _moveMinionReceiver;
	private Player_hp_reciever_socket _player_hp_reciever;
	private Minion_health_reciever_socket _minion_health_reciever;

	private CannonSync_reciever _CannonSync_reciever;
	private createPlayerReceiver _createPlayerReceiver;
	private preUsers_reciever_2 _preUsers_reciever_2;
	private preUserPlayerReceiver _preUserPlayerReceiver;
	private createObserver _createObserver;


	private bool resBool;
	private string resString;

	void Start () {
		resBool = false;
		_mAttackReceiver = GetComponent<minionAttackReceiver>();
		_mDieReceiver = GetComponent<minionDieReceiver> ();
		_movePlayerReceiver = GetComponent<movePlayerReceiver> ();
		skill_reciever = GetComponent<Skill_socket_reciever> ();
		_pAttackReceiver = GetComponent<playerAttackReceiver> ();
		_moveMinionReceiver = GetComponent<moveMinionReceiver> ();

		_createPlayerReceiver = GetComponent<createPlayerReceiver> ();

		_player_hp_reciever = GetComponent<Player_hp_reciever_socket>();

		_minion_health_reciever= GetComponent<Minion_health_reciever_socket>();
		_CannonSync_reciever =GetComponent<CannonSync_reciever>();

		_preUsers_reciever_2 = GetComponent<preUsers_reciever_2>();
		_preUserPlayerReceiver = GetComponent<preUserPlayerReceiver>();
		_createObserver = GetComponent<createObserver> ();

		Screen.SetResolution(480, 800, true);

		_spawnPlayer = GetComponent<SpawnPlayer> ();
		_spawnMinion = GetComponent<SpawnMinion> ();
		_lobbyUI = GameObject.Find("MultiManager").GetComponent<LobbyUI>();
		ClientID = ClientState.id;
		
		moveUserSwitch=false;
		outUserSwitch = false;
		attackSwitch = false;
		moveSyncSwitch = false;
		minionSyncSwitch = false;

		SocketStarter.Socket.On ("createRoomRES", (data) =>{
			string temp = data.Json.args[0].ToString();

			if(temp== ClientID){
				Debug.Log("created: "+ temp);
				if(temp!="ob")
					loadlevelSwitch = true;
				else{
					_createObserver.receive();
					/*SocketStarter.Socket.Emit ("preuserREQ", addId);
					player = (GameObject)Resources.Load("Observer");
					a = (GameObject)Instantiate(player,pos,Quaternion.identity);
					a.name=_id;*/
				}
			}
		});

		SocketStarter.Socket.On ("youMaster", (data) =>{
			Debug.Log("i'am Master");//edit
			ClientState.isMaster = true;

		});
//cannon die
		SocketStarter.Socket.On ("cannonDie", (data) =>{

			Debug.Log("candie: "+data.Json.args[0].ToString());//edit
			_CannonSync_reciever.killCannon(data.Json.args[0].ToString());
		});
	
		SocketStarter.Socket.On("createPlayerRES",(data) =>
		{//접속한 플레이어가 있을때 호출된다.
			_createPlayerReceiver.receive(data.Json.args[0].ToString());
		});

		SocketStarter.Socket.On("createRedMinionRES",(data) =>
		{
			string temp = data.Json.args[0].ToString();
			string[] pos;
			Vector3 spawnPos;
			string id;
			
			pos = temp.Split(':');
			id = pos[0];//접속한 유저의 아이디
			pos = pos[1].Split(',');
			
			spawnPos = new Vector3(float.Parse(pos[0]),
			                       float.Parse(pos[1]),
			                       float.Parse(pos[2]));

			while(_spawnMinion.REDspawnSwitch==true){
			}
			_spawnMinion.REDsetSpawn(id,spawnPos);//해당 user를 instantiate한다.
		});

		SocketStarter.Socket.On("createBlueMinionRES",(data) =>
		{
			string temp = data.Json.args[0].ToString();
			string[] pos;
			Vector3 spawnPos;
			string id;
			
			pos = temp.Split(':');
			id = pos[0];//접속한 유저의 아이디
			pos = pos[1].Split(',');
			
			spawnPos = new Vector3(float.Parse(pos[0]),
			                       float.Parse(pos[1]),
			                       float.Parse(pos[2]));
			
			while(_spawnMinion.BLUEspawnSwitch==true){
			}
			_spawnMinion.BLUEsetSpawn(id,spawnPos);//해당 user를 instantiate한다.
		});

		SocketStarter.Socket.On ("preuser1RES", (data) => {			
			string temp = data.Json.args[0].ToString();
			string[] list;
			string sender;
			string[] pos;
			string id;
			string[] temp3;
			Vector3 spawnPos;
			
			string[] temp2 = temp.Split('=');
			sender = temp2[0];
			list = temp2[1].Split('_');
			if(ClientID==sender){
				for(int i=0;i<list.Length-2;i++)
				{
					temp3 = list[i].Split(':');
					id =temp3[0];
					pos = temp3[1].Split(',');
					spawnPos = new Vector3(float.Parse(pos[0]),
					                       float.Parse(pos[1]),
					                       float.Parse(pos[2]));
					if(id[0]=='r'){
						while(_spawnMinion.REDspawnSwitch==true){
						}
						_spawnMinion.REDsetSpawn(id,spawnPos);
					}else if(id[0]=='b'){
						while(_spawnMinion.BLUEspawnSwitch==true){
						}
						_spawnMinion.BLUEsetSpawn(id,spawnPos);
					}
				}
			}
		});

		SocketStarter.Socket.On ("preuser2RES", (data) => {	

			//_preUsers_reciever_2.preReciever(data.Json.args[0].ToString());
			_preUserPlayerReceiver.receive(data.Json.args[0].ToString());


		});

		SocketStarter.Socket.On ("movePlayerRES", (data) =>
		{
			string temp = data.Json.args[0].ToString();
			string[] temp2 = temp.Split(':');
			if(ClientID !=temp2[0]){
				_movePlayerReceiver.receive(temp);
			}
		});

		SocketStarter.Socket.On ("moveMinionRES", (data) =>
		{
			string temp = data.Json.args[0].ToString();
			string[] temp2 = temp.Split(':');
			if(ClientID !=temp2[0]){
				_moveMinionReceiver.receive(temp);
			}
		});

		SocketStarter.Socket.On ("minionAttackRES", (data) =>
		{
			if(ClientState.isMaster==false){
				_mAttackReceiver.receive(data.Json.args[0].ToString());
			}
		});

		SocketStarter.Socket.On ("attackRES", (data) =>
		{
			string temp = data.Json.args[0].ToString();
			string[] temp2 = temp.Split(':');
			if(ClientID !=temp2[0]){
				_pAttackReceiver.receive(temp);
			}
		});

		SocketStarter.Socket.On ("imoutRES", (data) =>{			
			string temp = data.Json.args[0].ToString();
			outID = temp;

			outUserSwitch=true;
		});


		SocketStarter.Socket.On ("attackMinion", (data) =>{
			
			_minion_health_reciever.receive(data.Json.args[0].ToString());
			
			
		});

		//building attack
		SocketStarter.Socket.On ("attackBuilding", (data) =>{
			
			string[] temp = data.Json.args[0].ToString().Split(':');
			building_name = temp[0];
			building_hp_int = int.Parse(temp[1]);
			
			
			//Debug.Log("attack: " + building_name+":"+building_hp_int);
			
			building_health_change= true;
		});

		SocketStarter.Socket.On ("minionDieRES", (data) =>{
			string[] temp = data.Json.args[0].ToString().Split(':');
			if(temp[0]!=ClientID){
				_mDieReceiver.receive(temp[1]);
			}
		});

//changed player health sync

		SocketStarter.Socket.On ("HealthSync", (data) =>{
			string[] temp = data.Json.args[0].ToString().Split(':');
			//if(temp[0]!=ClientID){

				_player_hp_reciever.receive(data.Json.args[0].ToString());
					
			//}
		});
		

//skills sync

	
		//skill attack
		SocketStarter.Socket.On ("SkillAttack", (data) =>{
			skill_reciever.skillShot(data.Json.args[0].ToString());


		});

//master or not? 
		if (ClientState.isMaster) {

			SocketStarter.Socket.Emit ("createRoomREQ", ClientID+":"+ClientState.room+":master");

				} else {
			SocketStarter.Socket.Emit ("createRoomREQ", ClientID+":"+ClientState.room+":notmaster");

				}

			

	}//end start
	
	// Update is called once per frame
	void Update () {

		if (outUserSwitch) {
			GameObject a = GameObject.Find(outID);
			GameObject.Destroy(a);
			outUserSwitch=false;
		}

		if (loadlevelSwitch) {
			_lobbyUI.isUI =false;
			StartCoroutine(_spawnPlayer.CreatePlayer());
			loadlevelSwitch=false;
		}

		if (minionSyncSwitch) {
			GameObject a = GameObject.Find (minionID);
			if(a!=null){
				if(a.name[0]=='r')
					a.GetComponent<minionCtrl>().setSync(minionPos,minionTg);
				else
					a.GetComponent<blueMinionCtrl>().setSync(minionPos,minionTg);
			}
			minionSyncSwitch=false;
		}
	}

//building health change
	void change_building_health(){
		GameObject buildingnow = GameObject.Find (building_name);
		buildingnow.GetComponent<MainFortress>().hp = building_hp_int;
		
	}
	//change minion health
	void change_minion_health(){
		GameObject mininow = GameObject.Find (""+minion_name);
		mininow.GetComponent<minion_state>().hp = minion_hp_int;		
	}
}