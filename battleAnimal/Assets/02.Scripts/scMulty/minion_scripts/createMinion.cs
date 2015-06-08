using UnityEngine;
using System.Collections;

public class createMinion : MonoBehaviour {
	private SpawnMinion _spawnMinion;

	// Use this for initialization
	void Start(){
		_spawnMinion = GetComponent<SpawnMinion> ();
		if(ClientState.isMaster)
			StartCoroutine (startM ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	private IEnumerator startM(){
		yield return new WaitForSeconds(10.0f);
		StartCoroutine (loopM ());
	}

	private IEnumerator loopM(){
		while (true) {
			yield return new WaitForSeconds (5.0f);
			_spawnMinion.createMinion();
		}
	}
}