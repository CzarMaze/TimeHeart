using UnityEngine;

public class Sharenold : Story {
	void OnCollisionEnter2D(Collision2D other){
		/*if (other.gameObject.name == "Player") {
			StartCoroutine (meet ("A",this.gameObject.name));
		}*/
	}
	void OnCollisionExit2D(Collision2D other){
		/*if (other.gameObject.name == "Player") {
			StopAllCoroutines ();
		}*/
	}
	protected override void otherthing(){
		for (int i = 0; i < SumVariable.team.Length; i++) {
			if (SumVariable.team[i].ToString()==this.gameObject.name) {
				SumVariable.teamban [i] = false;
			}
		}
		Invoke ("logindele", 0.4f);
	}
	void logindele(){
		Destroy (this.gameObject);
	}
}
