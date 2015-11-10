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
	
	private GameObject[] effectPool;
	private int maxEffect;
	
	private BlueCannon_OutterCtrl _outterCtrl;
	
	// Use this for initialization
	void Awake () {
		maxEffect = 5;
		effectPool = new GameObject[maxEffect];
		for (int i=0; i<maxEffect; i++)
		{
			effectPool[i] = (GameObject)Instantiate(bloodEffect);
			effectPool[i].transform.parent = gameObject.transform;
			effectPool[i].SetActive(false);
		}
		
		maxhp = 1000;
		hp = maxhp;
		isDie = false;
		_outterCtrl = GetComponentInChildren<BlueCannon_OutterCtrl> ();
		_moneyUI = GameObject.Find ("UIManager").GetComponent<moneyUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Heated(string firedby,GameObject obj,int damage){		
		Collider coll = obj.collider;
		StartCoroutine (this.CreateBloodEffect(coll.transform.position));
		
		if (ClientState.isMaster)
		{
			hp -= damage;		
			
			if(ClientState.isMulty){
				string data = this.name + ":" + hp.ToString () + "";
				SocketStarter.Socket.Emit ("attackCannon", data);	
			}
			
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
	
	public void hitbySkill(string firedby,GameObject obj){
		
		
		hp -= obj.GetComponent<SkillFirstCrl>().damage;
		
		StartCoroutine (this.CreateBloodEffect(obj.transform.position));
		
		if (ClientState.isMulty) {
			string data = this.name + ":" + hp.ToString () + "";
			SocketStarter.Socket.Emit ("attackMinion", data);	
		}
	}
	
	
	void playerDie(string firedby){
		_outterCtrl.isRun = false;
		if (ClientState.isMulty) {
			string data = this.name;
			SocketStarter.Socket.Emit ("cannonDie", data); 
		}
		
		this.collider.enabled = false;
		isDie = true;
		//GetComponent<MoveCtrl> ().isDie = true;
		
		this.tag = "DIE";
		GameObject.Find (this.name + "/touchCollider").tag = "DIE";
		
		int oldInt = PlayerPrefs.GetInt ("minions_killed");
		PlayerPrefs.SetInt ("minions_killed",oldInt+1);
		
		
		//float  distance = Vector3.Distance(GameObject.Find(ClientState.id).transform.position, this.transform.position);
		//if (distance<10.0f) {
		
		if (firedby == ClientState.id) {	
			GameObject.Find (ClientState.id).GetComponent<Level_up_evolve>().expUp(10);
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
