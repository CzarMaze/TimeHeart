using UnityEngine;
using UnityEngine.UI;
using System;
public class testhito : MonoBehaviour {
	GameObject [] itemUI,friendsUI,mainUI;
	void Start(){
		itemUI=GameObject.FindGameObjectsWithTag("itemUI");
		friendsUI=GameObject.FindGameObjectsWithTag("friendsUI");
		mainUI=GameObject.FindGameObjectsWithTag("mainUI");
	}
	void OnCollisionEnter2D(Collision2D other){
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初",2,"XX","YY");
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初1",2,"SS","AA");
		/*menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初2",2);
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初3",2);
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初4",2);
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初5",2);
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初6",2);
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初7",2);
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","人之初8",2);
		menu.friendsSave.gift=menu.Addnewitems(menu.friendsSave,friendsUI,"friendsList","friendsitem","XX",2);
		menu.mainSave.gift=menu.Addnewitems(menu.mainSave,mainUI,"mainList","mainitem","發呆",5);
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","性本善",7);
		menu.friendsSave.gift=menu.Addnewitems(menu.friendsSave,friendsUI,"friendsList","friendsitem","XYX",9);
		menu.mainSave.gift=menu.Addnewitems(menu.mainSave,mainUI,"mainList","mainitem","發X呆",1);*/
	}
}
