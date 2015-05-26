using UnityEngine;
using System.Collections;

public class swirl_sword : MonoBehaviour {
	
	public GameObject bulleta;

	public GameObject glyphAround;
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
	
	public void swirlIt(string firedBy){
		
		
		Debug.Log ("fireman1: "+ firedBy);
		
		StartCoroutine (this.CreateBullet (firedBy));
		StartCoroutine (this.ShowMuzzleFlash ());
		birth = Time.time;
		
		
	}
	
	IEnumerator CreateBullet(string firedBy){
		
		GameObject a = (GameObject)Instantiate(bulleta,firePosa.position,firePosa.rotation);
		
		a.GetComponent<SwirlSwordCtrl> ().shotByname(firedBy);

		GameObject meobj = GameObject.Find (firedBy);

		a.transform.parent = meobj.transform;

		
		yield return null;
	}
	
	IEnumerator ShowMuzzleFlash(){
		//_renderera.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		//	_renderera.enabled = false;
	}
}