using UnityEngine;
using System.Collections;
public class 澤肯NPC銀愷item: Story {
	GameObject [] itemUI;
	bool x=false;
	int ta=1;
	void OnCollisionEnter2D(Collision2D other){
		x=true;
		GameObject.Find("Emoticons").GetComponent<Animator>().enabled=true;
	}
	void LateUpdate(){
		if(Input.GetKeyUp(KeyCode.Space)&&x){
			x=false;
			itemUI=GameObject.FindGameObjectsWithTag("itemUI");
			meet("澤肯NPC",this.gameObject.name+ta); 
			GameObject.Find("Emoticons").GetComponent<Animator>().enabled=false;
			GameObject.Find("Emoticons").GetComponent<SpriteRenderer>().enabled=false;
			if (ta <=1) {
				ta ++ ;
			}
		}
	}
	void OnCollisionExit2D(Collision2D other){
		GameObject.Find ("Emoticons").GetComponent<Animator> ().enabled = false;
		GameObject.Find ("Emoticons").GetComponent<SpriteRenderer> ().enabled = false;
		x = false;
	}
	protected override void otherthing(){
		menu.itemSave.gift=menu.Addnewitems(menu.itemSave,itemUI,"itemList","itemitem","itemitem","小生命藥劑",5,
			""
			," 單人補品\n 恢復 HP 200");
		Invoke ("del", 1);
	}
}