using UnityEngine;
public static class SumVariable{

	//---------------------------------Save
	public static string charactor="1";//角色名字
	public static int [] battleteam={1,0,0};
	public static bool [] teamban={true,true,true};
	public static int key=1;
	public static int [] [] charactorlv=new int[][]
	{//-----------{LV,HP(limit),HP(now),MP(limit),MP(now),EXP(limit),EXP(now),力,智,物防,魔防,速,靈敏}
		new int [] {0,0,0,0,0,0,0,0,0,0,0,0,0},
		new int [] {24,985,887,1295,995,13200,12200,67,285,42,251,425,395},
		new int [] {22,2585,2585,355,300,15428,10245,358,50,415,122,255,70},
		new int [] {20,2052,1820,985,658,16852,7558,275,421,202,254,154,65},
	};
	public static float Music=(float)0.4;//再加強 第一次賦予的值
	public static float Sound=(float)1;//
	public static float ESound=(float)0.15;//
	//---------------------------------Variable
	public static string [] charactorname={null,"艾憐娜","薩雷諾","凱斯伏"};
	public static bool keyboardopen = true; //鎖定/開啟鍵盤與行動動畫
	public static int [] team={1,2,3};
	public static Vector3 [] charactorxyz={new Vector3(0,0,0),new Vector3(1.131f,-0.336f,0),new Vector3(-0.53f,-0.323f,0)};
	public static string nextlevel=null;
	public static Vector3 nextad=new Vector3(0,0,0);
	public static string nextdt = "down";
	public static AudioClip nextmuc=null;
	public static float nextmucpth=1;
	
}
 