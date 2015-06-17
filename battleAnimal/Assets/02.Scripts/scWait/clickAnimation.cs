using UnityEngine;
using System.Collections;

public class clickAnimation : MonoBehaviour {

	public bool moveChiChi;
	public GameObject chichibtn, stola ;


	float fracJourney = 4.5f;
	// Use this for initialization
	Vector3 newpos ;
	void Start () {

		moveChiChi = true;
		newpos = new Vector3 (100.0f, -75, 0);
		chichibtn = GameObject.Find ("Dog");

	}
	
	// Update is called once per frame
	void Update () {
	
		if(moveChiChi){

			chichibtn.transform.localPosition = Vector3.MoveTowards(chichibtn.transform.localPosition, newpos, fracJourney);

			Debug.Log("xhi: "+chichibtn.transform.position.x); 
			Debug.Log("yhi: "+chichibtn.transform.position.y); 
		
		}

		if(chichibtn.transform.localPosition == newpos){
			moveChiChi =false;
		}



	}//end update




}
