using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class skill3Plus : MonoBehaviour {
	
	Button b;
	private DogSkill_GUI _dog;
	private Tutu_skill_gui _turtle;
	
	public void setPlayer(){
		Button b = gameObject.GetComponent<Button>();
		
		if (ClientState.character == "dog") {
			_dog = GameObject.Find(ClientState.id).GetComponent<DogSkill_GUI>();
			b.onClick.AddListener(delegate() { dogUI(); });
		} else if (ClientState.character == "turtle") {
			_turtle = GameObject.Find(ClientState.id).GetComponent<Tutu_skill_gui>();
			b.onClick.AddListener(delegate() { turtleUI(); });
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
}

