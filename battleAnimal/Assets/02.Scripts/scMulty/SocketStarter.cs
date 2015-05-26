using UnityEngine;
using System.Collections;
using SocketIOClient; // 이 네임스페이스를 반드시 추가해주어야 함.

public class SocketStarter : MonoBehaviour
{
	string url;
	public static Client Socket { get; private set; }
	
	void Awake()
	{
		url = "http://119.9.76.77:8000"; //"http://192.168.0.21:3000";//"http://127.0.0.1:3000/";//"http://119.9.76.77:3000/";
		Socket = new Client(url);
		Socket.Opened += SocketOpened;
		Socket.Connect();
	}
	
	private void SocketOpened(object sender, System.EventArgs e)
	{
		Debug.Log("Socket Opened!");
	}
	
	void OnDisable()
	{
		Socket.Close();
	}
}
