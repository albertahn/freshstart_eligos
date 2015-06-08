using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	[System.Serializable]
	public struct TeamStruct{
		public string name;
		public string team;
		public string level;
	}

	public int idx;

	public TeamStruct[] _teamStruct;

	// Use this for initialization
	void Awake () {
		idx = 0;
		_teamStruct = new TeamStruct[6];
		for (int i=0; i<6; i++)
			_teamStruct [i] = new TeamStruct ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
