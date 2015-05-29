using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	public Transform target;
	public float dist = 10.0f;
	public float height = 5.0f;
	public float side = 10.0f;
	public float dampRotate = 5.0f;

	private Transform tr;

	// Use this for initialization
	void Start () {
		dist = 5.0f;
		height = 15.0f;
		side = 5.0f;
		tr = GetComponent<Transform> ();
	
	}

	public void setTarget(Transform a){
		target = a;

	}
	// Update is called once per frame
	void LateUpdate () {
		if (target != null) {
			tr.position = target.position + (Vector3.up * height) - Vector3.forward*dist -  Vector3.left*side ;
			//tr.LookAt (target);
		}
	}
}