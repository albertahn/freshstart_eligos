using UnityEngine;
using System.Collections;

public class dogStat : MonoBehaviour {
	private int maxHp;
	private float move_speed;
	private int damage;
	private float attack_distance;
	private float attack_speed;

	// Use this for initialization
	void Awake () {
		maxHp = 400;
		move_speed = 5.0f;
		damage = 20;
		attack_distance = 7.0f;
		attack_speed = 0.5f;
		initiate ();
	}

	public void initiate(){
		playerStat.maxHp = maxHp;
		playerStat.speed = move_speed;
		playerStat.damage = damage;
		playerStat.attack_speed = attack_speed;
		playerStat.attack_distance = attack_distance;
	}
}
