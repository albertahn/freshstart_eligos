using UnityEngine;
using System.Collections;

public class EndGameManager : MonoBehaviour {

	public GameObject stat_row, pannel_content;

	public int itemCount = 10, columnCount = 1;

	// Use this for initialization
	void Start () {

		RectTransform containerRectTransform = pannel_content.GetComponent<RectTransform>();
		
		//calculate the width and height of each child item.
		//float width = containerRectTransform.rect.width / columnCount;

		float height = 100;
		int rowCount = itemCount / columnCount;



		containerRectTransform.sizeDelta = new Vector2( 20, 100*7);

		for (int i=0; i<7; i++) {
		


			//create a new item, name it, and set the parent
			GameObject newItem = Instantiate(stat_row) as GameObject;
			//GameObject pannelContent = Instantiate(pannel_content) as GameObject;

			newItem.name = stat_row.name + " item at (" + i  + ")";
			newItem.transform.parent = pannel_content.transform;
			
			//move and size the new item
			RectTransform rectTransform = newItem.GetComponent<RectTransform>();


			float y = containerRectTransform.rect.height / 2 - height * i;
			rectTransform.offsetMin = new Vector2(0, y);
			

			y = rectTransform.offsetMin.y + height;
			rectTransform.offsetMax = new Vector2(0, y);
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
