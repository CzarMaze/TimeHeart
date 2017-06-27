using UnityEngine;
using System.Collections;
public class blackdown1 : Story {
	bool x=false;
	int ta=1;
	void OnCollisionEnter2D(Collision2D other){
		x=true;
	}
	void LateUpdate(){
		if(Input.GetKeyUp(KeyCode.Space)&&x){
			x=false;
			meet("A",this.gameObject.name+ta);
			if (ta < 3) {
				ta++;
			} else {
				ta = 3;
			}

		}
	}
	void OnCollisionExit2D(Collision2D other){
		x=false;
	}
	protected override void otherthing(){

	}

}
