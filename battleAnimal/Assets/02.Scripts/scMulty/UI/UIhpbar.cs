using UnityEngine;
using System.Collections;

public class UIhpbar : MonoBehaviour {

	public GameObject player, hpText;
	// Use this for initialization
	private bool switch_;

	void Start () {
	}

	public void setPlayer(){
		switch_ = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
						int hp = player.GetComponent<PlayerHealthState> ().hp;
						Vector3 temp = new Vector3 ((float)hp / playerStat.maxHp, 1, 1);
						GetComponent<RectTransform> ().localScale = temp;
						//this.transform.localScale = temp;
		
					//	hpText.GetComponent<TextMesh> ().text = "" + hp.ToString ();
		}
		if (switch_) {			
			player = GameObject.Find (ClientState.id);
			switch_=false;
		}		
	}
}
