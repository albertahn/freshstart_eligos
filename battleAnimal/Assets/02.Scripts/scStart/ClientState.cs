using UnityEngine;
using System.Collections;

public class ClientState : MonoBehaviour {
	public static string id;
	public static bool isMaster;
	public static string room;
	public static int order;
	public static string character;
	public static string team;
	public static int kill, death, level, cs_kill;
	public static string members_index, items;
	public static int exp;
	public static int money, points;
	public static string[] inventory;

	public static bool isMulty;

	public static int[] maxExp;

	public static int skillPoint;

	// Use this for initialization
	void Awake(){
		id = PlayerPrefs.GetString ("email");

		members_index = PlayerPrefs.GetString ("user_index");

		level = 1;
		money = 0;
		points = 0;
		items = "";
		death = 0;

		isMulty = false;

		isMaster = false;
		exp = 0;



		skillPoint = 0;
		
		inventory = new string[6];
		maxExp = new int[7];
		maxExp [0] = 100;
		maxExp [1] = 300;
		maxExp [2] = 600;
		maxExp [3] = 1000;
		maxExp [4] = 1500;
		maxExp [5] = 2200;
		maxExp [6] = 3000;
	}

	public static void addInventory(string a,int idx){
		inventory [idx] = a.ToString();
	}
}