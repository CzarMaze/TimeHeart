﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttoneffect : MonoBehaviour {
	protected EventSystem es;
	protected GameObject YN;
	public GameObject tmp;
	void Start(){
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		YN=GameObject.Find("Selectitem");
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
	public void buttoneffectsYes(){
		switch(tmp.transform.GetChild(0).GetComponent<Text>().text){
			case "人之初":
			Debug.Log("SA");
			buttonthingmathdel(tmp,1);
			break;
			default:
			StartCoroutine(errbox());
			break;
		}
		YN.GetComponent<CanvasGroup>().alpha=0;
		YN.GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(1).GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(2).GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(1).GetComponent<Button>().interactable=false;
		YN.transform.GetChild(2).GetComponent<Button>().interactable=false;
	}
	IEnumerator errbox(){
		GameObject.Find("Erroritem").GetComponent<CanvasGroup>().alpha=1;
		yield return new WaitForSeconds(1);
		GameObject.Find("Erroritem").GetComponent<CanvasGroup>().alpha=0;
		yield return null;
	}
	public void buttoneffectsNo(){
		YN.GetComponent<CanvasGroup>().alpha=0;
		YN.GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(1).GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(2).GetComponent<Canvas>().overrideSorting=false;
		YN.transform.GetChild(1).GetComponent<Button>().interactable=false;
		YN.transform.GetChild(2).GetComponent<Button>().interactable=false;
	}

	void buttonthingmathdel(GameObject tmp,int x){
		tmp.transform.GetChild(1).GetComponent<Text>().text=(Int32.Parse(tmp.transform.GetChild(1).GetComponent<Text>().text)-x).ToString();
	}
}
