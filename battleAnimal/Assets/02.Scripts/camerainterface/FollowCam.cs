using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	public Transform target;
	public float dist = 10.0f;
	public float height = 5.0f;
	public float side = 10.0f;
	public float dampRotate = 5.0f;
	public float smooth= 5.0f;
	private Transform tr;



	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		dist = 5.0f;
		height = 15.0f;
		side = 5.0f;
		tr = GetComponent<Transform> ();


	
	}

	public void setTarget(Transform a){
		target = a;

	}
	// Update is called once per frame
	void LateUpdate () {
		if (target != null) {

			/*target.position.x = target.position.x + side; //target.position - Vector3.forward*dist -  Vector3.left*side ;
			target.position.y = target.position.y + height;


			tr.position = Vector3.Lerp (
				transform.position, target.position,
				Time.deltaTime * smooth);
*/
			//tr.position = target.position + (Vector3.up * height) - Vector3.forward*dist -  Vector3.left*side ;

			Vector3 point = tr.position;
			Vector3 delta = target.position + (Vector3.up * height) - Vector3.forward*dist -  Vector3.left*side ;//target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = delta;
			tr.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			//tr.LookAt (target);

		}
	}
}