using UnityEngine;
using System.Collections;

public class Kaisvo : Story {
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Player") {
			StartCoroutine (meet ("A",this.gameObject.name));
		}
	}
	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.name == "Player") {
			StopAllCoroutines ();
		}
	}
	protected override void otherthing(){
		for (int i = 0; i < SumVariable.add.Length; i++) {
			if (SumVariable.add[i]==this.gameObject.name) {
				SumVariable.ban [i] = false;
			}
		}
		Invoke ("logindele", 0.4f);
	}
	void logindele(){
		Destroy (this.gameObject);
	}
		
}
