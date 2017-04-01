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
	protected Image statusUI;
	void Start () {
		backgroundUI = GameObject.Find ("backgroundUI").GetComponent<CanvasGroup> ();
		leftlist=GameObject.FindGameObjectsWithTag("menuleftlist");
		upcharactor=GameObject.FindGameObjectsWithTag("menuupcharactor");
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		statusUI=GameObject.Find("statusUI").GetComponent<Image>();

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
		if (Input.GetKeyUp (KeyCode.Escape) && backgroundUI.alpha == 0 ) {
				backgroundUI.interactable=true;
				loadselected(leftlist[2]);
				es.sendNavigationEvents=true;
				StartCoroutine(Sumthing.view(backgroundUI,0,1,0.0625,0.005f));
				SumVariable.keyboardopen = false;
				arraygameobjectbutton(leftlist,true);
				//checkbutton ();
		}else if(Input.GetKeyUp (KeyCode.Escape) && backgroundUI.alpha == 1 && leftlist[0].GetComponent<Button>().interactable==true){
				StartCoroutine(Sumthing.notview(backgroundUI,1,0,0.0625,0.005f));
				SumVariable.keyboardopen = true;
				arraygameobjectbutton(leftlist,false);
				es.sendNavigationEvents=false;
				backgroundUI.interactable=false;
				loadselected(null);
			}
			if(Input.GetKeyUp (KeyCode.Escape) && leftlist[0].GetComponent<Button>().interactable==false && upcharactor[0].GetComponent<Button>().interactable==true){
				StopAllCoroutines();
				StartCoroutine(upcharselect());
			}
			
		}

		protected IEnumerator Imagechange(){
			if(statusUI.sprite != Resources.Load<Sprite>("chatboxpicture/statusUI"+es.currentSelectedGameObject.name.Substring(4)) as Sprite ){
				statusUI.sprite=Resources.Load<Sprite>("chatboxpicture/statusUI"+es.currentSelectedGameObject.name.Substring(4)) as Sprite;
				Debug.Log("chatboxpicture/statusUI"+es.currentSelectedGameObject.name.Substring(4));
			}
			yield return new WaitForSeconds(0.01f);
			StartCoroutine(Imagechange());
			yield return null;
		}
		protected IEnumerator upcharselect(){
			yield return new WaitForSeconds(0.01f);
			for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUI") as Sprite;
			}
			arraygameobjectbutton(leftlist,true);
			arraygameobjectbutton(upcharactor,false);
			loadselected(leftlist[0]);
			yield return null;
		}
	public void Statusmode(){
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Status"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUILight") as Sprite;
				}
		}
		arraygameobjectbutton(upcharactor,true);
		loadselected(upcharactor[0]);
		StartCoroutine(Imagechange());
	}


	void arraygameobjectbutton(GameObject [] a,bool b){
		for(int i=0;i<a.Length;i++){
				a[i].GetComponent<Button> ().interactable = b;
		}

	}
	void loadselected(GameObject a){
		es.firstSelectedGameObject=a;
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
