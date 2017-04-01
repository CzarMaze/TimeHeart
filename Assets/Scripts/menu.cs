using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class menu : MonoBehaviour {
	protected CanvasGroup backgroundUI;
	protected GameObject [] selectonchr;
	protected GameObject player;
	protected GameObject NPC;
	protected GameObject [] leftlist,upcharactor;
	protected EventSystem es;

	protected
	void Start () {
		backgroundUI = GameObject.Find ("backgroundUI").GetComponent<CanvasGroup> ();
		leftlist=GameObject.FindGameObjectsWithTag("menuleftlist");
		upcharactor=GameObject.FindGameObjectsWithTag("menuupcharactor");
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();

		//selectonchr = GameObject.FindGameObjectsWithTag ("menubuttons");
		//player = GameObject.Find ("Player");
		//NPC = GameObject.Find ("NPC");
	/*	for(int i=0;i<selectonchr.Length;i++){
			if (selectonchr [i].name.Substring (0, 3) == "sel") {
				string k = selectonchr [i].name.Substring (3).ToString ();
				selectonchr [i].GetComponent<Button> ().onClick.AddListener (delegate {
					chator (k);
				});
			}
		}*/
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape) && backgroundUI.alpha == 0) {
				es.firstSelectedGameObject=leftlist[0];
				es.SetSelectedGameObject(null);
 				es.SetSelectedGameObject(es.firstSelectedGameObject);
				StartCoroutine(Sumthing.view(backgroundUI,0,1,0.0625,0.005f));
				SumVariable.keyboardopen = false;
				backgroundUI.interactable=true;
				UnityEngine.EventSystems.EventSystem.current.sendNavigationEvents=true;
				for(int i=0;i<upcharactor.Length;i++){
					upcharactor[i].GetComponent<Button> ().interactable = false;
				}
				//checkbutton ();
		}else if(Input.GetKeyUp (KeyCode.Escape) && backgroundUI.alpha == 1){
				StartCoroutine(Sumthing.notview(backgroundUI,1,0,0.0625,0.005f));
				SumVariable.keyboardopen = true;
				backgroundUI.interactable=false;
				UnityEngine.EventSystems.EventSystem.current.sendNavigationEvents=false;
				es.SetSelectedGameObject(null);
			}
		}

	public void Statusmode(){
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
		}
		for(int i=0;i<upcharactor.Length;i++){
				upcharactor[i].GetComponent<Button> ().interactable = true;
		}
		es.firstSelectedGameObject=upcharactor[0];
		es.SetSelectedGameObject(null);
 		es.SetSelectedGameObject(es.firstSelectedGameObject);
	}
	/*void checkbutton(){
		for(int i=0;i<selectonchr.Length;i++){
			if (selectonchr [i].name.Substring (0, 3) == "sel") {
				for(int j=0;j<SumVariable.ban.Length;j++){
					if(selectonchr [i].name.Substring (3).ToString()== SumVariable.add[j]){
						if (SumVariable.ban [j]) {
							selectonchr [i].GetComponent<Button> ().interactable = false;
						} else {
							selectonchr [i].GetComponent<Button> ().interactable = true;
						}
						break;
					}

				}
			}
		}
	}
	void chator(string s){
		for (int i = 0; i < selectonchr.Length; i++) {
			if (s == selectonchr [i].name.Substring (3).ToString ()) {
				selectonchr [i].GetComponent<Button> ().interactable = false;
				break;
			}
		}
		for (int i = 0; i < SumVariable.ban.Length; i++) {
			if (s == SumVariable.add [i]) {
				GameObject q=Instantiate(Resources.Load ("prefabs/" + s),SumVariable.charactorxyz[i],Quaternion.identity) as GameObject;
				q.transform.SetParent (NPC.gameObject.transform);
				q.name = s;
				SumVariable.ban [i] = true;
				if (SumVariable.add [i] == SumVariable.charactor) {
					player.GetComponent<animeaction> ().reselection ();
				}
			}
		}*/

	}
