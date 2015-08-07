using UnityEngine;
using System.Collections;

public class skillAttackReceiver : MonoBehaviour {
	private GameObject _obj;
	private bool switch_;
	private string _id;
	private string[] posTemp;
	private Vector3 _currPos;
	private Vector3 _targetPos;
	private string _character;
	private string _skill_num;

	private GuciSkill_GUI _guci_skill;
	private StolaSkill_GUI _stola_skill;
	private FurfurSkill_GUI _furfur_skill;
	private Barbas_GUI _barbas_skill;

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

	public void guciProcess(){
		_obj.transform.position = _currPos;
		_guci_skill = _obj.GetComponent<GuciSkill_GUI> ();
		if (_skill_num == "first") {
			_guci_skill.skill1Net(_targetPos);
		}else if (_skill_num == "second") {
			_guci_skill.skill2Net();
		}else if (_skill_num == "third") {
			_guci_skill.skill3Net(_targetPos);
		}
	}
	public void stolaProcess(){
		_obj.transform.position = _currPos;
		_stola_skill = _obj.GetComponent<StolaSkill_GUI> ();
		if (_skill_num == "first") {
			_stola_skill.skill1Net(_targetPos);
		}else if (_skill_num == "second") {
			_stola_skill.skill2Net(_targetPos);
		}else if (_skill_num == "third") {
			_stola_skill.skill3Net(_targetPos);
		}
	}
	public void furfurProcess(){
		_obj.transform.position = _currPos;
		_furfur_skill = _obj.GetComponent<FurfurSkill_GUI> ();
		if (_skill_num == "first") {
			_furfur_skill.skill1Net(_targetPos);
		}else if (_skill_num == "second") {
			_furfur_skill.skill2Net(_targetPos);
		}else if (_skill_num == "third") {
			_furfur_skill.skill3Net(_targetPos);
		}
	}
	public void barbasProcess(){
		_obj.transform.position = _currPos;
		_barbas_skill = _obj.GetComponent<Barbas_GUI> ();
		if (_skill_num == "first") {
			_barbas_skill.skill1Net(_targetPos);
		}else if (_skill_num == "second") {
			_barbas_skill.skill2Net(_targetPos);
		}else if (_skill_num == "third") {
			_barbas_skill.skill3Net(_targetPos);
		}
	}

	public void receive(string data){
		string[] temp = data.Split (':');
		Debug.Log ("receive = "+data);
		_id = temp[0];
		_character = temp [1];
		_skill_num = temp [2];
		posTemp = temp [3].Split (',');
		_currPos = new Vector3 (float.Parse(posTemp [0]),
		                   		float.Parse(posTemp [1]),
		                        float.Parse(posTemp [2]));
		posTemp = temp [4].Split (',');
		_targetPos = new Vector3 (float.Parse(posTemp [0]),
		                          float.Parse(posTemp [1]),
		                          float.Parse(posTemp [2]));
		switch_ = true;
	}

	private IEnumerator doit(){
		_obj = GameObject.Find (_id);
		if(_character=="guci")
		{
			guciProcess();
		}else if(_character=="stola")
		{
			stolaProcess();
		}else if(_character=="furfur")
		{
			furfurProcess();
		}else if(_character=="barbas")
		{
			barbasProcess();
		}
		yield return null;
	}
}
