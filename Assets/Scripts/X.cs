using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
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
			}
			
		}
	}
	void OnCollisionExit2D(Collision2D other){
		x=false;
	}
	protected override void otherthing(){
		menu.taskwords("main","D","跟婊子說話");

		Instantiate(Resources.Load("PrefabsMainGame/D"),new Vector3(0.3f,-1,0),Quaternion.Euler(0,0,0));
	}
}
