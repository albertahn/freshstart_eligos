﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class MoveCtrl : MonoBehaviour {	
	private Transform tr;
	private CharacterController _controller;
	private FireCtrl _fireCtrl;
	public AudioClip stepSfx;
	private attackMarkMaker _attackMarkMaker;
	
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

	private int layerMask;
	private int touchLayerMask;
	private int floorLayerMask;

	private float terrainHeight;
	
	IEnumerator playSfx(AudioClip _clip){
		audio.PlayOneShot (_clip, 0.9f);
		yield return null;
	}
	
	// Use this for initialization
	void Start () {
		GameObject movetomark = (GameObject)Resources.Load("moveToMark");
		Vector3 point = new Vector3 (0,0,0);
		
		GameObject mark = (GameObject)Instantiate(movetomark,point,Quaternion.identity);
		mark.name="MoveMark";
		
		layerMask = (1 << LayerMask.NameToLayer ("FLOOR"))|(1 << LayerMask.NameToLayer ("TOUCH"));
		touchLayerMask = 1 << LayerMask.NameToLayer ("TOUCH");
		floorLayerMask = 1 << LayerMask.NameToLayer ("FLOOR");

		_attackMarkMaker = GetComponent<attackMarkMaker> ();
		attackPoint = Vector3.zero;
		tr = this.GetComponent<Transform> ();
		_fireCtrl = this.GetComponent<FireCtrl> ();
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
		terrainHeight = 50.1f;
	}	
	
	// Update is called once per frame
	void Update () {
		//id가 내 캐릭터 일때
		if (ClientID == gameObject.name && _state.isDie == false) {
			#if UNITY_ANDROID||UNITY_IPHONE						
				mobileController();
			#else
				PCcontroller();
			#endif
		} 
		
		if(!_state.isDie){
			//ifmove
			if (playermoving) {
				tr.LookAt (clickendpoint);
				//if (clickendpoint != tr.position) {
				float step = playerStat.speed * Time.deltaTime;

				clickendpoint.y = terrainHeight;
				Vector3 dir = clickendpoint - tr.position;
				Vector3 movement = dir.normalized * step;
				if (movement.magnitude > dir.magnitude)
					movement = dir;
				_controller.Move (movement);
				
				
				//tr.position = Vector3.MoveTowards(tr.position, clickendpoint, step);
				//}
				_aniCtrl._animation.CrossFade (_aniCtrl.anim.run.name, 0.3f);
				_aniCtrl._animation ["attack"].speed = 2.5f;
				_aniCtrl._animation ["run"].speed = 2.5f;
			}
			
			if (isAttack) {
				if (targetObj != null) {
					_aniCtrl._animation.CrossFade (_aniCtrl.anim.attack.name, 0.3f);
					//if(targetObj.GetComponent<minionCtrl>()!=null){
					if (targetObj.tag == "MINION") {
						if (targetObj.name [0] == 'b') {
							if (targetObj.GetComponent<blueMinionCtrl> ().isDie == true)
								idle ();
							else {
								tr.LookAt (targetObj.transform.position);			
								_fireCtrl.Fire (targetObj.name);
								
								if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
									clickendpoint = targetObj.transform.position;
									isMoveAndAttack = true;
									playermoving = true;
								}
							}
						} else if (targetObj.name [0] == 'r') {
							if (targetObj.GetComponent<minionCtrl> ().isDie == true)
								idle ();
							else {
								tr.LookAt (targetObj.transform.position);			
								_fireCtrl.Fire (targetObj.name);
								
								if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
									clickendpoint = targetObj.transform.position;
									isMoveAndAttack = true;
									playermoving = true;
								}
							}
						}
					}
					else if(targetObj.tag == "DIE"){
						idle ();
					}else {//non minions
						Vector3 tt;
						tt = targetObj.transform.position;
						tt.y = terrainHeight;
						tr.LookAt (tt);			
						_fireCtrl.Fire (targetObj.name);
						
						if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
							clickendpoint = targetObj.transform.position;
							isMoveAndAttack = true;
							playermoving = true;
						}
					}//npnmins
				}
			}
			
			if (Vector3.Distance (clickendpoint, tr.position) <= 0.5f) {
				playermoving = false;
				_aniCtrl._animation.CrossFade (_aniCtrl.anim.idle.name, 0.3f);
			}
			
			if (isMoveAndAttack) {
				if (Vector3.Distance (tr.position, targetObj.transform.position) <= _fireCtrl.distance) {
					isMoveAndAttack = false;
					attack (targetObj.name);
				}
			}
		}
	}//end update
	
	public void attack(string _targetName){
		targetObj = GameObject.Find(_targetName);
		if (targetObj != null) {
			float dist = Vector3.Distance (tr.position, targetObj.transform.position);
			dist = dist-(targetObj.transform.localScale.x/2);
			if (dist > _fireCtrl.distance) {
				clickendpoint = targetObj.transform.position;
				clickendpoint.y = terrainHeight;
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
		if(ClientID ==this.name)
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
	
	public void moveToPointMark(Vector3 point){		
		GameObject pastmovetomark = GameObject.Find ("MoveMark"); 		
		pastmovetomark.transform.position = point;	
	}












	//Input process
	private void mobileController(){
		if (Input.touchCount >0  && Input.GetTouch(0).phase ==TouchPhase.Began) {
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit3;
			RaycastHit hit4;
			
			if(Physics.Raycast (ray, out hit3, Mathf.Infinity,touchLayerMask)){
				if(hit3.collider.tag =="DIE")
				{
					if (EventSystem.current.IsPointerOverGameObject () == false) {
						_attackMarkMaker.deleteMarker();
						myxpos = hit3.point.x; //Input.touches [0].position.x;
						myypos = hit3.point.y;  //Input.touches [0].position.y;
						myzpos = hit3.point.z;
						
						clickendpoint = hit3.point;
						
						string data = ClientID + ":"+ClientState.character + ":" + tr.position.x + "," + tr.position.y + "," + tr.position.z +
							":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
						SocketStarter.Socket.Emit ("movePlayerREQ",data);//내위치를 서버에 알린다.
						
						move ();
					}
				}else{
					string targetName= hit3.collider.transform.parent.name;
					
					if (hit3.collider.tag == "Player") {
						string parentName = hit3.collider.transform.parent.transform.parent.name;
						
						if (ClientState.team == "red" && parentName == "BlueTeam"
						    || ClientState.team == "blue" && parentName == "RedTeam") {
							_attackMarkMaker.mark(hit3.collider.gameObject);
							Vector3 target = hit3.point;
							attackPoint = target;
							
							string data = ClientID + ":"+ClientState.character + ":" + targetName;
							SocketStarter.Socket.Emit ("attackREQ", data);	
							attack (targetName);
						}
					} else {
						if (ClientState.team == "red" && targetName [0] == 'b'
						    || ClientState.team == "blue" && targetName [0] == 'r') {
							_attackMarkMaker.mark(hit3.collider.gameObject);
							Vector3 target = hit3.point;
							attackPoint = target;
							
							string data = ClientID  + ":"+ClientState.character + ":" + targetName;
							SocketStarter.Socket.Emit ("attackREQ", data);	
							attack (targetName);
						}
					}
					
				}
			}else if(Physics.Raycast(ray, out hit4, Mathf.Infinity,floorLayerMask)){
					//int pointerID = Input.touches; //EventSystem.current.IsPointerOverGameObject
					_attackMarkMaker.deleteMarker();
					
					Vector3 target = new Vector3(hit4.point.x, 0 , hit4.point.z);
					
					clickendpoint = hit4.point;					
					string data = ClientID+  ":"+ClientState.character+ ":" +tr.position.x+","+tr.position.y+","+tr.position.z+
						":"+ clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
					SocketStarter.Socket.Emit ("movePlayerREQ", data);//내위치를 서버에 알린다.
					
					move();

					tr.LookAt(hit4.point); 
					myxpos    =hit4.point.x; //Input.touches [0].position.x;
					myypos    =hit4.point.z;  //Input.touches [0].position.y;
				}//end 
			}
		}//if		
		//}// if touchcount 1

	private void PCcontroller(){	
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		Debug.DrawRay (ray.origin, ray.direction * 100.0f, Color.green);
		
		RaycastHit hitman;
		RaycastHit hitman2;		
		
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (ray, out hitman2, Mathf.Infinity,touchLayerMask)) {
				if(hitman2.collider.tag =="DIE")
				{
					if (EventSystem.current.IsPointerOverGameObject () == false) {
						_attackMarkMaker.deleteMarker();
						myxpos = hitman2.point.x; //Input.touches [0].position.x;
						myypos = hitman2.point.y;  //Input.touches [0].position.y;
						myzpos = hitman2.point.z;
						
						clickendpoint = hitman2.point;
						
						string data = ClientID + ":"+ClientState.character + ":" + tr.position.x + "," + tr.position.y + "," + tr.position.z +
							":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
						SocketStarter.Socket.Emit ("movePlayerREQ",data);//내위치를 서버에 알린다.
						
						move ();
					}
				}else{
					string targetName= hitman2.collider.transform.parent.name;
					
					if (hitman2.collider.tag == "Player") {
						string parentName = hitman2.collider.transform.parent.transform.parent.name;					
						if (ClientState.team == "red" && parentName == "BlueTeam"
						    || ClientState.team == "blue" && parentName == "RedTeam") {
							_attackMarkMaker.mark(hitman2.collider.gameObject);
							Vector3 target = hitman2.point;
							attackPoint = target;
							
							string data = ClientID+ ":"+ClientState.character + ":" + targetName;
							SocketStarter.Socket.Emit ("attackREQ",data);	
							attack (targetName);
						}
					}
					else {
						if (ClientState.team == "red" && targetName [0] == 'b'
						    || ClientState.team == "blue" && targetName [0] == 'r') {
							_attackMarkMaker.mark(hitman2.collider.gameObject);
							Vector3 target = hitman2.point;
							attackPoint = target;
							
							string data = ClientID + ":"+ClientState.character + ":" + targetName;
							SocketStarter.Socket.Emit ("attackREQ",data);	
							attack (targetName);
						}
					}
				}
			}else if (Physics.Raycast (ray, out hitman, Mathf.Infinity, floorLayerMask)) {
					if (EventSystem.current.IsPointerOverGameObject () == false) {
						_attackMarkMaker.deleteMarker();
						myxpos = hitman.point.x; //Input.touches [0].position.x;
						myypos = hitman.point.y;  //Input.touches [0].position.y;
						myzpos = hitman.point.z;
						
						clickendpoint = hitman.point;
						
						string data = ClientID + ":"+ClientState.character + ":" + tr.position.x + "," + tr.position.y + "," + tr.position.z +
							":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
						SocketStarter.Socket.Emit ("movePlayerREQ",data);//내위치를 서버에 알린다.
						
						move ();
					}
			}
		}
	} ///raycasr
}