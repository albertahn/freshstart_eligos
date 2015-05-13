using UnityEngine;
using System.Collections;

public class BlueCannonFire : MonoBehaviour {
	public GameObject bullet;
	public Transform firePos;
	public MeshRenderer _renderer;
	
	private float birth;
	private float duration;
	
	
	// Use this for initialization
	void Start () {
		_renderer.enabled = false;	
		duration = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Fire(string _target){
		if ((Time.time - birth) > duration) {
			StartCoroutine (this.CreateBullet (_target));
			StartCoroutine (this.ShowMuzzleFlash ());
			birth = Time.time;
		}
	}
	
	IEnumerator CreateBullet(string _target){
		GameObject a =(GameObject)Instantiate(bullet,firePos.position,firePos.rotation);
		a.GetComponent<BulletCtrl_BlueCannon> ().setTarget(_target);
		yield return null;
	}
	
	IEnumerator ShowMuzzleFlash(){
		_renderer.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		_renderer.enabled = false;
	}
}
