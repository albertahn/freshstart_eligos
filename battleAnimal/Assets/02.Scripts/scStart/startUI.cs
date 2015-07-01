using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Boomlagoon.JSON;

public class startUI : MonoBehaviour {

	public Text UserName, Userlevel, Gold, Cash;

	public GameObject friendPannel, myProfilePannel;

	public GameObject following_board, my_followers_board;

	public Vector3 newpos, outofScreen;

	public Vector3 followers_pos, my_followers_pos;

	private JSONObject availRoomObj;
	
	// Use this for initialization
	void Start () {

		//Screen.SetResolution(800, 480, true);
		Screen.SetResolution(1280, 800, true);
		//PlayerPrefs.SetString ("email","aa");//not internet

		UserName.text = PlayerPrefs.GetString("username");
		Userlevel.text = PlayerPrefs.GetString ("userlevel");
		Gold.text = PlayerPrefs.GetString ("gold");
		Cash.text = PlayerPrefs.GetString ("cash");
		ClientState.id = UserName.text;

		newpos = new Vector3 (-150.0f, friendPannel.transform.localPosition.y, 0);
		outofScreen = friendPannel.transform.localPosition;


	}

	
	public void Multy()
	{
		Application.LoadLevel("scRoomsList");

	}
	
	public void Logout()
	{
		PlayerPrefs.SetString("email", "");
		
		PlayerPrefs.SetString("username", "");
		
		PlayerPrefs.SetString("user_index", "");
		Application.LoadLevel("scLogIn");
	}
	
	public void Exit()
	{
		Application.Quit();
			//Application.LoadLevel("scLogin");
	}//exit

	public void showFriendPannel(){



		friendPannel.transform.localPosition = newpos;

		myProfilePannel.transform.localPosition = outofScreen;

	}//show
	public void showMyProfile(){
		friendPannel.transform.localPosition = outofScreen;
			myProfilePannel.transform.localPosition =  newpos; 

	}

	//show my followers

	public void showMyFollowers(){

		
		followers_pos =  following_board.transform.position;
		my_followers_pos = my_followers_board.transform.position;
		Debug.Log ("floowers");
		my_followers_board.transform.position = followers_pos;
		following_board.transform.position = my_followers_pos;

	}//sh

	public void startRandomRoom(){

		//Debug.Log ("randosm");
		StartCoroutine (getAvailRoomsDataAndStart());

	}//end start

	public IEnumerator getAvailRoomsDataAndStart(){

		string url = "http://mobile.sharebasket.com/room/get_available_rooms";
		WWW downloadbabe = new WWW( url );
		// Wait until the download is done
		yield return downloadbabe;
		if(downloadbabe.error !=null) {
			
			Debug.Log( "Error downloading: " + downloadbabe.error );
			//return;
			
		} else {

			Debug.Log(downloadbabe.text);
		}

		if (downloadbabe.size <= 2) {
			
			yield return null;
			
		} else {
			
			availRoomObj = JSONObject.Parse(downloadbabe.text);

			string[] temp2 = availRoomObj.ToString().Split(',');
			string[] roomarray = temp2 [0].ToString ().Split (':');
			
			//Debug.Log ("room array: "+roomarray[1].ToString());

			roomarray [1] = roomarray [1].Replace("\"","");
			
			ClientState.room = roomarray [1].ToString ();
			
			Application.LoadLevel ("scWait");

		}

	}//end availroos
	
}
