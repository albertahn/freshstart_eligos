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


	// Use this for initialization
	void Start () {
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
		GameObject a =(GameObject)Instantiate(bullet,firePos.position,firePos.rotation);
		a.GetComponent<BulletCtrl> ().setTarget(ClientState.id, _target);
		yield return null;
	}

	IEnumerator ShowMuzzleFlash(){
		_renderer.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		_renderer.enabled = false;
	}
}