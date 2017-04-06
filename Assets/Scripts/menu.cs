﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class menu : MonoBehaviour {
	protected CanvasGroup backgroundUI;
	protected GameObject player;
	protected GameObject NPC,Icon;
	protected GameObject [] leftlist,upcharactor,battleteam,Items;
	protected EventSystem es;
	protected GameObject statusUI;
	protected Text StatusName,LVStatus,HPStatus,MPStatus,EXPStatus,STRStatus,MDEFStatus,INTStatus,SPDStatus,DEFStatus,AGIStatus;
	protected int mode=0;
	protected Sprite [] ass;
	protected RectTransform valueStatusHP,valueStatusMP,valueStatusEXP;
	void Start () {
		backgroundUI = GameObject.Find ("backgroundUI").GetComponent<CanvasGroup> ();
		leftlist=GameObject.FindGameObjectsWithTag("menuleftlist");
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();

		statusUI=GameObject.Find("statusUI");   //角色大圖
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
		
		Icon=GameObject.Find("Icon");
		battleteam=GameObject.FindGameObjectsWithTag("battleteam");
		Items=GameObject.FindGameObjectsWithTag("Items");


		ass=Resources.LoadAll<Sprite>("chatboxpicture/TeamCH");
		for(int i=0;i<battleteam.Length;i++){
			if(battleteam[i].name=="STeam1"||battleteam[i].name=="STeam2"||battleteam[i].name=="STeam3"){
				for(int j=0;j<ass.Length;j++){
					if(ass[j].name==SumVariable.battleteam[Int32.Parse(battleteam[i].name.Substring(5))-1].ToString()){
						battleteam[i].transform.GetChild(0).GetComponent<Image>().sprite=ass[j];
						break;
					}
				}
			}
		}


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
				startmenu();
				charactormenu(1.ToString());
				backgroundUI.interactable=true;
				loadselected(leftlist[2]);
				es.sendNavigationEvents=true;
				SumVariable.keyboardopen = false;
				arraygameobjectbutton(upcharactor,false,1);
				arraygameobjectbutton(leftlist,true,1);
				StartCoroutine(Sumthing.view(backgroundUI,0,1,0.0625,0.005f));
			}else if(Input.GetKeyUp (KeyCode.Escape) && backgroundUI.alpha == 1 && leftlist[0].GetComponent<Button>().interactable==true){
					StartCoroutine(Sumthing.notview(backgroundUI,1,0,0.0625,0.005f));
					SumVariable.keyboardopen = true;
					arraygameobjectbutton(leftlist,false,0);
					es.sendNavigationEvents=false;
					backgroundUI.interactable=false;
					loadselected(null);
				}
			if(Input.GetKeyUp (KeyCode.Escape) && leftlist[0].GetComponent<Button>().interactable==false && mode==1){
				mode=0;
				StopAllCoroutines();
				StartCoroutine(exitupchar(upcharactor,1));
			}else if(Input.GetKeyUp (KeyCode.Escape) && leftlist[0].GetComponent<Button>().interactable==false && mode==2){
				mode=0;
				for(int i=0;i<battleteam.Length;i++){
					battleteam[i].GetComponentInParent<CanvasGroup>().alpha=0;
				}
				StopAllCoroutines();
				statusUI.GetComponent<Image>().sprite=Resources.Load<Sprite>("chatboxpicture/statusImage"+1.ToString()) as Sprite;
				charactormenu(1.ToString());
				StartCoroutine(exitupchar(battleteam,0));
				
			}else if(Input.GetKeyUp (KeyCode.Escape) && leftlist[0].GetComponent<Button>().interactable==false && mode==4){
				mode=0;
				for(int i=0;i<Items.Length;i++){
					if(Items[i].name=="Item"){
						Items[i].GetComponent<Canvas>().sortingOrder=0;
					}
				}
				statusUI.GetComponentInParent<CanvasGroup>().alpha=1;
				StartCoroutine(exitupchar(Items,0));
			}
		}

	protected IEnumerator Imagechange(){//----------------------上方角色選擇
			if(es.currentSelectedGameObject!=null){
			string s=es.currentSelectedGameObject.name.Substring(4);
				if(statusUI.GetComponent<Image>().sprite != Resources.Load<Sprite>("chatboxpicture/statusImage"+s) as Sprite ){
					if(mode==1){
						statusUI.GetComponent<Image>().sprite=Resources.Load<Sprite>("chatboxpicture/statusImage"+s) as Sprite;
						charactormenu(s);
					}else if(mode==2){
						statusUI.GetComponent<Image>().sprite=Resources.Load<Sprite>("chatboxpicture/statusUI") as Sprite;
						charactormenu(es.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite.name);
					}
				}
			}
			yield return new WaitForSeconds(0.01f);
			StartCoroutine(Imagechange());
			yield return null;
		}

	protected void charactormenu(string s){//--------------角色選單資料讀取
			if(Int32.Parse(s)!=0){
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
			}else{
				StatusName.text="";
				LVStatus.text="";
				HPStatus.text="";
				valueStatusHP.localPosition=new Vector3(0,0,0);
				MPStatus.text="";
				valueStatusMP.localPosition=new Vector3(0,0,0);
				EXPStatus.text="";
				valueStatusEXP.localPosition=new Vector3(0,0,0);
				STRStatus.text="";
				INTStatus.text="";
				DEFStatus.text="";
				MDEFStatus.text="";
				SPDStatus.text="";
				AGIStatus.text="";
			}
		}
	protected IEnumerator exitupchar(GameObject [] b,int c){
			yield return new WaitForSeconds(0.01f);
			for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUI") as Sprite;
			}
			arraygameobjectbutton(leftlist,true,1);
			arraygameobjectbutton(b,false,c);
			loadselected(leftlist[0]);
			yield return null;
		}

	protected void startmenu(){//-----隊伍偵測
		int j=0;
		GameObject [] q=new GameObject [SumVariable.team.Length];
		for(int i=0;i<SumVariable.team.Length;i++){
			if(SumVariable.teamban[i]){
				q[j]=Instantiate(Resources.Load ("prefabs/team/icon" + SumVariable.team[i]),new Vector3((-1026.5f+187*j),-1.6f,0),Quaternion.Euler(0,0,180)) as GameObject;
				q[j].transform.SetParent (Icon.gameObject.transform,false);
				q[j].name = "icon"+SumVariable.team[i];
				if(j>0){
					SetSelectedGameObjects(q,j-1,j);
				}
				j++;
			}
		}
		SetSelectedGameObjects(q,j-1,0);
		upcharactor=GameObject.FindGameObjectsWithTag("menuupcharactor");
		for(int i=0;i<upcharactor.Length;i++){
			string a=upcharactor[i].name.Substring(4);
			upcharactor[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text=SumVariable.charactorlv[Int32.Parse(a)][0].ToString();
			upcharactor[i].transform.GetChild(2).transform.GetChild(0).GetComponent<RectTransform>().localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(a)][2]/(float)SumVariable.charactorlv[Int32.Parse(a)][1]),0,0);
			upcharactor[i].transform.GetChild(3).transform.GetChild(0).GetComponent<RectTransform>().localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(a)][4]/(float)SumVariable.charactorlv[Int32.Parse(a)][3]),0,0);	
		}
	}

	protected void SetSelectedGameObjects(GameObject [] q,int a,int b){//-----------------設定創建按鈕/選項後左右鍵指向其他按鈕/選項
		Navigation r,s;
		r=q[a].gameObject.GetComponent<Button>().navigation;
		r.selectOnRight=q[b].gameObject.GetComponent<Button>();
		q[a].gameObject.GetComponent<Button>().navigation=r;
		s=q[b].gameObject.GetComponent<Button>().navigation;
		s.selectOnLeft=q[a].gameObject.GetComponent<Button>();
		q[b].gameObject.GetComponent<Button>().navigation=s;
	}
	void arraygameobjectbutton(GameObject [] a,bool b,float c){//----------------------------關閉/開啟系列按鈕功能
		for(int i=0;i<a.Length;i++){
			if(a[i].GetComponent<Button> ()!=null){
				a[i].GetComponent<Button> ().interactable = b;
			}
			if(a[i].GetComponent<CanvasGroup>()!=null){
				a[i].GetComponent<CanvasGroup>().alpha=c;
				a[i].GetComponent<CanvasGroup> ().interactable=b;
			}
		}

	}
	void loadselected(GameObject a){// -----------------------------------設定第一選項
		es.firstSelectedGameObject=a;
		es.SetSelectedGameObject(null);
 		es.SetSelectedGameObject(es.firstSelectedGameObject);
	}
