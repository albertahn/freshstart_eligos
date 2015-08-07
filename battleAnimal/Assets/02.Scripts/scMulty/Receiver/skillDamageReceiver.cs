using UnityEngine;
using System.Collections;

public class skillDamageReceiver : MonoBehaviour {
	private bool switch_;

	private string _id,_char,_skill,_targetName,_order,_damage;
	private GameObject _targetObj;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			StartCoroutine(doit());
			switch_=false;
		}	
	}
	public void receive(string data){
		string[] temp = data.Split (':');

		Debug.Log ("skill damage receive = "+data);

		_id = temp [0];
		_char = temp [1];
		_skill = temp [2];
		_targetName = temp [3];
		_order = temp [4];
		_damage = temp [5];

		switch_ = true;
	}
	
	private IEnumerator doit(){
		_targetObj = GameObject.Find (_targetName);
		if(_char=="guci")
		{
			guciProcess();
		}else if(_char=="stola")
		{
			stolaProcess();
		}else if(_char=="furfur")
		{
			furfurProcess();
		}else if(_char=="barbas")
		{
			barbasProcess();
		}
		yield return null;
	}

	public void guciProcess(){
		if (_skill == "first") {
			switch (int.Parse(_order)) {
			case 1:
				_targetObj.GetComponent<minion_state> ().Heated ("skill", gameObject,int.Parse(_damage));
				break;
			case 2:
				_targetObj.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 3:
				_targetObj.GetComponent<PlayerHealthState> ().hitbySkill (_id, this.gameObject,int.Parse(_damage));
				break;
			}
		}else if (_skill == "second") {
			//_guci_skill.skill2Net();
		}else if (_skill == "third") {
			switch (int.Parse(_order)) {
			case 1:
				_targetObj.GetComponent<minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 2:
				_targetObj.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 3:
				_targetObj.GetComponent<PlayerHealthState> ().hitbySkill (_id, this.gameObject,int.Parse(_damage));
				break;
			}
		}
	}
	
	public void barbasProcess(){
		if (_skill == "first") {
			switch (int.Parse(_order)) {
			case 1:
				_targetObj.GetComponent<minion_state> ().Heated ("skill", gameObject,int.Parse(_damage));
				break;
			case 2:
				_targetObj.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 3:
				_targetObj.GetComponent<PlayerHealthState> ().hitbySkill (_id, this.gameObject,int.Parse(_damage));
				break;
			}
		}else if (_skill == "third") {
			switch (int.Parse(_order)) {
			case 1:
				_targetObj.GetComponent<minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 2:
				_targetObj.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 3:
				_targetObj.GetComponent<PlayerHealthState> ().hitbySkill (_id, this.gameObject,int.Parse(_damage));
				break;
			}
		}
	}
	
	public void furfurProcess(){
		if (_skill == "first") {
			switch (int.Parse(_order)) {
			case 1:
				_targetObj.GetComponent<minion_state> ().Heated ("skill", gameObject,int.Parse(_damage));
				break;
			case 2:
				_targetObj.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 3:
				_targetObj.GetComponent<PlayerHealthState> ().hitbySkill (_id, this.gameObject,int.Parse(_damage));
				break;
			}
		}else if (_skill == "third") {
			switch (int.Parse(_order)) {
			case 1:
				_targetObj.GetComponent<minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 2:
				_targetObj.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 3:
				_targetObj.GetComponent<PlayerHealthState> ().hitbySkill (_id, this.gameObject,int.Parse(_damage));
				break;
			}
		}
	}
	
	public void stolaProcess(){
		if (_skill == "first") {
			switch (int.Parse(_order)) {
			case 1:
				_targetObj.GetComponent<minion_state> ().Heated ("skill", gameObject,int.Parse(_damage));
				break;
			case 2:
				_targetObj.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 3:
				_targetObj.GetComponent<PlayerHealthState> ().hitbySkill (_id, this.gameObject,int.Parse(_damage));
				break;
			}
		}else if (_skill == "third") {
			switch (int.Parse(_order)) {
			case 1:
				_targetObj.GetComponent<minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 2:
				_targetObj.GetComponent<blue_minion_state> ().Heated ("skill", gameObject, int.Parse(_damage));
				break;
			case 3:
				_targetObj.GetComponent<PlayerHealthState> ().hitbySkill (_id, this.gameObject,int.Parse(_damage));
				break;
			}
		}
	}
}