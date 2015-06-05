using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class startUI : MonoBehaviour {

	public Text UserName, Userlevel, Gold, Cash;
	
	// Use this for initialization
	void Start () {
		Screen.SetResolution(800, 480, true);
		//PlayerPrefs.SetString ("email","aa");//not internet
		
		UserName.text = PlayerPrefs.GetString("username");
		Userlevel.text = PlayerPrefs.GetString ("userlevel");
		Gold.text = PlayerPrefs.GetString ("gold");
		Cash.text = PlayerPrefs.GetString ("cash");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Multy()
	{
		//Application.LoadLevel("scWait");
	
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
	}
	
}
