using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class battle : MonoBehaviour {
	List <Animator> team=new List<Animator>();
	EventSystem es;
	GameObject E1,E3;
		
	string [] battlename={"Team1","Team2","Team3"};
	string [] Ebattle={"ETeam1","ETeam3"};
	public static int num=0;
	// Use this for initialization
	void Start () {
		for(int i=0;i<battlename.Length;i++ ){
			team.Add(GameObject.Find(battlename[i]).gameObject.transform.GetChild(0).GetComponent<Animator>());
		}
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		E1=GameObject.Find("EnemyButton1");
		E3=GameObject.Find("EnemyButton3");
		loadselected(E1);
		//SetSelectedGameObjects("du",GameObject.Find("EnemyButton1").GetComponent<Button>(),GameObject.Find("EnemyButton3").GetComponent<Button>());
	}
	void Update(){
		if(es.currentSelectedGameObject!=null && es.currentSelectedGameObject.gameObject.name.Substring(0,7)=="EnemyBu"){
			E1.GetComponent<CanvasGroup>().alpha=0;
			E3.GetComponent<CanvasGroup>().alpha=0;				
			es.currentSelectedGameObject.GetComponent<CanvasGroup>().alpha=1;
			battleplay.EA=GameObject.Find("ETeam"+es.currentSelectedGameObject.gameObject.name.Substring(11)).gameObject.transform.GetChild(0).GetComponent<Animator>();
		}
		if(num==3){
			num=0;
		}
	}
	void loadselected(GameObject a){// -----------------------------------設定第一選項
		es.firstSelectedGameObject=a;
		es.SetSelectedGameObject(null);
 		es.SetSelectedGameObject(es.firstSelectedGameObject);
	}
	// protected void SetSelectedGameObjects(string check,Button a,Button b){//-----------------設定創建按鈕/選項後左右鍵指向其他按鈕/選項
	// 	Navigation r,s;
	// 	switch(check){
	// 		case "rl":
	// 			r=a.navigation;
	// 			r.selectOnRight=b;
	// 			a.navigation=r;
	// 			s=b.navigation;
	// 			s.selectOnLeft=a;
	// 			b.navigation=s;
	// 		break;
	// 		case "du":
	// 			r=a.navigation;
	// 			r.selectOnDown=b;
	// 			a.navigation=r;
	// 			s=b.navigation;
	// 			s.selectOnUp=a;
	// 			b.navigation=s;
	// 		break;
	// 	}
	// }
	// Update is called once per frame
	public void attrack(){
		team[num].Play(Animator.StringToHash("Attack"));
	}
	public void DEF(){
		team[num].Play(Animator.StringToHash("DEF"));
		num++;
	}
	public void runback(){
		team[0].Play(Animator.StringToHash("run"));
		team[1].Play(Animator.StringToHash("run"));
		team[2].Play(Animator.StringToHash("run"));
	}
}
