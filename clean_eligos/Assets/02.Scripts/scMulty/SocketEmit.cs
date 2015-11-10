using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SocketEmit : MonoBehaviour {
//	public users[];
//	public USERS users;
	List<USERS> users;

	// Use this for initialization
	void Start () {
		users = new List<USERS>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void addPlayer(string _id,Vector3 _pos){
		USERS a = new USERS ();
		a.name = _id;
		a.pos = _pos;
		users.Add (a);

		foreach (USERS data in users) {
			Debug.Log(data.name + ":" + data.pos.x.ToString());
		}
	}

	public string[] getPlayerList(){
		List<string> ret = new List<string> ();

		foreach (USERS data in users) {
			ret.Add(data.name);
		}
		return ret.ToArray();
	}
}

public class USERS{
	public string name;
	public Vector3 pos;
}