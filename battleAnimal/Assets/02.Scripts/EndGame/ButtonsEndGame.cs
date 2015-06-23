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


	public void click_red_friend(int num){

		red_indexArray = endGameManager.red_index;

		switch (num)
		{
		case 3:
			Debug.Log (""+red_indexArray[2].ToString());

			StartCoroutine(add_friend(my_index, red_indexArray[2].ToString()));

			break;
		case 2:
			Debug.Log (""+red_indexArray[1].ToString());
			StartCoroutine(add_friend(my_index, red_indexArray[1].ToString()));

			break;
		case 1:
			Debug.Log (""+red_indexArray[0].ToString());
			StartCoroutine(add_friend(my_index, red_indexArray[0].ToString()));
			break;
		default:
			print ("Incorrect intelligence level.");
			break;
		}

		//friendDatabase.sendFriendReq (my_index, );

	}

	public void click_blue_friend(int num){
		
		blue_indexArray = endGameManager.blue_index;
		
		switch (num)
		{
		case 3:
			Debug.Log (""+blue_indexArray[2].ToString());

			StartCoroutine(add_friend(my_index, blue_indexArray[2].ToString()));

			break;
		case 2:
			Debug.Log (""+blue_indexArray[1].ToString());
			StartCoroutine(add_friend(my_index, blue_indexArray[1].ToString()));

			break;
		case 1:
			Debug.Log (""+blue_indexArray[0].ToString());
			StartCoroutine(add_friend(my_index, blue_indexArray[0].ToString()));


			break;
		default:
			print ("Incorrect intelligence level.");
			break;
		}
		
	}


	public IEnumerator add_friend(string my_index, string other_guy){

		yield return StartCoroutine (friendDatabase.sendfriendReq(my_index, other_guy)); // id를 Email로 바꿔야 하지 않을까

		Debug.Log ("workdam: "+ friendDatabase.returnObj.ToString());

		yield return null;	

	}


	public void backToProfile(){

		Application.LoadLevel ("scStart");

	}//


	public void backToRoom(){
		
		Application.LoadLevel ("scWait");
		
	}//


}
