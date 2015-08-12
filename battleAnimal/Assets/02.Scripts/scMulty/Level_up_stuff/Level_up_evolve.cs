using UnityEngine;
using System.Collections;

public class Level_up_evolve : MonoBehaviour {
	public int currentlevel;	
	public int killint;	
	public GameObject firstEvolvPlayer;	
	public bool switchToEvol;	
	public bool evol_already;
	public int exp;
	private RectTransform plusWindow;
	private RectTransform plusSkill1;
	private RectTransform plusSkill2;
	private RectTransform plusSkill3;
	
	private PlayerHealthState _playerHealthState;
	
	private float wx,wy,s1x,s1y,s2x,s2y,s3x,s3y;
	private expBar _expBar;
	private moneyUI _moneyUI;
	
	
	// Use this for initialization
	void Start () {
		switchToEvol = false;		
		evol_already = false;
		
		plusWindow = GameObject.Find ("skill+window").GetComponent<RectTransform>();
		plusSkill1 = GameObject.Find ("skill1+").GetComponent<RectTransform>();
		plusSkill2 = GameObject.Find ("skill2+").GetComponent<RectTransform>();
		plusSkill3 = GameObject.Find ("skill3+").GetComponent<RectTransform>();
		_expBar = GameObject.Find ("ExpBarParent").GetComponent<expBar> ();
		_moneyUI = GameObject.Find ("UIManager").GetComponent<moneyUI> ();
		_playerHealthState = GetComponent<PlayerHealthState> ();
		
		wx = plusWindow.sizeDelta.x; wy=plusWindow.sizeDelta.y;
		s1x = plusSkill1.sizeDelta.x; s1y = plusSkill1.sizeDelta.y;
		s2x = plusSkill2.sizeDelta.x; s2y = plusSkill2.sizeDelta.y;
		s3x = plusSkill3.sizeDelta.x; s3y = plusSkill3.sizeDelta.y;
		
		plusWindow.GetComponent<RectTransform>().sizeDelta =new Vector2 (0, 0);
		plusSkill1.GetComponent<RectTransform>().sizeDelta =new Vector2 (0, 0);
		plusSkill2.GetComponent<RectTransform>().sizeDelta =new Vector2 (0, 0);
		plusSkill3.GetComponent<RectTransform>().sizeDelta =new Vector2 (0, 0);
		
		PlayerPrefs.SetInt ("minions_killed", 0);	
		currentlevel = 1;
		ClientState.skillPoint++;
		openSkillPlus();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(switchToEvol==true && PlayerPrefs.GetString("evolved") =="false"){			
			StartEvolution();
			PlayerPrefs.SetString ("evolved", "true");
			switchToEvol=false;
			evol_already = true;
		}
	} //end update
	
	void openSkillPlus(){
		plusWindow.GetComponent<RectTransform>().sizeDelta =new Vector2 (wx, wy);
		plusSkill1.GetComponent<RectTransform>().sizeDelta =new Vector2 (s1x, s1y);
		plusSkill2.GetComponent<RectTransform>().sizeDelta =new Vector2 (s2x, s2y);
		plusSkill3.GetComponent<RectTransform>().sizeDelta =new Vector2 (s3x, s3y);	
	}
	
	public void closeSkillPlus(){	
		plusWindow.GetComponent<RectTransform>().sizeDelta =new Vector2 (0,0);
		plusSkill1.GetComponent<RectTransform>().sizeDelta =new Vector2 (0,0);
		plusSkill2.GetComponent<RectTransform>().sizeDelta =new Vector2 (0,0);
		plusSkill3.GetComponent<RectTransform>().sizeDelta =new Vector2 (0,0);	
	}
	
	public void expUp(int _exp){
		ClientState.exp += _exp;
		exp = ClientState.exp;
		if (exp >= ClientState.maxExp[6])
			ClientState.exp = 3000;
		
		if (exp >= ClientState.maxExp[0]&&ClientState.level<1) {
			if(currentlevel!=1){
				process(1);
			}
		}
		if (exp >= ClientState.maxExp[1]&&ClientState.level<2) {			
			if(currentlevel!=2){
				process(2);
			}
		}
		if (exp >= ClientState.maxExp[2]&&ClientState.level<3) {
			if(currentlevel!=3){
				process(3);
				Debug.Log("lv3");
			}
		}else if (exp >= ClientState.maxExp[3]&&ClientState.level<4){
			if(currentlevel!=4){
				process(4);
			}
		}
		if (exp >= ClientState.maxExp[4]&&ClientState.level<5){
			if(currentlevel!=5){
				process(5);;
			}
		}
		if (exp >=ClientState.maxExp[5]&&ClientState.level<6) {
			if(currentlevel!=6){
				process(6);
			}
		}
		if (exp >=ClientState.maxExp[6]&&ClientState.level<7) {
			if(currentlevel!=7){
				process(7);
			}
		}
	}
	
	private void process(int _level){
		ClientState.level=_level;
		ClientState.skillPoint++;
		currentlevel=_level;
		_expBar.setExp();
		openSkillPlus();
		playerStat.maxHp += playerStat.HpIncTerm;
		_playerHealthState.maxhp = playerStat.maxHp;
		_playerHealthState.hp = _playerHealthState.maxhp;
		_moneyUI.levelPrint();
		//string data = ClientState.id+":"+playerStat.maxHp;
		//SocketStarter.Socket.Emit ("levelUpREQ",data);
	}
	
	public void processSync(int _maxHp){
		_playerHealthState.maxhp = _maxHp;
		_playerHealthState.hp = _maxHp;
	}
	
	/*
	 void OnGUI(){		
		if (GUI.Button (new Rect (400, 0, 70, 30), "kill: "+ PlayerPrefs.GetInt("minions_killed"))) {
		}
		if (GUI.Button (new Rect (470, 0, 70, 30), "Level: "+ currentlevel)) {			
		}
	} //end gui
	*/
	
	void StartEvolution(){
		
		switchToEvol = false;
		
		GameObject prevob = GameObject.Find (PlayerPrefs.GetString("email"));
		
		Vector3 pastpos = prevob.transform.position;
		
		if(evol_already == false && PlayerPrefs.GetString("evolved")=="false"){
			
			Destroy (prevob); 
			
			GameObject aa;
			aa = (GameObject)Instantiate(firstEvolvPlayer, pastpos, Quaternion.identity);
			
			aa.name= PlayerPrefs.GetString("email");
			if(ClientState.team=="red")				
				aa.transform.parent =  GameObject.Find ("RedTeam").transform;
			else				
				aa.transform.parent =  GameObject.Find ("BlueTeam").transform;
		}
		
		PlayerPrefs.SetString ("evolved", "true");
		evol_already = true;
		
	}//end evolution
	
}
