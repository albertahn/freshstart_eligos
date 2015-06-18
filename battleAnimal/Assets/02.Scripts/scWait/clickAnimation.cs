using UnityEngine;
using System.Collections;

public class clickAnimation : MonoBehaviour {

	public bool move_furfur, move_stola, move_gusi, move_barbas ;
	public GameObject furfur_btn, stola_btn, gusi_btn, barbas_btn ;


	float fracJourney = 4.5f;
	// Use this for initialization
	Vector3 newpos ;
	void Start () {

		move_furfur =move_stola = move_gusi= move_barbas= false;
	
		newpos = new Vector3 (200.0f, -75, 0);


	}
	
	// Update is called once per frame
	void Update () {
	
		if(move_furfur){

			furfur_btn.transform.localPosition = Vector3.MoveTowards(furfur_btn.transform.localPosition, newpos, fracJourney);

			Debug.Log("xhi: "+furfur_btn.transform.position.x); 
			//Debug.Log("yhi: "+chichibtn.transform.position.y); 
		
		}


		if(move_barbas){
			
			barbas_btn.transform.localPosition = Vector3.MoveTowards(barbas_btn.transform.localPosition, newpos, fracJourney);
			
			Debug.Log("xhi: "+barbas_btn.transform.position.x); 
			//Debug.Log("yhi: "+chichibtn.transform.position.y); 
			
		}

		if(move_stola){
			
			stola_btn.transform.localPosition = Vector3.MoveTowards(stola_btn.transform.localPosition, newpos, fracJourney);
			
			Debug.Log("xhi: "+barbas_btn.transform.position.x); 
			//Debug.Log("yhi: "+chichibtn.transform.position.y); 
			
		}

		if(move_gusi){
			
			gusi_btn.transform.localPosition = Vector3.MoveTowards(gusi_btn.transform.localPosition, newpos, fracJourney);
			
			Debug.Log("xhi: "+gusi_btn.transform.position.x); 
			//Debug.Log("yhi: "+chichibtn.transform.position.y); 
			
		}

//stops
		if(furfur_btn.transform.localPosition == newpos){
			move_furfur =false;
		}
		
		if(barbas_btn.transform.localPosition == newpos){

			move_barbas =false;
		}

		if(stola_btn.transform.localPosition == newpos){
			
			move_stola =false;
		}

		if(gusi_btn.transform.localPosition == newpos){
			
			move_gusi =false;
		}




	}//end update

	public void press_char_button(string charname){

		switch (charname)
		{
		case "furfur":

			move_barbas= move_gusi= move_stola =true;
			break;
		case "stola":

			move_barbas= move_gusi= move_furfur =true;
			break;
		case "gusi":

			move_barbas= move_furfur= move_stola =true;
			break;
		case "barbas":
			move_furfur = move_gusi= move_stola =true;
			break;
		default:

			break;
		}



	}




}
