using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class SocketNetworkFucker : MonoBehaviour {


	public SocketIOClient.Client socket;


	//start server
	public string PlayerUsername;

	public GameObject prefabPlayer;

	public JSONObject fuckdata = new JSONObject();

	public JSONArray arrayBitch = new JSONArray ();

	public GameObject newman_prefab;

	public GameObject otherplayer;

	public float relx, relz;

	Transform trans;

	public string positionData, xman, zman, usernamesocket;

//start socket
	private void Start(){

		//get the sample player

		trans = GameObject.Find("SamplePlayer").GetComponent<Transform> ();


		socket = new SocketIOClient.Client ("http://localhost:3000/"); //("http://localhost:3000");//


		socket.On("connect", (fn) => {


			//Debug.Log ("connect - socket");
			
			//Dictionary<string, string> args = new Dictionary<string, string>();


			//args.Add("zcock", "0");
			//args.Add("xcock", "0");

			//socket.Emit("send message", args);

			//socket.Emit("send message", "yo new socket in town");



		});

		socket.On("allppl", (data) => {

			Debug.Log ("cockfucker"+data.Json.ToJsonString());

			//string jot =  JSONObject.Parse(data).ToString();

		



		});

		socket.On ("new pos", (data) => {

			fuckdata = JSONObject.Parse(data.Json.ToJsonString());
			JSONArray awesa = fuckdata.GetArray("args");
			positionData = awesa.ToString();//(string) data.Json.ToJsonString();

			usernamesocket = awesa[0].ToString();
			
			try{
				
				
				
				if(awesa[1].Obj.GetString("xcock")!=null && awesa[1].Obj.GetString("zcock")!=null){
					
					xman =  awesa[1].Obj.GetString("xcock");
					zman =  awesa[1].Obj.GetString("zcock");

					
				}
				
				
			}catch{
				
				
				
			}



		});


		socket.On("new message", (data) => {



		//	Debug.Log ("fukeroo"+ data.ToString());

			fuckdata = JSONObject.Parse(data.Json.ToJsonString());





		});
		socket.Error += (sender, e) => {

			//Debug.Log ("socket Error: " + e.Message.ToString ());
		};
		socket.Connect();

	


		}//end

//init

	IEnumerator changePosition(string xpos, string zpos){

		//if mine dont move. if other get tage and instantiate if not in pref

		//get player name if object null isntantiate

		if (usernamesocket == PlayerPrefs.GetString ("username") ) {

			yield return null;
		}

		//otherplayer =(GameObject[]) new GameObject[] ();

		if (usernamesocket != null) {
			try{
				otherplayer =  GameObject.Find(usernamesocket);//.FindGameObjectsWithTag (usernamesocket);

			}catch{

				otherplayer =  null;
			}


		} else {

			otherplayer = null;
		
		}



		if (otherplayer == null) { //instantiate
		
			//NewPlayerConnectSpawn(usernamesocket);

		} else { //move the guy

		
			relx = float.Parse (xpos);//(float)double.Parse (xpos, System.Globalization.NumberStyles.AllowDecimalPoint);
			//float.Parse (xpos);
			relz = float.Parse (zpos);//(float)double.Parse (zpos, System.Globalization.NumberStyles.AllowDecimalPoint);

			Vector3 pos = new Vector3 (relx, 0.0f, relz);


			otherplayer.transform.position = Vector3.Lerp (otherplayer.transform.position, pos, Time.deltaTime * 10.0f); //Vector3(relx, 0, relz);



		}

		yield return null;
	
}//end changepos

// make new player instantiate

	IEnumerator NewPlayerConnectSpawn(string playername){

		//newman_prefab =(GameObject) new GameObject ();

		newman_prefab =(GameObject) Instantiate(prefabPlayer, new Vector3(0, 0, 0), Quaternion.identity);

		newman_prefab.name = playername;

		//newman_prefab.GetInstanceID = playername; //.tag = playername;

		Debug.Log ("created player spawn : "+ playername);


		yield return null;

	}

	void Update () {


		//update  player position

		if (zman != null && xman != null) {
			
			
			StartCoroutine(this.changePosition(xman, zman));
		}



		//check for other players

		otherplayer =  GameObject.Find(usernamesocket); 

		if (otherplayer == null && usernamesocket!="" && usernamesocket !=null) { //instantiate
			
			StartCoroutine(this.NewPlayerConnectSpawn (usernamesocket));
		}

		
		
		
	}
	
	
	
	
	
	
	public void OnGUI(){

		//label for position

		GUILayout.Label("position: "+  positionData);

		
		GUILayout.Label ("relx: " + relx);

		GUILayout.Label ("relz: " + relz);

		GUILayout.Label ("myusername:  "+  usernamesocket);



		if (GUI.Button (new Rect (280, 270, 150, 30), "SEND messageer")) {
			
			//Debug.Log ("Sending");
			Dictionary<string, string> args = new Dictionary<string, string>();
			args.Add("msg", "hello!");
			socket.Emit("send message", args);
			
		}

		if (GUI.Button (new Rect (320, 170, 150, 30), "get all users on socket")) {
			
			//Debug.Log ("Sending");
			Dictionary<string, string> args = new Dictionary<string, string>();
			args.Add("msg", "hello!");
			socket.Emit("getallppl", args);
			
		}

		if (GUI.Button (new Rect (520, 170, 150, 30), "sendpos")) {
			
			//Debug.Log ("Sending");
			Dictionary<string, string> args = new Dictionary<string, string>();
			args.Add("0.22", "0.222");
			socket.Emit("sendpos", args);
			
		}


		PlayerUsername = GUI.TextArea (new Rect(20, 20,200,50), PlayerUsername,  100);
		
		if (GUI.Button (new Rect (120, 70, 150, 30), "SEND adduser name")) {

			
//			Dictionary<string, string> args = new Dictionary<string, string>();
		
			socket.Emit("adduser", PlayerUsername);

			
			PlayerPrefs.SetString("username", PlayerUsername);

		}
		
		if (GUI.Button (new Rect (120, 120, 150, 30), "Close Connection")) {
			//Debug.Log ("Closing");
			
			socket.Close();
		}


//spawn player
		if (GUI.Button (new Rect (220, 120, 150, 30), "spawnplayer")) {

			Vector3 pos = new Vector3(20.0f, 0.0f, 20.0f);
			//Instantiate(prefabPlayer, pos, Quaternion.identity);

			Instantiate(prefabPlayer, pos,  Quaternion.identity);

		}





	}//end gui

//gui

		
	public void sendfukMessage(Dictionary<string, string>  message){

		Debug.Log ("sending: " +message);

		socket.Emit("sendpos", message);

	}

}
