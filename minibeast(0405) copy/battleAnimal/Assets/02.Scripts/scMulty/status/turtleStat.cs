using UnityEngine;
using System.Collections;

public class turtleStat : MonoBehaviour {
	private int maxHp;
	private float speed;
	private int damage;

	// Use this for initialization
	void Awake () {
		maxHp = 1000;
		speed = 5.0f;
		damage = 10;
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
