using UnityEngine;
using System.Collections;

public class preUsers_reciever_2 : MonoBehaviour {

	public bool ifJustArrive;

	public string[] list;
	public string sender;
	public string[] pos;
	public string id;
	public string[] temp3;
	public Vector3 spawnPos;
	public string _char;
	public string team;

	private SpawnPlayer _spawnPlayer;

	private GameObject Rteam, Bteam;
	private Respawn _respawn;

	private expBar expBarFucker;
	private UI_skill_manager _ui_skill_manager;
	private CameraTouch _cameraTouch;
	
	private skill1Plus _skill1Plus;
	private skill2Plus _skill2Plus;
	private skill3Plus _skill3Plus;


	
	// Use this for initialization
	void Start () {

		ifJustArrive = false;
		_spawnPlayer = GetComponent<SpawnPlayer> ();
		_respawn = GetComponent<Respawn> ();

			expBarFucker = GameObject.Find ("ExpBarParent").GetComponent<expBar>();
		_ui_skill_manager = GameObject.Find ("UIManager").GetComponent<UI_skill_manager> ();
		_cameraTouch = GameObject.Find ("CameraWrap").GetComponent<CameraTouch>();
		
		_skill1Plus = GameObject.Find ("skill1+").GetComponent<skill1Plus> ();
		_skill2Plus = GameObject.Find ("skill2+").GetComponent<skill2Plus> ();
		_skill3Plus = GameObject.Find ("skill3+").GetComponent<skill3Plus> ();

		Rteam = GameObject.Find ("RedTeam");
		Bteam = GameObject.Find ("BlueTeam");
	}
	

	void LateUpdate() {		
		if (ifJustArrive) {
			StartCoroutine(doit ());			
			ifJustArrive = false;
			
		}//if just arrive



	}//late


	public void preReciever(string data){
		string[] temp2 = data.Split('=');
		sender = temp2[0];
		list = temp2[1].Split('_');
		

		ifJustArrive = true;		
	}
	
	private IEnumerator doit(){
		
		//find  other players if they are there  . else instantiate
		
		//for each if (GameObject.Find("WhateverItsCalled") == null)  {instant}
		
		for(int i=0;i<list.Length-2;i++)
		{
			temp3 = list[i].Split(':');
			id = temp3[0];
			pos = temp3[1].Split(',');
			spawnPos = new Vector3(float.Parse(pos[0]),
			                       float.Parse(pos[1]),
			                       float.Parse(pos[2]));
			_char = temp3[2];
			team = temp3[3];
			
			GameObject a;
			
			if(ClientState.id == sender){
				
				GameObject player = (GameObject)Resources.Load(_char);
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
				expBarFucker.setPlayer();			
				_skill1Plus.setPlayer();
				_skill2Plus.setPlayer();
				_skill3Plus.setPlayer();
				_cameraTouch.setPlayer();
				
			}
			
			
		}//for	
		yield return null;
	}
}
