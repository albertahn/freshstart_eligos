using UnityEngine;
using System.Collections;

public class Guci_firstSkill : MonoBehaviour {
	public GameObject bulleta;
	public Transform firePosa;
	//public MeshRenderer _renderera;
	
	private float birth;
	private float duration;
	
	public float distance;
	private MoveCtrl _moveCtrl;
	
	
	// Use this for initialization
	void Start () {
		//_renderera.enabled = false;
		_moveCtrl = GetComponent<MoveCtrl> ();	
		duration = 0.5f;
		distance = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void fireBall(string firedBy,Vector3 _pos){
		StartCoroutine (this.CreateBullet (firedBy,_pos));
		StartCoroutine (this.ShowMuzzleFlash ());
		birth = Time.time;
	}

	
	IEnumerator CreateBullet(string firedBy,Vector3 _pos){
		while (true) {
			yield return new WaitForSeconds(1.0f);
			float dist = Vector3.Distance (this.transform.position, _pos);
			if (dist > distance) {
				_moveCtrl.clickendpoint = _pos;
				_moveCtrl.clickendpoint.y = _moveCtrl.terrainHeight;
				//_moveCtrl.isMoveAndAttack = true;
				_moveCtrl.playermoving = true;
				//moveAndAttack ();
			} else {
				GameObject a = (GameObject)Instantiate (bulleta, firePosa.position, this.transform.rotation);
				a.GetComponent<GUCI_firstBulletCtrl>().setTarget (firedBy, _pos);
				_moveCtrl.idle();
				break;	
			}
		}
	}
	
	/*IEnumerator CreateBullet(string firedBy,Vector3 _pos){
		
		GameObject a = (GameObject)Instantiate(bulleta,firePosa.position,firePosa.rotation);
		
		a.GetComponent<GUCI_firstBulletCtrl> ().setTarget (firedBy, _pos);
		
		
		yield return null;
	}*/
	
	IEnumerator ShowMuzzleFlash(){
		//_renderera.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		//	_renderera.enabled = false;
	}
}