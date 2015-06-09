using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public static string[] name;
	public static string[] team;
	public static int[] level;
	
	public static int idx;
	
	// Use this for initialization
	void Awake () {
		idx = 0;
		
		name = new string[6];
		team = new string[6];
		level = new int[6];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static int search_by_name(string _name){
		for (int i=0; i<idx; i++) {
			if(name[i] ==_name)
				return i;
		}
		
		return -1;
	}
}