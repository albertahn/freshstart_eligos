using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Boomlagoon.JSON;

public class EndGameManager : MonoBehaviour {



	public GameObject stat_row, pannel_content;

	private EndServerDatabase endServerDatabase;

	private FriendDatabase friendDatabase;

	public int itemCount = 10, columnCount = 1;

	public string my_index;


	RectTransform containerRectTransform ;

	int red_count, blue_count;

	// Use this for initialization
	void Start () {

		red_count = 1;
		blue_count = 1;

		my_index = PlayerPrefs.GetString ("user_index");

		endServerDatabase = GetComponent<EndServerDatabase> ();

		friendDatabase = GetComponent<FriendDatabase> ();

		StartCoroutine (GetStatData ("1"));//ClientState.room));

//		containerRectTransform = pannel_content.GetComponent<RectTransform>();
		
		//calculate the width and height of each child item.
		//float width = containerRectTransform.rect.width / columnCount;


		//int rowCount = itemCount / columnCount;






	
	}
	

	private IEnumerator GetStatData (string roomIndex)
	{		
		yield return StartCoroutine (endServerDatabase.getRoomStats(roomIndex)); // id를 Email로 바꿔야 하지 않을까

		//Debug.Log("myindex:  "+ PlayerPrefs.GetString("user_index"));	

		//containerRectTransform.sizeDelta = new Vector2( 20, 100*endServerDatabase.fuckArray.Length);

		for ( int i =0; i< endServerDatabase.fuckArray.Length; i ++){

			JSONObject jsonobj = JSONObject.Parse(endServerDatabase.fuckArray[i].ToString());



				setStatRow_red (i, 
			                jsonobj.GetString("members_index"),
				                jsonobj.GetString("profile_pic"),
				                jsonobj.GetString("team"),
				                jsonobj.GetString("kills"),
				                jsonobj.GetString("assists"),
				                jsonobj.GetString("deaths"),
				                jsonobj.GetString("gold"),
			                    jsonobj.GetString("level"),
			                    jsonobj.GetString("items"),
				                jsonobj.GetString("points") );

		}//end for 


		yield return null;		
	}


	public void setStatRow_red(int num,
	                           string members_index,
	                           string profile_pic,
	                           string team,
	                           string kills,
	                           string assists,
	                           string deaths,
	                           string gold,
	                           string level,
	                           string items,
	                           string points){


		if (team == "red") {
			GameObject newItem = GameObject.Find ("stat_row_red"+red_count);
			
			//newItem.transform.FindChild ("profile_tx").transform.GetComponent<Text> ().text = kills;
			newItem.transform.FindChild ("kills_tx").transform.GetComponent<Text> ().text = kills;
			newItem.transform.FindChild ("assists_tx").transform.GetComponent<Text> ().text = assists;
			newItem.transform.FindChild ("deaths_tx").transform.GetComponent<Text> ().text = deaths;
			newItem.transform.FindChild ("gold_tx").transform.GetComponent<Text> ().text = gold;
			newItem.transform.FindChild ("level_tx").transform.GetComponent<Text> ().text = level;
			newItem.transform.FindChild ("items_tx").transform.GetComponent<Text> ().text = items;
			newItem.transform.FindChild ("points_tx").transform.GetComponent<Text> ().text = points;

			StartCoroutine(checkFriend(newItem, members_index));

			red_count++;
		
		}else if (team =="blue"){
			GameObject newItem = GameObject.Find ("stat_row_blue"+blue_count);
			
			//newItem.transform.FindChild ("profile_tx").transform.GetComponent<Text> ().text = kills;
			newItem.transform.FindChild ("kills_tx").transform.GetComponent<Text> ().text = kills;
			newItem.transform.FindChild ("assists_tx").transform.GetComponent<Text> ().text = assists;
			newItem.transform.FindChild ("deaths_tx").transform.GetComponent<Text> ().text = deaths;
			newItem.transform.FindChild ("gold_tx").transform.GetComponent<Text> ().text = gold;
			newItem.transform.FindChild ("level_tx").transform.GetComponent<Text> ().text = level;
			newItem.transform.FindChild ("items_tx").transform.GetComponent<Text> ().text = items;
			newItem.transform.FindChild ("points_tx").transform.GetComponent<Text> ().text = points;

			 StartCoroutine(checkFriend(newItem, members_index));

			blue_count++;



		}


	}

	private IEnumerator checkFriend(GameObject statrow, string members_index){

		if (members_index == my_index) {

						statrow.transform.FindChild ("add_friend").localScale = new Vector2 (0, 0);
		

				} else {

				
			yield return StartCoroutine (friendDatabase.getfriendState(my_index, members_index)); // id를 Email로 바꿔야 하지 않을까
			
			Debug.Log("myindex:  "+ PlayerPrefs.GetString("user_index"));	
			
			//containerRectTransform.sizeDelta = new Vector2( 20, 100*endServerDatabase.fuckArray.Length);
			
			for ( int i =0; i< endServerDatabase.fuckArray.Length; i ++){
				
				JSONObject jsonobj = JSONObject.Parse(endServerDatabase.fuckArray[i].ToString());

				if(jsonobj.GetString("status")=="follow"){

					statrow.transform.FindChild ("already_follow").localScale = new Vector2 (5, 5);
					
					statrow.transform.FindChild ("add_friend").localScale = new Vector2 (0, 0);

				}else{

					statrow.transform.FindChild ("already_follow").localScale = new Vector2 (0, 0);

					statrow.transform.FindChild ("add_friend").localScale = new Vector2 (5, 5);

				}
				
			}//end for 
			
			
			yield return null;		
		
		}




	}



}
