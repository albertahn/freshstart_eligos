using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;
using UnityEngine.UI;
public class friendList : MonoBehaviour {
	
	public GameObject friend_row;
	
	private GameObject  pannel_content, followers_content;
	private FriendDatabase friendDatabase;
	 public string myIndex ;



	// Use this for initialization
	void Start () {

		friendDatabase = GetComponent<FriendDatabase> ();

		myIndex = PlayerPrefs.GetString ("user_index");
		StartCoroutine (GetFriendData(myIndex));

		StartCoroutine (GetFollowersData(myIndex));
	
		pannel_content = GameObject.Find ("Friend_inside_pannel");

		followers_content = GameObject.Find ("followers_inside_pannel");
	
	}//end start


	private IEnumerator GetFriendData (string myIndex)
	{		

		yield return StartCoroutine (friendDatabase.getFriendList(myIndex)); // id를 Email로 바꿔야 하지 않을까
		
		Debug.Log("friends:  "+ friendDatabase.myFriendsList);	

		for ( int i =0; i< friendDatabase.myFriendsList.Length; i ++){
			
			JSONObject jsonobj = JSONObject.Parse(friendDatabase.myFriendsList[i].ToString());
			
			setFriendRow (i, jsonobj.GetString("friend_username"),jsonobj.GetString("friend_profile_pic"),jsonobj.GetString("other_friend_index"));

		}

		yield return null;		
	}//end get friend

	private IEnumerator GetFollowersData (string myIndex){
		
		yield return StartCoroutine (friendDatabase.getFriendList(myIndex)); // id를 Email로 바꿔야 하지 않을까
		
		Debug.Log("friends:  "+ friendDatabase.myFriendsList);	
		
		for ( int i =0; i< friendDatabase.myFriendsList.Length; i ++){
			
			JSONObject jsonobj = JSONObject.Parse(friendDatabase.myFriendsList[i].ToString());
			
			setFollower (i, jsonobj.GetString("friend_username"),jsonobj.GetString("friend_profile_pic"),jsonobj.GetString("other_friend_index"));
			
		}
		
		yield return null;	
	
	} //string myIndex

	public void setFriendRow(int num, string friend_username, string friend_profile_pic, string friend_index){


		GameObject newItem = Instantiate(friend_row) as GameObject;
		float itemheight = newItem.GetComponent<RectTransform>().rect.height;

		Debug.Log ("height: "+itemheight);
		//newItem.GetComponent<RectTransform>().rect.height;

		//GameObject pannelContent = Instantiate(pannel_content) as GameObject;

		newItem.transform.FindChild ("username_tx").transform.GetComponent<Text> ().text = friend_username;
		newItem.transform.FindChild ("profile_pic_tx").transform.GetComponent<Text> ().text = ""+ num;
		
		//Debug.Log ("len"+kills_tx.GetComponent<Text>());
		//textmesh[0].text = "hi";

		//statrow.transform.FindChild ("add_friend").localScale = new Vector2 (0, 0);
		//newItem.transform.localScale = new Vector2 (1, 1);

		newItem.transform.position = new Vector3 (0,- num * itemheight, 0);

		//newItem.transform.localPosition = new Vector3 (0,0, 0);//num *itemheight
		newItem.name =   "friend"+friend_index;

		//pannel_content.GetComponent<RectTransform>().sizeDelta = new Vector2(2, 20*num * itemheight);

		newItem.transform.SetParent (pannel_content.transform,false);
	}



	public void setFollower(int num, string friend_username, string friend_profile_pic, string friend_index){

		GameObject newItem = Instantiate(friend_row) as GameObject;

		float itemheight = newItem.GetComponent<RectTransform>().rect.height;
		
		Debug.Log ("height: "+itemheight);
		//newItem.GetComponent<RectTransform>().rect.height;
		
		//GameObject pannelContent = Instantiate(pannel_content) as GameObject;
		
		newItem.transform.FindChild ("username_tx").transform.GetComponent<Text> ().text = friend_username;
		newItem.transform.FindChild ("profile_pic_tx").transform.GetComponent<Text> ().text = ""+ num;
		
		//Debug.Log ("len"+kills_tx.GetComponent<Text>());
		//textmesh[0].text = "hi";
		
		//statrow.transform.FindChild ("add_friend").localScale = new Vector2 (0, 0);
		//newItem.transform.localScale = new Vector2 (1, 1);
		
		newItem.transform.position = new Vector3 (0,- num * itemheight, 0);
		
		//newItem.transform.localPosition = new Vector3 (0,0, 0);//num *itemheight
		newItem.name =   "friend"+friend_index;
		
		//pannel_content.GetComponent<RectTransform>().sizeDelta = new Vector2(2, 20*num * itemheight);
		
		newItem.transform.SetParent (followers_content.transform,false);
	
	}


}
