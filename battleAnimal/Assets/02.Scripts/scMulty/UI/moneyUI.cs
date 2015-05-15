using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moneyUI : MonoBehaviour {
	private Text money;
	private Text level;
	
	// Use this for initialization
	void Start () {
		money = GameObject.Find ("moneyText").GetComponent<Text>();
		level = GameObject.Find ("levelText").GetComponent<Text>();
		money.text = ClientState.money.ToString();
		level.text = ClientState.level.ToString ();
	}
	public void levelPrint(){		
		level.text = ClientState.level.ToString ();
	}
	
	public void makeMoney(int a){
		ClientState.money += a;
		money.text = ClientState.money.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}