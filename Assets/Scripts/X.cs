using UnityEngine;
public class X : Story {
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
		
	}

}
