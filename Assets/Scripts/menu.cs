using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menu : MonoBehaviour {
	protected GameObject menuimage;
	protected GameObject [] selectonchr;
	protected GameObject player;
	protected GameObject NPC;
	void Start () {
		menuimage = GameObject.Find ("menuImage");
		selectonchr = GameObject.FindGameObjectsWithTag ("menubuttons");
		player = GameObject.Find ("Player");
		NPC = GameObject.Find ("NPC");
		for(int i=0;i<selectonchr.Length;i++){
			if (selectonchr [i].name.Substring (0, 3) == "sel") {
				string k = selectonchr [i].name.Substring (3).ToString ();
				selectonchr [i].GetComponent<Button> ().onClick.AddListener (delegate {
					chator (k);
				});
			}
		}
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape) && SumVariable.keyboardopen) {
				SumVariable.keyboardopen = false;
				menuimage.GetComponent<CanvasGroup> ().alpha = 1;
				checkbutton ();
		}else if(Input.GetKeyUp (KeyCode.Escape) && menuimage.GetComponent<CanvasGroup> ().alpha == 1){
				SumVariable.keyboardopen = true;
				menuimage.GetComponent<CanvasGroup> ().alpha = 0;
				for (int i = 0; i < selectonchr.Length; i++) {
					selectonchr [i].GetComponent<Button> ().interactable = false;
				}
			}
		}
	void checkbutton(){
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
		}

	}
}
