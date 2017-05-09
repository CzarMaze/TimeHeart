using UnityEngine;
using System.Collections;
public class X : Story {
	bool x=false;
	void OnCollisionEnter2D(Collision2D other){
		x=true;
	}
	void LateUpdate(){
		if(Input.GetKeyUp(KeyCode.Space)&&x){
			x=false;
			meet("A",this.gameObject.name);
		}
	}
	void OnCollisionExit2D(Collision2D other){
		x=false;
	}
	protected override void otherthing(){	
	}
}
