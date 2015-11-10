using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class LogInUI : MonoBehaviour {
	public string id,password,username;
	public InputField ID, PW;	
	private loginDatabase _dbManager;

	public bool loginerror;
	
	private GameObject guestID_pannel;
	private Vector3 inCamPos, outCamPos;


	// Use this for initialization
	void Start () {		
		guestID_pannel = GameObject.Find ("guestID_pannel");		
		inCamPos = new Vector3 (-50,guestID_pannel.transform.localPosition.y,guestID_pannel.transform.position.z );
		outCamPos =  new Vector3 (778,guestID_pannel.transform.localPosition.y,guestID_pannel.transform.position.z );

		loginerror = false;
		if(PlayerPrefs.GetString("email")!=""){

			Application.LoadLevel("scStart");

		}
		Screen.SetResolution(1280, 800, true);
		//Screen.SetResolution(800, 480, true);
		
		guestID_pannel.transform.localPosition = outCamPos;
		_dbManager = GetComponent<loginDatabase> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(loginerror){

			Debug.Log("error in login");


		}

	}

	IEnumerator PlaySfx(AudioClip _clip){
		audio.PlayOneShot (_clip, 0.9f);
		yield return null;
	}

	public void Login()
	{
		id = (string)ID.text;
		password = (string)PW.text;
		StartCoroutine (GetLoginData((string)ID.text,(string)PW.text));
	}

	public void Join()
	{
		Application.LoadLevel ("scRegist");
	}

	public void openGuestID(){
		guestID_pannel.transform.localPosition = inCamPos;
	}
	public void closeGuestID(){
		guestID_pannel.transform.localPosition = outCamPos;
	}

	public void loginPlayer1(){
		PlayerPrefs.SetString("username","player1");
		Application.LoadLevel ("scStart");
	}

	public void loginPlayer2(){
		PlayerPrefs.SetString("username","player2");
		Application.LoadLevel ("scStart");
	}
	
	public void loginObserver(){
		PlayerPrefs.SetString("username","ob");
		Application.LoadLevel ("scStart");
	}

	public void Cancel()
	{
		Application.Quit ();
		//Application.LoadLevel ("scPreStart");
	}

	private IEnumerator GetLoginData (string email, string password)
	{		
		yield return StartCoroutine (_dbManager.LoginUser(email, password)); // id를 Email로 바꿔야 하지 않을까


		//_dbManager.fuckdata.GetString ("");
		//try
		//{

		 


 		   // username = _dbManager.fuckdata.GetString("username");
		
		if(_dbManager.loginwell ==true){

			Debug.Log("loggedin fucker : "+_dbManager.fuckdata.GetString("email")) ;
			
			PlayerPrefs.SetString("email", _dbManager.fuckdata.GetString("email"));
			
			PlayerPrefs.SetString("username", _dbManager.fuckdata.GetString("username"));
			
			PlayerPrefs.SetString("user_index", _dbManager.fuckdata.GetString("index"));

			PlayerPrefs.SetString("gold", _dbManager.fuckdata.GetString("gold"));
			PlayerPrefs.SetString("cash", _dbManager.fuckdata.GetString("cash"));
			PlayerPrefs.SetString("userlevel", _dbManager.fuckdata.GetString("level"));
			PlayerPrefs.SetString("language", _dbManager.fuckdata.GetString("language"));

			Application.LoadLevel ("scStart");
			
		}else{	

			loginerror = true;

			Debug.Log("not logged in : ") ;
		}		
		


		//}catch (Exception e) 
	//	{
		//	/
	//	}//try
		yield return null;	
	}
	
}