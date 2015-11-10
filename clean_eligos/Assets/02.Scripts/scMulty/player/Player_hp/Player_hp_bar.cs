using UnityEngine;
using System.Collections;

public class Player_hp_bar : MonoBehaviour {

	public GameObject player, hpText;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		int hp = player.GetComponent<PlayerHealthState> ().hp;
		
		Vector3 temp = new Vector3 ((float)hp / playerStat.maxHp, 1, 1);
		if(hp<=0){
			hp=0;
			temp = new Vector3 (0, 1, 1);
		}
		this.transform.localScale = temp;
		
		hpText.GetComponent<TextMesh>().text = ""+hp.ToString();
		
	}
}
