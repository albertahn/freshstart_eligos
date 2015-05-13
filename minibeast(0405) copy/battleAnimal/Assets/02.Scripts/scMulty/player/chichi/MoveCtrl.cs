using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MoveCtrl : MonoBehaviour {	
	private Transform tr;
	Vector3 pre_tr;
	private CharacterController _controller;
	private FireCtrl _fireCtrl;
	
	public float h = 0.0f;
	public float v = 0.0f;
	
	public float rotSpeed = 50.0f;
	
	private Vector3 movDir = Vector3.zero;	
	private string ClientID;	
	public float myypos, myxpos,myzpos;	
	
	public Vector3 clickendpoint;	
	public bool playermoving = false;
	
	private bool screenmoveonly;
	public Vector2 startPos;
	public Vector2 direction;
	public bool directionChosen;
	
	public float timeOfTouch;
	
	public bool isAttack;
	public Vector3 attackPoint;
	
	public bool isMoveAndAttack;
	
	public GameObject targetObj;
	
	public bool swiped;
	
	private PlayerHealthState _state;
	
	private AniCtrl _aniCtrl;
	
	// Use this for initialization
	void Start () {
		attackPoint = Vector3.zero;
		tr = this.GetComponent<Transform> ();
		_fireCtrl = this.GetComponent<FireCtrl> ();
		pre_tr = t2v(tr);
		_controller = GetComponent<CharacterController> ();
		_state = GetComponent<PlayerHealthState> ();
		ClientID = ClientState.id;
		_aniCtrl = GetComponent<AniCtrl> ();
		
		myxpos = tr.transform.position.x;
		myypos = tr.transform.position.y;
		
		directionChosen = false;
		isAttack = false;
		isMoveAndAttack=false;
		
		swiped = false;
	}	
	
	// Update is called once per frame
	void Update () {
		if (ClientID == gameObject.name && _state.isDie == false) {
			
			//id가 내 캐릭터 일때
			#if UNITY_ANDROID||UNITY_IPHONE
			if (Input.touchCount == 1 && Input.touchCount  <2) {
				
				var touch = Input.GetTouch(0);                
				
				switch (touch.phase) {
					// Record initial touch position.
				case TouchPhase.Began:
					timeOfTouch = Time.time;
					
					swiped = false;
					
					break;                    
					// Determine direction by comparing the current touch position with the initial one.
				case TouchPhase.Moved:
					direction = touch.position - startPos;
					
					swiped = true;
					
					Debug.Log("swiped");
					
					break;                    
					// Report that a direction has been chosen when the finger is lifted.
				case TouchPhase.Ended:        
					
					if(Time.time - timeOfTouch>3.0f || swiped ==true){
						directionChosen = true;    
						
					}else{
						directionChosen = false;    
						
						swiped = false;
						
						
					}    //                
					break;
				}//end switch
				
			}
			
			if (Input.touchCount == 1  && Input.GetTouch(0).phase != TouchPhase.Moved  && directionChosen ==false) {
				Ray ray3 = Camera.main.ScreenPointToRay (Input.touches [0].position);
				RaycastHit hit3;
				RaycastHit hit4;    
				
				if(Physics.Raycast (ray3, out hit3, Mathf.Infinity,(-1) -(1 << LayerMask.NameToLayer("OBSTACLE")))){
					if(hit3.collider.tag =="BUILDING" || hit3.collider.tag =="MINION"||hit3.collider.tag =="Player"
					   ||hit3.collider.tag=="BLUE_CANNON"||hit3.collider.tag=="RED_CANNON"){
						string targetName = hit3.collider.name;
						Debug.Log("target = "+targetName);
						Vector3 target = hit3.point;
						target.y=50.0f;
						attackPoint = target;
						
						string data = ClientID + ":"+ClientState.character + ":" + targetName;
						SocketStarter.Socket.Emit ("attackREQ", data);    
						attack(targetName);                                
					}//else hit player
					else if(Physics.Raycast(ray3, out hit4, Mathf.Infinity, 1<<LayerMask.NameToLayer("FLOOR"))){
						//int pointerID = Input.touches; //EventSystem.current.IsPointerOverGameObject
						
						Touch currentTouch = Input.GetTouch(0);
						
						bool inputIsActive = (currentTouch.phase != TouchPhase.Ended &&
						                      currentTouch.phase != TouchPhase.Canceled);
						
						
						
						if( inputIsActive && EventSystem.current.IsPointerOverGameObject(currentTouch.fingerId)==false){  
							Vector3 target = new Vector3(hit4.point.x, 0 , hit4.point.z);
							
							clickendpoint = hit4.point;
							
							string data = ClientID+ ":"+ClientState.character + ":" +tr.position.x+","+tr.position.y+","+tr.position.z+
								":"+ clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
							SocketStarter.Socket.Emit ("movePlayerREQ", data);//내위치를 서버에 알린다.        
							
							move();
							
							//playermoving = true;
							
							tr.LookAt(hit4.point); 
							myxpos    =hit4.point.x; //Input.touches [0].position.x;
							myypos    =hit4.point.z;  //Input.touches [0].position.y;    
							
							
						}//if false                        
					}//end 
				}
			}//if
			
			
			
			
			//}// if touchcount 1
			#else
			
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			Debug.DrawRay (ray.origin, ray.direction * 100.0f, Color.green);
			
			RaycastHit hitman;
			RaycastHit hitman2;
			
			
			if (Input.GetMouseButtonDown (0)) {						
				if (Physics.Raycast (ray, out hitman2, Mathf.Infinity,(-1) -(1 << LayerMask.NameToLayer("OBSTACLE")))) {
					if (hitman2.collider.tag == "BUILDING" || hitman2.collider.tag == "MINION" || hitman2.collider.tag == "Player"
					    || hitman2.collider.tag == "BLUE_CANNON" || hitman2.collider.tag == "RED_CANNON") {
						string targetName = hitman2.collider.name;
						if (hitman2.collider.tag == "Player") {
							string parentName = hitman2.collider.gameObject.transform.parent.name;
							
							if (ClientState.team == "red" && parentName == "BlueTeam"
							    || ClientState.team == "blue" && parentName == "RedTeam") {
								Vector3 target = hitman2.point;
								attackPoint = target;
								
								string data = ClientID+ ":"+ClientState.character + ":" + targetName;
								SocketStarter.Socket.Emit ("attackREQ", data);	
								attack (targetName);
							}
						} else {
							if (ClientState.team == "red" && targetName [0] == 'b'
							    || ClientState.team == "blue" && targetName [0] == 'r') {
								Vector3 target = hitman2.point;
								attackPoint = target;
								
								string data = ClientID + ":"+ClientState.character + ":" + targetName;
								SocketStarter.Socket.Emit ("attackREQ", data);	
								attack (targetName);
							}
						}
					} else if (Physics.Raycast (ray, out hitman, Mathf.Infinity, 1 << LayerMask.NameToLayer ("FLOOR"))) {
						if (EventSystem.current.IsPointerOverGameObject () == false) {
							myxpos = hitman.point.x; //Input.touches [0].position.x;
							myypos = hitman.point.y;  //Input.touches [0].position.y;
							myzpos = hitman.point.z;
							
							clickendpoint = hitman.point;
							
							string data = ClientID + ":"+ClientState.character + ":" + tr.position.x + "," + tr.position.y + "," + tr.position.z +
								":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
							SocketStarter.Socket.Emit ("movePlayerREQ", data);//내위치를 서버에 알린다.							
							
							move ();
						}
					}
				}
			} ///raycasr
			#endif
		} 
		
		if(_state.isDie){
			_aniCtrl._animation.CrossFade(_aniCtrl.anim.die.name,0.3f);
		}
		
		
		//ifmove
		if (playermoving) {
			tr.LookAt (clickendpoint);
			//if (clickendpoint != tr.position) {
			float step = playerStat.speed* Time.deltaTime;

			Vector3 dir = clickendpoint - tr.position;
			Vector3 movement = dir.normalized*step;
			if(movement.magnitude>dir.magnitude)movement = dir;
			GetComponent<CharacterController>().Move(movement);


			//tr.position = Vector3.MoveTowards(tr.position, clickendpoint, step);
			//}
			_aniCtrl._animation.CrossFade(_aniCtrl.anim.run.name,0.3f);
			_aniCtrl._animation["attack"].speed = 2.5f;
			_aniCtrl._animation["run"].speed = 2.5f;
		}
		
		if (isAttack) {
			if(targetObj!=null){				
				_aniCtrl._animation.CrossFade(_aniCtrl.anim.attack.name,0.3f);
				//if(targetObj.GetComponent<minionCtrl>()!=null){
				if(targetObj.tag=="MINION"){
					if(targetObj.name[0]=='b'){
						if(targetObj.GetComponent<blueMinionCtrl>().isDie==true)
							idle ();
						else{
							tr.LookAt (targetObj.transform.position);			
							_fireCtrl.Fire(targetObj.name);
							
							if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
								clickendpoint=targetObj.transform.position;
								isMoveAndAttack = true;
								playermoving = true;
							}
						}
					}else if(targetObj.name[0]=='r'){
						if(targetObj.GetComponent<minionCtrl>().isDie==true)
							idle ();
						else{
							tr.LookAt (targetObj.transform.position);			
							_fireCtrl.Fire(targetObj.name);
							
							if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
								clickendpoint=targetObj.transform.position;
								isMoveAndAttack = true;
								playermoving = true;
							}
						}
					}
				}else{//non minions
					Vector3 tt;
					tt= targetObj.transform.position;
					tt.y = 50.0f;
					tr.LookAt (tt);			
					_fireCtrl.Fire(targetObj.name);
					
					if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
						clickendpoint=targetObj.transform.position;
						isMoveAndAttack = true;
						playermoving = true;
					}
				}//npnmins
			}
		}
		
		if(Vector3.Distance( clickendpoint,tr.position)<=0.5f) {
			playermoving = false;
			_aniCtrl._animation.CrossFade(_aniCtrl.anim.idle.name,0.3f);
		}
		
		if (isMoveAndAttack) {
			if(Vector3.Distance (tr.position, targetObj.transform.position) <= _fireCtrl.distance){
				isMoveAndAttack = false;
				attack (targetObj.name);
			}
		}
	}//end update
	
	public void attack(string _targetName){
		targetObj = GameObject.Find(_targetName);
		if (targetObj != null) {
			if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
				clickendpoint = targetObj.transform.position;
				clickendpoint.y = 50.1f;
				isMoveAndAttack = true;
				playermoving = true;
				//moveAndAttack ();
			} else {
				isAttack = true;
				playermoving = false;
			}
		}
	}
	
	private void moveAndAttack(){
		isMoveAndAttack = true;
		playermoving = true;
	}
	
	public void move(){
		//mark the plack  moveToPointMark
		moveToPointMark(clickendpoint);
		
		playermoving = true;
		isAttack = false;
		isMoveAndAttack = false;
	}
	public void idle(){
		playermoving = false;
		isAttack = false;
		isMoveAndAttack = false;
	}
	
	bool isSame(Transform a,Vector3 b){
		if (a.position.x == b.x &&
		    a.position.y == b.y &&
		    a.position.z == b.z)
			return true;
		else
			return false;
	}
	
	Vector3 t2v(Transform t){		
		Vector3 a;
		a.x = t.position.x;
		a.y = t.position.y;
		a.z = t.position.z;
		return a;
	}
	public void moveToPointMark(Vector3 point){
		
		GameObject pastmovetomark = GameObject.Find ("MoveMark"); 
		
		Destroy (pastmovetomark);
		
		GameObject movetomark = (GameObject)Resources.Load("moveToMark");
		
		GameObject mark = (GameObject)Instantiate(movetomark,point,Quaternion.identity);
		mark.name="MoveMark";
		
	}
	
	
	/*void OnGUI(){

		if (GUI.Button (new Rect (100, 110, 180, 180), "moving: "+playermoving )) {

		}//doanloaded image
	
	}
*/
	//gui test
	
	
}