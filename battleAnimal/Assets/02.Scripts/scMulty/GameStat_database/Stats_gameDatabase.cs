using UnityEngine;
using System.Collections;
using System;

using Boomlagoon.JSON;
public class Stats_gameDatabase : MonoBehaviour {

	public JSONObject fuckdata;

	// Use this for initialization
	void Start () {
		fuckdata = new JSONObject();
		StartCoroutine (SaveBestScore("123","ffff","12443","hhh"));

	}

	public IEnumerator SaveBestScore(string kills, string deaths, string assists, string gold)
	{
		string url = "http://mobile.sharebasket.com/room/endgame_set_stats";
		
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		
		// The name of the player submitting the scores
		form.AddField( "kills",  kills);
		form.AddField( "deaths", deaths);
		form.AddField( "assists",   assists);
		form.AddField( "gold",   gold);
		
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
			
			fuckdata = JSONObject.Parse(downloadbabe.text);
		}
		
		
	}//end public

}
