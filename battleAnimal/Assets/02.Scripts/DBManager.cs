using UnityEngine;
using System.Collections;
using System;

using Boomlagoon.JSON;


public class DBManager :MonoBehaviour{
	
	
	public JSONObject fuckdata;
	
	
	protected string _host = "http://mobile.coachparrot.com/";
	
	protected string _controller;
	protected string _method;

	void Start(){
		fuckdata = new JSONObject();

	}
	
	public IEnumerator SaveBestScore(string index, string email, string score)
	{
		
		Debug.Log("saving scroe:  "+email);
		
		string url = "http://mobile.coachparrot.com/best_score/add_my";
		
		// Create a form object for sending high score data to the server
		var form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		
		// The name of the player submitting the scores
		form.AddField( "index",  index);
		form.AddField( "email", email );
		form.AddField( "score",   score);
		
		// Create a download object
		var downloadbabe = new WWW( url, form );
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
	
	
	//login user
	
	public IEnumerator LoginUser(string email, string password)
	{
		
		Debug.Log("fucking:  "+email);
		
		string url = "http://mobile.coachparrot.com/login/run";
		
		// Create a form object for sending high score data to the server
		var form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField( "email", email );
		// The name of the player submitting the scores
		form.AddField( "password",  password);
		
		// Create a download object
		var downloadbabe = new WWW( url, form );
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
	}//end
	
	
	public IEnumerator RegUser(string email, string password, string password2, string username)
	{
		
		Debug.Log("fucking:  "+email);
		
		string url = "http://mobile.coachparrot.com/register/reg";
		
		// Create a form object for sending high score data to the server
		var form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField( "username", username );
		form.AddField( "email", email );
		// The name of the player submitting the scores
		form.AddField( "password",  password);
		form.AddField( "password2",  password2);
		
		// Create a download object
		var downloadbabe = new WWW( url, form );
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
	}
	
	
	
	
	
}//end server
