using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Building_hp : MonoBehaviour {
	public GameObject building, hpText;
	private int maxHP;
	
	// Use this for initialization
	void Start () {
		maxHP = building.GetComponent<MainFortress> ().hp;		
		//hpText = GameObject.Find ("3_Hpval"); 
	}
	// Update is called once per frame
	void Update () {
		if (building != null) {			
			int hp = building.GetComponent<MainFortress> ().hp;
			Vector3 temp = new Vector3 ((float)hp / playerStat.maxHp, 1, 1);
			this.transform.localScale = temp;			
		}
		
	}
}
