using UnityEngine;
using System.Collections;

public class CameraTouch : MonoBehaviour {
	
	
	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	public float orthoZoomSpeed = 0.3f;        // The rate of change of the orthographic size in orthographic mode.
	
	public Camera camera; 
	
	public float h,v;
	
	public GameObject myplayer;
	public bool focusCamPlayer;
	
	
	// Use this for initialization
	public float speed;
	void Start () {
	}

	public void setPlayer(){		
		myplayer = GameObject.Find(ClientState.id);
		
		speed  = 0.5f;		
		h = 0.0f;
		v = 0.0f;
		
		camera = GameObject.Find ("Main Camera").camera;
		/*
			GameObject a = GameObject.Find (PlayerPrefs.GetString("email"));

		camera.transform.LookAt(a.transform.position);*/
		
		
		focusCamPlayer = true;

	}
	
	// Update is called once per frame
	void Update () {		
		#if UNITY_ANDROID||UNITY_IPHONE
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			
			Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			
			speed  = 0.55F;
			
			transform.Translate(-touchDeltaPosition.x * speed,0, -touchDeltaPosition.y * speed);
		}
		
		if (Input.touchCount == 2)
		{
			//Debug.Log("2touched man!");
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			// If the camera is orthographic...
			if (camera.isOrthoGraphic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				
				// Make sure the orthographic size never drops below zero.
				camera.orthographicSize = Mathf.Max(camera.orthographicSize, 5.1f);
			}
			else
			{
				// Otherwise change the field of view based on the change in distance between the touches.
				camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 5.1f, 79.9f);
			}
		}		
		#else
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		
		Vector3 moveDir = (Vector3.forward*v) + (Vector3.right*h);
		transform.Translate(moveDir * Time.deltaTime*10.0f,Space.Self);
		
		#endif
		
		
		//focus the player here
		
		if(focusCamPlayer){			
			camera.transform.position = myplayer.transform.position -Vector3.forward*20 +Vector3.up * 20;
			camera.transform.LookAt (myplayer.transform.position);			
		}
		
	}//end update
	
	
	public void cameraFocusPlayer(){
		
		
		Debug.Log ("camera focus: "+ focusCamPlayer);
		
		if(focusCamPlayer){
			focusCamPlayer = false;
			
		}else{
			
			focusCamPlayer = true;
		}
		
		
		
	}//end camfoc
}
