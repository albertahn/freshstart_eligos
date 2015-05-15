using UnityEngine;
using System.Collections;

public class expBar : MonoBehaviour {
	private GameObject player;
	public int maxExp;
	private float maxUI;

	// Use this for initialization
	void Start () {
		maxUI = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3 (0, 1, 1);
	}

	public void setPlayer(){
		player = GameObject.Find (ClientState.id);
		maxExp = ClientState.maxExp[ClientState.level-1];
	}

	public void setExp(){
		maxExp = ClientState.maxExp[ClientState.level-1];
	}

	// Update is called once per frame
	void Update () {
		if (player != null) {
			int exp = ClientState.exp;
			Vector3 temp = new Vector3 ((maxUI*(float)exp) / maxExp, 1, 1);
			GetComponent<RectTransform> ().localScale = temp;
		}
	}
}
