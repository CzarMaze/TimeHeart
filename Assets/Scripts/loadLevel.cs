﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class loadLevel : MonoBehaviour {
	CanvasGroup wx;
	public string a;
	public Vector3 b;
	public string c;
    int i;
    void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Player") {
            wx = GameObject.Find ("sendtoblack").GetComponent<CanvasGroup> ();
			SumVariable.keyboardopen = false;
			Invoke ("nexts", 0.5f);
            i = Random.Range(1, 3);
		}
	}
	void nexts(){
		StartCoroutine(Sumthing.view (wx,0, 1, 0.0625,0.03f));
		Invoke ("nexts2", 0.5f);
	}
	void nexts2(){
		SumVariable.nextlevel = a;
		SumVariable.nextad = b;
		SumVariable.nextdt = c;
		SceneManager.LoadScene ("scan/loading"+i.ToString());
	}
}
