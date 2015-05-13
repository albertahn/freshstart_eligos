using UnityEngine;
using System.Collections;

public class dogStat : MonoBehaviour {
	private int maxHp;
	private float speed;
	private int damage;

	// Use this for initialization
	void Awake () {
		maxHp = 500;
		speed = 5.0f;
		damage = 20;
		initiate ();
	}

	public void initiate(){
		playerStat.maxHp = maxHp;
		playerStat.speed = speed;
		playerStat.damage = damage;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
