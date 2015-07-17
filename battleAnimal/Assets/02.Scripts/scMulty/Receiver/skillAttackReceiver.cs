using UnityEngine;
using System.Collections;

public class skillAttackReceiver : MonoBehaviour {
	private GameObject _obj;
	private bool switch_;
	private string _id;
	private string[] posTemp;
	private Vector3 _pos;
	private string _character;
	private string _skill_num;

	private GuciSkill_GUI _guci_skill;


	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			_obj = GameObject.Find (_id);
			if(_character=="guci")
			{
				guciProcess();
			}
			switch_=false;
		}	
	}

	public void guciProcess(){
		_guci_skill = _obj.GetComponent<GuciSkill_GUI> ();
		if (_skill_num == "first") {
						_guci_skill.fireFirst(_obj,_pos,_id);
		}
	}

	public void receive(string data){
		string[] temp = data.Split (':');
		_id = temp[0];
		posTemp = temp [1].Split (',');
		_pos = new Vector3 (float.Parse(posTemp [0]),
		                   float.Parse(posTemp [1]),
		                   float.Parse(posTemp [2]));
		_character = temp [2];
		_skill_num = temp [3];

		switch_ = true;
	}

	private IEnumerator doit(){
		yield return null;
	}
}
