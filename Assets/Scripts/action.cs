﻿using UnityEngine;
using System.Collections;

public class action : MonoBehaviour {
    private double Yposition;
    private double Xposition;
    private double speed = 0.022;
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

			transform.position = new Vector3 ((float)Xposition, (float)Yposition, (float)Yposition);
		}
    }
}
