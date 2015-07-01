using UnityEngine;
using System.Collections;

public class stolaStat : MonoBehaviour {
	private int maxHp;
	private float move_speed;
	private int damage;
	private float attack_distance;
	private float attack_speed;
	private int HpIncTerm;
	
	public static int skill1_damage;
	public static int skill2_damage;
	public static int skill3_damage;
	
	// Use this for initialization
	void Awake () {
		maxHp = 400;
		move_speed = 5.0f;
		damage = 20;
		attack_distance = 7.0f;
		attack_speed = 0.5f;
		HpIncTerm = 150;
		
		skill1_damage=100;
		skill2_damage=100;
		skill3_damage=100;
		initiate ();
	}
	
	public void initiate(){
		playerStat.maxHp = maxHp;
		playerStat.speed = move_speed;
		playerStat.damage = damage;
		playerStat.attack_speed = attack_speed;
		playerStat.attack_distance = attack_distance;
		playerStat.HpIncTerm = HpIncTerm;
		playerStat.skill1_damage = skill1_damage;
		playerStat.skill2_damage = skill2_damage;
		playerStat.skill3_damage = skill3_damage;
	}
}