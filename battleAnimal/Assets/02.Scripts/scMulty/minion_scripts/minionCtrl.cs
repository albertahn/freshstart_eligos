using UnityEngine;
using System.Collections;

public class minionCtrl : MonoBehaviour {
	private Transform minionTr;
	public Transform playerTr;
	
	public bool isMove;
	public Transform[] point;
	public Vector3 dest;
	public Vector3 target;
	public Vector3 syncTarget;
	
	public mFireCtrl _fireCtrl;
	
	public int idx;
	private int speed;
	
	public enum MinionState{idle,trace,attack,die};
	public MinionState minionState;
	public float traceDist;
	public float attackDist;
	
	public bool isDie;
	private bool isPlayer;
	public bool isTrace;
	public bool isAttack;
	
	public float dist;
	
	public bool moveKey;
	public bool traceKey;
	public bool attackKey;
	
	public bool isMaster;
	
	private string minionID;
	private Vector3 minionPos, minionTg;
	private red_outer_collider _outterCtrl;
	public GameObject targetObj;
	
	private NavMeshAgent nvAgent;
	
	// Use this for initialization
	void Start () {
		traceDist = 20.0f;
		attackDist = 7.0f;
		
		moveKey = true;
		traceKey = false;
		attackKey = false;
		
		nvAgent = this.gameObject.GetComponent<NavMeshAgent> ();
		
		minionState = MinionState.idle;		
		_fireCtrl = GetComponent<mFireCtrl>();
		
		isMove = false;
		isDie = false;
		isPlayer = false;
		isTrace = false;
		
		idx = 1;
		speed = 2;
		minionTr = gameObject.GetComponent<Transform>();		
		int number = extractNum(gameObject.name);
		
		_outterCtrl = GetComponentInChildren<red_outer_collider> ();
		/*if (number % 3 == 0) {
			point = GameObject.Find ("redMovePoints/route1").GetComponentsInChildren<Transform> ();
		} else if (number % 3 == 1) {
			point = GameObject.Find ("redMovePoints/route2").GetComponentsInChildren<Transform> ();
		} else if (number % 3 == 2) {
			point = GameObject.Find ("redMovePoints/route3").GetComponentsInChildren<Transform> ();
		}*/
		point = GameObject.Find ("redMovePoints/route2").GetComponentsInChildren<Transform> ();
		
		syncTarget = dest = point[idx].position;
		
		if (isMaster) {
			StartCoroutine (this.CheckMonsterState ());
		}
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
				nvAgent.Stop();
				minionTr.LookAt (dest);
				if(dest!=minionTr.position){
					float step = speed * Time.deltaTime;
					minionTr.position = Vector3.MoveTowards (minionTr.position, dest, step);
				}
				if(Vector3.Distance(dest,minionTr.position)<=5.0f)
				{
					if(idx<8){
						idx++;
						dest = point [idx].position;
						moveKey = true;
					}
					
				}											
			}
			
			if (isTrace) {
				if (targetObj != null) {					
					nvAgent.destination = targetObj.transform.position;
					/*syncTarget = target = playerTr.position;
										minionTr.LookAt (target);
										float step = speed * Time.deltaTime;
										minionTr.position = Vector3.MoveTowards (minionTr.position, target, step);*/
				}
			}
			
			if (isAttack) {
				nvAgent.Stop();
				if (targetObj != null) {
					if(targetObj==null||targetObj.tag=="DIE"){
						move();
						_outterCtrl.targetDie(targetObj.transform);
					}else{
						minionTr.LookAt (targetObj.transform.position);
						_fireCtrl.Fire (targetObj.name);						
					}										
				}
			}
		}
	}
	
	int extractNum(string a){
		string temp=null;
		
		for (int i=0; i<name.Length-2; i++) {
			temp += a[2+i];
		}
		
		int number = int.Parse(temp+"");
		return number;
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
	
	public IEnumerator CheckMonsterState(){
		while (!isDie) {
			yield return new WaitForSeconds(0.2f);
			
			if(targetObj!=null){
				float innerSize = targetObj.transform.localScale.x/2;
				dist = Vector3.Distance(targetObj.transform.position,minionTr.position)-innerSize;
			}
			else{
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
}
