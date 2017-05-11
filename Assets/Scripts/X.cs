using UnityEngine;
using System.Collections;
public class X : Story {
	bool x=false;
	int ta=1;
	void OnCollisionEnter2D(Collision2D other){
		x=true;
	}
	void LateUpdate(){
		if(Input.GetKeyUp(KeyCode.Space)&&x){
			x=false;
				meet("A",this.gameObject.name+ta);
				if (ta <=1) {
					ta++;
				//C>D>B
				/*此為範例
						meet("A",this.gameObject.name+ta);
						if(ta<"該對話資料表最大數值"){
							ta++;
						}
					}
				 */
			}
			
		}
	}
	void OnCollisionExit2D(Collision2D other){
		x=false;
	}
	protected override void otherthing(){
		menu.taskwords("main","D","跟婊子說話");
	}
}
