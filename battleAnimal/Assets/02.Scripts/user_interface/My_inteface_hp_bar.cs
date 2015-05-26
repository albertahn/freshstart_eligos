using UnityEngine;
using System.Collections;

public class My_inteface_hp_bar : MonoBehaviour {
	public GameObject player, hpText;
	private int maxHP;
	// Use this for initialization
	
	private int maxBarLength, currentBarLength;
	private GUITexture display;
	
	void Start () {
		
		display = this.GetComponent<GUITexture> ();
		maxBarLength = (int) this.GetComponent<GUITexture>().pixelInset.width; 
		
		
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find (PlayerPrefs.GetString("email"));
		if (player!= null) {
			
			maxHP = playerStat.maxHp;
			
			//Debug.Log("myhp: "+maxHP);
			
			int hp = player.GetComponent<PlayerHealthState> ().hp;
			
			currentBarLength = (int) maxBarLength* hp / maxHP;
			
			this.GetComponent<GUITexture>().pixelInset = new Rect(display.pixelInset.x, display.pixelInset.y, currentBarLength , display.pixelInset.height );
			
			//Vector3 temp = new Vector3 ((float) hp / maxHP, this.transform, 1);
			
			
			
			//this.transform.localScale = temp;
			
			
			
			//hpText.GetComponent<TextMesh> ().text = "" + hp.ToString ();
		} 
	}
}
