using UnityEngine;
using System.Collections;

using Boomlagoon.JSON;

public class FriendDatabase : MonoBehaviour {
	public JSONArray friendArray;
	public JSONObject returnObj;
	// Use this for initialization
	void Start () {
		friendArray = new JSONArray ();
		returnObj = new JSONObject ();
	}


	public IEnumerator getfriendState(string my_index, string friend_index)
	{
		string url = "http://mobile.sharebasket.com/friend/check_if_battle_sent/"+my_index+"/"+friend_index;
		
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
	
		
		// Create a download object
		WWW downloadbabe = new WWW( url );
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
		
		if (downloadbabe.size <= 2) {
			
			yield return null;
			
		} else {
			
			friendArray = JSONArray.Parse(downloadbabe.text);
		}
		
		
	}//end getdata


	public IEnumerator sendfriendReq(string my_index, string friend_index)
	{
		string url = "http://mobile.sharebasket.com/friend/add_friend/"+my_index+"/"+friend_index;

		
		// Create a download object
		WWW downloadbabe = new WWW( url );
		// Wait until the download is done
		yield return downloadbabe;
		if(downloadbabe.error !=null) {
			
			Debug.Log( "Error downloading: " + downloadbabe.error );
			//return;
		} else {
			// show the highscores
			Debug.Log(downloadbabe.text);
		}

		if (downloadbabe.size <= 2) {
			
			yield return null;
			
		} else {
			
			returnObj = JSONObject.Parse(downloadbabe.text);
		}
		
		
	}//end getdata

}
