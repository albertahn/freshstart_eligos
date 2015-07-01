using UnityEngine;
using System.Collections;

public class playerStat : MonoBehaviour {
	public static int maxHp;
	public static float speed;
	public static int damage;
	public static float attack_distance;
	public static float attack_speed;
	
	public static int skill1_damage;
	public static int skill2_damage;
	public static int skill3_damage;
	
	public static int HpIncTerm;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}	
	
	public static void changeHp(int a){
		maxHp += a;
	}
	
	public static void changeSpeed(float a){
		speed += a;
	}
	
	public static void changeDamage(int a){
		damage+= a;
	}
}