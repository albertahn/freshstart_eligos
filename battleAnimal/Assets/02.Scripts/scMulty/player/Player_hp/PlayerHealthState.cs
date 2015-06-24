using UnityEngine;
using System.Collections;

public class PlayerHealthState : MonoBehaviour {
	public GameObject bloodEffect;
	public GameObject bloodDecal;
	public int hp, maxhp;
	public bool isDie;
	private Respawn _respawn;
	private float hpBirth;
	public GameObject myplayer, red_building, blue_building;
	public Texture2D victory, defeat;
	private GameObject[] effectPool;
	private int maxEffect;
	private AniCtrl _aniCtrl;
	
	// Use this for initialization
	void Start () {
		maxEffect = 5;
		effectPool = new GameObject[maxEffect];
		for (int i=0; i<maxEffect; i++)
		{
			effectPool[i] = (GameObject)Instantiate(bloodEffect);
			effectPool[i].transform.parent = gameObject.transform;
			effectPool[i].SetActive(false);
		}
		
		isDie = false;
		_respawn = GameObject.Find ("NetworkManager").GetComponent<Respawn> ();
		myplayer = this.gameObject;
		hp = playerStat.maxHp;
		maxhp = playerStat.maxHp;
		
		red_building = GameObject.Find ("red_building");
		blue_building = GameObject.Find ("blue_building");
		_aniCtrl = GetComponent<AniCtrl> ();
	}
	
	// Update is called once per frame
	void Update(){
		//if near the building health up per sec
		/*if (ClientState.team == "red"){
			float distance = Vector3.Distance (red_building.transform.position, myplayer.transform.position);

			if (distance < 10.0f) {
				if (hp < playerStat.maxHp) {
					hp ++;
				}
			}
		} else {//not red team		
			float distance = Vector3.Distance (blue_building.transform.position, myplayer.transform.position);
			
			if(distance < 10.0f) {
				if (hp < playerStat.maxHp) {					
					hp ++;
				}
			}		
		}//end blue team health recover*/
		
		if (hp >= playerStat.maxHp)
			hp = playerStat.maxHp;
	}
	
	public void Heated(string firedby,GameObject obj,int damage){
		
		//Debug.Log ("playerhp: "+hp);
		
		Collider coll = obj.collider;
		
		StartCoroutine (this.CreateBloodEffect(coll.transform.position));
		
		
		if (ClientState.isMaster) {
			hp -= damage;		
			string data = this.name + ":" + hp.ToString () + "";
			SocketStarter.Socket.Emit ("playerHpSync", data);
			
			if (hp <= 0) {
				hp = 0;
				if (!isDie)
					playerDie ();
			}
		}
	}//end heated
	
	public void HeatedSync(int _hp){
		hp = _hp;
		Debug.Log ("player hp sync");
		if (hp <= 0) {
			hp = 0;
			playerDie ();
		}
	}
	
	public void hitbySkill(string firedby,GameObject obj){		
		Debug.Log ("skill hit: "+ firedby);		
		//hp -= obj.GetComponent<SkillFirstCrl>().damage;
		hp -= 50;		
		StartCoroutine (this.CreateBloodEffect(obj.transform.position));
		string data = this.name+":" + hp.ToString()+"";
		//SocketStarter.Socket.Emit ("attackMinion", data);	
	}
	
	
	void playerDie(){
		this.tag = "DIE";
		_aniCtrl._animation.CrossFade (_aniCtrl.anim.die.name, 0.3f);
		this.transform.FindChild ("touchCollider").tag = "DIE";
		this.collider.enabled = false;
		isDie = true;
		//GetComponent<MoveCtrl> ().isDie = true;
		
		if (ClientState.isMaster) {
			ClientState.death++;
			_respawn.Set(this.name);
			SocketStarter.Socket.Emit ("respawnREQ", this.name);	
		}
		
		this.collider.enabled = false;
		int oldInt = PlayerPrefs.GetInt ("minions_killed");
		PlayerPrefs.SetInt ("minions_killed",oldInt+1);
		
		/*if(PlayerPrefs.GetInt ("minions_killed") >1  && PlayerPrefs.GetString("evolved")=="false"){			
			GameObject.Find (ClientState.id).GetComponent<Level_up_evolve>().switchToEvol=true;			
			//PlayerPrefs.SetString("evolved", "true");			
		}*/
		//	Destroy (this.gameObject, 3.0f);
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
	
	/*void OnGUI(){		
		if (this.gameObject.name == ClientState.id && isDie ==true ) {
			GUI.DrawTexture(new Rect (10, 100, 450, 300), defeat);			
			if (GUI.Button (new Rect (100, 400, 150, 100), "exit")) {
				Application.LoadLevel ("scStart");
			}
		}else if(this.gameObject.name != ClientState.id && isDie==true){
			GUI.DrawTexture(new Rect (10, 100, 450, 300), victory);			
			if (GUI.Button (new Rect (100, 400, 150, 100), "exit")) {
				
				Application.LoadLevel ("scStart");
				
			}
		}
	}*/
}