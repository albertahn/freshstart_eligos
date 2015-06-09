using UnityEngine;
using System.Collections;

public class HP_Bar : MonoBehaviour {
	private GameObject _camera;
	private Transform tr;
	private Transform cameraTr;
	//public Transform target;
	
	//float zdepth;
	
	// Use this for initialization
	void Start () {
		_camera = GameObject.Find ("CameraWrap/Main Camera");
		tr = GetComponent<Transform> ();
		cameraTr = _camera.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		tr.LookAt (_camera.transform);
		tr.rotation= Quaternion.Euler(60,320,tr.rotation.z);
	}
}
