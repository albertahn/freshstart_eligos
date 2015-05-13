using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour {
	private RectTransform itemWindow;
	private RectTransform Xbutton;
	private RectTransform[] itemlist;
	public RectTransform[] inventorylist;
	
	private Image[] inventoryWindow;
	public Sprite sword,amor,blank;
	
	private float sx,sy;
	private float xx,xy;
	private float ix,iy;
	
	private int[] stack;
	private int sp;
	private int count;
	private int[] invBool;
	
	private int sellIdx;
	
	private moneyUI _moneyUI;
	private itemInfoUI _itemInfoUI;
	
	// Use this for initialization
	void Start () {
		itemWindow = GameObject.Find ("ItemWindow").GetComponent<RectTransform>();
		Xbutton = GameObject.Find ("Xbutton").GetComponent<RectTransform>();
		itemlist = GameObject.Find ("item_list").GetComponentsInChildren<RectTransform> ();
		inventorylist = GameObject.Find ("inventory_list").GetComponentsInChildren<RectTransform> ();
		inventoryWindow = GameObject.Find ("inventory_list").GetComponentsInChildren<Image> ();
		_moneyUI = GameObject.Find ("UIManager").GetComponent<moneyUI>();
		_itemInfoUI = GameObject.Find ("ItemInfo").GetComponent<itemInfoUI> ();
		invBool = new int[6];
		for(int i=0;i<6;i++){
			inventoryWindow[i].sprite = blank;
			invBool[i] = -1;//-1은 아이템이 없다는 뜻
		}
		
		sx = itemWindow.sizeDelta.x;
		sy = itemWindow.sizeDelta.y;
		
		xx = Xbutton.sizeDelta.x;
		xy = Xbutton.sizeDelta.y;
		
		ix = itemlist [1].sizeDelta.x;
		iy = itemlist [1].sizeDelta.y;
		
		stack = new int[6];
		stack[0]=5;
		stack[1]=4;
		stack[2]=3;
		stack[3]=2;
		stack[4]=1;
		stack[5]=0;
		sp=5;
		count = 0;
		
		closeWindow ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void openWindow(){
		itemWindow.sizeDelta = new Vector2 (sx, sy);
		Xbutton.sizeDelta = new Vector2 (xx, xy);
		
		for (int i=1; i<itemlist.Length; i++) {
			itemlist[i].sizeDelta = new Vector2 (ix, iy);
		}
		for (int i=1; i<inventorylist.Length; i++) {
			inventorylist[i].sizeDelta = new Vector2 (ix, iy);
		}
	}
	
	public void closeWindow(){
		itemWindow.sizeDelta = new Vector2 (0, 0);
		Xbutton.sizeDelta = new Vector2 (0,0);
		
		for (int i=1;i< itemlist.Length; i++) {
			itemlist[i].sizeDelta = new Vector2 (0, 0);
		}
		for (int i=1; i<inventorylist.Length; i++) {
			inventorylist[i].sizeDelta = new Vector2 (0, 0);
		}
	}
	
	public void buySword(){
		int _money = itemInfo.list [0].money;
		if (count < 6 && ClientState.money>=_money) {
			_itemInfoUI.buy_sword_info();
			
			/*_moneyUI.makeMoney(-_money);
			inventoryWindow[stack[sp]].sprite = sword;
			ClientState.addInventory ("sword",stack[sp]);
			invBool[stack[sp]] = 0;
			playerStat.changeDamage(itemInfo.list[0].option);//데미지를 증가시킨다.
			sp--;
			count++;*/
		}
	}
	public void buySword2(){	
		int _money = itemInfo.list [0].money;	
		_moneyUI.makeMoney(-_money);
		inventoryWindow[stack[sp]].sprite = sword;
		ClientState.addInventory ("sword",stack[sp]);
		invBool[stack[sp]] = 0;
		playerStat.changeDamage(itemInfo.list[0].option);//데미지를 증가시킨다.
		sp--;
		count++;
	}
	
	public void buyAmor(){
		int _money = itemInfo.list [1].money;
		if (count < 6 && ClientState.money>=_money) {			
			_itemInfoUI.buy_amor_info();
			/*_moneyUI.makeMoney(-_money);
			inventoryWindow [stack[sp]].sprite = amor;
			ClientState.addInventory ("amor",stack[sp]);
			invBool[stack[sp]] = 1;
			playerStat.changeHp(itemInfo.list[1].option);//체력을 증가시킨다.
			sp--;
			count++;*/	
		}
	}
	public void buyAmor2(){
		int _money = itemInfo.list [1].money;
		_moneyUI.makeMoney(-_money);
		inventoryWindow [stack[sp]].sprite = amor;
		ClientState.addInventory ("amor",stack[sp]);
		invBool[stack[sp]] = 1;
		playerStat.changeHp(itemInfo.list[1].option);//체력을 증가시킨다.
		sp--;
		count++;
	}
	
	public void sellItem1(){
		if (invBool [0]!=-1) {
			sellIdx=0;
			if(invBool[0]==0){
				_itemInfoUI.sell_sword_info();
			}else if(invBool[0]==1){
				_itemInfoUI.sell_amor_info();
			}
		}
	}
	
	public void sellItem2(){
		if (invBool [1] != -1) {
			sellIdx=1;
			if (invBool [1] == 0) {
				_itemInfoUI.sell_sword_info();
			} else if (invBool [1] == 1) {
				_itemInfoUI.sell_amor_info();
			}
		}
	}
	
	public void sellItem3(){
		if (invBool [2]!=-1) {
			sellIdx=2;
			if(invBool[2]==0){
				_itemInfoUI.sell_sword_info();
			}else if(invBool[2]==1){
				_itemInfoUI.sell_amor_info();
			}
		}
	}
	
	public void sellItem4(){
		if (invBool [3]!=-1) {
			sellIdx=3;
			if(invBool[3]==0){
				_itemInfoUI.sell_sword_info();
			}else if(invBool[3]==1){
				_itemInfoUI.sell_amor_info();
			}
		}
	}
	
	public void sellItem5(){
		if (invBool [4]!=-1) {
			sellIdx=4;
			if(invBool[4]==0){
				_itemInfoUI.sell_sword_info();
			}else if(invBool[4]==1){
				_itemInfoUI.sell_amor_info();
			}
		}
	}
	
	public void sellItem6(){
		if (invBool [5]!=-1) {
			sellIdx=5;
			if(invBool[5]==0){
				_itemInfoUI.sell_sword_info();
			}else if(invBool[5]==1){
				_itemInfoUI.sell_amor_info();
			}
		}
	}
	
	public void sellSword(){
		_moneyUI.makeMoney(itemInfo.list[0].money/2);
		sp++;
		stack[sp] = sellIdx;
		inventoryWindow[sellIdx].sprite = blank;
		ClientState.addInventory ("",sellIdx);
		playerStat.changeDamage(-itemInfo.list[0].option);//데미지를 감소시킨다.
		count--;
		invBool[sellIdx] = -1;
	}
	
	public void sellAmor(){		
		_moneyUI.makeMoney(itemInfo.list[1].money/2);
		sp++;
		stack[sp] = sellIdx;
		inventoryWindow[sellIdx].sprite = blank;
		ClientState.addInventory ("",sellIdx);
		playerStat.changeHp(-itemInfo.list[1].option);//체력을 감소시킨다.
		count--;
		invBool[sellIdx] = -1;
	}
}