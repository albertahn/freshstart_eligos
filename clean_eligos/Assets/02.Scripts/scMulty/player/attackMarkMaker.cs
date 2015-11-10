using UnityEngine;
using System.Collections;

public class attackMarkMaker : MonoBehaviour {
	public GameObject attackMark;
	private GameObject pre_target;

	private GameObject marker;
	private attackMarkCtrl _markerCtrl;

	// Use this for initialization
	void Start () {
		Vector3 pos = new Vector3 (0, 0, 0);
		Quaternion rot = new Quaternion (1.0f,2.0f,3.0f,4.0f);
		marker = (GameObject)Instantiate(attackMark, pos, rot);
		_markerCtrl = marker.GetComponent<attackMarkCtrl> ();
		marker.SetActive (false);
	}

	public void mark(GameObject _target){
		if (pre_target != _target) {
			pre_target = _target;
			marker.transform.position = _target.transform.position;
			marker.transform.rotation = _target.transform.rotation;
			_markerCtrl.setTarget(_target);
			marker.SetActive(true);
		}
	}

	public void deleteMarker(){
		pre_target = null;
		marker.SetActive (false);
	}
}
