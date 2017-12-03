using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadbattle : MonoBehaviour {

	
	public static void battlestart () {
		SumVariable.tempbattlead=new Vector3();
		SumVariable.tempbattlead=GameObject.Find("Player").transform.position;
		SumVariable.tempbattlename="";
		SumVariable.tempbattlename=SceneManager.GetActiveScene().name.Substring(5);
		SumVariable.keyboardopen=false;
		SumVariable.nextlevel="test/Battletest";
		SceneManager.LoadScene ("scan/loading"+1);
	}
	public static void battleend(string a=null,Vector3 b=new Vector3()){
		SumVariable.nextad=new Vector3();
		SumVariable.nextlevel="";
		if(a==null){
			SumVariable.nextlevel=SumVariable.tempbattlename;
			SumVariable.nextad=SumVariable.tempbattlead;
		}else{
			SumVariable.nextlevel=a;
			SumVariable.nextad=b;
		}
		SumVariable.keyboardopen=true;
		SumVariable.nextdt="down";
		SceneManager.LoadScene ("scan/loading"+1);
	}
	
}
