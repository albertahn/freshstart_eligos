using UnityEngine;
using System.Collections;

public class AniCtrl : MonoBehaviour {
	[System.Serializable]
	public class Anim{
		public AnimationClip idle;
		public AnimationClip run;
		public AnimationClip attack;
		public AnimationClip die;
	}
	public Anim anim;
	public Animation _animation;


	// Use this for initialization
	void Start () {
		_animation = GetComponentInChildren<Animation> ();
		_animation.clip = anim.idle;
		_animation.Play ();
	}
}
