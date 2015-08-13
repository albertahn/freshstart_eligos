using UnityEngine;
using System.Collections;

public class Guci_secondSkill: MonoBehaviour {
	private float speedTerm;
	private float duration;

	// Use this for initialization
	void Start () {
		speedTerm = 5.0f;
		duration = 2.0f;
	}

	public void startSkill(){
		playerStat.speed += speedTerm;
		StartCoroutine (stopSkill ());
	}

	private IEnumerator stopSkill(){
		yield return new WaitForSeconds (duration);
		playerStat.speed -= speedTerm;
	}
}
