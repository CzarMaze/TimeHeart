using UnityEngine;
public static class SumVariable{

	//---------------------------------Save
	public static string charactor="1";//角色名字
	public static int [] battleteam={1,0,0};
	public static bool [] teamban={true,true,true,false,false,false,false,true,false,true,false,false};
	public static int key=1;
	public static string [] charactorname={null,"艾憐娜","薩雷諾","凱斯伏","伊莉雅","希恩","勇帝","隊長","櫻","烈爾","蒙德士","孟妮","薩豪"};
	public static int [] [] charactorlv=new int[][]
	{//-----------{LV,HP(limit),HP(now),MP(limit),MP(now),EXP(limit),EXP(now),力,智,物防,魔防,速,靈敏}
		new int [] {0,0,0,0,0,0,0,0,0,0,0,0,0},
		new int [] {24,985,887,1295,995,13200,12200,67,285,42,251,425,395},
		new int [] {22,2585,2585,355,300,15428,10245,358,50,415,122,255,70},
		new int [] {20,2052,1820,985,658,16852,7558,275,421,202,254,154,65},
		new int [] {25,1421,1224,765,245,13577,2208,88,300,100,268,276,384},
		new int [] {23,1376,1195,658,345,9887,5220,122,145,187,205,402,321},
		new int [] {21,2375,2020,215,145,16025,13207,368,49,405,322,207,72},
		new int [] {19,2257,1820,325,115,13475,3257,310,58,302,75,286,198},
		new int [] {18,1175,320,745,735,12578,12250,325,275,67,81,348,301},
		new int [] {26,1981,1820,275,95,16668,1000,224,75,307,124,357,285},
		new int [] {27,3245,87,172,95,17852,7220,359,27,368,101,151,30},
		new int [] {28,1768,1468,985,452,16852,5768,210,88,56,42,515,554},
		new int [] {30,2852,2805,1585,798,20852,3335,258,481,285,352,207,30}
	};
	//---------------------------------Variable
	public static bool keyboardopen = true; //鎖定/開啟鍵盤與行動動畫
	public static int [] team={1,2,3,4,5,6,7,8,9,10,11,12};
	public static Vector3 [] charactorxyz={new Vector3(0,0,0),new Vector3(1.131f,-0.336f,0),new Vector3(-0.53f,-0.323f,0)};
	public static string nextlevel=null;
	public static Vector3 nextad=new Vector3(0,0,0);
	public static string nextdt = "down";
	public static AudioClip nextmuc=null;
	public static float nextmucpth=1;
	
}
 