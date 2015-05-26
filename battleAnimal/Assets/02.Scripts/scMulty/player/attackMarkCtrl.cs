using UnityEngine;
using System.Collections;

public class attackMarkCtrl : MonoBehaviour {
	public GameObject target;
	private Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
	}

	public void setTarget(GameObject _target){
		target = _target;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
						tr.position = target.transform.position;
		} else {			
			Destroy (this.gameObject);
		}
	}
}
