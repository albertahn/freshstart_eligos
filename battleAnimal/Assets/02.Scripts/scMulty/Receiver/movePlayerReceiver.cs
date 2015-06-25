using UnityEngine;
using System.Collections;

public class movePlayerReceiver : MonoBehaviour {
	private bool switch_;
	private string id;
	private string character;
	private Vector3 destPos;
	private Vector3 currPos;
	private MoveCtrl _moveCtrl;
	//private tutu_MoveCtrl _tutu_moveCtrl;
	
	private float limit;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
		limit = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (switch_) {
			StartCoroutine(doit ());
			switch_ = false;
		}
	}
	
	public void receive(string data){
		string[] temp = data.Split (':');
		string[] posTemp;
		while (switch_) {}
		id = temp [0];
		character = temp [1];
		posTemp = temp [2].Split (',');
		currPos = new Vector3(float.Parse(posTemp[0]),
		                      float.Parse(posTemp[1]),
		                      float.Parse(posTemp[2]));
		posTemp = temp [3].Split (',');
		destPos = new Vector3(float.Parse(posTemp[0]),
		                      float.Parse(posTemp[1]),
		                      float.Parse(posTemp[2]));
		switch_ = true;
	}
	
	private IEnumerator doit(){
		GameObject a = GameObject.Find (id);
		
		if (a != null) {
			if(Vector3.Distance(a.transform.position,currPos)>limit)
				a.transform.position = currPos;
			
			a.transform.LookAt( destPos);
			
			_moveCtrl = a.GetComponent<MoveCtrl>();
			_moveCtrl.clickendpoint= destPos;
			_moveCtrl.move();
		}
		yield return null;
	}
}