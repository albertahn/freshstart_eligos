using UnityEngine;
using System.Collections;

public class LobbyUI : MonoBehaviour {
	public bool isUI;
	private string data;
	// Use this for initialization
	void Start () {
		isUI = true;
		data = ClientState.id;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnGUI(){
		/*if (isUI) {
				if (GUI.Button (new Rect (10, 10, 100, 100), "Enter the room")) {
						string data = ClientState.id;
						SocketStarter.Socket.Emit ("createRoomREQ", data);
				}
				if (GUI.Button (new Rect (10, 120, 100, 100), "Quit"))
						Application.Quit ();
		}*/
		
	/*	GUI.Label(new Rect(200,10,50,50),"id = "+ClientState.id);
		GUI.Label(new Rect(200,70,50,50),"room = "+ClientState.room);
		GUI.Label(new Rect(200,130,50,50),"order = "+ClientState.order);
		GUI.Label(new Rect(200,190,50,50),"character = "+ClientState.character);
		GUI.Label(new Rect(200,250,50,50),"team = "+ClientState.team);*/
		
		GUI.Label(new Rect(200,10,50,50),"master = "+ClientState.isMaster);
	}
}