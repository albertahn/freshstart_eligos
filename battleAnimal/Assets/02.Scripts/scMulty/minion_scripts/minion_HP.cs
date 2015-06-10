using UnityEngine;
using System.Collections;

public class minion_HP : MonoBehaviour {

	public GameObject minion, hpText;
	private int maxHP;
	// Use this for initialization
	void Start () {

		maxHP = minion.GetComponent<minion_state> ().hp;
	}
	
	// Update is called once per frame
	void Update () {
		if (minion != null) {
			
			int hp = minion.GetComponent<minion_state> ().hp;
			Vector3 temp = new Vector3 ((float)hp / maxHP, 1, 1);
			this.transform.localScale = temp;
			
//			hpText.GetComponent<TextMesh>().text = ""+hp.ToString();
			
		}
	}
}
