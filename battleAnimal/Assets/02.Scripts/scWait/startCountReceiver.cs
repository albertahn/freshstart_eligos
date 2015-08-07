using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startCountReceiver : MonoBehaviour {
	private bool switch_;
	private int time;
	private GameObject TimeObj;
	public Text timeText;

	string temp;

	// Use this for initialization
	void Start () {
		time = 3;//10;
		switch_ = false;
		TimeObj = (GameObject)GameObject.Find ("Canvas/Time");
		TimeObj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit ());
			switch_=false;
		}	
	}
	public void receive(string data){
		//string[] temp = data.Split (':');
		temp = data;

		switch_ = true;
	}
	
	private IEnumerator doit(){
		starterInfo.info = temp;
		StartCoroutine (TimeCheck ());
		yield return null;
	}

	private IEnumerator TimeCheck(){
		TimeObj.SetActive (true);
		timeText = GameObject.Find ("Canvas/Time/waitTime").GetComponent<Text> ();
		while (true) {
			yield return new WaitForSeconds (1.0f);
			time--;
			timeText.text = time.ToString();
			if(time<=0){
				Application.LoadLevel("scMulty");
				break;
			}
		}
	}
}
