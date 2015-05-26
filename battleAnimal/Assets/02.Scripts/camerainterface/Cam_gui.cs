using UnityEngine;
using System.Collections;

public class Cam_gui : MonoBehaviour {


	//public string url = "http://tanggoal.com/public/uploads/members_pic/3b617cd38f01d9b1cbb6737f24cf881a.jpg";
	//public WWW www;
	Texture2D tex;
	Texture profiletexture;
	Texture2D texTmp ;

	// Use this for initialization
	void Start () {

		Screen.SetResolution(480, 800, true);


	//	www= new WWW(url);


		
	}


	void OnGUI(){
		
				if (GUI.Button (new Rect (10, 0, 50, 30), "Exit")) {


			Application.LoadLevel ("scStart");
			
				}
		//www= new WWW(url);
	/*	if (www.isDone == true) {
			texTmp = new Texture2D (200, 200);
			www.LoadImageIntoTexture (texTmp);
		}

				
			if (GUI.Button (new Rect (100, 20, 80, 80), texTmp)) {
			
				

		}//doanloaded image
*/



		}//end gui


	/*public IEnumerator showProfilePic(){

		WWW www = new WWW(url);
		yield return www;
		renderer.material.mainTexture = www.texture;

	}*/
	
	// Update is called once per frame
	void Update () {
	
	}





}
