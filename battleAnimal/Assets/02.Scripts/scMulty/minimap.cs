using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class minimap : MonoBehaviour {
	public Image redNexusImg,blueNexusImg,redPlayerImg,bluePlayerImg,myPlayerImg,minimapImg;
	public Image[] redTowerImg;
	public Image[] blueTowerImg;
	public RectTransform redNexusRect,blueNexusRect,redPlayerRect,bluePlayerRect,myPlayerRect,minimapRect;
	public RectTransform[] redTowerRect;
	public RectTransform[] blueTowerRect;

	public Transform redNexusTr,blueNexusTr;
	public Transform moveFieldTr;	
	public Transform[] redTowerTr;
	public Transform[] blueTowerTr;
	public List<Transform> otherPlayerTr;
	public Transform myPlayerTr;

	public int redTowerNum;
	public int blueTowerNum;

	// Use this for initialization
	void Start () {
		redNexusTr = GameObject.Find ("red_building").GetComponent<Transform> ();
		blueNexusTr = GameObject.Find ("blue_building").GetComponent<Transform> ();
		moveFieldTr = GameObject.Find ("MoveField").GetComponent<Transform> ();

		minimapImg = GameObject.Find ("bg").GetComponent<Image> ();
		redNexusImg= GameObject.Find ("redNexusImg").GetComponent<Image>();
		blueNexusImg= GameObject.Find ("blueNexusImg").GetComponent<Image>();
		redTowerImg= GameObject.Find ("redTowerImg").GetComponentsInChildren<Image>();
		blueTowerImg= GameObject.Find ("blueTowerImg").GetComponentsInChildren<Image>();
		redPlayerImg= GameObject.Find ("redPlayerImg").GetComponent<Image>();
		bluePlayerImg= GameObject.Find ("bluePlayerImg").GetComponent<Image>();
		myPlayerImg= GameObject.Find ("myPlayerImg").GetComponent<Image>();
		
		redTowerNum = redTowerImg.Length;
		blueTowerNum = blueTowerImg.Length;

		minimapRect = minimapImg.GetComponent<RectTransform> ();
		redNexusRect= redNexusImg.GetComponent<RectTransform>();
		blueNexusRect= blueNexusImg.GetComponent<RectTransform>();
		redTowerRect = new RectTransform[redTowerNum];
		blueTowerRect = new RectTransform[blueTowerNum];

		for (int i=0; i<redTowerImg.Length; i++) {
			redTowerRect[i]= redTowerImg[i].GetComponent<RectTransform>();
		}

		for (int i=0; i<blueTowerImg.Length; i++) {
			blueTowerRect[i]= blueTowerImg[i].GetComponent<RectTransform>();
		}

		redPlayerRect= redPlayerImg.GetComponent<RectTransform>();
		bluePlayerRect= bluePlayerImg.GetComponent<RectTransform>();
		myPlayerRect= myPlayerImg.GetComponent<RectTransform>();

		//minimapRect.localPosition = new Vector2 (10.0f, 10.0f);
	}

	public void setPlayer(Transform _tr){
		myPlayerTr = _tr;
	}

	public void setOtherPlayer(Transform _tr){
		otherPlayerTr.Add (_tr);
	}

	// Update is called once per frame
	void Update () {
		if (redNexusTr != null) {
			Vector2 mapPos = FindMapPos(redNexusTr);
			redNexusRect.localPosition= new Vector2(mapPos.x,mapPos.y);
		}

		if (blueNexusTr != null) {
			Vector2 mapPos = FindMapPos(blueNexusTr);			
			blueNexusRect.localPosition= new Vector2(mapPos.x,mapPos.y);
		}

		for(int i=0;i<redTowerNum;i++){
			if(redTowerTr[i] != null){
				Vector2 mapPos = FindMapPos(redTowerTr[i]);			
				redTowerRect[i].localPosition= new Vector2(mapPos.x,mapPos.y);
			}
		}

		for(int i=0;i<blueTowerNum;i++){
			if(blueTowerTr[i] != null){
				Vector2 mapPos = FindMapPos(blueTowerTr[i]);			
				blueTowerRect[i].localPosition= new Vector2(mapPos.x,mapPos.y);
			}
		}

		if (myPlayerTr != null) {
			Vector2 mapPos = FindMapPos(myPlayerTr);			
			myPlayerRect.localPosition= new Vector2(mapPos.x,mapPos.y);
		}
		
	}

	Vector2 FindMapPos(Transform _a){
		Vector2 ret = new Vector2();
		float x, z;		
		float prePivotX,prePivotZ;
		float nextPivotX,nextPivotZ;

		prePivotX = moveFieldTr.position.x - moveFieldTr.localScale.x / 2;
		prePivotZ = moveFieldTr.position.z - moveFieldTr.localScale.y / 2;

		nextPivotX = minimapRect.localPosition.x;
		nextPivotZ = minimapRect.localPosition.y;

		x = _a.position.x - prePivotX;
		z = _a.position.z - prePivotZ;

		x = (x * minimapRect.rect.width) / moveFieldTr.localScale.x;
		z = (z * minimapRect.rect.height) / moveFieldTr.localScale.y;


		ret.x = nextPivotX+x;
		ret.y = nextPivotZ+z;

		return ret;
	}
}
