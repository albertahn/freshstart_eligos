using UnityEngine;
using System.Collections;

public class Stats_sync_reciever : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void recieve(string data){

		Debug.Log ("recied stats:"+ data);

	}//end recieve stats


	public void sendMyData(){

		string data = ClientState.id + ":" + ClientState.level + ":" + ClientState.money+":"+ClientState.death+":"+ClientState.kill;
		SocketStarter.Socket.Emit ("statSyncREQ", data);//내위치를 서버에 알린다.

	}//end sendmydata
}
