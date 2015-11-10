using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moneyUI : MonoBehaviour {
	private Text money;
	private Text level;
	private Text kill;
	private Text death;
	
	// Use this for initialization
	void Start () {
		money = GameObject.Find ("moneyText").GetComponent<Text>();
		kill = GameObject.Find ("killText").GetComponent<Text>();
		death = GameObject.Find ("deathText").GetComponent<Text>();

		level = GameObject.Find ("levelText").GetComponent<Text>();

		money.text = ClientState.money.ToString();
		kill.text = ClientState.kill.ToString ();
		death.text = ClientState.death.ToString();
		level.text = ClientState.level.ToString ();
	}
	public void levelPrint(){		
		level.text = ClientState.level.ToString ();
	}
	
	public void makeMoney(int a){
		ClientState.money += a;
		money.text = ClientState.money.ToString();
	}

	public void deathPrint(){
		death.text = ClientState.death.ToString ();
	}

	public void killPrint(){
		kill.text = ClientState.kill.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}