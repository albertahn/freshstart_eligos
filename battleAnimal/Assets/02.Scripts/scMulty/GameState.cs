using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public static string[] name;
	public static string[] team;
	public static int[] level;
	public static string[] items_array;
	public static string[] user_index_array;
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

		user_index_array = new string[6];
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
	} //awake 
	/*
	void OnGUI(){
		
		GUI.Label(new Rect(200,10,50,50),"id = "+ClientState.id);
		GUI.Label(new Rect(200,70,50,50),"room = "+ClientState.room);
		GUI.Label(new Rect(200,130,50,50),"order = "+ClientState.order);
		GUI.Label(new Rect(200,190,50,50),"character = "+ClientState.character);
		GUI.Label(new Rect(200,250,50,50),"team = "+ClientState.team);
		GUI.Label(new Rect(200,310,50,50),"isMaster = "+ClientState.isMaster);
	}
*/
	public static int search_by_name(string _name){
		
		for (int i=0; i<idx; i++) {
			if(name[i] ==_name)
				return i;
			
		}//fir
		
		return -1;
	}//search by name 
	
	//game ended
	
	//set user kills stat
	
	public static void setusers_cs_kills(string username, int cskills){
		int user_index = search_by_name (username);

		if(ClientState.isMulty)
			cs_kills_array[user_index] = cskills;
		else
			cs_kills_array[0] = cskills;

		/*Debug.Log ("stats bro! :"+ cs_kills_array[user_index].ToString() +"  "+ cs_kills_array.ToString());

		string data = ClientState.id + 
			             ":"+ClientState.members_index +
						":" + ClientState.level + 
						":" + ClientState.money+
						":"+ClientState.death+
						":"+ClientState.kill+":"+
						":"+ClientState.cs_kill
						;
		SocketStarter.Socket.Emit ("statSyncReq", data);*/

	}//

	public static void setuser_index_array(string username, string members_index){

		int index = search_by_name (username);

		if(ClientState.isMulty)
			user_index_array [index] = members_index;
		else
			user_index_array [0] = members_index;
	
	}//end
	
	
	public static void sendData(){

		senddatabool = true;
	}

	
	void Update(){
		
		if(senddatabool){

//			Debug.Log("ClientState.cs_kill: "+ ClientState.cs_kill);
			     

			if (ClientState.isMulty) {
			StartCoroutine (Stats_gameDatabase.SaveBestScore(ClientState.room.ToString(),
			                                                 ClientState.members_index.ToString(),
			                                                 ClientState.level.ToString(), // string level
			                                                 ClientState.items.ToString(),
			                                                 ClientState.kill.ToString(),
			                                                 ClientState.death.ToString(),
			                                                 ClientState.cs_kill.ToString(),
			                                                 ClientState.money.ToString(),
			                                                 ClientState.team.ToString(),
			                                                 ClientState.points.ToString())); 
			}

			
			senddatabool = false;
		}//end send bo
	}//end update
	
	
	
	
	
}