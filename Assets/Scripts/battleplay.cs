using UnityEngine;
using System.Collections;
using System;

public class battleplay : MonoBehaviour {
	public static Animator EA;
	public static Animator MA;
	public static int [] [] Eteamlv=new int[][]
	{//-----------{LV,HP(limit),HP(now),MP(limit),MP(now),EXP(limit),EXP(now),力,智,物防,魔防,速,靈敏}
		new int [] {0,0,0,0,0,0,0,0,0,0,0,0,0},
		new int [] {24,5985,5985,795,795,20000,20000,355,321,120,115,225,195},
		new int [] {22,9585,9585,355,355,15000,15000,241,50,241,50,315,70},
	};
	public void attrackchoice(){
		EA.Play(Animator.StringToHash("hurt"));
		if(EA.gameObject.name.Substring(0,6)=="Battle"){
			SumVariable.charactorlv[Int32.Parse(EA.gameObject.name.Substring(6))][2]=SumVariable.charactorlv[Int32.Parse(EA.gameObject.name.Substring(6))][2]-400;
		}else if(EA.gameObject.name.Substring(0,6)=="EBattl"){
			Debug.Log(Int32.Parse(EA.gameObject.name.Substring(7)));
			Debug.Log("敵人扣血");
		}
		// SumVariable.charactorlv[EA.gameObject.name.get][]
		battle.num++;
	}
}
