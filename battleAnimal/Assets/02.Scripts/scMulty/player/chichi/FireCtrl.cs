using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour {

	public GameObject bullet;
	public Transform firePos;
	public MeshRenderer _renderer;

	private float birth;
	private float duration;

	public float distance;
	public AudioClip fireSfx;

	private GameObject[] bulletPool;
	private BulletCtrl[] _bulletCtrl;
	private TrailRenderer[] _trail;
	private int maxBullet;


	// Use this for initialization
	void Start () {
		maxBullet = 6;
		bulletPool = new GameObject[maxBullet];
		_bulletCtrl = new BulletCtrl[maxBullet];
		_trail = new TrailRenderer[maxBullet];

		for (int i=0; i<maxBullet; i++) {
			bulletPool[i] = (GameObject)Instantiate(bullet);
			_bulletCtrl[i] = bulletPool[i].GetComponent<BulletCtrl>();
			_trail[i] =bulletPool[i].GetComponent<TrailRenderer>();
			bulletPool[i].name = "Bullet_"+i.ToString();
			bulletPool[i].transform.parent = GameObject.Find("bullets").transform;
			bulletPool[i].SetActive(false);
		}

		_renderer.enabled = false;	
		duration = playerStat.attack_speed;
		distance = playerStat.attack_distance;
	}

	public void Fire(string _target){
		if ((Time.time - birth) > duration) {
			StartCoroutine (this.CreateBullet (_target));
			StartCoroutine (this.ShowMuzzleFlash ());
			//StartCoroutine (this.PlaySfx(fireSfx));
			birth = Time.time;
		}
	}
	IEnumerator PlaySfx(AudioClip _clip){
		audio.PlayOneShot (_clip, 0.9f);
		yield return null;
	}

	IEnumerator CreateBullet(string _target){
		for (int i=0; i<maxBullet; i++) {
			if(bulletPool[i].activeSelf==false){
				bulletPool[i].transform.position = firePos.position;
				_bulletCtrl[i].setTarget(ClientState.id, _target);
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