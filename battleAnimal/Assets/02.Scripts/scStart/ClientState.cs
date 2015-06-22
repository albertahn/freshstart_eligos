using UnityEngine;
using System.Collections;

public class ClientState : MonoBehaviour {
	public static string id;
	public static bool isMaster;
	public static string room;
	public static int order;
	public static string character;
	public static string team;
	public static int kill,death,level;
	public static int exp;
	public static int money;
	public static string[] inventory;

	public static int[] maxExp;

	public static int skillPoint;

	// Use this for initialization
	void Start(){
		//PlayerPrefs.SetString ("email","aa");//not internet
		id = PlayerPrefs.GetString ("email");
		isMaster = false;
		exp = 0;
		level = 1;
		money = 0;
		skillPoint = 0;
		
		inventory = new string[6];
		maxExp = new int[7];
		maxExp [0] = 10;
		maxExp [1] = 30;
		maxExp [2] = 60;
		maxExp [3] = 1000;
		maxExp [4] = 1500;
		maxExp [5] = 2200;
		maxExp [6] = 3000;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public static void addInventory(string a,int idx){
		inventory [idx] = a.ToString();
	}
	void OnGUI(){
		
		GUI.Label(new Rect(200,10,50,50),"id = "+ClientState.id);
		GUI.Label(new Rect(200,70,50,50),"room = "+ClientState.room);
		GUI.Label(new Rect(200,130,50,50),"order = "+ClientState.order);
		GUI.Label(new Rect(200,190,50,50),"character = "+ClientState.character);
		GUI.Label(new Rect(200,250,50,50),"team = "+ClientState.team);
		GUI.Label(new Rect(200,310,50,50),"isMaster = "+ClientState.isMaster);
	}
}