using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class menu : MonoBehaviour {
	protected CanvasGroup backgroundUI;
	protected GameObject Icon,statusUI,YN,SYN;
	protected GameObject [] leftlist,upcharactor,battleteam,Items,itemUI,mainUI,friendsUI,Skills,AttackSkill,HelpSkill,CureSkill;
	protected EventSystem es;
	protected Text StatusName,LVStatus,HPStatus,MPStatus,EXPStatus,STRStatus,MDEFStatus,INTStatus,SPDStatus,DEFStatus,AGIStatus,ItemEx,SkillEx;
	protected Image Itemimage,Skillimage;
	protected int mode=0;
	protected Sprite [] ass;
	protected RectTransform valueStatusHP,valueStatusMP,valueStatusEXP;
	public static MyjsonSQL itemSave,friendsSave,mainSave,CureSkillSave,HelpSkillSave,AttackSkillSave;

	protected String setteam;
	void startfind(){
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
		//-----------------------------------------------------------
		Itemimage=GameObject.Find("Itemimage").GetComponent<Image>();
		ItemEx=GameObject.Find("ItemEx").GetComponent<Text>();
		Items=GameObject.FindGameObjectsWithTag("Itemss");
		itemUI=GameObject.FindGameObjectsWithTag("itemUI");
		mainUI=GameObject.FindGameObjectsWithTag("mainUI");
		friendsUI=GameObject.FindGameObjectsWithTag("friendsUI");
		//------------------------------------------------------------
		Skillimage=GameObject.Find("Skillimage").GetComponent<Image>();
		SkillEx=GameObject.Find("SkillEx").GetComponent<Text>();
		Skills=GameObject.FindGameObjectsWithTag("Skills");
		AttackSkill=GameObject.FindGameObjectsWithTag("AttackSkill");
		HelpSkill=GameObject.FindGameObjectsWithTag("HelpSkill");
		CureSkill=GameObject.FindGameObjectsWithTag("CureSkill");
		//------------------------------------------------------------
		ass=Resources.LoadAll<Sprite>("chatboxpicture/TeamCH");
		YN=GameObject.Find("Selectitem");
		SYN=GameObject.Find("SkillSelect");
	}
	void Start () {
		startfind();
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
		itemSave=cachearticleread(itemUI,"cacheitemSave","itemList","itemitem","itemitem");
		friendsSave=cachearticleread(friendsUI,"cachefriendsSave","friendsList","itemitem","friendsitem");
		mainSave=cachearticleread(mainUI,"cachemainSave","mainList","itemitem","mainitem");
		CureSkillSave=cachearticleread(CureSkill,"cacheCureSkill","CureList","skllskill","CureSkill");
		HelpSkillSave=cachearticleread(HelpSkill,"cacheHelpSkill","HelpList","skllskill","HelpSkill");
		AttackSkillSave=cachearticleread(AttackSkill,"cacheAttackSkill","AttackList","skllskill","AttackSkill");
	}
	void Update () {
			if (Input.GetKeyUp (KeyCode.Escape) && backgroundUI.alpha == 0 ) {
				if(GameObject.Find("icon1")==null){
					startmenu();
				}
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
			if(leftlist[0].GetComponent<Button>().interactable==false && mode==1){//狀態
				if(es.currentSelectedGameObject!=null){
						Imagechange();
					}
				if(Input.GetKeyUp(KeyCode.Escape)){
					mode=0;
					StartCoroutine(exitupchar(upcharactor,1));
				}
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==2){
				if(Input.GetKeyUp (KeyCode.Escape)){
					mode=0;
					for(int i=0;i<battleteam.Length;i++){
						battleteam[i].GetComponentInParent<CanvasGroup>().alpha=0;
					}
					statusUI.GetComponent<Image>().sprite=Resources.Load<Sprite>("chatboxpicture/statusImage"+1.ToString()) as Sprite;
					charactormenu(1.ToString());
					StartCoroutine(exitupchar(battleteam,0));
				}
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==3){
				//----------
				if(es.currentSelectedGameObject!=null){
					string a=es.currentSelectedGameObject.name.Substring(4);
					charactormenu(a);
				}
				if(Input.GetKeyUp(KeyCode.Escape)){
					for(int i=0;i<battleteam.Length;i++){
						if(battleteam[i].name==setteam){
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
							if(battleteam[i].name==setteam){
								for(int j=0;j<ass.Length;j++){
									if(ass[j].name==es.currentSelectedGameObject.name.Substring(4)){
										battleteam[i].transform.GetChild(0).GetComponent<Image>().sprite=ass[j];
										SumVariable.battleteam[Int32.Parse(setteam.Substring(5))-1]=Int32.Parse(es.currentSelectedGameObject.name.Substring(4));
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
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==4){
				if(YN.GetComponent<CanvasGroup>().alpha==1){
					if(GameObject.Find("itemUI").GetComponent<Button> ().interactable==true){
						GameObject.Find("friendsUI").GetComponent<Button>().interactable=false;
						GameObject.Find("itemUI").GetComponent<Button>().interactable=false;
						GameObject.Find("mainUI").GetComponent<Button>().interactable=false;
						itemUI=GameObject.FindGameObjectsWithTag("itemUI");
						mainUI=GameObject.FindGameObjectsWithTag("mainUI");
						friendsUI=GameObject.FindGameObjectsWithTag("friendsUI");
						arraygameobjectbutton(itemUI,false,1);
						arraygameobjectbutton(mainUI,false,1);
						arraygameobjectbutton(friendsUI,false,1);
						loadselected(GameObject.Find("Yesitem"));
					}
				}else{
					if(GameObject.Find("itemUI").GetComponent<Button> ().interactable==false){
						GameObject.Find("friendsUI").GetComponent<Button>().interactable=true;
						GameObject.Find("itemUI").GetComponent<Button>().interactable=true;
						GameObject.Find("mainUI").GetComponent<Button>().interactable=true;
						itemUI=GameObject.FindGameObjectsWithTag("itemUI");
						mainUI=GameObject.FindGameObjectsWithTag("mainUI");
						friendsUI=GameObject.FindGameObjectsWithTag("friendsUI");
						arraygameobjectbutton(itemUI,true,1);
						arraygameobjectbutton(mainUI,true,1);
						arraygameobjectbutton(friendsUI,true,1);
						loadselected(GameObject.Find("itemUI"));
					}
					if(es.currentSelectedGameObject!=null){
						if(es.currentSelectedGameObject.GetComponent<itemsbuttonps>()!=null){	
							Byte[] imabytes=Convert.FromBase64String(es.currentSelectedGameObject.GetComponent<itemsbuttonps>().image);
							Texture2D outima;
							outima=new Texture2D(1,1);
							outima.LoadImage(imabytes);
							outima.filterMode=FilterMode.Point;
							Rect rect = new Rect(0, 0, outima.width, outima.height);
							Itemimage.sprite=Sprite.Create(outima,rect,new Vector2(),100f);
							ItemEx.text=es.currentSelectedGameObject.GetComponent<itemsbuttonps>().explanation;
						}else{
							Itemimage.sprite=Resources.Load<Sprite>("Image/0") as Sprite;
							ItemEx.text="";
						}
					}
					if(Input.GetKeyUp (KeyCode.Escape)){
						mode=0;
						for(int i=0;i<Items.Length;i++){
							Items[i].GetComponent<Canvas>().sortingOrder=0;
						}
						statusUI.GetComponentInParent<CanvasGroup>().alpha=1;
						arraygameobjectbutton(itemUI,false,1);
						arraygameobjectbutton(mainUI,false,0);
						arraygameobjectbutton(friendsUI,false,0);
						StartCoroutine(exitupchar(Items,0));
					}
				}
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==5){
				if(SYN.GetComponent<CanvasGroup>().alpha==1){
					if(GameObject.Find("AttackSkill").GetComponent<Button> ().interactable==true){
						GameObject.Find("AttackSkill").GetComponent<Button>().interactable=false;
						GameObject.Find("HelpSkill").GetComponent<Button>().interactable=false;
						GameObject.Find("CureSkill").GetComponent<Button>().interactable=false;
						AttackSkill=GameObject.FindGameObjectsWithTag("AttackSkill");
						HelpSkill=GameObject.FindGameObjectsWithTag("HelpSkill");
						CureSkill=GameObject.FindGameObjectsWithTag("CureSkill");
						arraygameobjectbutton(AttackSkill,false,1);
						arraygameobjectbutton(HelpSkill,false,1);
						arraygameobjectbutton(CureSkill,false,1);
						loadselected(GameObject.Find("SkillYes"));
					}
				}else{
					
					if(GameObject.Find("AttackSkill").GetComponent<Button> ().interactable==false){
						GameObject.Find("AttackSkill").GetComponent<Button>().interactable=true;
						GameObject.Find("HelpSkill").GetComponent<Button>().interactable=true;
						GameObject.Find("CureSkill").GetComponent<Button>().interactable=true;
						AttackSkill=GameObject.FindGameObjectsWithTag("AttackSkill");
						HelpSkill=GameObject.FindGameObjectsWithTag("HelpSkill");
						CureSkill=GameObject.FindGameObjectsWithTag("CureSkill");
						arraygameobjectbutton(AttackSkill,true,1);
						arraygameobjectbutton(HelpSkill,true,1);
						arraygameobjectbutton(CureSkill,true,1);
						loadselected(GameObject.Find("AttackSkill"));
					}
					if(es.currentSelectedGameObject!=null){
						if(es.currentSelectedGameObject.GetComponent<itemsbuttonps>()!=null){	
							Byte[] imabytes=Convert.FromBase64String(es.currentSelectedGameObject.GetComponent<itemsbuttonps>().image);
							Texture2D outima;
							outima=new Texture2D(1,1);
							outima.LoadImage(imabytes);
							outima.filterMode=FilterMode.Point;
							Rect rect = new Rect(0, 0, outima.width, outima.height);
							Skillimage.sprite=Sprite.Create(outima,rect,new Vector2(),100f);
							SkillEx.text=es.currentSelectedGameObject.GetComponent<itemsbuttonps>().explanation;
						}else{
							Skillimage.sprite=Resources.Load<Sprite>("Image/0") as Sprite;
							SkillEx.text="";
						}
					}
					if(Input.GetKeyUp (KeyCode.Escape)){
						mode=0;
						for(int i=0;i<Skills.Length;i++){
							Skills[i].GetComponent<Canvas>().sortingOrder=0;
						}
						statusUI.GetComponentInParent<CanvasGroup>().alpha=1;
						arraygameobjectbutton(AttackSkill,false,1);
						arraygameobjectbutton(HelpSkill,false,0);
						arraygameobjectbutton(CureSkill,false,0);
						StartCoroutine(exitupchar(Skills,0));
					}
				}
			}
		}

	private void OnDisable(){
		/*cachearticlereadOnDisable(itemSave,itemUI,"cacheitemSave","itemList");
		cachearticlereadOnDisable(friendsSave,friendsUI,"cachefriendsSave","friendsList");
		cachearticlereadOnDisable(mainSave,mainUI,"cachemainSave","mainList");
		cachearticlereadOnDisable(CureSkillSave,CureSkill,"cacheCureSkillSave","CureList");
		cachearticlereadOnDisable(HelpSkillSave,HelpSkill,"cacheHelpSkillSave","HelpList");
		cachearticlereadOnDisable(AttackSkillSave,AttackSkill,"cacheAttackSkillSave","AttackList");
		PlayerPrefs.Save();*/
	}
	private void OnApplicationQuit(){
		//PlayerPrefs.DeleteKey("cacheitemSave");
		//PlayerPrefs.DeleteKey("cachefriendsSave");
		//PlayerPrefs.DeleteKey("cachemainSave");
	}

//----------------------------------------------------------------------------------------------------------------------
	MyjsonSQL cachearticleread(GameObject [] UI,string cachesave,string list,string prefabsname,string buttonnames){
		MyjsonSQL cacheMyjsonSQL;
		if(PlayerPrefs.GetString(cachesave)!=""){//--cacheitemSave--變數1
			var cacheitemSave=PlayerPrefs.GetString(cachesave);//->-cacheitemSave--變數1
			cacheMyjsonSQL=JsonUtility.FromJson<MyjsonSQL>(cacheitemSave);//--------itemSave 變數0
			GameObject [] ites=new GameObject [cacheMyjsonSQL.gift];
			if(cacheMyjsonSQL.gift>0){
				for(int i=0;i<UI.Length;i++){
					if(UI[i].name==list){//------itemList---變數2
						for(int j=0;j<cacheMyjsonSQL.gift;j++){
							ites[j]=Instantiate(Resources.Load ("prefabs/"+prefabsname),new Vector3(0,2430.3300f-137.5072f*j,0),Quaternion.Euler(0,0,0)) as GameObject;
							ites[j].transform.SetParent (UI[i].gameObject.transform,false);//526.5784f
							ites[j].name = buttonnames+(j+1);//-----itemitem----變數3
							ites[j].transform.GetChild(0).GetComponent<Text>().text=cacheMyjsonSQL.main[j].Name;
							ites[j].transform.GetChild(1).GetComponent<Text>().text=cacheMyjsonSQL.main[j].number.ToString();
							ites[j].GetComponent<itemsbuttonps>().image=cacheMyjsonSQL.main[j].image;
							ites[j].GetComponent<itemsbuttonps>().explanation=cacheMyjsonSQL.main[j].explanation;
							ites[j].tag=UI[0].tag;
							if(j>0){
								SetSelectedGameObjects("du",ites[j-1].gameObject.GetComponent<Button>(),ites[j].gameObject.GetComponent<Button>());
							}
						}
					}
				}
			} 
			PlayerPrefs.DeleteKey(cachesave);//--cacheitemSave--變數1
		}else{
			cacheMyjsonSQL=new MyjsonSQL();
			cacheMyjsonSQL.gift=0;
			cacheMyjsonSQL.main=new MyjsonSQL.Mains[0];
		}
		return cacheMyjsonSQL;
	}
	void cachearticlereadOnDisable(MyjsonSQL cacheMyjsonSQL,GameObject [] UI,string cachesave,string list){
		int q=0;
		if(cacheMyjsonSQL.gift>0){
			cacheMyjsonSQL.main=new MyjsonSQL.Mains[cacheMyjsonSQL.gift];
			for(int i=0;i<UI.Length;i++){
					if(UI[i].name==list){
						while(cacheMyjsonSQL.gift>q){
							cacheMyjsonSQL.main[q].Name= UI[i].transform.GetChild(q).transform.GetChild(0).GetComponent<Text>().text;
							cacheMyjsonSQL.main[q].number=Int32.Parse(UI[i].transform.GetChild(q).transform.GetChild(1).GetComponent<Text>().text);
							cacheMyjsonSQL.main[q].image=UI[i].transform.GetChild(q).GetComponent<itemsbuttonps>().image;
							cacheMyjsonSQL.main[q].explanation=UI[i].transform.GetChild(q).GetComponent<itemsbuttonps>().explanation;
							q++;
						}
                    break;
					}
			}
		}
		var cacheitemSave=JsonUtility.ToJson(cacheMyjsonSQL);
		PlayerPrefs.SetString(cachesave,cacheitemSave);
	}
	public static int Addnewitems(MyjsonSQL cacheMyjsonSQL,GameObject [] UI,string list,string prefabsname,string buttonnames,string addthings,int addnum,string im,string ex){
				for(int i=0;i<UI.Length;i++){
					if(UI[i].name==list){
						for(int j=0;j<=cacheMyjsonSQL.gift;j++){
							if(cacheMyjsonSQL.gift==j||cacheMyjsonSQL.gift==0){
								cacheMyjsonSQL.gift++;
								GameObject ites=Instantiate(Resources.Load("prefabs/"+prefabsname), new Vector3(0,2430.3300f-137.5072f*(cacheMyjsonSQL.gift-1),0),Quaternion.Euler(0,0,0)) as GameObject;
								ites.transform.SetParent (UI[i].gameObject.transform,false);//new Vector3(0.0004882813f,526.5784f-137.5072f*(cacheMyjsonSQL.gift-1),0)
								ites.name = buttonnames+(cacheMyjsonSQL.gift);
								ites.transform.GetChild(0).GetComponent<Text>().text=addthings;
								ites.transform.GetChild(1).GetComponent<Text>().text=addnum.ToString();
								ites.GetComponent<itemsbuttonps>().image=im;
								ites.GetComponent<itemsbuttonps>().explanation=ex;
								ites.tag=UI[0].tag;
								if(cacheMyjsonSQL.gift>1){
									Button a=GameObject.Find(buttonnames+(cacheMyjsonSQL.gift-1)).GetComponent<Button>();
									Button b=ites.GetComponent<Button>();
									Navigation r,s;
									r=a.navigation;
									r.selectOnDown=b;
									a.navigation=r;
									s=b.navigation;
									s.selectOnUp=a;
									b.navigation=s;
								}
								break;
							}else if(UI[i].transform.GetChild(j).transform.GetChild(0).GetComponent<Text>().text==addthings){
								UI[i].transform.GetChild(j).transform.GetChild(1).GetComponent<Text>().text=(Int32.Parse(UI[i].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text)+addnum).ToString();
								break;
							}		
						}
					
					break;
				}

			}
			return cacheMyjsonSQL.gift;
		}
//-------------------------------------------------------------------------------------------------------------------------
	protected void Imagechange(){//----------------------上方角色選擇
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
					SetSelectedGameObjects("rl",q[j-1].gameObject.GetComponent<Button>(),q[j].gameObject.GetComponent<Button>());
				}
				j++;
			}
		}
		SetSelectedGameObjects("rl",q[j-1].gameObject.GetComponent<Button>(),q[0].gameObject.GetComponent<Button>());
		upcharactor=GameObject.FindGameObjectsWithTag("menuupcharactor");
		for(int i=0;i<upcharactor.Length;i++){
			string a=upcharactor[i].name.Substring(4);
			upcharactor[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text=SumVariable.charactorlv[Int32.Parse(a)][0].ToString();
			upcharactor[i].transform.GetChild(2).transform.GetChild(0).GetComponent<RectTransform>().localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(a)][2]/(float)SumVariable.charactorlv[Int32.Parse(a)][1]),0,0);
			upcharactor[i].transform.GetChild(3).transform.GetChild(0).GetComponent<RectTransform>().localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(a)][4]/(float)SumVariable.charactorlv[Int32.Parse(a)][3]),0,0);	
		}
	}

	protected void SetSelectedGameObjects(string check,Button a,Button b){//-----------------設定創建按鈕/選項後左右鍵指向其他按鈕/選項
		Navigation r,s;
		switch(check){
			case "rl":
				r=a.navigation;
				r.selectOnRight=b;
				a.navigation=r;
				s=b.navigation;
				s.selectOnLeft=a;
				b.navigation=s;
			break;
			case "du":
				r=a.navigation;
				r.selectOnDown=b;
				a.navigation=r;
				s=b.navigation;
				s.selectOnUp=a;
				b.navigation=s;
			break;
		}
	}
	public void arraygameobjectbutton(GameObject [] a,bool b,float c){//----------------------------關閉/開啟系列按鈕功能
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
	}
	public void teammodeselectchar(string s){
		
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
		setteam=s;
		StartCoroutine(delayteam());
		
	}
	IEnumerator delayteam(){
		yield return new WaitForSeconds(0.05f);
		mode=3;
		yield return null;
	}
	
	public void Itemsmode(){
		mode=4;
		itemUI=GameObject.FindGameObjectsWithTag("itemUI");
		mainUI=GameObject.FindGameObjectsWithTag("mainUI");
		friendsUI=GameObject.FindGameObjectsWithTag("friendsUI");
		statusUI.GetComponentInParent<CanvasGroup>().alpha=0;
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Item"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUILight") as Sprite;
				}
		}
		arraygameobjectbutton(Items,true,1);
		for(int i=0;i<Items.Length;i++){
			Items[i].GetComponent<Canvas>().sortingOrder=50;
			if(Items[i].name=="Item"){
				Items[i].GetComponent<Canvas>().sortingOrder=101;
			}
			if(Items[i].name=="ItemUI"){
				Items[i].GetComponent<Canvas>().sortingOrder=1;
			}
			if(Items[i].name=="itemUI"){
				Items[i].GetComponent<Canvas>().sortingOrder=102;
				loadselected(Items[i]);
			}
			if(Items[i].name=="friendsUI"){
				Items[i].GetComponent<Canvas>().sortingOrder=2;
			}
			if(Items[i].name=="mainUI"){
				Items[i].GetComponent<Canvas>().sortingOrder=2;
			}
		}

	}
	public void ItemsitemUI(){
		mode=4;
		for(int i=0;i<Items.Length;i++){
					if(Items[i].name=="friendsUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
					if(Items[i].name=="mainUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
					if(Items[i].name=="itemUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("itemitem1")!=null){
							SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),GameObject.Find("itemitem1").GetComponent<Button>());
						}
					}
				}	
		arraygameobjectbutton(itemUI,true,1);
		arraygameobjectbutton(mainUI,false,0);
		arraygameobjectbutton(friendsUI,false,0);
	}
	public void ItemsmainUI(){
		mode=4;
		for(int i=0;i<Items.Length;i++){
					if(Items[i].name=="friendsUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
					if(Items[i].name=="mainUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("mainitem1")!=null){
							SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),GameObject.Find("mainitem1").GetComponent<Button>());
						}
					}
					if(Items[i].name=="itemUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
				}
		arraygameobjectbutton(itemUI,false,0);
		arraygameobjectbutton(mainUI,true,1);
		arraygameobjectbutton(friendsUI,false,0);
	}
	public void ItemsfriendsUI(){
		mode=4;
		for(int i=0;i<Items.Length;i++){
					if(Items[i].name=="friendsUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("friendsitem1")!=null){
							SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),GameObject.Find("friendsitem1").GetComponent<Button>());
						}
					}
					if(Items[i].name=="mainUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
					if(Items[i].name=="itemUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
				}
		arraygameobjectbutton(itemUI,false,0);
		arraygameobjectbutton(mainUI,false,0);
		arraygameobjectbutton(friendsUI,true,1);
	}
	public void Skillsmode(){
		mode=5;
		AttackSkill=GameObject.FindGameObjectsWithTag("AttackSkill");
		HelpSkill=GameObject.FindGameObjectsWithTag("HelpSkill");
		CureSkill=GameObject.FindGameObjectsWithTag("CureSkill");
		statusUI.GetComponentInParent<CanvasGroup>().alpha=0;
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Skill"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/MapNameUILight") as Sprite;
				}
		}
		arraygameobjectbutton(Skills,true,1);
		for(int i=0;i<Skills.Length;i++){
			if(Skills[i].name=="Skill"){
				Skills[i].GetComponent<Canvas>().sortingOrder=101;
			}
			if(Skills[i].name=="skillUI"){
				Skills[i].GetComponent<Canvas>().sortingOrder=1;
			}
			if(Skills[i].name=="Skillimage"){
				Skills[i].GetComponent<Canvas>().sortingOrder=50;
			}
			if(Skills[i].name=="SkillEx"){
				Skills[i].GetComponent<Canvas>().sortingOrder=50;
			}
			if(Skills[i].name=="Point"){
				Skills[i].GetComponent<Canvas>().sortingOrder=50;
			}
			if(Skills[i].name=="AttackSkill"){
				Skills[i].GetComponent<Canvas>().sortingOrder=102;
				loadselected(Skills[i]);
			}
			if(Skills[i].name=="HelpSkill"){
				Skills[i].GetComponent<Canvas>().sortingOrder=2;
			}
			if(Skills[i].name=="CureSkill"){
				Skills[i].GetComponent<Canvas>().sortingOrder=2;
			}
		}
	}
	public void HelpSkillmode(){
		mode=5;
		for(int i=0;i<Skills.Length;i++){
					if(Skills[i].name=="AttackSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),Skills[i].GetComponent<Button>());
					}
					if(Skills[i].name=="HelpSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("HelpSkill1")!=null){
							SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),GameObject.Find("HelpSkill1").GetComponent<Button>());
						}
					}
					if(Skills[i].name=="CureSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),Skills[i].GetComponent<Button>());
					}
				}
		arraygameobjectbutton(AttackSkill,false,0);
		arraygameobjectbutton(HelpSkill,true,1);
		arraygameobjectbutton(CureSkill,false,0);
	}
	public void CureSkillmode(){
		mode=5;
		for(int i=0;i<Skills.Length;i++){
					if(Skills[i].name=="AttackSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),Skills[i].GetComponent<Button>());
					}
					if(Skills[i].name=="HelpSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),Skills[i].GetComponent<Button>());
						
					}
					if(Skills[i].name=="CureSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("CureSkill1")!=null){
							SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),GameObject.Find("CureSkill1").GetComponent<Button>());
						}
					}
				}
		arraygameobjectbutton(AttackSkill,false,0);
		arraygameobjectbutton(HelpSkill,false,0);
		arraygameobjectbutton(CureSkill,true,1);
	}
	public void AttackSkillmode(){
		mode=5;
		for(int i=0;i<Skills.Length;i++){
					if(Skills[i].name=="AttackSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("AttackSkill1")!=null){
							SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),GameObject.Find("AttackSkill1").GetComponent<Button>());
						}
					}
					if(Skills[i].name=="HelpSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),Skills[i].GetComponent<Button>());
					}
					if(Skills[i].name=="CureSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("du",Skills[i].GetComponent<Button>(),Skills[i].GetComponent<Button>());
					}
				}
		arraygameobjectbutton(AttackSkill,true,1);
		arraygameobjectbutton(HelpSkill,false,0);
		arraygameobjectbutton(CureSkill,false,0);
	}

	[System.Serializable]
	public struct MyjsonSQL
	{	
    	public int gift;
    	[System.Serializable]
    	public struct Mains
    	{
       	 	public string Name;
       	    public int number;
            public float schedule;
            public string image;
            public string explanation;
            public string friendimage;
            public string friendname;
        }
        public Mains [] main;
	}
}
