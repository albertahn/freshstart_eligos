using UnityEngine;
using System.Collections;

public class blue_minion_state : MonoBehaviour {
	
	public GameObject bloodEffect;
	public GameObject bloodDecal;
	
	public int hp;	
	public string firedbyname;
	
	private moneyUI _moneyUI;
		
	private GameObject[] effectPool;

	private int maxEffect;

	void Awake(){
		hp = 100;
		maxEffect = 5;
		effectPool = new GameObject[maxEffect];
		for (int i=0; i<maxEffect; i++)
		{
			effectPool[i] = (GameObject)Instantiate(bloodEffect);
			effectPool[i].transform.parent = gameObject.transform;
			effectPool[i].SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		_moneyUI = GameObject.Find ("UIManager").GetComponent<moneyUI>();
	}
	
	// Update is called once per frame
	void Update () {	
		
	}		
	
	public void Heated(string firedby, GameObject obj,int damage){
		if (ClientState.isMaster) 
		{	
			hp -= damage;
			
			string data = this.name+":" + hp.ToString()+"";
			SocketStarter.Socket.Emit ("attackMinion", data);	

			if(hp<=0)
			{
				hp=0;
				minionDie();
				
				string data2 = ClientState.id+":"+this.name;
				SocketStarter.Socket.Emit ("minionDieREQ", data2);
			}
		}

		firedbyname = firedby;
		Collider coll = obj.collider;	
		StartCoroutine (this.CreateBloodEffect(coll.transform.position));	

	}
	
	public void minionDie(){
		this.collider.enabled = false;
		GetComponent<blueMinionCtrl> ().isDie = true;
		
		if(ClientState.id==firedbyname){			
			int oldInt = PlayerPrefs.GetInt ("minions_killed");
			PlayerPrefs.SetInt ("minions_killed",oldInt+1);
			
			GameObject.Find (ClientState.id).GetComponent<Level_up_evolve>().expUp(10);
			_moneyUI.makeMoney(10);
		}
		StartCoroutine (PushObjectPool ());
	}
	
	
	IEnumerator CreateBloodEffect(Vector3 pos)
	{
		for (int i=0; i<maxEffect; i++) 
		{
			if(effectPool[i].activeSelf==false)
			{
				effectPool[i].transform.position = pos;
				effectPool[i].SetActive(true);
				StartCoroutine(PushObjectEffectPool(effectPool[i]));
				break;
			}
		}
		
		yield return null;
	}
	IEnumerator PushObjectEffectPool(GameObject a)
	{
		yield return new WaitForSeconds (0.2f);
		a.SetActive (false);
	}
	IEnumerator PushObjectPool(){
		yield return new WaitForSeconds(3.0f);
		
		hp = 100;

		this.collider.enabled = true;
		
		gameObject.SetActive (false);
	}
}