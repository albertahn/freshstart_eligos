using UnityEngine;
using System.Collections;

public class GUCI_firstBulletCtrl : MonoBehaviour {
	
	public int damage;
	private float speed;
	public float birth;
	private float durationTime;
	public string firedByName;
	public GameObject damageEffectObject;
	private SpriteRenderer damageEffectSprite;
	
	// Use this for initialization
	void Start () {
		damage = 50;
		speed = 1000.0f;
		rigidbody.AddForce (transform.forward * speed);
		birth = Time.time;
		durationTime = 5.0f;

		damageEffectSprite = damageEffectObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birth) > durationTime)
			Destroy (this.gameObject);	
	}
	
	public void shotByname(string firedBy,Vector3 _pos){
		firedByName = firedBy;	
	}
	
	void OnTriggerEnter(Collider coll){
		Debug.Log ("coll.gameObject.tag = "+coll.gameObject.tag);
		if (coll.gameObject.tag == "MINION") {			
						string hitParentName = coll.transform.parent.name;
						string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
						if ((ClientState.team == "red" && coll.name [0] == 'b') ||
								(ClientState.team == "blue" && coll.name [0] == 'r')) {
								//Debug.Log("skill first hit min");

								damageEffectFunc (this.transform.position);

								if (coll.gameObject.name [0] == 'r')
										coll.gameObject.GetComponent<minion_state> ().Heated ("skill", gameObject, damage);
								else if (coll.gameObject.name [0] == 'b')
										coll.gameObject.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, damage);
								Destroy (this.gameObject);
						}
				} else if (coll.gameObject.tag == "Player" && coll.name != "touchCollider") {
			
						string hitParentName = coll.transform.parent.name;
						string firedparentName = GameObject.Find (firedByName).transform.parent.name;
			
						if (hitParentName != firedparentName && hitParentName != firedByName) {
								Debug.Log ("hit target = " + coll.name);

								coll.gameObject.GetComponent<PlayerHealthState> ().hitbySkill (firedByName, this.gameObject);
								Destroy (this.gameObject);
						}//if
				} else if (coll.gameObject.tag == "FLOOR") {
						damageEffectFunc (this.transform.position);
				}
	}//end colide

	void damageEffectFunc(Vector3 _pos){
		GameObject a = (GameObject)Instantiate(damageEffectObject);
		_pos.y += 3.0f;
		a.transform.position = _pos;
	}
}