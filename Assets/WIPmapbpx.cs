using UnityEngine;
using System.Collections;

public class WIPmapbpx : Story {
	bool x=false;
	int ta=1;
	void OnCollisionEnter2D(Collision2D other){
		x=true;
	}
	void LateUpdate(){
		if(x){
			x=false;
			meet("A",this.gameObject.name+ta);
			if (ta <=1) {
				ta++;
			}
		}
	}
	void OnCollisionExit2D(Collision2D other){
		x = false;
	}
	protected override void otherthing(){

	}
}
