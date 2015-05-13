using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moneyUI : MonoBehaviour {
	private Text money;

	// Use this for initialization
	void Start () {
		money = GameObject.Find ("moneyText").GetComponent<Text>();
		money.text = ClientState.money.ToString();
	}

	public void makeMoney(int a){
		ClientState.money += a;
		money.text = ClientState.money.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}