using System;
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
	protected Text StatusName,LVStatus,HPStatus,MPStatus,EXPStatus,STRStatus,MDEFStatus,INTStatus,SPDStatus,DEFStatus,AGIStatus;
	protected int mode=0;
	protected RectTransform valueStatusHP,valueStatusMP,valueStatusEXP;
	void Start () {
		backgroundUI = GameObject.Find ("backgroundUI").GetComponent<CanvasGroup> ();
		leftlist=GameObject.FindGameObjectsWithTag("menuleftlist");
		upcharactor=GameObject.FindGameObjectsWithTag("menuupcharactor");
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();

		statusUI=GameObject.Find("statusUI").GetComponent<Image>();    //角色大圖
		StatusName=GameObject.Find("StatusName").GetComponent<Text>();     //姓名
		LVStatus=GameObject.Find("LVStatus").GetComponent<Text>(); //等級
		HPStatus=GameObject.Find("HPStatus").GetComponent<Text>(); //血量
		MPStatus=GameObject.Find("MPStatus").GetComponent<Text>(); //魔力
		EXPStatus=GameObject.Find("EXPStatus").GetComponent<Text>(); //經驗值
		STRStatus=GameObject.Find("STRStatus").GetComponent<Text>(); //力量
		MDEFStatus=GameObject.Find("MDEFStatus").GetComponent<Text>(); //魔防
		INTStatus=GameObject.Find("INTStatus").GetComponent<Text>(); //智力
		SPDStatus=GameObject.Find("SPDStatus").GetComponent<Text>(); //速度
		DEFStatus=GameObject.Find("DEFStatus").GetComponent<Text>(); //物防
		AGIStatus=GameObject.Find("AGIStatus").GetComponent<Text>(); //靈敏

		valueStatusHP=GameObject.Find("valueStatusHP").GetComponent<RectTransform>();
		valueStatusMP=GameObject.Find("valueStatusMP").GetComponent<RectTransform>();
		valueStatusEXP=GameObject.Find("valueStatusEXP").GetComponent<RectTransform>();
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
				charactormenu(1.ToString());
				backgroundUI.interactable=true;
				loadselected(leftlist[2]);
				es.sendNavigationEvents=true;
				SumVariable.keyboardopen = false;
				arraygameobjectbutton(upcharactor,false);
				arraygameobjectbutton(leftlist,true);
				StartCoroutine(Sumthing.view(backgroundUI,0,1,0.0625,0.005f));
				//checkbutton ();
		}else if(Input.GetKeyUp (KeyCode.Escape) && backgroundUI.alpha == 1 && leftlist[0].GetComponent<Button>().interactable==true){
				StartCoroutine(Sumthing.notview(backgroundUI,1,0,0.0625,0.005f));
				SumVariable.keyboardopen = true;
				arraygameobjectbutton(leftlist,false);
				es.sendNavigationEvents=false;
				backgroundUI.interactable=false;
				loadselected(null);
			}
			if(Input.GetKeyUp (KeyCode.Escape) && leftlist[0].GetComponent<Button>().interactable==false && mode==1){
				mode=0;
				StopAllCoroutines();
				StartCoroutine(upcharselect());
			}
			
		}

		protected IEnumerator Imagechange(){
			if(es.currentSelectedGameObject!=null){
			string s=es.currentSelectedGameObject.name.Substring(4);
				if(statusUI.sprite != Resources.Load<Sprite>("chatboxpicture/statusImage"+s) as Sprite ){
					charactormenu(s);
				}
			}
			yield return new WaitForSeconds(0.01f);
			StartCoroutine(Imagechange());
			yield return null;
		}

		protected void charactormenu(string s){//--------------角色選單資料讀取
			statusUI.sprite=Resources.Load<Sprite>("chatboxpicture/statusImage"+s) as Sprite;
			StatusName.text=SumVariable.charactorname[Int32.Parse(s)];
			LVStatus.text=SumVariable.charactorlv[Int32.Parse(s)][0].ToString();
			HPStatus.text=SumVariable.charactorlv[Int32.Parse(s)][1].ToString()+"/"+SumVariable.charactorlv[Int32.Parse(s)][2].ToString();
			valueStatusHP.localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(s)][2]/(float)SumVariable.charactorlv[Int32.Parse(s)][1]),0,0);
			MPStatus.text=SumVariable.charactorlv[Int32.Parse(s)][3].ToString()+"/"+SumVariable.charactorlv[Int32.Parse(s)][4].ToString();
		valueStatusMP.localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(s)][4]/(float)SumVariable.charactorlv[Int32.Parse(s)][3]),0,0);
			EXPStatus.text=SumVariable.charactorlv[Int32.Parse(s)][5].ToString()+"/"+SumVariable.charactorlv[Int32.Parse(s)][6].ToString();
		valueStatusEXP.localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(s)][6]/(float)SumVariable.charactorlv[Int32.Parse(s)][5]),0,0);
			STRStatus.text=SumVariable.charactorlv[Int32.Parse(s)][7].ToString();
			INTStatus.text=SumVariable.charactorlv[Int32.Parse(s)][8].ToString();
			DEFStatus.text=SumVariable.charactorlv[Int32.Parse(s)][9].ToString();
			MDEFStatus.text=SumVariable.charactorlv[Int32.Parse(s)][10].ToString();
			SPDStatus.text=SumVariable.charactorlv[Int32.Parse(s)][11].ToString();
			AGIStatus.text=SumVariable.charactorlv[Int32.Parse(s)][12].ToString();
		}
		protected IEnumerator upcharselect(){//----------------------上方角色選擇
			yield return new WaitForSeconds(0.01f);
			for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUI") as Sprite;
			}
			arraygameobjectbutton(leftlist,true);
			arraygameobjectbutton(upcharactor,false);
			loadselected(leftlist[0]);
			yield return null;
		}


	public void Statusmode(){//-----------------------------狀態模式(按鈕)
		mode=1;
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
	public void teammode(){//-----------------------隊伍模式
		mode=1;
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Team"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUILight") as Sprite;
				}
		}

	}

	void arraygameobjectbutton(GameObject [] a,bool b){//----------------------------關閉/開啟系列按鈕功能
		for(int i=0;i<a.Length;i++){
				a[i].GetComponent<Button> ().interactable = b;
		}

	}
	void loadselected(GameObject a){// -----------------------------------設定第一選項
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
