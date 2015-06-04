using UnityEngine;
using System.Collections;

public class SpawnMinion : MonoBehaviour {
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
	
	// Update is called once per frame
	
	public void REDsetSpawn(string _id,Vector3 _data){
		for (int i=0; i<redMax; i++) {
			if(redMinionPool[i].activeSelf==false){
				redMinionPool[i].transform.position = _data;
				if(ClientState.isMaster){//edit
					_redMinionCtrl[i].isMaster = true;
					StartCoroutine(_redMinionCtrl[i].CheckMonsterState());
				}
				_redMinionCtrl[i].move();
				redMinionPool[i].SetActive(true);
				break;
			}
		}
	}
	
	public void BLUEsetSpawn(string _id,Vector3 _data){
		for (int i=0; i<blueMax; i++) {
			if(blueMinionPool[i].activeSelf==false){
				blueMinionPool[i].transform.position = _data;			
				if(ClientState.isMaster){//edit
					_blueMinionCtrl[i].isMaster = true;
					StartCoroutine(_blueMinionCtrl[i].CheckMonsterState());
				}
				_blueMinionCtrl[i].move();
				blueMinionPool[i].SetActive(true);	
				break;				
			}
		}
	}
}
