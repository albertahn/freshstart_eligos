using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegUI : MonoBehaviour {
	public string id,password,repassword,username;
	public InputField Email,PW,PW_check,Username;

	private DBManager _dbManager;
	// Use this for initialization
	void Start () {

		_dbManager = GetComponent<DBManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Signin() {
		StartCoroutine (RegLoginData((string)Email.text,(string)PW.text,(string)PW_check.text,(string)Username.text));
	}
	public void Cancel() {
		Application.LoadLevel ("scLogIn");
	}

	private IEnumerator RegLoginData (string email, string password, string password2, string username)
	{
		
		yield return StartCoroutine (_dbManager.RegUser(email, password,password2, username ));
		
		string emailman = _dbManager.fuckdata.GetString ("password");
		
		
		Debug.Log("emailman : "+ _dbManager.fuckdata.GetString("email")) ;
		
		username = _dbManager.fuckdata.GetString("username");
		
		if(username !=""){			
			Application.LoadLevel ("scPreStart");
			Debug.Log("loggedin fucker : "+_dbManager.fuckdata.GetString("email")) ;
			
		}else{
			
			Debug.Log("not logged in : "+_dbManager.fuckdata.GetString("email")) ;
		}	
		
		yield return null;	
		
	}
	
	

}