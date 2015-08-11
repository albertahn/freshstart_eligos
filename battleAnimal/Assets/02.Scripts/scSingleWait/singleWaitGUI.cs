using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class singleWaitGUI : MonoBehaviour {
	public Sprite furfurPortrait,
	stolaPortrait,
	barbasPortrait,
	guciPortrait,
	randomPortrait,emptyPortrait,nameTag;
	
	private string[] name;
	//public Texture2D[] portrait;
	public Image[] portrait_ = new Image[2];
	private int userNum;
	public GameObject img;
	
	int addUserOrder;
	string addUserId;
	bool addUserSwitch;
	
	int charSelectOrder;
	string charSelectCharacter;
	bool charSelectSwitch;
	
	int delUserOrder;
	bool delUserSwitch;
	
	private startCountReceiver _startCountReceiver;
	private GameObject TimeObj;
	public Text timeText;
	private int time;

	// Use this for initialization
	void Start (){
		time = 3;//10;
		TimeObj = (GameObject)GameObject.Find ("Canvas/Time");
		TimeObj.SetActive (false);
		
		addUserSwitch = false;
		charSelectSwitch = false;
		delUserSwitch = false;
		
		_startCountReceiver = GetComponent<startCountReceiver> ();

		userNum = 0;
		name = new string[2];
		//portrait = new Texture2D[6];
		portrait_=img.GetComponentsInChildren<Image> ();
		
		for(int i=0;i<2;i++)
			name [i] = "";
		
		for(int i=0;i<2;i++){
			//portrait[i] = emptyPortrait;
			portrait_[i].sprite = emptyPortrait;
			
		}
		switch(Random.Range(1,3)){
			
		case 1:
			ClientState.character = "Furfur";					
			break;
		case 2:
			ClientState.character = "Barbas";						
			break;
			
		}
	}
	
	public void remoteAddUser(int _order,string _id){
		while (addUserSwitch) {	}
		addUserOrder = _order;
		addUserId = _id;
		addUserSwitch = true;
	}
	
	void addUser(int _order,string _id){
		name [_order] = _id;
		portrait_ [_order].sprite = randomPortrait;
		userNum++;
	}
	
	public void deleteUser(int _order){
		name [_order] = "";
		portrait_ [_order].sprite = emptyPortrait;
		userNum--;
	}
	
	public void remoteDeleteUser(int _order){
		while (delUserSwitch) {	}
		delUserOrder = _order;
		delUserSwitch = true;
	}
	
	// Update is called once per framess
	void Update () {
		if (addUserSwitch) {
			addUser(addUserOrder,addUserId);
			addUserSwitch = false;
		}

		if (delUserSwitch) {
			deleteUser(delUserOrder);
			delUserSwitch = false;
		}
	}
	
	void OnGUI(){
		GUI.Label(new Rect(200,10,50,50),"id = "+ClientState.id);
		GUI.Label(new Rect(200,70,50,50),"room = "+ClientState.room);
		GUI.Label(new Rect(200,130,50,50),"order = "+ClientState.order);
		GUI.Label(new Rect(200,190,50,50),"character = "+ClientState.character);
		GUI.Label(new Rect(200,250,50,50),"team = "+ClientState.team);
		GUI.Label(new Rect(200,310,50,50),"isMaster = "+ClientState.isMaster);
	}
	
	public void FurFur_bot()
	{		
		portrait_[ClientState.order].sprite = furfurPortrait;
		ClientState.character = "furfur";
	}
	
	public void Stola_bot()
	{
		portrait_[ClientState.order].sprite = stolaPortrait;
		ClientState.character = "stola";

	}
	
	public void Guci_bot()
	{
		portrait_[ClientState.order].sprite = guciPortrait;
		ClientState.character = "guci";
	}
	
	
	public void Barbas_bot()
	{
		portrait_[ClientState.order].sprite = barbasPortrait;
		ClientState.character = "barbas";
	}
	
	
	
	public void Random_bot()
	{
		if (ClientState.id != "ob") {
			string data = ClientState.id + ":" + ClientState.order + ":random";
			waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
			switch (Random.Range (1, 5)) {
			case 1:
				ClientState.character = "furfur";					
				break;
			case 2:
				ClientState.character = "stola";						
				break;
			case 3:
				ClientState.character = "guci";					
				break;
			case 4:
				ClientState.character = "barbas";						
				break;
			}
		}
	}
	
	public void Ready()
	{
		StartCoroutine (TimeCheck ());
	}
	
	public void Back()
	{
		Application.LoadLevel ("scSinglePlay");
	}

	public IEnumerator TimeCheck(){
		TimeObj.SetActive (true);
		timeText = GameObject.Find ("Canvas/Time/waitTime").GetComponent<Text> ();
		while (true) {
			yield return new WaitForSeconds (1.0f);
			time--;
			timeText.text = time.ToString();
			if(time<=0){
				Application.LoadLevel("scSinglePlay");
				break;
			}
		}
	}
}