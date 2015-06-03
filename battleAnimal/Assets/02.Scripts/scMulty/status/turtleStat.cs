using UnityEngine;
using System.Collections;

public class turtleStat : MonoBehaviour {
	private int maxHp;
	private float move_speed;
	private int damage;
	private float attack_distance;
	private float attack_speed;

	// Use this for initialization
	void Awake () {
		maxHp = 500;
		move_speed = 5.0f;
		damage = 10;
		attack_distance=4.0f;
		attack_speed=0.4f;
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
