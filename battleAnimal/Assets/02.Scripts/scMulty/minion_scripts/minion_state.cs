using UnityEngine;
using System.Collections;

public class minion_state : MonoBehaviour {
	
	public GameObject bloodEffect;
	public GameObject bloodDecal;
	
	public int hp = 100;
	public string firedbyname;

	private moneyUI _moneyUI;
	// Use this for initialization
	void Start () {
		_moneyUI = GameObject.Find ("UIManager").GetComponent<moneyUI>();
	}
	
	// Update is called once per frame
	void Update () {	
				
	}



	public void Heated(string firedby, GameObject obj,int damage){
		Collider coll = obj.collider;		
		StartCoroutine (this.CreateBloodEffect(coll.transform.position));

		firedbyname = firedby;	
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
		
		//Destroy (obj.gameObject);
	}

	public void minionDie(){
		this.collider.enabled = false;
		GetComponent<minionCtrl> ().isDie = true;

		if(ClientState.id==firedbyname){			
			int oldInt = PlayerPrefs.GetInt ("minions_killed");
			PlayerPrefs.SetInt ("minions_killed",oldInt+1);

			GameObject.Find (ClientState.id).GetComponent<Level_up_evolve>().expUp(10);
			_moneyUI.makeMoney(10);
		}
		Destroy (this.gameObject, 3.0f);
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
