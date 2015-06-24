using UnityEngine;
using System.Collections;

public class attackBuildingReceiver : MonoBehaviour {
	private bool switch_;
	private string building_name;
	private int building_hp_int;
	
	private MainFortress targetScript;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit());
			switch_=false;
		}	
	}
	
	public void receive(string data){
		string[] temp = data.Split (':');
		building_name = temp[0];
		building_hp_int = int.Parse(temp[1]);
		
		switch_ = true;
	}
	
	private IEnumerator doit(){
		targetScript = GameObject.Find (building_name).GetComponent<MainFortress>();
		targetScript.HeatedSync (building_hp_int);
		yield return null;
	}
}