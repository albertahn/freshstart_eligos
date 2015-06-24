using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public static string[] name;
	public static string[] team;
	public static int[] level;
	public static int[] kills_array;
	
	public static int idx;
	
	// Use this for initialization
	void Awake () {
		idx = 0;
		
		name = new string[6];
		team = new string[6];
		level = new int[6];

		kills_array = new int[6];
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

	public void setusers_kills(string username, int kills){

		int user_index = search_by_name (username);

		kills_array[user_index]= kills;

		Debug.Log (""+kills_array.ToString());

	}//





}