using UnityEngine;
using System.Collections;

public class Barbas_firstSkill : MonoBehaviour {
	public GameObject bulleta;
	public Transform firePosa;
	//public MeshRenderer _renderera;
	
	private float birth;
	private float duration;
	
	public float distancea;
	
	
	// Use this for initialization
	void Start () {
		//_renderera.enabled = false;	
		duration = 0.5f;
		distancea = 10.0f;
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
		
		GameObject a = (GameObject)Instantiate(bulleta,firePosa.position,firePosa.rotation);
		
		a.GetComponent<GUCI_firstBulletCtrl> ().setTarget (firedBy, _pos);
		
		
		yield return null;
	}
	
	IEnumerator ShowMuzzleFlash(){
		//_renderera.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		//	_renderera.enabled = false;
	}
}