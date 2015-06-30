using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FurfurSkill_GUI : MonoBehaviour {
	//public Texture2D stexture1, stexture2 , stexture3 ;
	private Transform trans;
	private string ClientID;
	
	public Image[] skills;
	public Sprite skill1_spr,skill2_spr,skill3_spr;
	public Sprite skill1Blank_spr,skill2Blank_spr,skill3Blank_spr;
	
	
	public int xfirst, yfirst, xsecond, ysecond, xthird, ythird, xfourth ;	
	public MoveCtrl myMoveCtrl;
	
	public GameObject firstskill, secondskill, thirdskill;
	public GUILayer guilayer;
	
	//public FireSkill skillfire;
	
	public bool skillOneReady =false;
	public bool skillTwoReady =false;
	public bool skillThreeReady = false;
	
	public bool[] skill_state;
	public bool[] skill_live;
	private Level_up_evolve _lvUpEvolve;
	
	private float[] skillCool;
	private float[] skillStartTime;
	
	public PlayerHealthState _playerHealthState;
	
	public bool isSet;
	
	/*
	void Start(){
		isSet = false;
	}*/
	
	public void setPlayer(){
		ClientID = ClientState.id;	
		//Get game object
		myMoveCtrl = GetComponent<MoveCtrl> ();
		guilayer = Camera.main.GetComponent<GUILayer> ();		
		_playerHealthState = this.GetComponent<PlayerHealthState> ();
		
		trans = GetComponent<Transform> ();
		//FireSkill skillfire = GetComponent<FireSkill>();
		
		skills = GameObject.Find ("skillWindow").GetComponentsInChildren <Image> ();
		
		skill_state = new bool[3];
		for (int i=0; i<3; i++)
			skill_state [i] = true;
		
		skillCool = new float[3];
		skillCool [0] = 5.0f;
		skillCool [1] = 5.0f;
		skillCool [2] = 15.0f;
		
		skillStartTime = new float[3];
		
		skill_live = new bool[3];
		for (int i=0; i<3; i++) {
			skill_live [i] = true;//false;
		}
		_lvUpEvolve = GetComponent<Level_up_evolve> ();
		isSet = true;
	}
	
	
	public void Skill1_bot()
	{		
		if (skill_state [0]&&Time.time-skillStartTime[0]>=skillCool[0]&&!_playerHealthState.isDie) {
			GameObject dogy = GameObject.Find (ClientState.id);
			
			Vector3 spawnPos = dogy.transform.position;
			Quaternion rotationdog = dogy.transform.rotation;
			
			GameObject a;
			a = (GameObject)Instantiate (firstskill, spawnPos, rotationdog);
			a.name = "firstskill";
			
			a.transform.parent = dogy.transform;	
			skillOneReady = true;
			skillStartTime[0] = Time.time;
			skill_state [0] = false;
			skills [0].sprite = skill1Blank_spr;
		}
	}
	
	public void Skill2_bot()
	{
		if (skill_state [1]&&Time.time-skillStartTime[1]>=skillCool[1]&&!_playerHealthState.isDie) {		
			Debug.Log ("clicked 2 man");
			GameObject dogy = GameObject.Find (ClientID);
			//dogy.transform.position = dogy.transform.position+ Vector3.up * 10;
			
			Vector3 spawnPos = dogy.transform.position;
			Quaternion rotationdog = dogy.transform.rotation;
			if( _playerHealthState.hp < _playerHealthState.maxhp){
				_playerHealthState.hp= _playerHealthState.hp+150;				
			}
			
			GameObject a=  GameObject.Find(ClientState.id);
			fireSecond(a);
			
			skillStartTime[1] = Time.time;
			skill_state [1] = false;
			skills [1].sprite = skill2Blank_spr;			
		}
	}
	
	
	public void Skill3_bot()
	{
		if (skill_state [0]&&Time.time-skillStartTime[0]>=skillCool[0]&&!_playerHealthState.isDie) {
			GameObject dogy = GameObject.Find (ClientState.id);
			
			Vector3 spawnPos = dogy.transform.position;
			Quaternion rotationdog = dogy.transform.rotation;
			
			GameObject a;
			a = (GameObject)Instantiate (thirdskill, spawnPos, rotationdog);
			a.name = "thirdskill";
			
			a.transform.parent = dogy.transform;	
			skillThreeReady = true;
			skillStartTime[2] = Time.time;
			skill_state [2] = false;
			skills [2].sprite = skill3Blank_spr;
		}
	}
	
	
	public void skill1Plus_bot(){
		skills [0].sprite = skill1_spr;
		skill_state [0] = true;
		skill_live [0] = true;
		ClientState.skillPoint--;
		if(ClientState.skillPoint<=0)
			_lvUpEvolve.closeSkillPlus ();
		
	}
	
	public void skill2Plus_bot(){
		skills [1].sprite = skill2_spr;
		skill_state [1] = true;
		skill_live [1] = true;
		ClientState.skillPoint--;
		if(ClientState.skillPoint<=0)
			_lvUpEvolve.closeSkillPlus ();
	}
	
	public void skill3Plus_bot(){
		skills [2].sprite = skill3_spr;
		skill_state [2] = true;
		skill_live [2] = true;
		ClientState.skillPoint--;
		if(ClientState.skillPoint<=0)
			_lvUpEvolve.closeSkillPlus ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isSet == true) {
			
			RaycastHit hitman;
			
			if (skill_live [0] && Time.time - skillStartTime [0] >= skillCool [0]) {
				skills [0].sprite = skill1_spr;
				skill_state [0] = true;
			}
			
			if (skill_live [1] && Time.time - skillStartTime [1] >= skillCool [1]) {
				skills [1].sprite = skill2_spr;
				skill_state [1] = true;
			}
			
			if (skill_live [2] && Time.time - skillStartTime [2] >= skillCool [2]) {
				skills [2].sprite = skill3_spr;
				skill_state [2] = true;
			}
			
			
			if (Input.GetMouseButtonDown (0)) {
				
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				
				RaycastHit hiterone;
				
				//GUIElement	hitObject = guilayer.HitTest( Input.mousePosition );if (Physics.Raycast (ray, out hitman, Mathf.Infinity)) {
				
				if (Physics.Raycast (ray, out hiterone, Mathf.Infinity, 1 << LayerMask.NameToLayer ("FLOOR"))) { 
					
					
					if (skillOneReady == true) {
						
						GameObject dog = GameObject.Find (ClientState.id);
						
						Vector3 clickendpoint = hiterone.point;
						
						fireFirst (dog, clickendpoint, ClientState.id);
						
						
						dog.transform.LookAt (hiterone.point);
						
						clearSkillWraps ();
						
						skillOneReady = false;
						
						
						
						string data = ClientID + ":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z + ":" + ClientState.character + ":first";
						
						SocketStarter.Socket.Emit ("SkillAttack", data);  //내위치를 서버에 알린다.				
						
						
					}//skill 1 ready true
					
					if (skillTwoReady == true) {
						
						Debug.Log ("sec skill fired");
						
						GameObject dog = GameObject.Find (ClientID);
						
						dog.transform.LookAt (hiterone.point);
						
						Vector3 clickendpoint = hiterone.point;
						float step = 35 * Time.deltaTime;
						
						
						
						
						
						dog.transform.position = Vector3.MoveTowards (dog.transform.position, clickendpoint, step);
						
						//dog.transform.position.y = 50.0f;
						
						skillTwoReady = false;
						
						GameObject skill1 = GameObject.Find ("secondskill");
						Destroy (skill1);	
						
						
						clickendpoint = hiterone.point;
						string data = ClientID + ":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z + ":" + ClientState.character + ":second";
						
						SocketStarter.Socket.Emit ("SkillAttack", data);
						
					}
					
					
					
					if (skillThreeReady) {
						
						//	Debug.Log("fired: skill "+skillfire.ToString());
						GameObject dog = GameObject.Find (ClientState.id);
						
						dog.transform.LookAt (hiterone.point);
						
						fireThird (dog, hiterone.point, ClientState.id);
						
						
						//destroy gameobject]
						//destroy all wraps
						clearSkillWraps ();
						skillThreeReady = false;
						
						Vector3 clickendpoint = hiterone.point;
						string data = ClientID + ":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z + ":" + ClientState.character + ":third";
						
						SocketStarter.Socket.Emit ("SkillAttack", data);  //내위치를 서버에 알린다.				
						
					}//skill 1 ready true
					
					
				} ///raycasr
				
			}
		}
		
		
	}//end yupdate
	
	
	void clearSkillWraps(){
		
		GameObject[] skillwraps =  GameObject.FindGameObjectsWithTag("SKILL_WRAP");//Find("firstskill");
		
		for (var i = 0; i <  skillwraps.Length; i ++) {			
			Destroy (skillwraps [i]);	
		}		
		
	}
	
	public void fireFirst(GameObject gameobject, Vector3 vector, string firedBy){
		
		GameObject dog = gameobject;
		
		dog.transform.LookAt(vector);
		
		Furfur_firstSkill firstSkill = dog.GetComponent<Furfur_firstSkill> ();	
		firstSkill.fireBall (firedBy,vector);
		
		clearSkillWraps();
		
		skillOneReady = false;
		
	}
	
	public void fireSecond(GameObject gameobject){		
		GameObject dog = gameobject;
		
		Furfur_secondSkill secondSkill = dog.GetComponent<Furfur_secondSkill> ();	
		secondSkill.startSkill (this.name);
		
		clearSkillWraps();
		
		skillOneReady = false;		
	}
	
	public void fireThird(GameObject gameobject, Vector3 vector, string firedBy){
		
		GameObject dog = gameobject;
		
		dog.transform.LookAt(vector);
		
		Furfur_thirdSkill thirdSkill = dog.GetComponent<Furfur_thirdSkill> ();	
		thirdSkill.startSkill (firedBy, vector);
		
		clearSkillWraps();
		
		skillOneReady = false;	
	}
}