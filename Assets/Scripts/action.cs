using UnityEngine;

public class action : MonoBehaviour {
    private double Yposition;
    private double Xposition;
    private double speed = 0.028;
	void Update () {
		if (SumVariable.keyboardopen) {
			Xposition = transform.position.x;
			Yposition = transform.position.y;
			if (Input.GetKey (KeyCode.RightArrow)) {
				Xposition = Xposition + speed;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				Xposition = Xposition - speed;
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				Yposition = Yposition + speed;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				Yposition = Yposition - speed;
			}
			transform.position=new Vector3 ((float)Xposition,(float)Yposition,(float)Yposition);
			//transform.position = new Vector3 ((float)Xposition, (float)Yposition, (float)Yposition);
		}
    }
	/*void LateUpdate(){
		move.MovePosition( new Vector2 ((float)Xposition, (float)Yposition));
	}*/
}
