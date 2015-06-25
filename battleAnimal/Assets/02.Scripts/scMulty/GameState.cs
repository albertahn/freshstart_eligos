using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	/*
SaveBestScore(
											string rooms_index,
											string members_index,
	                                        string level,
	                                        string items,
											string kills, 
	                                        string deaths,
	                                        string cs_kills,
	                                        string gold,
											string team,
											string points)
			 */
	
	public static string[] name;
	public static string[] team;
	public static int[] level;
	public static string[] items_array;
	public static int[] kills_array;
	public static int[] deaths_array;
	public static int[] cs_kills_array;
	public static int[] gold_array;
	public static int[] teams_array;
	public static int[] points_array;
	
	public static int idx;
	
	public Stats_gameDatabase statsgamedb;
	
	public static bool senddatabool;
	
	// Use this for initialization
	void Awake () {
		idx = 0;
		
		name = new string[6];
		team = new string[6];
		level = new int[6];
		items_array = new string[6];
		kills_array = new int[6];

		deaths_array = new int[6];
		cs_kills_array = new int[6];

		gold_array = new int[6];
		teams_array = new int[6];
		points_array = new int[6];
		
		statsgamedb = GetComponent<Stats_gameDatabase> ();
	}
	
	
	public static int search_by_name(string _name){
		
		for (int i=0; i<idx; i++) {
			if(name[i] ==_name)
				return i;
			
		}//fir
		
		return -1;
	}//search by name 
	
	//game ended
	
	//set user kills stat
	
	public static void setusers_kills(string username, int cskills){
		
		int user_index = search_by_name (username);
		kills_array[user_index] = cskills;
		
		
		Debug.Log ("stats bro! :"+kills_array[user_index].ToString()+"  "+kills_array.ToString());
		
		
		
	}//
	
	
	public static void sendData(){
		
		senddatabool = true;
	}
	
	void Update(){
		
		if(senddatabool){
			/*
SaveBestScore(
											string rooms_index,
											string members_index,
	                                        string level,
	                                        string items,
											string kills, 
	                                        string deaths,
	                                        string cs_kills,
	                                        string gold,
											string team,
											string points)
			 */

			
			StartCoroutine (Stats_gameDatabase.SaveBestScore("1",
			                                                 PlayerPrefs.GetString("user_index"),
			                                                 ClientState.level.ToString(), // string level
			                                                 "items",
			                                                 "123",
			                                                 "4555",
			                                                 "1231",
			                                                 "88888",
			                                                 "red",
			                                                 "12123")); 
			
			senddatabool = false;
		}
	}
	
	
	
	
	
}