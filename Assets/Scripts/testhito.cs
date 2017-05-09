﻿using UnityEngine;
public class testhito : MonoBehaviour {
	GameObject [] itemUI,friendsUI,mainUI,AttackSkill,HelpSkill,CureSkill;
	void Start(){
		itemUI=GameObject.FindGameObjectsWithTag("itemUI");
		friendsUI=GameObject.FindGameObjectsWithTag("friendsUI");
		mainUI=GameObject.FindGameObjectsWithTag("mainUI");
		AttackSkill=GameObject.FindGameObjectsWithTag("AttackSkill");
		HelpSkill=GameObject.FindGameObjectsWithTag("HelpSkill");
		CureSkill=GameObject.FindGameObjectsWithTag("CureSkill");
	}
	void OnCollisionEnter2D(Collision2D other){
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","itemitem","人之初",2,"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","YY");
		menu.friendsSave.gift=menu.Addnewitems(menu.friendsSave,friendsUI,"friendsList","itemitem","friendsitem","人之初1",2,"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","AA");

		//--------------------------------------------------------------------------------------------------------------------------------
		menu.AttackSkillSave.gift=menu.Addnewitems(menu.AttackSkillSave,AttackSkill,"AttackList","skllskill","AttackSkill","跑步",2,"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","提升跑步速度");
		menu.taskwords("main","123456","你什麼也不用做");
	}
}
