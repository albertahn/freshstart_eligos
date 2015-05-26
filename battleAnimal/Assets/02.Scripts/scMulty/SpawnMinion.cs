using UnityEngine;
using System.Collections;

public class SpawnMinion : MonoBehaviour {
	public GameObject redMinion;
	public GameObject blueMinion;

	public GameObject rms;
	public GameObject bms;

	// Use this for initialization
	void Start () {
		rms = GameObject.Find ("redMinions");
		bms = GameObject.Find ("blueMinions");
	}
	
	// Update is called once per frame

	public void REDsetSpawn(string _id,Vector3 _data){
		GameObject a;
		a = (GameObject)Instantiate(redMinion,_data,Quaternion.identity);
		a.name=_id;
		a.transform.parent = rms.transform;
		if(ClientState.isMaster){//edit
			a.GetComponent<minionCtrl>().isMaster = true;
		}
	}

	public void BLUEsetSpawn(string _id,Vector3 _data){		
		GameObject a;
		a = (GameObject)Instantiate(blueMinion,_data,Quaternion.identity);
		a.name=_id;
		a.transform.parent = bms.transform;
		if(ClientState.isMaster){//edit
			a.GetComponent<blueMinionCtrl>().isMaster = true;
		}
	}
}
