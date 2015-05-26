using UnityEngine;
using System.Collections;

public class CannonSync_reciever : MonoBehaviour {


	public bool destroyTrue;

	public string cannonName ;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(destroyTrue){
			GameObject cannon = GameObject.Find(cannonName);
			Destroy(cannon);
			destroyTrue =false;
		}	
	}


	public void killCannon(string data){

		cannonName =data;

		destroyTrue = true;

	}
}
