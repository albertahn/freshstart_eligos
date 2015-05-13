using UnityEngine;
using System.Collections;

public class itemInfo : MonoBehaviour{
	public static Item[] list;

	// Use this for initialization
	void Start () {
		list = new Item[10];
		for (int i=0; i<list.Length; i++) {
			list[i] = new Item();
		}				
		//0 : sword
		list[0].name = "sword";
		list[0].money = 50;
		list[0].option = 2;//공격력
		
		//1 : amor
		list[1].name = "amor";
		list[1].money = 50;
		list[1].option = 500;//체력

	}
	
	// Update is called once per frame
	void Update (){
	}
}

public class Item{	
	public string name;
	public int money;
	public int option;
}