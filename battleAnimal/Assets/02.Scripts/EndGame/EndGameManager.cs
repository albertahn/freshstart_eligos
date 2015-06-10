using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Boomlagoon.JSON;

public class EndGameManager : MonoBehaviour {



	public GameObject stat_row, pannel_content;

	private EndServerDatabase endServerDatabase;

	public int itemCount = 10, columnCount = 1;

	RectTransform containerRectTransform ;

	// Use this for initialization
	void Start () {

		endServerDatabase = GetComponent<EndServerDatabase> ();

		StartCoroutine (GetStatData ("1"));//ClientState.room));

//		containerRectTransform = pannel_content.GetComponent<RectTransform>();
		
		//calculate the width and height of each child item.
		//float width = containerRectTransform.rect.width / columnCount;


		//int rowCount = itemCount / columnCount;






	
	}
	

	private IEnumerator GetStatData (string roomIndex)
	{		
		yield return StartCoroutine (endServerDatabase.getRoomStats(roomIndex)); // id를 Email로 바꿔야 하지 않을까

		Debug.Log("kills:  "+ endServerDatabase.fuckArray.Length);	

		//containerRectTransform.sizeDelta = new Vector2( 20, 100*endServerDatabase.fuckArray.Length);

		for ( int i =0; i< endServerDatabase.fuckArray.Length; i ++){

			JSONObject jsonobj = JSONObject.Parse(endServerDatabase.fuckArray[i].ToString());

			setStatRow (i, jsonobj.GetString("kills"),jsonobj.GetString("assists"),jsonobj.GetString("deaths"),jsonobj.GetString("points") );


		}


		yield return null;		
	}


	public void setStatRow(int num, string kills, string assists, string deaths, string points){


		Debug.Log ("stats: "+kills+"assis: "+assists);


		//create a new item, name it, and set the parent

		GameObject newItem = GameObject.Find ("stat_row_red1");

		//GameObject newItem = Instantiate(stat_row) as GameObject;
		//GameObject pannelContent = Instantiate(pannel_content) as GameObject;

		newItem.transform.FindChild ("kills_tx").transform.GetComponent<Text> ().text = kills;
		newItem.transform.FindChild ("points_tx").transform.GetComponent<Text> ().text = points;


		//Debug.Log ("len"+kills_tx.GetComponent<Text>());

		//textmesh[0].text = "hi";
		
		newItem.name = stat_row.name + " item at ()";
		//newItem.transform.parent = pannel_content.transform;
		
//		newItem.transform.SetParent (pannel_content.transform);




	}

}
