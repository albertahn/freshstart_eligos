using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;
using System;
public class loginDatabase : MonoBehaviour {

	public LogInUI login_ui;
	public JSONObject fuckdata;

	public bool loginwell;
	// Use this for initialization
	void Start () {

		login_ui = GetComponent<LogInUI> ();
		loginwell = false;
	}
	
	public IEnumerator LoginUser(string email, string password)
	{
		
		//		Debug.Log("fucking:  "+email);
		
		string url = "http://mobile.sharebasket.com/login/run";
		
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField( "email", email );
		// The name of the player submitting the scores
		form.AddField( "password",  password);
		
		// Create a download object
		WWW downloadbabe = new WWW( url, form );
		// Wait until the download is done
		yield return downloadbabe;
		if(downloadbabe.error !=null) {
			Debug.Log( "Error downloading: " + downloadbabe.error );
			//return;
			loginwell = false;

		} else {
			// show the highscores

			if(downloadbabe.text =="error"){
				loginwell = false;

			}else{

				Debug.Log(downloadbabe.text);
				
				loginwell = true;
			}


//show the pannel for not logged in

			login_ui.loginerror = false;

			
		}
		
		//WWW www = new WWW (url);
		//yield return www;
		
		if (downloadbabe.size <= 2) {
			
			yield return null;
			
		} else {
			
			fuckdata = JSONObject.Parse(downloadbabe.text);
		}
	}//end
}