//-----------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------
	public void Statusmode(){//-----------------------------狀態模式(按鈕)
		mode=1;
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Status"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUILight") as Sprite;
				}
		}
		arraygameobjectbutton(upcharactor,true,1);
		loadselected(upcharactor[0]);
		StartCoroutine(Imagechange());
	}
	public void teammode(){//-----------------------隊伍模式(按鈕)
		mode=2;
		statusUI.GetComponent<Image>().sprite=Resources.Load<Sprite>("chatboxpicture/statusUI") as Sprite;
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Team"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUILight") as Sprite;
				}
		}
		arraygameobjectbutton(battleteam,true,1);
		for(int i=0;i<battleteam.Length;i++){
			battleteam[i].GetComponentInParent<CanvasGroup>().alpha=1;
			if(battleteam[i].name=="STeam1"){
				loadselected(battleteam[i]);
			}
		}
		StartCoroutine(Imagechange());
	}
	public void teammodeselectchar(string s){
		mode=3;
		for(int i=0;i<battleteam.Length;i++){
				if(battleteam[i].name==s){
					battleteam[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/teamLight") as Sprite;
					for(int j=0;j<ass.Length;j++){
						if(ass[j].name=="0"){
							battleteam[i].transform.GetChild(0).GetComponent<Image>().sprite=ass[j];
							SumVariable.battleteam[Int32.Parse(s.Substring(5))-1]=0;
					}
				}
				}
				
		}
		arraygameobjectbutton(battleteam,false,1);
		arraygameobjectbutton(upcharactor,true,1);
		for(int i=0;i<upcharactor.Length;i++){
			if(upcharactor[i].name.Substring(4)==SumVariable.battleteam[0].ToString()){
				upcharactor[i].GetComponent<Button>().interactable=false;
			}
			if(upcharactor[i].name.Substring(4)==SumVariable.battleteam[1].ToString()){
				upcharactor[i].GetComponent<Button>().interactable=false;
			}
			if(upcharactor[i].name.Substring(4)==SumVariable.battleteam[2].ToString()){
				upcharactor[i].GetComponent<Button>().interactable=false;
			}
		}
		for(int i=0;i<upcharactor.Length;i++){
			if(es.currentSelectedGameObject==null){
				loadselected(upcharactor[i]);
			}
		}
		
		StartCoroutine(teammodeselectcharIE(s));
	}
	protected IEnumerator teammodeselectcharIE(string s){
		yield return new WaitForSeconds(0.01f);
		StartCoroutine(teammodeselectcharIE(s));
		if(es.currentSelectedGameObject!=null){
			string a=es.currentSelectedGameObject.name.Substring(4);
			charactormenu(a);
		}
		if(Input.GetKeyUp(KeyCode.Escape)){
			for(int i=0;i<battleteam.Length;i++){
				if(battleteam[i].name==s){
					battleteam[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/teamDark") as Sprite;
				}
			}
			arraygameobjectbutton(battleteam,true,1);
			arraygameobjectbutton(upcharactor,false,1);
			for(int i=0;i<battleteam.Length;i++){
				if(battleteam[i].name=="STeam1"){
					loadselected(battleteam[i]);
				}
			}
			mode=2;
			StopAllCoroutines();
		}
		
		if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.KeypadEnter)){
			if(es.currentSelectedGameObject.GetComponent<Button>().interactable==true){
			for(int i=0;i<battleteam.Length;i++){
				if(battleteam[i].name==s){
					for(int j=0;j<ass.Length;j++){
						if(ass[j].name==es.currentSelectedGameObject.name.Substring(4)){
							battleteam[i].transform.GetChild(0).GetComponent<Image>().sprite=ass[j];
							SumVariable.battleteam[Int32.Parse(s.Substring(5))-1]=Int32.Parse(es.currentSelectedGameObject.name.Substring(4));
							break;
							}
						}
					}	
				}
			}
			/*for(int i=0;i<battleteam.Length;i++){
				if(battleteam[i].name==s){
					battleteam[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/teamDark") as Sprite;
				}
			}
			arraygameobjectbutton(battleteam,true);
			arraygameobjectbutton(upcharactor,false);
			for(int i=0;i<battleteam.Length;i++){
				if(battleteam[i].name=="STeam1"){
					loadselected(battleteam[i]);
				}
			}
			mode=2;
			StopAllCoroutines();
			*/
		}
		yield return null;
	}
	public void Itemsmode(){
		mode=4;
		statusUI.GetComponentInParent<CanvasGroup>().alpha=0;
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Item"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUILight") as Sprite;
				}
		}
		arraygameobjectbutton(Items,true,1);
		for(int i=0;i<Items.Length;i++){
			if(Items[i].name=="Item"){
				Items[i].GetComponent<Canvas>().sortingOrder=101;
			}
			if(Items[i].name=="itemUI"){
				loadselected(Items[i]);
			}
		}

	}
	public void ItemsitemUI(){

	}
	public void ItemsmainUI(){
		
	}
	public void ItemsfriendsUI(){
		
	}
	}
