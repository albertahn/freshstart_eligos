using UnityEngine;
using System.Collections;

public class minionSync: MonoBehaviour {
	public Transform tr;
	public Vector3 pre_tr;
	public float duration;
	public float startTime;
	
	public Vector3 target;
	
	public GameObject[] redMinions;
	
	// Use this for initialization
	void Start () {
		
		duration = 0.5f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {		
		if (ClientState.isMaster==true) {
			if(Time.time-startTime>duration){
				string data=null;
				redMinions = GameObject.FindGameObjectsWithTag ("MINION");
				
				for(int i=0;i<redMinions.Length;i++){
					Vector3 dest;
					if(redMinions[i].name[0]=='r')
						dest = redMinions[i].GetComponent<minionCtrl>().syncTarget;
					else
						dest = redMinions[i].GetComponent<blueMinionCtrl>().syncTarget;

					data = data+redMinions[i].name+":"+redMinions[i].transform.position.x+","
						+redMinions[i].transform.position.y+","
							+redMinions[i].transform.position.z
							+":"
							+dest.x+","+dest.y+","+dest.z
									+"|";
				}
				//SocketStarter.Socket.Emit("minionSyncREQ",data);
				startTime =Time.time;
			}
		}
	}
	
	Vector3 t2v(Transform t){		
		Vector3 a;
		a.x = t.position.x;
		a.y = t.position.y;
		a.z = t.position.z;
		return a;
	}
	
	bool isEqual(Vector3 a,Vector3 b){
		int ax = (int)a.x;
		int ay = (int)a.y;
		int az = (int)a.z;
		
		int bx = (int)b.x;
		int by = (int)b.y;
		int bz = (int)b.z;
		
		if (ax == bx && ay == by && az == bz)
			return true;
		else
			return false;
	}
}