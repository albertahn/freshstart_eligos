using UnityEngine;
using System.Collections;

public class ButtonsEndGame : MonoBehaviour {

	public FriendDatabase friendDatabase;

	public EndGameManager endGameManager;

	public string my_index ;

	public string[] red_indexArray, blue_indexArray;

	// Use this for initialization
	void Start () {

		red_indexArray = new string[3];

		blue_indexArray= new string[3];

		friendDatabase = GetComponent<FriendDatabase> ();
		endGameManager = GetComponent<EndGameManager>();

		my_index = PlayerPrefs.GetString ("user_index");

	}


	public void click_red_friend1(){

		red_indexArray = endGameManager.red_index;

		Debug.Log (""+red_indexArray[0].ToString());

		//friendDatabase.sendFriendReq (my_index, );

	}

	public void click_red_friend2(){
		
		red_indexArray = endGameManager.red_index;
		
		Debug.Log (""+red_indexArray[1].ToString());
		
		//friendDatabase.sendFriendReq (my_index, );
		
	}

	public void click_red_friend3(){
		
		red_indexArray = endGameManager.red_index;
		
		Debug.Log (""+red_indexArray[2].ToString());
		
		//friendDatabase.sendFriendReq (my_index, );
		
	}

}
