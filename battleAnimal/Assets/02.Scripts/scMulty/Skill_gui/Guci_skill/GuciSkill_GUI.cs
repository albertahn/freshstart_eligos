using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuciSkill_GUI : MonoBehaviour {
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

	
	private Guci_firstSkill firstSkill;
	private Guci_thirdSkill thirdSkill_script;
	
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
		skillCool [2] = 5.0f;
		
		skillStartTime = new float[3];
		
		skill_live = new bool[3];
		for (int i=0; i<3; i++) {
			skill_live [i] = true;//false;
		}
		_lvUpEvolve = GetComponent<Level_up_evolve> ();
		isSet = true;

		firstSkill = GetComponent<Guci_firstSkill> ();
		thirdSkill_script = GetComponent<Guci_thirdSkill> ();	
	}
	
	
	public void Skill1_bot()
	{		
		if (skill_state [0]&&Time.time-skillStartTime[0]>=skillCool[0]&&!_playerHealthState.isDie) {
			myMoveCtrl.enabled = false;
			Vector3 spawnPos = transform.position;
			Quaternion rotationdog = transform.rotation;
			
			GameObject a;
			a = (GameObject)Instantiate (firstskill, spawnPos, rotationdog);
			a.name = "firstskill";
			
			a.transform.parent = transform;	
			skillOneReady = true;
			skillStartTime[0] = Time.time;
			skill_state [0] = false;
			myMoveCtrl.skillMode = true;
			skills [0].sprite = skill1Blank_spr;
		}
	}
	
	public void Skill2_bot()
	{
		if (skill_state [1]&&Time.time-skillStartTime[1]>=skillCool[1]&&!_playerHealthState.isDie) {

			//GameObject dogy = GameObject.Find (ClientID);
			
			Vector3 spawnPos = transform.position+ Vector3.up * 3;
			Quaternion rotationdog = transform.rotation;

			GameObject a;
			a = (GameObject)Instantiate (secondskill, spawnPos, rotationdog);
			a.name = "secondskill";			
			a.transform.parent = transform;

			GetComponent<Guci_secondSkill>().startSkill();

			skillStartTime[1] = Time.time;
			skill_state [1] = false;
			skills [1].sprite = skill2Blank_spr;			
		}
	}
	
	
	public void Skill3_bot()
	{
		if (skill_state [2]&& Time.time-skillStartTime[2] >= skillCool[2]&&!_playerHealthState.isDie) {
			myMoveCtrl.enabled = false;
			Vector3 spawnPos = transform.position;
			Quaternion rotationdog = transform.rotation;
			
			GameObject a;
			a = (GameObject)Instantiate (thirdskill, spawnPos, rotationdog);
			a.name = "thirdskill";
			
			a.transform.parent = transform;	
			skillThreeReady = true;
			skillStartTime[2] = Time.time;
			skill_state [2] = false;
			skills [2].sprite = skill1Blank_spr;
			myMoveCtrl.skillMode = true;
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
				myMoveCtrl.skillMode = false;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				
				RaycastHit hiterone;
				
				//GUIElement	hitObject = guilayer.HitTest( Input.mousePosition );if (Physics.Raycast (ray, out hitman, Mathf.Infinity)) {
				
				if (Physics.Raycast (ray, out hiterone, Mathf.Infinity, 1 << LayerMask.NameToLayer ("FLOOR"))) { 
					
					
					if (skillOneReady == true) {
						
						//GameObject dog = GameObject.Find (ClientState.id);
						
						Vector3 clickendpoint = hiterone.point;
						
						fireFirst (this.gameObject, clickendpoint, ClientState.id);
						
						
						transform.LookAt (hiterone.point);
						
						clearSkillWraps ();
						
						skillOneReady = false;
					}else{
						firstSkill.cancleSkill();
					}

					
					if (skillThreeReady) {
						
						transform.LookAt (hiterone.point);
						
						fireThird (this.gameObject, hiterone.point, ClientState.id);

						clearSkillWraps ();
						skillThreeReady = false;
						
						Vector3 clickendpoint = hiterone.point;
					}else{
						thirdSkill_script.cancleSkill();
					}
				} ///raycasr
				myMoveCtrl.enabled = true;
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
		
		//GameObject dog = gameobject;
		myMoveCtrl.idle ();
		transform.LookAt(vector);

		firstSkill.fireBall (firedBy,vector);
		
		clearSkillWraps();
		skillOneReady = false;
	}

	public void fireThird(GameObject gameobject, Vector3 vector, string firedBy){
		
		//GameObject dog = gameobject;
		myMoveCtrl.idle ();		
		transform.LookAt(vector);

		thirdSkill_script.startSkill(firedBy,vector);


		clearSkillWraps();		
		skillThreeReady = false;		
	}
}