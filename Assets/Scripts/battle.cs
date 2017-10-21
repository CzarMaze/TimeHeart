using UnityEngine;
using System.Collections;

public class battle : MonoBehaviour {
	Animator team1,team2,team3;
	// Use this for initialization
	void Start () {
		team1=GameObject.Find("Team1").gameObject.transform.GetChild(0).GetComponent<Animator>();
		team2=GameObject.Find("Team2").gameObject.transform.GetChild(0).GetComponent<Animator>();
		team3=GameObject.Find("Team3").gameObject.transform.GetChild(0).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void attrack(){
		team1.Play(Animator.StringToHash("3Attack"));
		Debug.Log("按鈕確定");
	}
}
