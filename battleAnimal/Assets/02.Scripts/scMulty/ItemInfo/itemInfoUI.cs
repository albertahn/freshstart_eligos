using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class itemInfoUI : MonoBehaviour {	
	private float fx,fy;
	private float nx,ny;
	private float ox,oy;
	private float cx,cy;
	private float bx,by;
	
	private RectTransform frame1,frame2,frame3;
	private Text name_txt,option_txt,cost_txt;
	private RectTransform name_rt,option_rt,cost_rt;
	private RectTransform ok,cancle;
	private RectTransform background;

	private ItemShop _itemShop;

	private int ItemType;

	// Use this for initialization
	void Start () {
		_itemShop = GameObject.Find ("UIManager").GetComponent<ItemShop> ();

		frame1 = GameObject.Find ("frame1").GetComponent<RectTransform>();
		frame2 = GameObject.Find ("frame2").GetComponent<RectTransform>();
		frame3 = GameObject.Find ("frame3").GetComponent<RectTransform>();
		
		name_txt = GameObject.Find ("name").GetComponent<Text> ();
		option_txt = GameObject.Find ("option").GetComponent<Text> ();
		cost_txt = GameObject.Find ("cost").GetComponent<Text> ();
		
		name_rt = GameObject.Find ("name").GetComponent<RectTransform> ();
		option_rt = GameObject.Find ("option").GetComponent<RectTransform> ();
		cost_rt = GameObject.Find ("cost").GetComponent<RectTransform> ();
		
		ok = GameObject.Find ("ok").GetComponent<RectTransform>();
		cancle = GameObject.Find ("cancle").GetComponent<RectTransform>();

		background = GameObject.Find ("ItemInfoWindow").GetComponent<RectTransform> ();
		
		fx = frame1.localScale.x;
		fy = frame1.localScale.y;
		nx = name_rt.localScale.x;
		ny = name_rt.localScale.y;
		ox = ok.localScale.x;
		oy = ok.localScale.y;
		cx = cancle.localScale.x;
		cy = cancle.localScale.y;
		bx = background.localScale.x;
		by = background.localScale.y;

		closeWindow ();
	}

	public void buy_sword_info(){
		name_txt.text = "sword";
		option_txt.text = "공격력 +2";
		cost_txt.text = "구입가격:50원";
		ItemType = 0;
		openWindow ();
	}

	public void buy_amor_info(){
		name_txt.text = "amor";
		option_txt.text = "HP +500";
		cost_txt.text = "구입가격:50원";
		ItemType = 1;
		openWindow ();
	}

	public void sell_sword_info(){
		name_txt.text = "sword";
		option_txt.text = "공격력 +2";
		cost_txt.text = "판매가격:25원";
		ItemType = 100;
		openWindow ();
	}
	
	public void sell_amor_info(){
		name_txt.text = "amor";
		option_txt.text = "HP +500";
		cost_txt.text = "판매가격:25원";	
		ItemType = 101;	
		openWindow ();
	}

	public void pressOK(){
		switch (ItemType) {
			case 0:
				_itemShop.buySword2();
				closeWindow();
			break;
			case 1:
				_itemShop.buyAmor2();
				closeWindow();
			break;
			case 100:
				_itemShop.sellSword();
				closeWindow();				
			break;
			case 101:
				_itemShop.sellAmor();
				closeWindow();				
			break;

		}
	}

	public void pressCancle(){		
		closeWindow();	
	}

	void closeWindow(){
		frame1.localScale = new Vector2 (0, 0);
		frame2.localScale = new Vector2 (0, 0);
		frame3.localScale = new Vector2 (0, 0);

		name_rt.localScale = new Vector2 (0, 0);		
		option_rt.localScale = new Vector2 (0, 0);
		cost_rt.localScale = new Vector2 (0, 0);
		
		ok.localScale = new Vector2 (0, 0);
		cancle.localScale = new Vector2 (0, 0);

		background.localScale = new Vector2 (0, 0);
	}

	void openWindow(){
		frame1.localScale = new Vector2 (fx, fy);
		frame2.localScale = new Vector2 (fx, fy);
		frame3.localScale = new Vector2 (fx, fy);
		
		name_rt.localScale = new Vector2 (nx, ny);		
		option_rt.localScale = new Vector2 (nx, ny);
		cost_rt.localScale = new Vector2 (nx, ny);
		
		ok.localScale = new Vector2 (ox, oy);
		cancle.localScale = new Vector2 (cx, cy);
		
		background.localScale = new Vector2 (bx, by);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}