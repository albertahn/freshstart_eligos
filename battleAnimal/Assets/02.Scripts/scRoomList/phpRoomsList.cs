using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;
using UnityEngine.UI;
public class phpRoomsList : MonoBehaviour {

	JSONObject  availRooms;

	public GameObject roomRow, pannel_content;

	// Use this for initialization
	void Start () {
	
		 StartCoroutine (GetAvailRoomData());

	}//end start
	
	private IEnumerator GetAvailRoomData ()
	{		
		string url = "http://mobile.sharebasket.com/room/get_available_rooms/";

		// Create a download object
		WWW downloadbabe = new WWW( url );
		// Wait until the download is done
		yield return downloadbabe;
		if(downloadbabe.error !=null) {
			
			Debug.Log( "Error downloading: " + downloadbabe.error );
			//return;
		} else {
			// show the highscores
			Debug.Log(downloadbabe.text);
		}
		
		if (downloadbabe.size <= 2) {
			
			yield return null;
			
		} else {

			availRooms = JSONObject.Parse(downloadbabe.text);

			string[] temp2 = downloadbabe.text.Split(',');

			for ( int i =0; i < temp2.Length; i ++){

				//Debug.Log("temp2: "+temp2[i]);
				//JSONObject jsonobj = JSONObject.Parse(availRooms[i].ToString());
				//setFriendRow (i, jsonobj.GetString("friend_username"),jsonobj.GetString("friend_profile_pic"),jsonobj.GetString("other_friend_index"));
			  
				temp2[i] = temp2[i].Replace("}", "");
				temp2[i] = temp2[i].Replace("{", "");

				setFriendRow(i, temp2[i]);

			}//end for


		} //end else

	}//end get friend


	public void setFriendRow(int num, string bracketlessString){

		//Debug.Log ("bracketlessString: "+bracketlessString);

		string[] roomstring  = bracketlessString.Split(':');

		GameObject newItem = Instantiate(roomRow) as GameObject;
		float itemheight = newItem.GetComponent<RectTransform>().rect.height;
		
	//	Debug.Log ("height: "+itemheight);
		//newItem.GetComponent<RectTransform>().rect.height;
		
		//GameObject pannelContent = Instantiate(pannel_content) as GameObject;
		
		newItem.transform.FindChild ("roomname_tx").transform.GetComponent<Text> ().text ="Room Number: "+ roomstring[0];
		newItem.transform.FindChild ("enterRoom_btn").transform.GetComponent<Button> ().onClick.AddListener(() => { 

			ClientState.room = roomstring[1];
			//Debug.Log("roomstring:"+ roomstring[0]);
			Application.LoadLevel("scWait");

		}); 

		
		newItem.transform.position = new Vector3 (0, -num * itemheight, 0);
		
		//newItem.transform.localPosition = new Vector3 (0,0, 0);//num *itemheight
		//newItem.name =   "friend"+friend_index;
		
		//pannel_content.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 20*num * itemheight);
		newItem.transform.SetParent (pannel_content.transform,false);
		
	}//end public
}
