using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class roomSocketOn : MonoBehaviour {
	private string clientID;
	public	waitGUI _waitGUI;

	 public bool loadRooms ;



	public string roomstring;

	// Use this for initialization
	void Start () {
		Screen.SetResolution(1024, 600, true);

		loadRooms = false;

		//Screen.SetResolution(480, 800, true);

		clientID = ClientState.id;
		_waitGUI = GetComponent<waitGUI> ();



		roomSocketStarter.Socket.On ("imoutRES", (data) =>{
			string temp = data.Json.args[0].ToString();	
			int a = int.Parse(temp);
			_waitGUI.deleteUser(a);
		});

		roomSocketStarter.Socket.On ("getAllRoomRES", (data) =>{

			loadRooms = true;

			roomstring = data.Json.args[0].ToString(); // JSONArray.Parse(data.Json.args[0].ToString()); //.Json.args[0].ToString();
			roomstring = JSONObject.Parse(roomstring ).ToString();

			roomstring = roomstring.Replace(@"{", @"");
			roomstring = roomstring.Replace(@"}", @"");

			//Debug.Log("hi: "+JSONArray.Parse(roomstring).ToString());

			
			string[] temp2 = roomstring.Split(',');
			//sender = temp2[0];
			//list = temp2[1].Split('_');

			/*for(int i=0;i<temp2.Length-2;i++)
				{
				//Debug.Log("hi: "+temp2[i].ToString());


				}
*/



			Debug.Log(""+data.Json.args[0].ToString());

		});

	//	string dataman = "hi" + ":" +"hiafdas";
		
		//roomSocketStarter.Socket.Emit ("getAllRoomREQ", dataman);
	}//end start

	
	// Update is called once per frame
	void Update () {

		if(loadRooms){

			GameObject text =  GameObject.Find("testrooms");
			text.GetComponent<GUIText>().text = roomstring;
			loadRooms = false;

		}//end if
	
	}
}
