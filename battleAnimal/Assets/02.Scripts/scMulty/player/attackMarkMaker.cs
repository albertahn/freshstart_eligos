using UnityEngine;
using System.Collections;

public class attackMarkMaker : MonoBehaviour {
	public GameObject attackMark;
	private GameObject pre_target;
	private GameObject marker;

	// Use this for initialization
	void Start () {
	
	}

	public void mark(GameObject _target){
		if (pre_target != _target) {
			if(pre_target!=null)
				Destroy (marker.gameObject);
			GameObject a = (GameObject)Instantiate (attackMark, _target.transform.position, _target.transform.rotation);
			a.GetComponent<attackMarkCtrl> ().setTarget (_target);
			pre_target = _target;
			marker = a;
		} else {
			if(pre_target==null){				
				GameObject a = (GameObject)Instantiate (attackMark, _target.transform.position, _target.transform.rotation);
				a.GetComponent<attackMarkCtrl> ().setTarget (_target);
				pre_target = _target;
				marker = a;
			}
		}
	}

	public void deleteMarker(){
		if (marker != null) {
			pre_target = null;
			Destroy (marker.gameObject);
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
