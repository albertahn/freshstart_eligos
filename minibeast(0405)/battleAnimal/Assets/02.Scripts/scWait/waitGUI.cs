using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class waitGUI : MonoBehaviour {
	public Sprite dogPortrait,turtlePortrait,randomPortrait,emptyPortrait,nameTag;

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
			ClientState.character = "dog";					
			break;
		case 2:
			ClientState.character = "turtle";						
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
	}

	void OnGUI(){
		/*
		GUI.DrawTexture (new Rect(20,50,100,100),portrait[0]);
		GUI.DrawTexture (new Rect(20,160,100,40),nameTag);
		GUI.Label (new Rect(40,170,100,40),name[0]);
		GUI.DrawTexture (new Rect(160,50,100,100),portrait[1]);
		GUI.DrawTexture (new Rect(160,160,100,40),nameTag);
		GUI.Label (new Rect(180,170,100,40),name[1]);
		GUI.DrawTexture (new Rect(300,50,100,100),portrait[2]);
		GUI.DrawTexture (new Rect(300,160,100,40),nameTag);
		GUI.Label (new Rect(320,170,100,40),name[2]);

				
		GUI.DrawTexture (new Rect(20,220,100,100),portrait[3]);
		GUI.DrawTexture (new Rect(20,330,100,40),nameTag);
		GUI.Label (new Rect(40,340,100,40),name[3]);
		GUI.DrawTexture (new Rect(160,220,100,100),portrait[4]);
		GUI.DrawTexture (new Rect(160,330,100,40),nameTag);
		GUI.Label (new Rect(180,340,100,40),name[4]);
		GUI.DrawTexture (new Rect(300,220,100,100),portrait[5]);
		GUI.DrawTexture (new Rect(300,330,100,40),nameTag);
		GUI.Label (new Rect(320,340,100,40),name[5]);
*/

		GUI.Label(new Rect(200,10,50,50),"id = "+ClientState.id);
		GUI.Label(new Rect(200,70,50,50),"room = "+ClientState.room);
		GUI.Label(new Rect(200,130,50,50),"order = "+ClientState.order);
		GUI.Label(new Rect(200,190,50,50),"character = "+ClientState.character);
		GUI.Label(new Rect(200,250,50,50),"team = "+ClientState.team);
	}

	public void Dog_bot()
	{
		//portrait[ClientState.order] = dogPortrait;
		string data = ClientState.id +":"+ClientState.order+":dog";
		waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
		ClientState.character = "dog";
	}

	public void Turtle_bot()
	{
		//portrait[ClientState.order] = turtlePortrait;
		string data = ClientState.id +":"+ClientState.order+":turtle";
		waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
		ClientState.character = "turtle";	
	}

	public void Random_bot()
	{
		//portrait[ClientState.order] = turtlePortrait;
		string data = ClientState.id +":"+ClientState.order+":random";
		waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
		switch(Random.Range(1,3)){
		case 1:
			ClientState.character = "dog";					
			break;
		case 2:
			ClientState.character = "turtle";						
			break;
		}
	}


	public void Ready()
	{
		if(ClientState.order % 2 ==0){
			ClientState.team = "red";
		}else
			ClientState.team = "blue";

		Application.LoadLevel("scMulty");
	}

	public void Back()
	{
		Application.LoadLevel ("scStart");
	}

	void setCharacter(int _order,string _char){
		if (_char == "dog")
			portrait_[_order].sprite = dogPortrait;
		else if(_char =="turtle")
			portrait_[_order].sprite = turtlePortrait;
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