using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class waitGUI : MonoBehaviour {

	public Sprite furfurPortrait,
	stolaPortrait,
	barbasProtrait,
	guciPortrait,
	randomPortrait,emptyPortrait,nameTag;

	private string[] name;
	//public Texture2D[] portrait;
	public Image[] portrait_ = new Image[6];
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

	// Use this for initialization
	void Start (){

		addUserSwitch = false;
		charSelectSwitch = false;
		delUserSwitch = false;

		userNum = 0;
		name = new string[6];
		//portrait = new Texture2D[6];
		portrait_=img.GetComponentsInChildren<Image> ();

		for(int i=0;i<6;i++)
			name [i] = "";

		for(int i=0;i<6;i++){
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
	/*
	id = ClientID;
	character = ClientState.character;
	*/

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

		if (charSelectSwitch) {
			setCharacter(charSelectOrder,charSelectCharacter);
			charSelectSwitch = false;
		}
		if (delUserSwitch) {
			deleteUser(delUserOrder);
			delUserSwitch = false;
		}

		if (ClientState.order % 2 == 0){
			
			ClientState.team = "red";
			
		} else{
			ClientState.team = "blue";
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
		if (ClientState.id != "ob") {
						string data = ClientState.id + ":" + ClientState.order + ":furfur";
						waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
						ClientState.character = "furfur";
				}
	}

	public void Stola_bot()
	{
		if (ClientState.id != "ob") {
						string data = ClientState.id + ":" + ClientState.order + ":stola";
						waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
						ClientState.character = "stola";	
				}
	}

	public void Guci_bot()
	{
		if (ClientState.id != "ob") {
			string data = ClientState.id + ":" + ClientState.order + ":guci";
			waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
			ClientState.character = "guci";	
		}
	}//gucibot


	public void Barbas_bot()
	{
		if (ClientState.id != "ob") {
			string data = ClientState.id + ":" + ClientState.order + ":barbas";
			waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
			ClientState.character = "barbas";	
		}
	}



	public void Random_bot()
	{
		if (ClientState.id != "ob") {
						string data = ClientState.id + ":" + ClientState.order + ":random";
						waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
						switch (Random.Range (1, 3)) {
						case 1:
								ClientState.character = "furfur";					
								break;
						case 2:
								ClientState.character = "stola";						
								break;
						}
				}
	}


	public void Ready()
	{		
		if (ClientState.id == "ob") {
			ClientState.team = "gray";
		} else {
			if (ClientState.order % 2 == 0){

				ClientState.team = "red";

			} else{
				ClientState.team = "blue";
			}
		}
		string data = ClientState.id + ":" + ClientState.team;
		waitSocketStarter.Socket.Emit ("readyButtonREQ", data);
		//Application.LoadLevel("scMulty");
	}

	public void Back()
	{
		Application.LoadLevel ("scStart");
	}

	void setCharacter(int _order,string _char){

		Debug.Log ("char: "+_char);

		if (_char == "furfur")
			portrait_[_order].sprite = furfurPortrait;
		else if(_char =="barbas")
			portrait_[_order].sprite = barbasProtrait;
		else if(_char =="guci")
			portrait_[_order].sprite = guciPortrait;
		else if(_char =="stola")
			portrait_[_order].sprite = stolaPortrait;
		else if(_char =="random")
			portrait_[_order].sprite = randomPortrait;
	}

	public void remoteSetCharacter(int _order,string _char){
		while(charSelectSwitch){ }
		charSelectOrder = _order;
		charSelectCharacter = _char;
		charSelectSwitch = true;
	}
}