using UnityEngine;
using System.Collections;

public class BlueCannonHp : MonoBehaviour {
	public GameObject cannon, hpText;
	private int maxHP;
	
	// Use this for initialization
	void Start () {
		
		//hpText = GameObject.Find ("3_Hpval"); 
	}
	// Update is called once per frame
	void Update () {
		if (cannon != null) {
			
			maxHP = cannon.GetComponent<BlueCannonState> ().maxhp;
			int hp = cannon.GetComponent<BlueCannonState> ().hp;
			Vector3 temp = new Vector3 ((float)hp / maxHP, 1, 1);
			this.transform.localScale = temp;
			
			hpText.GetComponent<TextMesh>().text = ""+hp.ToString();
			
		}
		
	}
}
