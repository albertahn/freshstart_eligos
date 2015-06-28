using UnityEngine;
using System.Collections;

public class clickAnimation : MonoBehaviour
{
	
	public bool move_furfur, move_stola, move_gusi, move_barbas ;
	public GameObject furfur_btn, stola_btn, gusi_btn, barbas_btn ;
	public bool reset_char_point ;
	private Vector3[] originplace = new Vector3[4];
	public GameObject[] gameobj = new GameObject[4];
	float fracJourney = 24.5f;
	// Use this for initialization
	Vector3 newpos, showingpos ;
	
	
	float nextUsage;
	
	void Start ()
	{
		
		//originplace = new Vector3[4];
		//gameobj = new GameObject[4];
		
		for (int i = 0; i < gameobj.Length; i++) {
			
			originplace [i] = gameobj [i].transform.localPosition;
			
		}
		
		move_furfur = move_stola = move_gusi = move_barbas = false;
		
		newpos = new Vector3 (1000.0f, gameobj[0].transform.localPosition.y, 0);
		reset_char_point = false;
		
		showingpos =  new Vector3 (0f, gameobj[0].transform.localPosition.y, 0);
		reset_char_point = false;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (move_furfur) {
			
			furfur_btn.transform.localPosition = Vector3.MoveTowards (furfur_btn.transform.localPosition, newpos, fracJourney);
			
			//Debug.Log ("xhi: " + furfur_btn.transform.position.x); 
			//Debug.Log("yhi: "+furfur_btn.transform.position.y); 
//			Debug.Log("movemomove_furfur :" +move_furfur);
			
		}
		
		
		if (move_barbas) {
			
			barbas_btn.transform.localPosition = Vector3.MoveTowards (barbas_btn.transform.localPosition, newpos, fracJourney);
			
			//Debug.Log ("xhi: " + barbas_btn.transform.position.x); 
			///Debug.Log("yhi: "+chichibtn.transform.position.y); 
			//Debug.Log("movemove_barbas :" +move_barbas);
		}
		
		if (move_stola) {
			
			stola_btn.transform.localPosition = Vector3.MoveTowards (stola_btn.transform.localPosition, newpos, fracJourney);
			
		//	Debug.Log ("xhi: " + stola_btn.transform.position.x); 
			//Debug.Log("yhi: "+chichibtn.transform.position.y); 
		//	Debug.Log("movemove_stola :" +move_stola);
		}
		
		if (move_gusi) {
			
			gusi_btn.transform.localPosition = Vector3.MoveTowards (gusi_btn.transform.localPosition, newpos, fracJourney);
			
		//	Debug.Log ("xhi: " + gusi_btn.transform.position.x); 
			//Debug.Log("yhi: "+chichibtn.transform.position.y);
			
		//	Debug.Log("move move_gusi:" +move_gusi);
			
		}
		
		//stops
		if (furfur_btn.transform.localPosition == newpos) {
			move_furfur = false;
		}
		
		if (barbas_btn.transform.localPosition == newpos) {
			
			move_barbas = false;
		}
		
		if (stola_btn.transform.localPosition == newpos) {
			
			move_stola = false;
		}
		
		if (gusi_btn.transform.localPosition == newpos) {
			
			move_gusi = false;
		}
		
		//if reset char place
		
		if (reset_char_point) {

			////no move other chars
			move_furfur=  move_barbas = move_gusi = move_stola = false;

			
			for (int i = 0; i < gameobj.Length; i++) {
				//originplace [i] = gameobj [i].transform.position;
				
				gameobj [i].transform.localPosition = Vector3.MoveTowards (gameobj [i].transform.localPosition, originplace [i], fracJourney);
				
				
			}//for
			//reset_char_point = false;
			//	StartCoroutine(delayedResetfalse());
			
		}//end if
		//for all back in place
		
		if(gameobj [0].transform.localPosition==originplace [0]
		   &&gameobj [1].transform.localPosition==originplace [1]
		   &&gameobj [2].transform.localPosition==originplace [2]
		   &&gameobj [3].transform.localPosition==originplace [3]
		   ){
			
			reset_char_point = false;
			
		}
		
	}//end update
	
	public void press_char_button (string charname)
	{
		
		switch (charname) {
		case "furfur":
			
			gameobj[0].transform.localPosition = showingpos;
			
			move_barbas = move_gusi = move_stola = true;
			break;
		case "stola":
			
			gameobj[1].transform.localPosition = showingpos;
			
			move_barbas = move_gusi = move_furfur = true;
			break;
		case "gusi":
			
			gameobj[2].transform.localPosition = showingpos;
			
			move_barbas = move_furfur = move_stola = true;
			break;
		case "barbas":
			
			gameobj[3].transform.localPosition = showingpos;
			move_furfur = move_gusi = move_stola = true;
			break;
		default:
			
			break;
		}
		
		
	}//press char button
	
	public void reset_char_place ()
	{
		
		reset_char_point = true;
		
		
	}
	
	

	
	
}