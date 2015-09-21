using UnityEngine;
using System.Collections;

public class LobbyUI : MonoBehaviour {
	public bool isUI;
	private string data;
	// Use this for initialization
	void Start () {
		isUI = true;
		data = ClientState.id;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}