using UnityEngine;
using System.Collections;

public class create_room : MonoBehaviour {

	public GameObject create_room_pannel;

	private Vector3 inCamPos, outCamPos;


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


	//createroom in database

}
