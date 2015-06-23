using UnityEngine;
using System.Collections;

public class SpawnMinion : MonoBehaviour {
	private Transform REDspawnSpot;
	private Transform BLUEspawnSpot;
	
	public GameObject redMinion;
	public GameObject blueMinion;
	
	public GameObject rms;
	public GameObject bms;
	
	private int redMax,blueMax;
	private GameObject[] redMinionPool;
	private GameObject[] blueMinionPool;
	private minionCtrl[] _redMinionCtrl;
	private blueMinionCtrl[] _blueMinionCtrl;
	private red_outer_collider[] _red_outter_ctrl;
	public blue_outer_collider[] _blue_outter_ctrl;
	
	// Use this for initialization
	void Start () {		
		REDspawnSpot = GameObject.Find("redMovePoints/spawnPoint").GetComponent<Transform>();
		BLUEspawnSpot = GameObject.Find("blueMovePoints/spawnPoint").GetComponent<Transform>();
		
		rms = GameObject.Find ("redMinions");
		bms = GameObject.Find ("blueMinions");
		redMax = 30;
		blueMax = 30;
		redMinionPool = new GameObject[redMax];
		blueMinionPool = new GameObject[blueMax];
		_redMinionCtrl = new minionCtrl[redMax];
		_blueMinionCtrl = new blueMinionCtrl[blueMax];
		_red_outter_ctrl = new red_outer_collider[redMax];
		_blue_outter_ctrl = new blue_outer_collider[blueMax];
		
		for (int i=0; i<redMax; i++) {
			redMinionPool[i] = (GameObject)Instantiate(redMinion);
			blueMinionPool[i] = (GameObject)Instantiate(blueMinion);
			redMinionPool[i].name = "rm"+i.ToString();
			blueMinionPool[i].name = "bm"+i.ToString();
			_redMinionCtrl[i] = redMinionPool[i].GetComponent<minionCtrl>();
			_blueMinionCtrl[i] = blueMinionPool[i].GetComponent<blueMinionCtrl>();
			_red_outter_ctrl[i] = redMinionPool[i].GetComponentInChildren<red_outer_collider>();
			_blue_outter_ctrl[i] = blueMinionPool[i].GetComponentInChildren<blue_outer_collider>();
			redMinionPool[i].SetActive(false);
			blueMinionPool[i].SetActive(false);
			redMinionPool[i].transform.parent = rms.transform;
			blueMinionPool[i].transform.parent = bms.transform;
			_redMinionCtrl[i].StartC();
			_blueMinionCtrl[i].StartC();
		}
	}
	
	//master
	public void createMinion(int _line){
		string data=null;
		for (int i=0; i<redMax; i++) {
			if(redMinionPool[i].activeSelf==false){
				redMinionPool[i].transform.tag = "MINION";
				redMinionPool[i].transform.FindChild ("touchCollider").transform.tag = "MINION";
				
				_redMinionCtrl[i].line=_line;
				redMinionPool[i].transform.position = REDspawnSpot.position;
				_redMinionCtrl[i].isMaster = true;
				_redMinionCtrl[i].move();
				redMinionPool[i].SetActive(true);
				_redMinionCtrl[i].isDie = false;
				data = redMinionPool[i].name+"_"+_redMinionCtrl[i].line;
				StartCoroutine(_redMinionCtrl[i].CheckMonsterState());
				_red_outter_ctrl[i].removeAll();
				_redMinionCtrl[i].initiatePoints();
				break;
			}
		}
		for (int i=0; i<blueMax; i++) {
			if(blueMinionPool[i].activeSelf==false){
				blueMinionPool[i].transform.tag = "MINION";
				blueMinionPool[i].transform.FindChild ("touchCollider").transform.tag = "MINION";
				
				_blueMinionCtrl[i].line=_line;
				blueMinionPool[i].transform.position = BLUEspawnSpot.position;
				_blueMinionCtrl[i].isMaster = true;
				_blueMinionCtrl[i].move();
				blueMinionPool[i].SetActive(true);	
				_blueMinionCtrl[i].isDie = false;
				data = data+":"+blueMinionPool[i].name+"_"+_blueMinionCtrl[i].line;
				StartCoroutine(_blueMinionCtrl[i].CheckMonsterState());
				_blue_outter_ctrl[i].removeAll();
				_blueMinionCtrl[i].initiatePoints();
				break;
			}
		}
		SocketStarter.Socket.Emit ("createMinionREQ",data);
	}
	
	
	//slave
	public void REDsetSpawn(string _id,int _line){
		for (int i=0; i<redMax; i++) {
			if(redMinionPool[i].name ==_id){
				Debug.Log(_line.ToString()+" : hello red mininon");
				redMinionPool[i].transform.tag = "MINION";
				redMinionPool[i].transform.FindChild ("touchCollider").transform.tag = "MINION";
				
				_redMinionCtrl[i].line=_line;
				redMinionPool[i].transform.position = REDspawnSpot.position;
				if(ClientState.isMaster){//edit
					_redMinionCtrl[i].isMaster = true;
					StartCoroutine(_redMinionCtrl[i].CheckMonsterState());
				}
				_redMinionCtrl[i].move();
				redMinionPool[i].SetActive(true);
				_redMinionCtrl[i].isDie = false;
				_red_outter_ctrl[i].removeAll();
				_redMinionCtrl[i].initiatePoints();
				break;
			}
		}
	}
	
	public void BLUEsetSpawn(string _id,int _line){
		for (int i=0; i<blueMax; i++) {
			if(blueMinionPool[i].name ==_id){
				Debug.Log(_line.ToString()+" : hello blue mininon");
				blueMinionPool[i].transform.tag = "MINION";
				blueMinionPool[i].transform.FindChild ("touchCollider").transform.tag = "MINION";
				
				_blueMinionCtrl[i].line=_line;
				blueMinionPool[i].transform.position = BLUEspawnSpot.position;	
				if(ClientState.isMaster){//edit
					_blueMinionCtrl[i].isMaster = true;
					StartCoroutine(_blueMinionCtrl[i].CheckMonsterState());
				}
				_blueMinionCtrl[i].move();
				blueMinionPool[i].SetActive(true);	
				_blueMinionCtrl[i].isDie = false;
				_blue_outter_ctrl[i].removeAll();
				_blueMinionCtrl[i].initiatePoints();
				break;				
			}
		}
	}
}