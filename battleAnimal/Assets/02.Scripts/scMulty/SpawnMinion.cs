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
	
	// Use this for initialization
	void Start () {		
		REDspawnSpot = GameObject.Find("redMovePoints/spawnPoint").GetComponent<Transform>();
		BLUEspawnSpot = GameObject.Find("blueMovePoints/spawnPoint").GetComponent<Transform>();
		
		rms = GameObject.Find ("redMinions");
		bms = GameObject.Find ("blueMinions");
		redMax = 10;
		blueMax = 10;
		redMinionPool = new GameObject[redMax];
		blueMinionPool = new GameObject[blueMax];
		_redMinionCtrl = new minionCtrl[redMax];
		_blueMinionCtrl = new blueMinionCtrl[blueMax];
		
		for (int i=0; i<redMax; i++) {
			redMinionPool[i] = (GameObject)Instantiate(redMinion);
			blueMinionPool[i] = (GameObject)Instantiate(blueMinion);
			redMinionPool[i].name = "rm"+i.ToString();
			blueMinionPool[i].name = "bm"+i.ToString();
			_redMinionCtrl[i] = redMinionPool[i].GetComponent<minionCtrl>();
			_blueMinionCtrl[i] = blueMinionPool[i].GetComponent<blueMinionCtrl>();
			redMinionPool[i].SetActive(false);
			blueMinionPool[i].SetActive(false);
			redMinionPool[i].transform.parent = rms.transform;
			blueMinionPool[i].transform.parent = bms.transform;
		}
	}
	
	//master
	public void createMinion(){
		string data=null;
		for (int i=0; i<redMax; i++) {
			if(redMinionPool[i].activeSelf==false){
				redMinionPool[i].transform.position = REDspawnSpot.position;
				_redMinionCtrl[i].isMaster = true;
				_redMinionCtrl[i].move();
				redMinionPool[i].SetActive(true);
				_redMinionCtrl[i].isDie = false;
				data = redMinionPool[i].name;
				StartCoroutine(_redMinionCtrl[i].CheckMonsterState());
				break;
			}
		}
		for (int i=0; i<blueMax; i++) {
			if(blueMinionPool[i].activeSelf==false){
				blueMinionPool[i].transform.position = BLUEspawnSpot.position;
				_blueMinionCtrl[i].isMaster = true;
				_blueMinionCtrl[i].move();
				blueMinionPool[i].SetActive(true);	
				_blueMinionCtrl[i].isDie = false;
				data = data+":"+blueMinionPool[i].name;
				StartCoroutine(_blueMinionCtrl[i].CheckMonsterState());
				break;
			}
		}
		SocketStarter.Socket.Emit ("createMinionREQ",data);
	}
	
	
	//slave
	public void REDsetSpawn(string _id){
		for (int i=0; i<redMax; i++) {
			if(redMinionPool[i].activeSelf==false){
				redMinionPool[i].transform.position = REDspawnSpot.position;
				if(ClientState.isMaster){//edit
					_redMinionCtrl[i].isMaster = true;
					StartCoroutine(_redMinionCtrl[i].CheckMonsterState());
				}
				_redMinionCtrl[i].move();
				redMinionPool[i].SetActive(true);
				_redMinionCtrl[i].isDie = false;
				break;
			}
		}
	}
	
	public void BLUEsetSpawn(string _id){
		for (int i=0; i<blueMax; i++) {
			if(blueMinionPool[i].activeSelf==false){
				blueMinionPool[i].transform.position = BLUEspawnSpot.position;	
				if(ClientState.isMaster){//edit
					_blueMinionCtrl[i].isMaster = true;
					StartCoroutine(_blueMinionCtrl[i].CheckMonsterState());
				}
				_blueMinionCtrl[i].move();
				blueMinionPool[i].SetActive(true);	
				_blueMinionCtrl[i].isDie = false;
				break;				
			}
		}
	}
}