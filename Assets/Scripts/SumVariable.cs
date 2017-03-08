using UnityEngine;
using System.Collections;
public static class SumVariable{
	public static bool keyboardopen = true; //鎖定/開啟鍵盤與行動動畫
	public static string charactor="aleana";//角色名字
	public static string [] add={"aleana","sharenold","kaisvo"};
	public static bool[] ban = { false, false, false };
	public static Vector3 [] charactorxyz={new Vector3(0,0,0),new Vector3(1.131f,-0.336f,0),new Vector3(-0.53f,-0.323f,0)};
	public static int key=1;
	public static string nextlevel=null;
	public static Vector3 nextad=new Vector3(0,0,0);
	public static string nextdt = "down";
	public static AudioClip nextmuc=null;
	public static float nextmucpth=1;
}
 