using UnityEngine;
using System.Collections;

using Boomlagoon.JSON;


public class EndServerDatabase : MonoBehaviour {
	public JSONArray fuckArray;



	// Use this for initialization
	void Start () {

		fuckArray = new JSONArray();


	
	}


	public IEnumerator getRoomStats(string roomindex)
	{
		string url = "http://mobile.sharebasket.com/room/get_game_stats/"+roomindex;
		
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		
		// The name of the player submitting the scores

		
		// Create a download object
		WWW downloadbabe = new WWW( url );
		// Wait until the download is done
		yield return downloadbabe;
		if(downloadbabe.error !=null) {
			
			Debug.Log( "Error downloading: " + downloadbabe.error );
			//return;

		} else {

			// show the highscores
			//Debug.Log(downloadbabe.text);
		}
		
		//WWW www = new WWW (url);
		//yield return www;
		
		if (downloadbabe.size <= 2) {
			
			yield return null;
			
		} else {
			
			fuckArray = JSONArray.Parse(downloadbabe.text);
		}
		
		
	}//end getdata



	public IEnumerator sendRoomStats(string roomindex, string[]  allStats )
	{
		string url = "http://mobile.sharebasket.com/room/get_game_stats/"+roomindex;
		
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		
		// The name of the player submitting the scores
		form.AddField( "index",  "index");
		form.AddField( "email", "email" );
		form.AddField( "score",   "score");
		
		// Create a download object
		WWW downloadbabe = new WWW( url, form );
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
			
			fuckArray = JSONArray.Parse(downloadbabe.text);
		}
		
		
	}//end getdata

	public IEnumerator checkFriend(string my_index, string members_index)
	{


		string url = "http://mobile.sharebasket.com/room/get_game_stats/"+my_index+"/"+members_index;
		
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		
		// The name of the player submitting the scores
		
		
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
			
			fuckArray = JSONArray.Parse(downloadbabe.text);
		}
		
		
	}//end getdata



}
