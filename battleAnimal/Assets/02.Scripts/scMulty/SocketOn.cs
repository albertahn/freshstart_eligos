using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//An element with the same key already exists in the dictionary에러를 피하려면 Socket.on을 모아놔야 한다.
public class SocketOn : MonoBehaviour {
	private SpawnMinion _spawnMinion;
	//private MoveCtrl _moveCtrl;
	private LobbyUI _lobbyUI;
	
	public string ClientID;
	private string addId;
	public Vector3 attackPos;
	
	private string attackID;
	private bool attackSwitch;
	private string attackTarget;
	private bool moveSyncSwitch;
	
	private bool building_health_change;
	private string building_name;
	private int building_hp_int;
	
	private bool minion_health_change;
	private string minion_name;
	private int minion_hp_int;
	
	private string minionID;
	private Vector3 minionPos, minionTg;
	// Use this for initialization
	
	public GameObject nmanager; // = GameObject.Find("NetworkManager");
	private minionAttackReceiver _mAttackReceiver;
	private minionDieReceiver _mDieReceiver;
	private movePlayerReceiver _movePlayerReceiver;
	private playerAttackReceiver _pAttackReceiver;
	private moveMinionReceiver _moveMinionReceiver;
	private Player_hp_reciever_socket _player_hp_reciever;
	private Minion_health_reciever_socket _minion_health_reciever;
	private ImOutReceiver _im_out_receiver;
	private CannonSync_reciever _CannonSync_reciever;
	private createPlayerReceiver _createPlayerReceiver;
	private preUsers_reciever_2 _preUsers_reciever_2;
	private preUserPlayerReceiver _preUserPlayerReceiver;
	private createRoomReceiver _createRoomReceiver;
	private createRedMinionReceiver _createRedMinionReceiver;
	private createBlueMinionReceiver _createBlueMinionReceiver;
	private preUserMininonReceiver _preUserMinionReceiver;
	private attackBuildingReceiver _attackBuildingReceiver;
	private attackCannonReceiver _attackCannonReceiver;
	private createObserver _createObserver;
	private createMinionReceiver _createMinionReceiver;
	private respawnReceiver _respawnReceiver;
	private playerHpSyncReceiver _playerHpSyncReceiver;
	private Stats_sync_reciever _statSync_reciever;
	private skillAttackReceiver _skillAttackReceiver;

	
	void Start () {

		//Screen.SetResolution( 800,480, true);

		Screen.SetResolution( 1024,600, true);

		_mAttackReceiver = GetComponent<minionAttackReceiver>();
		_mDieReceiver = GetComponent<minionDieReceiver> ();
		_movePlayerReceiver = GetComponent<movePlayerReceiver> ();
		_pAttackReceiver = GetComponent<playerAttackReceiver> ();
		_moveMinionReceiver = GetComponent<moveMinionReceiver> ();
		_createPlayerReceiver = GetComponent<createPlayerReceiver> ();
		_player_hp_reciever = GetComponent<Player_hp_reciever_socket>();
		_minion_health_reciever= GetComponent<Minion_health_reciever_socket>();
		_CannonSync_reciever =GetComponent<CannonSync_reciever>();
		_preUsers_reciever_2 = GetComponent<preUsers_reciever_2>();
		_preUserPlayerReceiver = GetComponent<preUserPlayerReceiver>();
		_im_out_receiver = GetComponent<ImOutReceiver> ();
		_createRoomReceiver = GetComponent<createRoomReceiver> ();
		_createRedMinionReceiver = GetComponent<createRedMinionReceiver> ();
		_createBlueMinionReceiver = GetComponent<createBlueMinionReceiver> ();
		_preUserMinionReceiver = GetComponent<preUserMininonReceiver> ();
		_attackBuildingReceiver = GetComponent<attackBuildingReceiver> ();
		_createMinionReceiver = GetComponent<createMinionReceiver> ();
		_respawnReceiver = GetComponent<respawnReceiver> ();		
		_createObserver = GetComponent<createObserver> ();
		_attackCannonReceiver = GetComponent<attackCannonReceiver> ();
		_playerHpSyncReceiver = GetComponent<playerHpSyncReceiver> ();
		

		
		_spawnMinion = GetComponent<SpawnMinion> ();
		_lobbyUI = GameObject.Find("MultiManager").GetComponent<LobbyUI>();
		ClientID = ClientState.id;
		
		attackSwitch = false;
		moveSyncSwitch = false;
		
		SocketStarter.Socket.On("createRoomRES", (data) =>{
			string temp = data.Json.args[0].ToString();
			
			Debug.Log("cretate RoomRes = "+temp);
			
			if(temp== ClientID){
				if(temp!="ob"){
					_createRoomReceiver.receive();
				}
				else{
					_createObserver.receive();
				}
			}
		});
		
		SocketStarter.Socket.On ("youMaster", (data) =>{
			ClientState.isMaster = true;
		});
		
		//cannon die
		SocketStarter.Socket.On ("cannonDie", (data) => {
			if(!ClientState.isMaster)
				_CannonSync_reciever.killCannon (data.Json.args [0].ToString ());
		});
		
		SocketStarter.Socket.On("createPlayerRES",(data) =>
		                        {//접속한 플레이어가 있을때 호출된다.
			_createPlayerReceiver.receive(data.Json.args[0].ToString());
		});
		
		SocketStarter.Socket.On("respawnRES",(data) =>
		                        {//접속한 플레이어가 있을때 호출된다.
			//if(!ClientState.isMaster)
				_respawnReceiver.receive(data.Json.args[0].ToString());
		});
		
		SocketStarter.Socket.On ("preuserRES", (data) => {
			_preUserPlayerReceiver.receive(data.Json.args[0].ToString());
		});

//statSyncReq
		SocketStarter.Socket.On("statSyncRes", (data) => {

			_statSync_reciever.recieve(data.Json.args[0].ToString());
		});
		
		SocketStarter.Socket.On ("movePlayerRES", (data) =>
		                         {
			string temp = data.Json.args[0].ToString();
			string[] temp2 = temp.Split(':');
			if(ClientID !=temp2[0]){
				_movePlayerReceiver.receive(temp);
			}
		});
		
		SocketStarter.Socket.On("createMinionRES",(data) =>
		                        {
			if(!ClientState.isMaster){
				string temp = data.Json.args[0].ToString();
				_createMinionReceiver.receive(temp);
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
			_im_out_receiver.receive(temp);
		});
		
		SocketStarter.Socket.On("attackMinion", (data) =>{
			if(!ClientState.isMaster)
				_minion_health_reciever.receive(data.Json.args[0].ToString());
		});
		
		//building attack
		SocketStarter.Socket.On ("attackBuilding", (data) =>{
			if(!ClientState.isMaster){
				_attackBuildingReceiver.receive(data.Json.args[0].ToString());
			}
		});
		
		SocketStarter.Socket.On ("attackCannonRES", (data) =>{
			if(!ClientState.isMaster){
				_attackCannonReceiver.receive(data.Json.args[0].ToString());
			}
		});
		
		SocketStarter.Socket.On ("minionDieRES", (data) =>{
			string[] temp = data.Json.args[0].ToString().Split(':');
			if(!ClientState.isMaster){
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
			string[] temp = data.Json.args[0].ToString().Split(':');
			if(temp[0]!=ClientState.id)
				_skillAttackReceiver.receive (data.Json.args[0].ToString());
		});
		
		
		SocketStarter.Socket.On ("playerHpSyncRES", (data) =>{
			if(!ClientState.isMaster){
				_playerHpSyncReceiver.receive(data.Json.args[0].ToString());
			}
		});
		
		//master or not? 
		if (ClientState.isMaster) {
			SocketStarter.Socket.Emit("createRoomREQ", ClientState.id+":"+ClientState.room+":master");
		} else {
			SocketStarter.Socket.Emit("createRoomREQ", ClientState.id+":"+ClientState.room+":notmaster");
		}
	}//end start
	
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