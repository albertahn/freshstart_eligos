using UnityEngine;
using System.Collections;

public class RedCannonState : MonoBehaviour {
	public GameObject bloodEffect;
	public GameObject bloodDecal;
	
	public int maxhp = 1100;
	
	public int hp = 1100;
	
	public bool isDie;
	public GameObject fireDie;
	
	public GameObject lavaDie;

	public string FiredBy;
	private moneyUI _moneyUI;
	// Use this for initialization
	void Start () {
		isDie = false;
		_moneyUI = GameObject.Find ("UIManager").GetComponent<moneyUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Heated(string firedby,GameObject obj,int damage){
		FiredBy = firedby;
		Collider coll = obj.collider;
		
		StartCoroutine (this.CreateBloodEffect(coll.transform.position));
		
		hp -= damage;
		
		//string data = this.name+":" + hp.ToString()+"";
		//SocketStarter.Socket.Emit ("attackMinion", data);			
		
		if(hp<=0)
		{
			hp=0;
			playerDie(firedby);
		}
		
		//Destroy (obj.gameObject);
	}//end heated
	
	public void hitbySkill(string firedby, GameObject obj){
		
		Debug.Log ("skill hit: "+ firedby);
		
		hp -= obj.GetComponent<SkillFirstCrl>().damage;
		
		StartCoroutine (this.CreateBloodEffect(obj.transform.position));
		
		string data = this.name+":" + hp.ToString()+"";
		SocketStarter.Socket.Emit ("attackMinion", data);    
		
	}
	
	
	void playerDie(string firedby){

		Debug.Log ("firedby: "+firedby);

		this.collider.enabled = false;
		isDie = true;
		//GetComponent<MoveCtrl> ().isDie = true;
		
		int oldInt = PlayerPrefs.GetInt ("minions_killed");
		
		PlayerPrefs.SetInt ("minions_killed",oldInt+1);

		if (firedby == ClientState.id) {
			
			GameObject.Find (ClientState.id).GetComponent<Level_up_evolve>().expUp(100);
			_moneyUI.makeMoney(100);
		}
		
		GameObject flash = (GameObject)Instantiate(fireDie,this.transform.position,Quaternion.identity);
		GameObject lava = (GameObject)Instantiate(lavaDie,this.transform.position,Quaternion.identity);
		
		Destroy (this.gameObject, 3.0f);
		Destroy (flash, 5.0f); Destroy (lava, 5.0f);

	
		
	}
	
	
	IEnumerator CreateBloodEffect(Vector3 pos)
	{
		GameObject _blood1 = (GameObject)Instantiate (bloodEffect, pos, Quaternion.identity);
		Destroy (_blood1, 2.0f);
		
		Vector3 decalPos = this.transform.position+(Vector3.right*5.01f);
		Quaternion decalRot = Quaternion.Euler(0,Random.Range(0,360),0);
		
		GameObject _blood2 = (GameObject)Instantiate (bloodDecal, decalPos, decalRot);
		float _scale = Random.Range (1.5f, 3.5f);
		_blood2.transform.localScale = new Vector3 (_scale, 1, _scale);
		Destroy (_blood2, 5.0f);
		
		yield return null;
	}
}
