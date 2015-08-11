using UnityEngine;
using System.Collections;
using SocketIOClient; // 이 네임스페이스를 반드시 추가해주어야 함.

public class waitSocketStarter : MonoBehaviour
{
	string url;
	public static Client Socket { get; private set; }
	
	void Awake()
	{
		url = "http://119.9.76.77:8080/";//"http://127.0.0.1:8080/";//"http://119.9.76.77:8080/";
		Socket = new Client(url);
		Socket.Opened += SocketOpened;
		Socket.Connect();

		Debug.Log("roo :"+ ClientState.room);
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

//4시 코칭 강좌