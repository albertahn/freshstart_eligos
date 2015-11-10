using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class create_room : MonoBehaviour {

	public GameObject create_room_pannel;

	private Vector3 inCamPos, outCamPos;

	public JSONObject createdRoomdata;

	// Use this for initialization
	void Start () {

		create_room_pannel = GameObject.Find ("create_room_pannel");

		inCamPos = new Vector3 (78,create_room_pannel.transform.localPosition.y,create_room_pannel.transform.position.z );
		outCamPos =  new Vector3 (778,create_room_pannel.transform.localPosition.y,create_room_pannel.transform.position.z );
	}
	


	public void createRoom_ui_move(){

		Debug.Log ("client index: "+ PlayerPrefs.GetString("user_index"));
		create_room_pannel.transform.localPosition = inCamPos;

	}//

	public void create_room_close(){

		create_room_pannel.transform.localPosition = outCamPos;

	}//create


	public void creatRoomAndEnter(){

		StartCoroutine (databaseCreateRoom ());

		
	}


	public IEnumerator databaseCreateRoom(){

		string url = "http://mobile.sharebasket.com/room/create_room/"+PlayerPrefs.GetString("user_index");
		

		// Create a download object
		WWW downloadbabe = new WWW( url);
		// Wait until the download is done
		yield return downloadbabe;
		if(downloadbabe.error !=null) {
			
			Debug.Log( "Error downloading: " + downloadbabe.error );
			//return;
		} else {
			// show the highscores
			Debug.Log(downloadbabe.text);
		}
		
		//WWW www = new WWW (url);
		//yield return www;
		
		if (downloadbabe.size <= 1) {
			
			yield return null;
			
		} else {
			
			createdRoomdata = JSONObject.Parse(downloadbabe.text);

			string roomid =  createdRoomdata.GetString ("index");
			
			
			ClientState.room = roomid;
			
			Application.LoadLevel ("scWait");
		}


	}//database


	//createroom in database

}
