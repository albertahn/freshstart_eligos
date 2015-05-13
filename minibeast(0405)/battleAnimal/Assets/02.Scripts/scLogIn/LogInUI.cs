using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogInUI : MonoBehaviour {
	public string id,password,username;
	public InputField ID, PW;
	
	private DBManager _dbManager;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString("email")!=""){
			Application.LoadLevel("scStart");
		}
		Screen.SetResolution(480, 800, true);
		
		_dbManager = GetComponent<DBManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
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
	
	public void Cancel()
	{
		Application.Quit ();
		//Application.LoadLevel ("scPreStart");
	}

	private IEnumerator GetLoginData (string email, string password)
	{		
		yield return StartCoroutine (_dbManager.LoginUser(email, password)); // id를 Email로 바꿔야 하지 않을까
		
		string emailman = _dbManager.fuckdata.GetString ("password");
		
		//Debug.Log("mailman:  "+ password);		
		// LoginServer.hello ();
		//if (_server.data.ContainsKey ("character")) {		
		//Debug.Log("emailman : "+ _dbManager.fuckdata.GetString("email")) ;
		
		username = _dbManager.fuckdata.GetString("username");
		
		if(username !=""){			
			Debug.Log("loggedin fucker : "+_dbManager.fuckdata.GetString("email")) ;
			
			PlayerPrefs.SetString("email", _dbManager.fuckdata.GetString("email"));
			
			PlayerPrefs.SetString("username", _dbManager.fuckdata.GetString("username"));
			
			PlayerPrefs.SetString("user_index", _dbManager.fuckdata.GetString("index"));
			Application.LoadLevel ("scStart");
			
		}else{			
			Debug.Log("not logged in : ") ;
		}		
		
		yield return null;		
	}
}