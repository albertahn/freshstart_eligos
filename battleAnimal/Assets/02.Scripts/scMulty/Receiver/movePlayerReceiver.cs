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
		limit = 10.0f;
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
	
	float distance(Vector3 a,Vector3 b){
		return Mathf.Sqrt ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
	}

	private IEnumerator doit(){
		GameObject a = GameObject.Find (id);
		
		if (a != null) {
			//if(distance(a.transform.position,currPos)>limit)
			a.transform.position = currPos;
			a.transform.LookAt( destPos);
		//	if(character=="dog"){
		//		Debug.Log ("dog");
				_moveCtrl = a.GetComponent<MoveCtrl>();
				_moveCtrl.clickendpoint= destPos;
				_moveCtrl.move();
		/*	}
			else if(character=="turtle"){
				Debug.Log ("turtle");
				_tutu_moveCtrl = a.GetComponent<tutu_MoveCtrl>();
				_tutu_moveCtrl.clickendpoint= destPos;
				_tutu_moveCtrl.move();
			}*/
		}
		yield return null;
	}
}