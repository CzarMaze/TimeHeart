using UnityEngine;
using System.Collections;

public class 家物件1 : Story {

	bool x=false;
	int ta=1;
	void OnCollisionEnter2D(Collision2D other){
		x=true;
		GameObject.Find("Emoticons").GetComponent<Animator>().enabled=true;
	}
	void LateUpdate(){
		if(Input.GetKeyUp(KeyCode.Space)&&x){
			x=false;
			meet("家物件",this.gameObject.name+ta); 
			GameObject.Find("Emoticons").GetComponent<Animator>().enabled=false;
			GameObject.Find("Emoticons").GetComponent<SpriteRenderer>().enabled=false;
			
			if (ta <=1) {
				ta ++ ;
			}
		}
	}
	void OnCollisionExit2D(Collision2D other){
		GameObject.Find("Emoticons").GetComponent<Animator>().enabled=false;
		GameObject.Find("Emoticons").GetComponent<SpriteRenderer>().enabled=false;
		x=false;
	}
	protected override void otherthing(){

	}
}