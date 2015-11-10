using UnityEngine;
using System.Collections;

public class blue_minion_HP : MonoBehaviour {
	
	public GameObject minion, hpText;
	private int maxHP;
	// Use this for initialization
	void Start () {
		
		maxHP = minion.GetComponent<blue_minion_state> ().hp;
	}
	
	// Update is called once per frame
	void Update () {
		if (minion != null) {
			
			int hp = minion.GetComponent<blue_minion_state> ().hp;
			Vector3 temp = new Vector3 ((float)hp / maxHP, 1, 1);
			if(hp<=0){
				hp=0;
				temp = new Vector3 (0, 1, 1);
			}
			this.transform.localScale = temp;			
			hpText.GetComponent<TextMesh>().text = ""+hp.ToString();			
		}
	}
}

