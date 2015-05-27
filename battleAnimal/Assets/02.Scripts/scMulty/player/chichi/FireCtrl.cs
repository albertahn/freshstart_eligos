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
	private int maxBullet;

	// Use this for initialization
	void Start () {
		maxBullet = 6;
		bulletPool = new GameObject[maxBullet];
		_bulletCtrl = new BulletCtrl[maxBullet];
		for (int i=0; i<maxBullet; i++) {
			bulletPool[i] = (GameObject)Instantiate(bullet);
			_bulletCtrl[i] = bulletPool[i].GetComponent<BulletCtrl>();
			bulletPool[i].name = "Bullet_"+i.ToString();
			bulletPool[i].SetActive(false);
			bulletPool[i].transform.parent = gameObject.transform;
		}

		_renderer.enabled = false;	
		duration = 0.5f;
		distance = 7.0f;
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