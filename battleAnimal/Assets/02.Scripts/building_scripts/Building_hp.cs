using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Building_hp : MonoBehaviour {
	public GameObject player, hpText;
	private int maxHP;
	public Image imgHpbar;
	
	// Use this for initialization
	void Start () {
		maxHP = player.GetComponent<MainFortress> ().hp;
		
		//hpText = GameObject.Find ("3_Hpval"); 
	}
	// Update is called once per frame
	void Update () {
		if (player != null) {
			
			int hp = player.GetComponent<MainFortress> ().hp;
			imgHpbar.fillAmount = (float)hp/(float)maxHP;
			//Vector3 temp = new Vector3 ((float)hp / maxHP, 1, 1);
			
			//this.transform.localScale = temp;
			
			//hpText.GetComponent<TextMesh>().text = ""+hp.ToString();
			
		}
		
	}
}
