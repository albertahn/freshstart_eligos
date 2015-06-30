﻿using UnityEngine;
using System.Collections;

public class Furfur_secondSkill : MonoBehaviour {

	public GameObject effect;
	//public MeshRenderer _renderera;
	
	private float birth;
	public float duration;
	
	GameObject a;
	// Use this for initialization
	void Start () {
		//_renderera.enabled = false;	
		duration =10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startSkill(string firedby){		
		a = (GameObject)Instantiate(effect);
		Vector3 _pos = this.transform.position;
		a.transform.position = _pos;
		a.transform.parent = GameObject.Find (firedby).transform;
		StartCoroutine(stopSkill ());
	}

	IEnumerator stopSkill(){
		yield return new WaitForSeconds (duration);
		Destroy (a);
	}

}