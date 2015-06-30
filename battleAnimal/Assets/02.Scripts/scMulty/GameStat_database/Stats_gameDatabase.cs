using UnityEngine;
using System.Collections;
using System;

using Boomlagoon.JSON;
public class Stats_gameDatabase : MonoBehaviour {

	public static JSONObject statsFromDB;

	// Use this for initialization
	void Start () {

		statsFromDB = new JSONObject();
		//StartCoroutine (SaveBestScore("123","ffff","12443","hhh"));

	}//end start

	public static IEnumerator SaveBestScore(
											string rooms_index,
											string members_index,
	                                        string level,
	                                        string items,
											string kills, 
	                                        string deaths,
	                                        string cs_kills,
	                                        string gold,
											string team,
											string points)
	{

		string url = "http://mobile.sharebasket.com/room/endgame_set_stats";
		
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		
		// The name of the player submitting the scores

		form.AddField( "rooms_index",  rooms_index);
		form.AddField( "members_index",  members_index);
		form.AddField( "level",  level);
		form.AddField( "items",  items);

		form.AddField( "kills",  kills);
		form.AddField( "deaths", deaths);
		form.AddField( "cs_kills",   cs_kills);
		form.AddField( "gold",   gold);

		form.AddField( "team",   team);
		form.AddField( "points",   points);
		
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
			
			statsFromDB = JSONObject.Parse(downloadbabe.text);
		}
		
		
	}//end public

}
