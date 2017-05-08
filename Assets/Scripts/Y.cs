using UnityEngine;
public class Y : Story {
	int i=1;
	void OnCollisionEnter2D(Collision2D other){
		/*if (other.gameObject.name == "Player") {
			StartCoroutine (meet ("A",this.gameObject.name+i));

			if (i < 2) {
				i++;
			}

		}*/
	}
	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.name == "Player") {
			StopAllCoroutines ();
		}
	}
	protected override void otherthing(){

	}

}
