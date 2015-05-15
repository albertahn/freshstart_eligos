using UnityEngine;
using System.Collections;

public class BlueCannonState : MonoBehaviour {
	public GameObject bloodEffect;
	public GameObject bloodDecal;
	
	public GameObject fireDie;
	
	public GameObject lavaDie;
	
	public int maxhp;
	
	public int hp;
	
	public bool isDie;
	private moneyUI _moneyUI;
	
	// Use this for initialization
	void Start () {
		maxhp = 200;
		hp = maxhp;
		isDie = false;
		_moneyUI = GameObject.Find ("UIManager").GetComponent<moneyUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Heated(string firedby,GameObject obj,int damage){
		
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
	
	public void hitbySkill(string firedby,GameObject obj){
		
		Debug.Log ("skill hit: "+ firedby);
		
		hp -= obj.GetComponent<SkillFirstCrl>().damage;
		
		StartCoroutine (this.CreateBloodEffect(obj.transform.position));
		
		string data = this.name+":" + hp.ToString()+"";
		SocketStarter.Socket.Emit ("attackMinion", data);    
		
	}
	
	
	void playerDie(string firedby){
		
		string data = this.name;
		SocketStarter.Socket.Emit ("cannonDie", data); 
		
		this.collider.enabled = false;
		isDie = true;
		//GetComponent<MoveCtrl> ().isDie = true;
		
		int oldInt = PlayerPrefs.GetInt ("minions_killed");
		PlayerPrefs.SetInt ("minions_killed",oldInt+1);
		

		float  distance = Vector3.Distance(GameObject.Find(ClientState.id).transform.position, this.transform.position);
		if (distance<10.0f) {
			
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
		
		/*GameObject _blood2 = (GameObject)Instantiate (bloodDecal, decalPos, decalRot);
		float _scale = Random.Range (1.5f, 3.5f);
		_blood2.transform.localScale = new Vector3 (_scale, 1, _scale);
		Destroy (_blood2, 5.0f);*/
		
		yield return null;
	}
}
