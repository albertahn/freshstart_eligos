using UnityEngine;
using System.Collections;

public class SpawnMinion : MonoBehaviour {
	public GameObject redMinion;
	public GameObject blueMinion;
	private Vector3 REDspawnPos;
	private Vector3 BLUEspawnPos;
	public bool REDspawnSwitch;
	public bool BLUEspawnSwitch;
	private string REDid;
	private string BLUEid;
	public GameObject rms;
	public GameObject bms;

	// Use this for initialization
	void Start () {
		REDspawnSwitch = false;
		BLUEspawnSwitch = false;
		rms = GameObject.Find ("redMinions");
		bms = GameObject.Find ("blueMinions");
	}
	
	// Update is called once per frame
	void Update () {
		if (REDspawnSwitch) {
			GameObject a;
			a = (GameObject)Instantiate(redMinion,REDspawnPos,Quaternion.identity);
			a.name=REDid;
			a.transform.parent = rms.transform;
			REDspawnSwitch = false;
			if(ClientState.isMaster){//edit
				a.GetComponent<minionCtrl>().isMaster = true;
			}
		}
		if (BLUEspawnSwitch) {
			GameObject a;
			a = (GameObject)Instantiate(blueMinion,BLUEspawnPos,Quaternion.identity);
			a.name=BLUEid;
			a.transform.parent = bms.transform;
			BLUEspawnSwitch = false;
			if(ClientState.isMaster){//edit
				a.GetComponent<blueMinionCtrl>().isMaster = true;
			}
		}
	
	}

	public void REDsetSpawn(string _id,Vector3 _data){
		REDid = _id;
		REDspawnPos = _data;
		REDspawnSwitch=true;
	}

	public void BLUEsetSpawn(string _id,Vector3 _data){
		BLUEid = _id;
		BLUEspawnPos = _data;
		BLUEspawnSwitch=true;
	}
}
