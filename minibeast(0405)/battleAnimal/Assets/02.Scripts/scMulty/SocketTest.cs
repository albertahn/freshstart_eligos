using UnityEngine;
using System.Collections;

public class SocketTest : MonoBehaviour
{
	void Start()
	{
		SocketStarter.Socket.On("MsgRes", (data) =>
		{
			Debug.Log(data.Json.args[0]);
		});
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 150, 100), "SEND"))
			SocketStarter.Socket.Emit("Msg", "Hello, World!");
	}
}