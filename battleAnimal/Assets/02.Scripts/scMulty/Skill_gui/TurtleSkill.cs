using UnityEngine;
using System.Collections;

public class TurtleSkill : MonoBehaviour {

	public Texture2D stexture1, stexture2 , stexture3 ;
	private Transform trans;
	private string ClientID;
	
	
	
	public int xfirst, yfirst, xsecond, ysecond, xthird, ythird, xfourth ;
	
	public MoveCtrl myMoveCtrl;
	
	public GameObject firstskill, secondskill, thirdskill;
	public GUILayer guilayer;
	
	public FireSkill skillfire;
	
	public bool skillOneReady =false;
	public bool skillTwoReady =false;
	
	// Use this for initialization
	void Start () {

				ClientID = ClientState.id;
		
				xfirst = 10;
				yfirst = 555;
				xsecond = 20 + stexture1.width;
				xthird = 20 + stexture1.width + stexture2.width;
		xfourth = 20 + stexture1.width + stexture2.width+stexture3.width;
		
		
		
				//Get game object
				myMoveCtrl = GetComponent<MoveCtrl> ();
				guilayer = Camera.main.GetComponent<GUILayer> ();
		
				trans = GetComponent<Transform> ();
				skillfire = GetComponent<FireSkill> ();

		}
	
	void OnGUI(){
		
		if (this.gameObject.name == PlayerPrefs.GetString ("email")) {
			
			
			
			
			if (GUI.Button (new Rect (xfirst, yfirst, stexture1.width, stexture1.height), stexture1)) {
				
				
				skillOneReady = true;
				
				GameObject dog =  GameObject.Find(ClientID);
				
				Vector3 spawnPos = dog.transform.position;
				Quaternion rotationdog = dog.transform.rotation;
				
				GameObject a;
				a = (GameObject) Instantiate(firstskill, spawnPos ,rotationdog);
				a.name="firstskill";
				
				a.transform.parent = dog.transform;
				
				
				//fixiate dog  select area to shoot  playermoving =false
				
			}
			
			
			if (GUI.Button (new Rect (xsecond, yfirst, stexture2.width, stexture2.height), stexture2)) {
				
				skillTwoReady = true;
				
				Debug.Log("clicked 2 man");
				GameObject dog =  GameObject.Find(ClientID);
				
				Vector3 spawnPos = dog.transform.position;
				Quaternion rotationdog = dog.transform.rotation;
				
				GameObject a;
				a = (GameObject) Instantiate(secondskill, spawnPos ,rotationdog);
				a.name="secondskill";
				
				a.transform.parent = dog.transform;
				
				
			}
			
			if (GUI.Button (new Rect (xthird, yfirst, stexture3.width, stexture3.height), stexture3)) {
				
				Debug.Log("clickedman");
				
				//myMoveCtrl.skill_touch = true;
				
			}


			
			if (GUI.Button (new Rect (xfourth, yfirst, stexture3.width, stexture3.height), "items")) {
				
				Debug.Log("clickedman");
				
				//myMoveCtrl.skill_touch = true;
				
			}
			
		}//if is mine
	}//end gui
	
	// Update is called once per frame
	void Update () {
		
		RaycastHit hitman;
		
		
		if (Input.GetMouseButtonDown (0) ) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			
			RaycastHit hiterone;
			
			//GUIElement	hitObject = guilayer.HitTest( Input.mousePosition );if (Physics.Raycast (ray, out hitman, Mathf.Infinity)) {
			
			if (Physics.Raycast (ray, out hiterone, Mathf.Infinity, 1<<LayerMask.NameToLayer("FLOOR"))) { 
				
				
				if(skillOneReady ==true){
					//Debug.Log(""+skillfire.ToString());
					GameObject myplayer =  GameObject.Find(ClientID);
					
					myplayer.transform.LookAt(hiterone.point);
					
					skillfire = myplayer.GetComponent<FireSkill> ();					
					//skillfire.Fireman(this.name);
					
					//destroy gameobject]
					
					skillOneReady = false;
					
					GameObject skill1 =  GameObject.Find("firstskill");
					Destroy (skill1);	

					Vector3	clickendpoint= hiterone.point;
					string data = ClientID + ":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
					
					SocketStarter.Socket.Emit ("SkillAttack", data);  //내위치를 서버에 알린다.				
					
					
				}//skill 1 ready true
				
				if(skillTwoReady == true){
					GameObject dog =  GameObject.Find(ClientID);
					
					dog.transform.LookAt(hiterone.point);
					
					Vector3 clickendpoint = hiterone.point;
					float step = 350 * Time.deltaTime;
					dog.transform.position = Vector3.MoveTowards(dog.transform.position, clickendpoint, step);
					
					skillTwoReady = false;
					
					GameObject skill1 =  GameObject.Find("secondskill");
					Destroy (skill1);	
				}
				
				
			} ///raycasr
			
		}//mousedown skillone ready 
		
		
		
		
		
	}
}