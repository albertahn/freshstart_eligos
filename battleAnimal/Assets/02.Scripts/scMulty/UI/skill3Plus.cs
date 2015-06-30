using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class skill3Plus : MonoBehaviour {
	
	Button b;
	private DogSkill_GUI _dog;
	private Tutu_skill_gui _turtle;
	private GuciSkill_GUI _guci;
	private Barbas_GUI _barbas;
	private FurfurSkill_GUI _furfur;
	
	public void setPlayer(){
		Button b = gameObject.GetComponent<Button>();
		
		if (ClientState.character == "dog") {
			_dog = GameObject.Find(ClientState.id).GetComponent<DogSkill_GUI>();
			b.onClick.AddListener(delegate() { dogUI(); });
		} else if (ClientState.character == "turtle") {
			_turtle = GameObject.Find(ClientState.id).GetComponent<Tutu_skill_gui>();
			b.onClick.AddListener(delegate() { turtleUI(); });
		}else if (ClientState.character == "guci") {
			_guci = GameObject.Find(ClientState.id).GetComponent<GuciSkill_GUI>();
			b.onClick.AddListener(delegate() { guciUI(); });
		}else if (ClientState.character == "barbas") {
			_barbas = GameObject.Find(ClientState.id).GetComponent<Barbas_GUI>();
			b.onClick.AddListener(delegate() { barbasUI(); });
		}else if (ClientState.character == "furfur") {
			_furfur = GameObject.Find(ClientState.id).GetComponent<FurfurSkill_GUI>();
			b.onClick.AddListener(delegate() { furfurUI(); });
		}
	}
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {	
	}
	
	public void dogUI()
	{
		_dog.skill3Plus_bot ();
	}
	
	public void turtleUI()
	{
		_turtle.skill3Plus_bot ();
	}
	
	public void guciUI()
	{
		_guci.skill3Plus_bot ();
	}
	
	public void barbasUI()
	{
		_barbas.skill1Plus_bot ();
	}
	
	public void furfurUI()
	{
		_furfur.skill1Plus_bot ();
	}
}
