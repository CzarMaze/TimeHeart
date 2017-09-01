using UnityEngine;
using System.Collections;

public class 澤肯物件1 : Story {

	bool x=false;
	int ta=1;
	void OnCollisionEnter2D(Collision2D other){
		x=true;
	}
	void LateUpdate(){
		if(Input.GetKeyUp(KeyCode.Space)&&x){
			x=false;
			meet("澤肯物件",this.gameObject.name+ta); 
			if (ta <=1) {
				ta ++ ;
			}
		}
	}
	void OnCollisionExit2D(Collision2D other){
		x=false;
	}
	protected override void otherthing(){

	}
}