using UnityEngine;
using System.Collections;

public class RedCannonFire : MonoBehaviour {
	public GameObject bullet;
	public Transform firePos;
	public MeshRenderer _renderer;
	
	private float birth;
	private float duration;
	
	private GameObject[] bulletPool;
	private BulletCtrl_RedCannon[] _bulletCtrl;
	private TrailRenderer[] _trail;
	private int maxBullet;

	// Use this for initialization
	void Start () {
		maxBullet = 6;		
		bulletPool = new GameObject[maxBullet];
		_bulletCtrl = new BulletCtrl_RedCannon[maxBullet];
		_trail = new TrailRenderer[maxBullet];
		
		for (int i=0; i<maxBullet; i++) {
			bulletPool[i] = (GameObject)Instantiate(bullet);
			_bulletCtrl[i] = bulletPool[i].GetComponent<BulletCtrl_RedCannon>();
			_trail[i] =bulletPool[i].GetComponent<TrailRenderer>();
			bulletPool[i].name = "Bullet_"+i.ToString();
			bulletPool[i].transform.parent = gameObject.transform;
			bulletPool[i].SetActive(false);
		}

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
		for (int i=0; i<maxBullet; i++) {
			if(bulletPool[i].activeSelf==false){
				bulletPool[i].transform.position = firePos.position;
				_bulletCtrl[i].setTarget(_target);
				_trail[i].enabled = true;
				bulletPool[i].SetActive(true);
				break;
			}
		}
		yield return null;
	}
	
	IEnumerator ShowMuzzleFlash(){
		_renderer.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		_renderer.enabled = false;
	}
}
