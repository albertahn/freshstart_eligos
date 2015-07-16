﻿using UnityEngine;
using System.Collections;

public class RedCannonState : MonoBehaviour {
	public GameObject bloodEffect;
	public GameObject bloodDecal;
	
	public int maxhp;
	
	public int hp;
	
	public bool isDie;
	public GameObject fireDie;
	
	public GameObject lavaDie;
	
	public string FiredBy;
	private moneyUI _moneyUI;
	// Use this for initialization
	
	
	private GameObject[] effectPool;
	private int maxEffect;
	
	
	private RedCannon_OutterCtrl _outterCtrl;
	
	
	void Start () {
		maxEffect = 5;
		effectPool = new GameObject[maxEffect];
		for (int i=0; i<maxEffect; i++)
		{
			effectPool[i] = (GameObject)Instantiate(bloodEffect);
			effectPool[i].transform.parent = gameObject.transform;
			effectPool[i].SetActive(false);
		}
		
		maxhp = 200;
		hp = maxhp;
		isDie = false;
		_outterCtrl = GetComponentInChildren<RedCannon_OutterCtrl> ();
		_moneyUI = GameObject.Find ("UIManager").GetComponent<moneyUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Heated(string firedby,GameObject obj,int damage){
		FiredBy = firedby;
		Collider coll = obj.collider;		
		StartCoroutine (this.CreateBloodEffect(coll.transform.position));
		
		if (ClientState.isMaster)
		{
			hp -= damage;		
			string data = this.name + ":" + hp.ToString () + "";
			SocketStarter.Socket.Emit ("attackCannon", data);
			
			if(hp<=0)
			{
				hp=0;
				playerDie(firedby);
			}			
		}		
		//Destroy (obj.gameObject);
	}//end heated
	
	public void HeatedSync(int _hp){
		hp = _hp;
		
		if (hp <= 0) {
			hp = 0;
		}
	}
	
	public void hitbySkill(string firedby, GameObject obj){
		
		Debug.Log ("skill hit: "+ firedby);
		
		hp -= obj.GetComponent<SkillFirstCrl>().damage;
		
		StartCoroutine (this.CreateBloodEffect(obj.transform.position));
		
		string data = this.name+":" + hp.ToString()+"";
		SocketStarter.Socket.Emit ("attackMinion", data);
	}
	
	
	void playerDie(string firedby){
		_outterCtrl.isRun = false;
		string data = this.name;
		SocketStarter.Socket.Emit ("cannonDie", data);  
		
		Debug.Log ("firedby: "+firedby);
		
		this.collider.enabled = false;
		isDie = true;
		//GetComponent<MoveCtrl> ().isDie = true;
		
		this.tag = "DIE";
		
		int oldInt = PlayerPrefs.GetInt ("minions_killed");
		
		PlayerPrefs.SetInt ("minions_killed",oldInt+1);
		
		if (firedby == ClientState.id) {
			Debug.Log("redCannon die_ firedBy = "+firedby);
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
		for (int i=0; i<maxEffect; i++) 
		{
			if(effectPool[i].activeSelf==false)
			{
				effectPool[i].transform.position = pos;
				effectPool[i].SetActive(true);
				StartCoroutine(PushObjectPool(effectPool[i]));
				break;
			}
		}
		
		yield return null;
	}
	IEnumerator PushObjectPool(GameObject a)
	{
		yield return new WaitForSeconds (0.2f);
		a.SetActive (false);
	}
}
