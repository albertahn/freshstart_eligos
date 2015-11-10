using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlueCannonHp : MonoBehaviour {
	public GameObject cannon, hpText;
	public int maxHP;
	
	// Use this for initialization
	void Start () {		
		maxHP = cannon.GetComponent<BlueCannonState> ().maxhp;		
		//hpText = GameObject.Find ("3_Hpval"); 
	}
	// Update is called once per frame
	void Update () {
		int hp = cannon.GetComponent<BlueCannonState> ().hp;
		Vector3 temp = new Vector3 ((float)hp / maxHP, 1, 1);
		this.transform.localScale = temp;		
	}
}
