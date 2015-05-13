using UnityEngine;
using System.Collections;

public class GUI_rooms : MonoBehaviour {

	private float hScrollbarValue;
	public Vector2 scrollPos = Vector2.zero;

	public bool gotString;

	//public string allrooms = roomSocketOn.roomstring;

	public  string innertext;

	// Use this for initialization
	void Start () {

		gotString = false;

		//innertext = this.GetComponent<roomSocketOn> ().roomstring;

	}



	void OnGUI (){

		innertext = this.GetComponent<roomSocketOn> ().roomstring;

		if (innertext != null) {
				
			gotString = true;
		}


		scrollPos = GUI.BeginScrollView (new Rect(100,150,300,420), scrollPos, new Rect (0,0,190,400));

		//innertext  =  GUI.TextArea (new Rect(0,0,200,200), innertext);

		string[] temp2 = innertext.Split(',');


		if (gotString == true) {
						for (int i=0; i<temp2.Length; i++) {
								//Debug.Log("hi: "+temp2[i].ToString());

								string[] roomarray = temp2 [i].ToString ().Split (':');

				roomarray [0] = roomarray [0].Replace("\"", "");

								if (GUI.Button (new Rect (50, 20 + 50 * i, 130, 30), roomarray [0])) {

										ClientState.room = roomarray [0];

										Application.LoadLevel ("scWait");
				
								}//doanloaded image
			
						}//for

			gotString = false;

				}//geot string



		GUI.EndScrollView ();

	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
