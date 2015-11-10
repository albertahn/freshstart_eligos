using UnityEngine;
using System.Collections;

public class Guci_thirdSkill : MonoBehaviour {
	public GameObject bulleta;
	private float distance;
	private MoveCtrl _moveCtrl;

	private bool playermoving;
	private Vector3 clickendpoint;
	private float terrainHeight;
	private CharacterController _controller;
	private AniCtrl _aniCtrl;
	private Transform tr;
	
	private Coroutine createBulletCoroutine;
	// Use this for initialization
	void Start () {
		_moveCtrl = GetComponent<MoveCtrl> ();
		distance = 10.0f;

		tr = transform;
		terrainHeight = 50.1f;
		_controller = GetComponent<CharacterController> ();
		_aniCtrl = GetComponent<AniCtrl> ();
	}

	public void cancleSkill(){
		if (createBulletCoroutine != null) {
			StopCoroutine (createBulletCoroutine);
			playermoving = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (playermoving) {
			tr.LookAt (clickendpoint);
			
			float step = playerStat.speed * Time.deltaTime;
			
			clickendpoint.y = terrainHeight;
			Vector3 dir = clickendpoint - tr.position;
			Vector3 movement = dir.normalized * step;
			if (movement.magnitude > dir.magnitude)
				movement = dir;
			_controller.Move (movement);
			
			_aniCtrl._animation.CrossFade (_aniCtrl.anim.run.name, 0.3f);
			_aniCtrl._animation ["attack"].speed = 2.5f;
			_aniCtrl._animation ["run"].speed = 2.5f;
		}
	}
	
	public void startSkill(string firedBy,Vector3 _pos){
		createBulletCoroutine = StartCoroutine (this.CreateBullet (firedBy,_pos));
		StartCoroutine (this.ShowMuzzleFlash ());
	}
	
	IEnumerator CreateBullet(string firedBy,Vector3 _pos){
		while (true) {
						float dist = Vector3.Distance (this.transform.position, _pos);
						if (dist > distance) {
								clickendpoint = _pos;
								clickendpoint.y = _moveCtrl.terrainHeight;
								playermoving = true;
			} else {
				playermoving = false;
								GameObject a = (GameObject)Instantiate (bulleta, _pos, this.transform.rotation);
								a.GetComponent<Guci_skill3Ctrl>().setOwner(this.name);
								_moveCtrl.idle();
								break;	
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	
	IEnumerator ShowMuzzleFlash(){
		//_renderera.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		//	_renderera.enabled = false;
	}
}