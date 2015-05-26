using UnityEngine;
using System.Collections;

public class Skill_socket_reciever : MonoBehaviour {

	// Use this for initialization

	public bool firedskill;

	public string userID;

	public Vector3 newPos;
	public DogSkill_GUI dog_skill_gui;
	public Tutu_skill_gui tutu_skill;
	public string skillNumber;

	public string userCharacter;

	public FireSkill skillfire;
	public GameObject firedplayer;

	void Start () {
	
		firedskill = false;

	}
	
	// Update is called once per frame
	void Update () {
		//show the skill to other player if not mine

		if(firedskill ==true && userID != ClientState.id){

			firedplayer = GameObject.Find(userID);

			firedplayer.transform.LookAt(newPos);

			switch (userCharacter) {
			case "dog":
				dog_skill_gui  = GameObject.Find (userID).GetComponent<DogSkill_GUI>();		

				for (int i=0; i<3; i++){
					dog_skill_gui.skill_state[i] = true;
					dog_skill_gui.skill_live[i] = true;
				}
					
				break;
			case "turtle":
				tutu_skill = GameObject.Find (userID).GetComponent<Tutu_skill_gui>();	

				for (int i=0; i<3; i++){

					tutu_skill.skill_state[i] = true;
					tutu_skill.skill_live[i] = true;
				}
				break;
			}

		
			fireSkillNow();
		
			firedskill = false;
			Debug.Log ("fired skill: "+ userCharacter);
		}
	
	}//update

	public void skillShot(string data){

		string[] temp = data.Split(':');

		userID = temp[0];
		string[] resPos = temp[1].Split(',');
		newPos = new Vector3(float.Parse(resPos[0]),
		                     float.Parse(resPos[1]),
		                     float.Parse(resPos[2]));

		userCharacter = temp [2];
		skillNumber=  temp[3];



		Debug.Log("attack: skill " + userID+":"+userCharacter+":"+data );

		firedskill = true;

		Debug.Log ("data fied: "+firedskill);

	}//


	public void fireSkillNow(){

		Debug.Log ("fireSkillNow()");


		switch (skillNumber) {

		case"first":
				firstSkill();
				break;

		case"second":
				secondSkill();
				break;
				
		case"third":
				thirdSkill();
				break;
		
		}//skill number

		firedskill = true;

	}//end fireskill now


	public void firstSkill(){

		Debug.Log ("firstSkill()");

		switch (userCharacter) {
		case "dog":

			dog_skill_gui.fireFirst(firedplayer, newPos, userID);

			break;
		case "turtle":
			tutu_skill.firedFirst(firedplayer, newPos, userID); 	
			break;
		}

	}
	
	public  void secondSkill(){

		Debug.Log ("secSkill()");

		switch (userCharacter) {
		case "dog":
			dog_skill_gui.Skill2_bot();
			break;
		case "turtle":
			tutu_skill.Skill2_bot();
			break;
		}
	}
	
	public void thirdSkill(){

		Debug.Log ("firstSkill()");

		switch (userCharacter) {
		case "dog":
			dog_skill_gui.fireThird(firedplayer, newPos, userID); 		
			break;
		case "turtle":
			tutu_skill.firedThird(firedplayer, newPos, userID); 			
			break;
		}	
	}//third

}
