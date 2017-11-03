using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class battleplay : MonoBehaviour {
	public static Animator EA;//敵方
	public static Animator MA;//我方

	bool delhurt=false;
	public static int [] [] Eteamlv=new int[][]
	{//-----------{LV,HP(limit),HP(now),MP(limit),MP(now),EXP(limit),EXP(now),力,智,物防,魔防,速,靈敏}
		new int [] {0,0,0,0,0,0,0,0,0,0,0,0,0},
		new int [] {24,10,10,795,795,20000,20000,3550,321,120,115,225,195},
		new int [] {0,0,0,0,0,0,0,0,0,0,0,0,0},
		new int [] {22,10,10,355,355,15000,15000,2410,50,241,50,315,70},
	};
	

	public void exit(){
		loadbattle.battleend();
	}
	public void D(){
		delhurt=true;
	}
	public void attrackchoice(){
		EA.Play(Animator.StringToHash("hurt"));
		if(EA.gameObject.name.Substring(0,6)=="Battle"){//敵方攻擊
			SumVariable.charactorlv[Int32.Parse(EA.gameObject.name.Substring(6))][2]=SumVariable.charactorlv[Int32.Parse(EA.gameObject.name.Substring(6))][2]-(int)Math.Round(delhurt?battlehurt(7,6)*0.5:battlehurt(7,6),0);
			if(SumVariable.charactorlv[Int32.Parse(EA.gameObject.name.Substring(6))][2]<=0){
				EA.Play(Animator.StringToHash("die"));
				battle.battlename=diedelete(battle.battlename,"Team"+EA.gameObject.name.Substring(6));
				for (int i=0;i<battle.battlename.Length;i++){
					if(battle.battlename[i].Substring(0,4)=="Team"){
						break;
					}
					if(i+1==battle.battlename.Length){
						Debug.Log("你輸了");
						loadbattle.battleend();
					}
				}
				
			}
		}else if(EA.gameObject.name.Substring(0,6)=="EBattl"){//我方攻擊
			Eteamlv[Int32.Parse(EA.gameObject.name.Substring(7))][2]=Eteamlv[Int32.Parse(EA.gameObject.name.Substring(7))][2]-(int)Math.Round(battlehurt(6,7),0);
			if(Eteamlv[Int32.Parse(EA.gameObject.name.Substring(7))][2]<=0){
				EA.Play(Animator.StringToHash("die"));
				battle.battlename=diedelete(battle.battlename,"ETeam"+EA.gameObject.name.Substring(7));
				for (int i=0;i<battle.battlename.Length;i++){
					if(battle.battlename[i].Substring(0,4)=="ETea"){
						break;
					}
					if(i+1==battle.battlename.Length){
						Debug.Log("你贏了");
						loadbattle.battleend();
					}
				}
			}
		}
		battle.num++;
	}
	public string[] diedelete(string[] a,string b){
		List<string> tmp= new List<string>(){};
		foreach(string i in a){
			if(i==b){
				tmp.Add("null");
			}else{
				tmp.Add(i);
			}
			
		}
		
		return tmp.ToArray();
	}
	private double battlehurt(int a,int b){
		return (
			(double)SumVariable.charactorlv[Int32.Parse(MA.gameObject.name.Substring(a))][7]
			*
			(double)SumVariable.charactorlv[Int32.Parse(MA.gameObject.name.Substring(a))][7]
			
			)/(
			
			(double)Eteamlv[Int32.Parse(EA.gameObject.name.Substring(b))][9]
			+
			(double)SumVariable.charactorlv[Int32.Parse(MA.gameObject.name.Substring(a))][7]
			);
			
	}
	
}
