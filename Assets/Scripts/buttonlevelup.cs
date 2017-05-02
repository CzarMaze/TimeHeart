using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class buttonlevelup : MonoBehaviour {
	protected EventSystem es;
	protected GameObject YN;
	protected Text PointNumber;
	public GameObject tmp;
	public int cache=0,point=0;
	void Start () {
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		YN=GameObject.Find("SkillSelect");
		PointNumber=GameObject.Find("PointNumber").GetComponent<Text>();
		//YN.GetComponent<buttonlevelup>().cache=SumVariable.charactorlv[][0]
		PointNumber.text="0";
		point=0;
	}
	public void buttoneffectsYes(){
		if(YN.GetComponent<buttonlevelup>().point-1!=-1 || Int32.Parse(tmp.transform.GetChild(1).GetComponent<Text>().text)>10 ){
			YN.GetComponent<buttonlevelup>().point--;
			PointNumber.text=YN.GetComponent<buttonlevelup>().point.ToString();
			tmp.transform.GetChild(1).GetComponent<Text>().text=(Int32.Parse(tmp.transform.GetChild(1).GetComponent<Text>().text)+1).ToString();
		}
		closemesgebox();
	}
	public void buttoneffectsNo(){
		closemesgebox();
	}
	public void buttoneffects(){
		YN.GetComponent<buttonlevelup>().tmp=es.currentSelectedGameObject.gameObject;
		YN.GetComponent<CanvasGroup>().alpha=1;
		YN.GetComponent<Canvas>().overrideSorting=true;
		YN.transform.GetChild(1).GetComponent<Canvas>().overrideSorting=true;
		YN.transform.GetChild(2).GetComponent<Canvas>().overrideSorting=true;
		YN.transform.GetChild(1).GetComponent<Button>().interactable=true;
		YN.transform.GetChild(2).GetComponent<Button>().interactable=true;
	}
	void closemesgebox(){
		PlayerPrefs.DeleteKey("point");
		YN.GetComponent<CanvasGroup>().alpha=0;
		YN.GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(1).GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(2).GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(1).GetComponent<Button>().interactable=false;
		YN.transform.GetChild(2).GetComponent<Button>().interactable=false;
	}
}
