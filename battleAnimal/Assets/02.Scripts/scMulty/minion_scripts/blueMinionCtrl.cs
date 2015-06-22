using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class blueMinionCtrl : MonoBehaviour {
	private Transform minionTr;
	public Transform playerTr;
	
	public bool isMove;
	public Transform[] pointTemp;
	public List<Transform> point;
	
	public Vector3 dest;
	public Vector3 target;
	public Vector3 syncTarget;
	
	public mFireCtrl _fireCtrl;

	private int speed;
	
	public enum MinionState{idle,trace,attack,die};
	public MinionState minionState;
	public float traceDist;
	public float attackDist;
	
	public bool isDie;
	public bool isPlayer;
	private bool isTrace;
	
	public float dist;
	
	public bool moveKey;
	public bool traceKey;
	public bool attackKey;
	
	
	public bool isAttack;
	public bool isMaster;	
	private Vector3 minionPos, minionTg;
	public blue_outer_collider _outterCtrl;
	public GameObject targetObj;
	
	private NavMeshAgent nvAgent;

	public int line;
	
	public void Start(){
		_outterCtrl = GetComponentInChildren<blue_outer_collider> ();
		
		nvAgent = this.gameObject.GetComponent<NavMeshAgent> ();
		minionTr = gameObject.GetComponent<Transform> ();
		_fireCtrl = GetComponent<mFireCtrl>();

	}

	// Use this for initialization
	public void StartC() {
		traceDist = 20.0f;
		attackDist = 7.0f;
		
		moveKey = true;
		traceKey = false;
		attackKey = false;
		
		minionState = MinionState.idle;
		
		isMove = false;		
		isDie = false;
		isPlayer = false;
		isTrace = false;
		isAttack = false;
		
		_outterCtrl = GetComponentInChildren<blue_outer_collider> ();
		
		nvAgent = this.gameObject.GetComponent<NavMeshAgent> ();

		speed = 2;
		minionTr = gameObject.GetComponent<Transform> ();
		_fireCtrl = GetComponent<mFireCtrl>();
		
		int number = extractNum(gameObject.name);
		
		/*if (number % 3 == 0) {
			point = GameObject.Find ("blueMovePoints/route1").GetComponentsInChildren<Transform> ();
		} else if (number % 3 == 1) {
			point = GameObject.Find ("blueMovePoints/route2").GetComponentsInChildren<Transform> ();
		} else if (number % 3 == 2) {
			point = GameObject.Find ("blueMovePoints/route3").GetComponentsInChildren<Transform> ();
		}*/
		point = new List<Transform> ();
		
		//initiatePoints ();
		
		if (isMaster) {
			StartCoroutine (this.CheckMonsterState ());
		}
	}
	
	public void initiatePoints(){
		point.Clear ();

		if(line==1)
			pointTemp = GameObject.Find ("redMovePoints/route1").GetComponentsInChildren<Transform> ();
		else if(line==2)
			pointTemp = GameObject.Find ("redMovePoints/route2").GetComponentsInChildren<Transform> ();

		for (int i=1; i<pointTemp.Length; i++) {
			point.Add(pointTemp[i]);
		}

		sortPointsByDistance ();
		
		dest = point[0].position;
		
		point.RemoveAt (0);
	}
	
	private void sortPointsByDistance(){
		point.Sort (delegate(Transform t1,Transform t2) {
			return(Vector3.Distance (t1.position, minionTr.position).CompareTo
			       (Vector3.Distance (t2.position, minionTr.position)));
		});
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDie) {
			if (isMaster) {
				if (moveKey) {
					moveKey = false;
					string data = gameObject.name + ":"+minionTr.position.x+","+minionTr.position.y+","+minionTr.position.z+
						":"+dest.x+","+dest.y+","+dest.z;
					SocketStarter.Socket.Emit("moveMinionREQ", data);
					move ();
				}
				if (traceKey) {
					traceKey = false;
					trace ();
				}
				if (attackKey) {
					attackKey = false;
					attack ();
				}
			}
			
			if (isMove) {
				minionTr.LookAt (dest);
				if(dest!=minionTr.position){
					//	float step = speed * Time.deltaTime;
					//	minionTr.position = Vector3.MoveTowards (minionTr.position, dest, step);
					nvAgent.destination = dest;
				}
				
				if(Vector3.Distance(dest,minionTr.position)<=5.0f)
				{
					if(point.Count>0){
						dest = point [0].position;
						point.RemoveAt (0);		
					}
				}	
			}
			
			if (isTrace) {
				if (targetObj != null&&targetObj.tag=="DIE") {
					nvAgent.destination = targetObj.transform.position;
				}
			}
			
			/*if (isAttack) {
				nvAgent.Stop();
				if (targetObj != null) {
					if(targetObj==null||targetObj.tag=="DIE"){
						move();
						_outterCtrl.refreshList();
					}
					else{
						minionTr.LookAt (targetObj.transform.position);
						_fireCtrl.Fire (targetObj.name);						
					}										
				}else{
					move();
				}
			}*/
			if (isAttack) {
				nvAgent.Stop();
				if (targetObj != null) {
					if(targetObj.tag=="DIE"){
						_outterCtrl.removeOne(targetObj);
					}else{
						minionTr.LookAt (targetObj.transform.position);
						_fireCtrl.Fire (targetObj.name);	
					}
				}else{					
					_outterCtrl.refreshList();	
					move();
				}
			}
		}
	}
	
	public void move(){
		isMove = true;
		isTrace = false;
		isAttack = false;
	}
	
	public void trace(){		
		isMove=false;
		isTrace = true;
		isAttack = false;
	}
	
	
	public void attack(){		
		isMove=false;
		isTrace = false;
		isAttack = true;
	}
	
	int extractNum(string a){
		string temp=null;
		
		for (int i=0; i<name.Length-2; i++) {
			temp += a[2+i];
		}		
		int number = int.Parse(temp+"");
		return number;
	}
	
	public IEnumerator CheckMonsterState(){
		while (!isDie) {
			yield return new WaitForSeconds(0.2f);
			
			if(targetObj!=null){
				float innerSize = targetObj.transform.localScale.x/2;
				dist = Vector3.Distance(targetObj.transform.position,minionTr.position)-innerSize;
			}else{
				dist = 1000.0f;
			}
			
			if(dist<=attackDist){
				if(isAttack==false){
					attackKey = true;	
					
					string data = gameObject.name + ":" + targetObj.name+":"+minionTr.position.x+","+minionTr.position.y+","+minionTr.position.z;
					SocketStarter.Socket.Emit ("minionAttackREQ", data);
				}
			}
			
			else if(dist<=traceDist)
			{
				if(isTrace==false)
					traceKey = true;
			}else
			{
				if(isMove==false){
					moveKey = true;
				}
			}			
		}
	}
	
	/*void OnTriggerEnter(Collider coll){
		if (coll.tag == "BluePoint") {
			if (isMaster) {
				if (idx < point.Length - 1) {
					syncTarget = dest = point [++idx].position;
					moveKey = true;
				}
			}
		}
	}*/
}
