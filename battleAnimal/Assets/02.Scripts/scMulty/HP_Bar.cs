using UnityEngine;
using System.Collections;

public class HP_Bar : MonoBehaviour {
	private GameObject _camera;
	private Transform tr;
	//public Transform target;

	//float zdepth;

	// Use this for initialization
	void Start () {
		_camera = GameObject.Find ("CameraWrap/Main Camera");
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		tr.LookAt (_camera.transform);
		tr.Rotate (0,180,0);

		//transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);      // Y 축 고정
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0,0);       // X 축 고정
		//transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z);

		/*if (target != null) {
			zdepth = Vector3.Distance(_camera.transform.position,target.position);
			Vector2 vc2=Vector2.zero;
			vc2 = Camera.main.WorldToScreenPoint(target.position);
			vc2.y +=10.0f;

			//tr.position = Camera.main.ScreenToWorldPoint(new Vector3(vc2.x,vc2.y,zdepth));
			//tr.position = target.position+new Vector3(0,3.0f,0);
		}*/
	}
}
