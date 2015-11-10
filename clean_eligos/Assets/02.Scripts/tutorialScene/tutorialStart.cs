using UnityEngine;
using System.Collections;

public class tutorialStart : MonoBehaviour {

	// Use this for initialization

	void Awake(){
		ClientState.character = "guci";	
		ClientState.id= "tutorialchar";
		ClientState.team ="red";
		EnemyState.character = "stola";


	}//awake 

	void Start () {
	
	}//start
	
	// Update is called once per frame
	void Update () {
	
	}
}
