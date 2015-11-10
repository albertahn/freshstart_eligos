using UnityEngine;
using System.Collections;

public class EnemyState : MonoBehaviour {
	public static string id;
	public static string character;
	public static string team;


	 void Awake(){
		id = "Enemy";
		character = "Stola";
		team = "blue";
	} 

	// Use this for initialization
	void Start () {
		id = "Enemy";
		character = "Stola";
		team = "blue";
	}//start 
	
	// Update is called once per frame
	void Update () {
	
	}//update
}
