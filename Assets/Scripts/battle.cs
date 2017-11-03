using UnityEngine;

using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class battle : MonoBehaviour {
	List<Animator> MteamAni=new List<Animator>{};
	List<Animator> team=new List<Animator>();
	EventSystem es;
	GameObject E1,E3;	
	List<string> list = new List<string> {"Team1","Team2","Team3","ETeam1","ETeam3"};
	public static string [] battlename;
	public static int num=0;
	// Use this for initialization
	void Start () {	
		battlename=Randomize(list).ToArray();
		for(int i=0;i<battlename.Length;i++ ){
			team.Add(GameObject.Find(battlename[i]).gameObject.transform.GetChild(0).GetComponent<Animator>());			
			if(battlename[i].Substring(0,4)=="Team"){
				MteamAni.Add(GameObject.Find(battlename[i]).gameObject.transform.GetChild(0).GetComponent<Animator>());

			}
		}
		// MteamAni.Sort();
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		E1=GameObject.Find("EnemyButton1");
		E3=GameObject.Find("EnemyButton3");
		loadselected(E1);
		//GameObject.Find("ChButton"+battlename[num].Substring(4)).GetComponent<CanvasGroup>().alpha=1; 
		
	}
    void Update()
    {
        if (num >= 5)
        {			
            num = 0;
        }
        if(battlename[num]!=null){
		if (es.currentSelectedGameObject != null)
        {
            if (es.currentSelectedGameObject.gameObject.name.Substring(0, 7) == "EnemyBu")
            {
                E1.GetComponent<CanvasGroup>().alpha = 0;
                E3.GetComponent<CanvasGroup>().alpha = 0;
                es.currentSelectedGameObject.GetComponent<CanvasGroup>().alpha = 1;
                battleplay.EA = GameObject.Find("ETeam" + es.currentSelectedGameObject.gameObject.name.Substring(11)).gameObject.transform.GetChild(0).GetComponent<Animator>();
            }
        }
		if (battlename[num].Substring(0, 4) == "ETea")
        {
            battleplay.EA = MteamAni[UnityEngine.Random.Range(0,2)];			
            attrack();
        }else{
                if (battlename[num].Substring(0, 4) == "Team")
                {
                    GameObject.Find("ChButton1").GetComponent<CanvasGroup>().alpha = 0;
                    GameObject.Find("ChButton2").GetComponent<CanvasGroup>().alpha = 0;
                    GameObject.Find("ChButton3").GetComponent<CanvasGroup>().alpha = 0;
                    GameObject.Find("ChButton" + battlename[num].Substring(4)).GetComponent<CanvasGroup>().alpha = 1;
                    menu(true);
                }

            }
        }
        if(battlename[num]=="null")
        {
            num++;
        }

    }
	public static List<T> Randomize<T>(List<T> list)
    {
        List<T> randomizedList = new List<T>();
        System.Random rnd = new System.Random();
        while (list.Count > 0)
        {
            int index = rnd.Next(0, list.Count); 
            randomizedList.Add(list[index]); 
            list.RemoveAt(index);
        }
        return randomizedList;
    }
	public void menu(bool a){
		if(a){
			GetComponent<CanvasGroup>().alpha=1;
			GetComponent<CanvasGroup>().interactable=a;
		}else{
			GetComponent<CanvasGroup>().alpha=0;
			GetComponent<CanvasGroup>().interactable=a;
		}
	}
	void loadselected(GameObject a){// -----------------------------------設定第一選項
		es.firstSelectedGameObject=a;
		es.SetSelectedGameObject(null);
 		es.SetSelectedGameObject(es.firstSelectedGameObject);
	}
	public void attrack(){
		menu(false);
		battleplay.MA=team[num];
		team[num].Play(Animator.StringToHash("Attack"));
	}
	public void DEF(){
		menu(false);
		team[num].Play(Animator.StringToHash("DEF"));
		num++;
	}
	public void runback(){
		menu(false);
		team[0].Play(Animator.StringToHash("run"));
		team[1].Play(Animator.StringToHash("run"));
		team[2].Play(Animator.StringToHash("run"));
		
	}
}
