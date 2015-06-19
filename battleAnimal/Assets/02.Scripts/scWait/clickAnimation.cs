using UnityEngine;
using System.Collections;

public class clickAnimation : MonoBehaviour
{

		public bool move_furfur, move_stola, move_gusi, move_barbas ;
		public GameObject furfur_btn, stola_btn, gusi_btn, barbas_btn ;
		public bool reset_char_point ;
		private Vector3[] originplace = new Vector3[4];
		public GameObject[] gameobj = new GameObject[4];
		float fracJourney = 14.5f;
		// Use this for initialization
		Vector3 newpos ;

	float nextUsage;

		void Start ()
		{

				//originplace = new Vector3[4];
				//gameobj = new GameObject[4];

				for (int i = 0; i < gameobj.Length; i++) {

						originplace [i] = gameobj [i].transform.localPosition;

				}

				move_furfur = move_stola = move_gusi = move_barbas = false;
	
		newpos = new Vector3 (500.0f, gameobj[0].transform.localPosition.y, 0);
				reset_char_point = false;

		}
	
		// Update is called once per frame
		void Update ()
		{
	
				if (move_furfur) {

						furfur_btn.transform.localPosition = Vector3.MoveTowards (furfur_btn.transform.localPosition, newpos, fracJourney);

						//Debug.Log ("xhi: " + furfur_btn.transform.position.x); 
			Debug.Log("yhi: "+furfur_btn.transform.position.y); 
			Debug.Log("movemomove_furfur :" +move_furfur);
		
				}


				if (move_barbas) {
			
						barbas_btn.transform.localPosition = Vector3.MoveTowards (barbas_btn.transform.localPosition, newpos, fracJourney);
			
						Debug.Log ("xhi: " + barbas_btn.transform.position.x); 
						//Debug.Log("yhi: "+chichibtn.transform.position.y); 
			Debug.Log("movemove_barbas :" +move_barbas);
				}

				if (move_stola) {
			
						stola_btn.transform.localPosition = Vector3.MoveTowards (stola_btn.transform.localPosition, newpos, fracJourney);
			
						Debug.Log ("xhi: " + stola_btn.transform.position.x); 
						//Debug.Log("yhi: "+chichibtn.transform.position.y); 
			Debug.Log("movemove_stola :" +move_stola);
				}

				if (move_gusi) {
			
						gusi_btn.transform.localPosition = Vector3.MoveTowards (gusi_btn.transform.localPosition, newpos, fracJourney);
			
						Debug.Log ("xhi: " + gusi_btn.transform.position.x); 
						//Debug.Log("yhi: "+chichibtn.transform.position.y); 

			Debug.Log("move move_gusi:" +move_gusi);
			
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



							for (int i = 0; i < gameobj.Length; i++) {
								//originplace [i] = gameobj [i].transform.position;

			    	gameobj [i].transform.localPosition = Vector3.MoveTowards (gameobj [i].transform.localPosition, originplace [i], fracJourney);
								
							}//for

			StartCoroutine(delayedResetfalse());

				}//end if


		}//end update

		public void press_char_button (string charname)
		{

				switch (charname) {
				case "furfur":

						move_barbas = move_gusi = move_stola = true;
						break;
				case "stola":

						move_barbas = move_gusi = move_furfur = true;
						break;
				case "gusi":

						move_barbas = move_furfur = move_stola = true;
						break;
				case "barbas":
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


	IEnumerator delayedResetfalse(){

		yield return new WaitForSeconds(1.0f);

		reset_char_point = false;

		Debug.Log ("hi");

	}


}
