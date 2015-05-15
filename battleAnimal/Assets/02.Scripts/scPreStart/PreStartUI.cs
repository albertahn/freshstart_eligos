using UnityEngine;
using System.Collections;

public class PreStartUI : MonoBehaviour {
	float x,y;
	float w,h;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString("email")!=""){
			Application.LoadLevel("scStart");
		}
		x = 10.0f;y = 10.0f;
		w = 100.0f;h = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(GUI.Button (new Rect(x,y,w,h),"Log In")){
			Application.LoadLevel("scLogIn");
		}
		if(GUI.Button (new Rect(x,y+h+10,w,h),"Registe ID")){
			Application.LoadLevel("scRegist");
		}
		if(GUI.Button (new Rect(x,y+2*(h+10),w,h),"Exit")){
			Application.Quit();
		}
	}
}
