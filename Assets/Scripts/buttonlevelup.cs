using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class buttonlevelup : MonoBehaviour {
	protected EventSystem es;
	protected GameObject YN;
	void Start () {
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		YN=GameObject.Find("SkillSelect");
	}

	public void buttoneffectsYes(){
		Debug.Log(es.currentSelectedGameObject.transform.GetChild(2).GetComponent<Text>().text);
		closemesgebox();
	}
	public void buttoneffectsNo(){
		closemesgebox();
	}
	public void buttoneffects(){
		YN.GetComponent<buttoneffect>().tmp=es.currentSelectedGameObject.gameObject;
		YN.GetComponent<CanvasGroup>().alpha=1;
		YN.GetComponent<Canvas>().overrideSorting=true;
		YN.transform.GetChild(1).GetComponent<Canvas>().overrideSorting=true;
		YN.transform.GetChild(2).GetComponent<Canvas>().overrideSorting=true;
		YN.transform.GetChild(1).GetComponent<Button>().interactable=true;
		YN.transform.GetChild(2).GetComponent<Button>().interactable=true;
	}
	void closemesgebox(){
		YN.GetComponent<CanvasGroup>().alpha=0;
		YN.GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(1).GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(2).GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(1).GetComponent<Button>().interactable=false;
		YN.transform.GetChild(2).GetComponent<Button>().interactable=false;
	}
}
