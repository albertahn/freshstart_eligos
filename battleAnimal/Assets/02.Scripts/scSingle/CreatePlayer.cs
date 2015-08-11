using UnityEngine;
using System.Collections;

public class CreatePlayer : MonoBehaviour {
	private Vector3 RspawnPoint, BspawnPoint;
	private SpawnPlayer _spawnPlayer;
	private createMinion _createMinion;

	// Use this for initialization
	void Start () {
		_spawnPlayer = GetComponent<SpawnPlayer> ();
		_createMinion = GetComponent<createMinion> ();

		RspawnPoint = GameObject.Find ("RedTeam/spawnPoint").transform.position;
		BspawnPoint = GameObject.Find ("BlueTeam/spawnPoint").transform.position;
		ClientState.team = "red";
		ClientState.isMaster = true;
		_spawnPlayer.setSpawn(ClientState.id,ClientState.character,ClientState.team);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
