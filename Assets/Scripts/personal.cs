using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class personal : MonoBehaviour {
	Text StatusName,LVStatus,HPStatus,MPStatus;
	RectTransform valueStatusHP,valueStatusMP;
	public int s;
	// Use this for initialization
	void Start () {
		StatusName=transform.GetChild(0).GetComponent<Text>();
		LVStatus=transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
		HPStatus=transform.GetChild(2).GetComponent<Text>();
		valueStatusHP=transform.GetChild(3).transform.GetChild(0).GetComponent<RectTransform>();
		MPStatus=transform.GetChild(4).GetComponent<Text>();
		valueStatusMP=transform.GetChild(5).transform.GetChild(0).GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		charactormenu(s);
	}
	protected void charactormenu(int s){//--------------角色選單資料讀取
			if(s!=0){
				StatusName.text=SumVariable.charactorname[s];
				LVStatus.text=SumVariable.charactorlv[s][0].ToString();
				HPStatus.text=SumVariable.charactorlv[s][1].ToString()+"/"+SumVariable.charactorlv[s][2].ToString();
				valueStatusHP.localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[s][2]/(float)SumVariable.charactorlv[s][1]),0,0);
				MPStatus.text=SumVariable.charactorlv[s][3].ToString()+"/"+SumVariable.charactorlv[s][4].ToString();
				valueStatusMP.localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[s][4]/(float)SumVariable.charactorlv[s][3]),0,0);
				// EXPStatus.text=SumVariable.charactorlv[Int32.Parse(s)][5].ToString()+"/"+SumVariable.charactorlv[Int32.Parse(s)][6].ToString();
				// valueStatusEXP.localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(s)][6]/(float)SumVariable.charactorlv[Int32.Parse(s)][5]),0,0);
				// STRStatus.text=SumVariable.charactorlv[Int32.Parse(s)][7].ToString();
				// INTStatus.text=SumVariable.charactorlv[Int32.Parse(s)][8].ToString();
				// DEFStatus.text=SumVariable.charactorlv[Int32.Parse(s)][9].ToString();
				// MDEFStatus.text=SumVariable.charactorlv[Int32.Parse(s)][10].ToString();
				// SPDStatus.text=SumVariable.charactorlv[Int32.Parse(s)][11].ToString();
				// AGIStatus.text=SumVariable.charactorlv[Int32.Parse(s)][12].ToString();
			}else{
				StatusName.text="";
				LVStatus.text="";
				HPStatus.text="";
				valueStatusHP.localPosition=new Vector3(0,0,0);
				MPStatus.text="";
				valueStatusMP.localPosition=new Vector3(0,0,0);
				// EXPStatus.text="";
				// valueStatusEXP.localPosition=new Vector3(0,0,0);
				// STRStatus.text="";
				// INTStatus.text="";
				// DEFStatus.text="";
				// MDEFStatus.text="";
				// SPDStatus.text="";
				// AGIStatus.text="";
			}
		}
}
