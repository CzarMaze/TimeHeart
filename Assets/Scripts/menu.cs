using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {
	protected CanvasGroup backgroundUI;
	protected GameObject Icon,statusUI,YN,SYN,Player;
	protected GameObject [] leftlist,upcharactor,battleteam,Items,itemUI,mainUI,friendsUI,Skills,AttackSkill,HelpSkill,CureSkill,tasks,taskmains,tasksecs,Friendstip,Systemset,Systemvoise,SystemExit;
	protected EventSystem es;
	protected Text StatusName,LVStatus,HPStatus,MPStatus,EXPStatus,STRStatus,MDEFStatus,INTStatus,SPDStatus,DEFStatus,AGIStatus,ItemEx,SkillEx,FriendExtext;
	protected Image Itemimage,Skillimage,FriendImage;
	protected int mode=0,point=0;
	protected Sprite [] ass;
	protected RectTransform valueStatusHP,valueStatusMP,valueStatusEXP;
	public static MyjsonSQL itemSave,friendsSave,mainSave,CureSkillSave,HelpSkillSave,AttackSkillSave,tasksSave,FriendstipSave;
	protected bool loadingtime=false;

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
		//------------------------------------------------------------
		Friendstip=GameObject.FindGameObjectsWithTag("Friendstip");
		FriendImage=GameObject.Find("FriendImage").GetComponent<Image>();
		FriendExtext=GameObject.Find("FriendExtext").GetComponent<Text>();
		//------------------------------------------------------------
		Systemset=GameObject.FindGameObjectsWithTag("Systemset");
		SystemExit=GameObject.FindGameObjectsWithTag("SystemExit");
		Systemvoise=GameObject.FindGameObjectsWithTag("Systemvoise");
		//--------------------------------------------------------------
		tasks=GameObject.FindGameObjectsWithTag("tasks");
		taskmains=GameObject.FindGameObjectsWithTag("taskmains");
		tasksecs=GameObject.FindGameObjectsWithTag("tasksecs");

		Player=GameObject.Find("Player");
	}
	void Start () {
		if(PlayerPrefs.GetFloat("Music")!=0 || PlayerPrefs.GetFloat("Sound")!=0 || PlayerPrefs.GetFloat("ESound")!=0){
			SumVariable.Music=PlayerPrefs.GetFloat("Music");
			SumVariable.Sound=PlayerPrefs.GetFloat("Sound");
			SumVariable.ESound=PlayerPrefs.GetFloat("ESound");
		}
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
		CureSkillSave=cachearticleread(CureSkill,"cacheCureSkillSave","CureList","skllskill","CureSkill");
		HelpSkillSave=cachearticleread(HelpSkill,"cacheHelpSkillSave","HelpList","skllskill","HelpSkill");
		AttackSkillSave=cachearticleread(AttackSkill,"cacheAttackSkillSave","AttackList","skllskill","AttackSkill");
		FriendstipSave=cachearticleread(Friendstip,"cacheFriendstipSave","FriendList","Frienditemitem","ListFriend");//--------------------------
		tasksSave=taskstartread("cachetasksSave");
	}
	void Update () {
			if(GameObject.Find("MUSIC").GetComponent<AudioSource>().volume != SumVariable.Music||GameObject.Find("talkbox").GetComponent<AudioSource>().volume!=SumVariable.Sound||GameObject.Find("ESOUND").GetComponent<AudioSource>().volume!=SumVariable.ESound){
				for(int i=0;i<Systemvoise.Length;i++){//------------------------------------------事前載入音量設定
					if(Systemvoise[i].name=="Music"){
						GameObject.Find("MUSIC").GetComponent<AudioSource>().volume=SumVariable.Music;
						Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value=SumVariable.Music;
					}
					if(Systemvoise[i].name=="Sound"){
						GameObject.Find("talkbox").GetComponent<AudioSource>().volume=SumVariable.Sound;
						Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value=SumVariable.Sound;
					}
					if(Systemvoise[i].name=="ESound"){
						GameObject.Find("ESOUND").GetComponent<AudioSource>().volume=SumVariable.ESound;
						Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value=SumVariable.ESound;
					}
				}
			}
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
					for(int i=1;i<=3;i++){
						if(GameObject.Find("icon"+i)!=null){
							Destroy(GameObject.Find("icon"+i));
						}
					}
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
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==2){//------------------隊伍模式
				if(Input.GetKeyUp (KeyCode.Escape)){
					mode=0;
					for(int i=0;i<battleteam.Length;i++){
						battleteam[i].GetComponentInParent<CanvasGroup>().alpha=0;
					}
					statusUI.GetComponent<Image>().sprite=Resources.Load<Sprite>("chatboxpicture/statusImage"+1.ToString()) as Sprite;
					charactormenu(1.ToString());
					StartCoroutine(exitupchar(battleteam,0));
				}
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==3){//--------------------選擇隊友
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
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==4){//-------------物品
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
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==5){//----------------技能
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
						loadselected(GameObject.Find("icon1"));
					}
					if(es.currentSelectedGameObject!=null){
						switch(es.currentSelectedGameObject.gameObject.name){
							case "icon1":
								AttackSkillmode();
								lastpointcheck("AttackSkill",SumVariable.charactorlv[1][0]);
							break;
							case "icon2":
								HelpSkillmode();
								lastpointcheck("HelpSkill",SumVariable.charactorlv[2][0]);
							break;
							case "icon3":
								CureSkillmode();
								lastpointcheck("CureSkill",SumVariable.charactorlv[3][0]);
							break;
							default:
							break;
						}
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
						arraygameobjectbutton(upcharactor,false,1);
						StartCoroutine(exitupchar(Skills,0));
					}
				}
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==6){//----------------------------------任務
				if(Input.GetKeyUp (KeyCode.Escape)){
					mode=0;
					statusUI.GetComponentInParent<CanvasGroup>().alpha=1;
					for(int i=0;i<tasks.Length;i++){
							tasks[i].GetComponent<Canvas>().sortingOrder=0;
						}
					StartCoroutine(exitupchar(tasks,0));
				}
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==7){//-------------------------------友誼
					if(es.currentSelectedGameObject!=null){
						if(es.currentSelectedGameObject.GetComponent<itemsbuttonps>()!=null){	
							Byte[] imabytes=Convert.FromBase64String(es.currentSelectedGameObject.GetComponent<itemsbuttonps>().image);
							Texture2D outima;
							outima=new Texture2D(1,1);
							outima.LoadImage(imabytes);
							outima.filterMode=FilterMode.Point;
							Rect rect = new Rect(0, 0, outima.width, outima.height);
							FriendImage.sprite=Sprite.Create(outima,rect,new Vector2(),100f);
							FriendExtext.text=es.currentSelectedGameObject.GetComponent<itemsbuttonps>().explanation;
						}else{
							FriendImage.sprite=Resources.Load<Sprite>("Image/0") as Sprite;
							FriendExtext.text="";
						}
					}
					if(Input.GetKeyUp (KeyCode.Escape)){
							mode=0;
							statusUI.GetComponentInParent<CanvasGroup>().alpha=1;
							arraygameobjectbutton(Friendstip,false,0);
							for(int i=0;i<Friendstip.Length;i++){
								if(Friendstip[i].name=="FriendstipMain"){
									Friendstip[i].GetComponent<Canvas>().sortingOrder=0;
								}
							}
							StartCoroutine(exitupchar(Friendstip,0));
					}
			}else if(leftlist[0].GetComponent<Button>().interactable==false && mode==8){//---------------------系統
				if(Input.GetKeyUp(KeyCode.Escape)){
					mode=0;
					statusUI.GetComponentInParent<CanvasGroup>().alpha=1;
					for(int i=0;i<Systemset.Length;i++){
						if(Systemset[i].GetComponent<Canvas>()!=null){
							Systemset[i].GetComponent<Canvas>().sortingOrder=0;
						}
					}
					for(int i=0;i<SystemExit.Length;i++){
						if(SystemExit[i].GetComponent<Canvas>()!=null){
							SystemExit[i].GetComponent<Canvas>().sortingOrder=51;
						}
					}
					arraygameobjectbutton(SystemExit,false,0);
					arraygameobjectbutton(Systemvoise,false,0);
					StartCoroutine(exitupchar(Systemset,0));
				}
				for(int i=0;i<Systemvoise.Length;i++){
					if(Systemvoise[i].name=="Music"){
						GameObject.Find("MUSIC").GetComponent<AudioSource>().volume=Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value;
						SumVariable.Music=Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value;
					}
					if(Systemvoise[i].name=="Sound"){
						GameObject.Find("talkbox").GetComponent<AudioSource>().volume=Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value;
						SumVariable.Sound=Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value;
					}
					if(Systemvoise[i].name=="ESound"){
						GameObject.Find("ESOUND").GetComponent<AudioSource>().volume=Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value;
						SumVariable.ESound=Systemvoise[i].transform.GetChild(1).GetComponent<Slider>().value;
					}
				}
				
			}
		}
	private void lastpointcheck(string list,int lv){
		GameObject YN=GameObject.Find("SkillSelect");
		Text PointNumber=GameObject.Find("PointNumber").GetComponent<Text>();
		int tmplv=1;
		YN.GetComponent<buttonlevelup>().cache=lv;
		while(GameObject.Find(list+tmplv)!=null){
				YN.GetComponent<buttonlevelup>().cache=YN.GetComponent<buttonlevelup>().cache-Int32.Parse(GameObject.Find(list+tmplv).transform.GetChild(1).GetComponent<Text>().text);
				tmplv++;
			}
		if(YN.GetComponent<buttonlevelup>().cache!=0){
			YN.GetComponent<buttonlevelup>().point=YN.GetComponent<buttonlevelup>().cache;
			PointNumber.text=YN.GetComponent<buttonlevelup>().cache.ToString();
		}else{
			PointNumber.text="0";
			point=0;
		}
	}

	private void OnDisable(){
		PlayerPrefs.SetFloat("Music",SumVariable.Music);
		PlayerPrefs.SetFloat("Sound",SumVariable.Sound);
		PlayerPrefs.SetFloat("ESound",SumVariable.ESound);
		if(!loadingtime){
		cachearticlereadOnDisable(itemSave,itemUI,"cacheitemSave","itemList");
		cachearticlereadOnDisable(friendsSave,friendsUI,"cachefriendsSave","friendsList");
		cachearticlereadOnDisable(mainSave,mainUI,"cachemainSave","mainList");
		cachearticlereadOnDisable(CureSkillSave,CureSkill,"cacheCureSkillSave","CureList");
		cachearticlereadOnDisable(HelpSkillSave,HelpSkill,"cacheHelpSkillSave","HelpList");
		cachearticlereadOnDisable(AttackSkillSave,AttackSkill,"cacheAttackSkillSave","AttackList");
		cachearticlereadOnDisable(FriendstipSave,Friendstip,"cacheFriendstipSave","FriendList");
		tasksOnDisable(tasksSave,"cachetasksSave");
		}
		PlayerPrefs.Save();
		Resources.UnloadUnusedAssets();
    }
	private void OnApplicationQuit(){
		PlayerPrefs.DeleteKey("cacheitemSave");
		PlayerPrefs.DeleteKey("cachefriendsSave");
		PlayerPrefs.DeleteKey("cachemainSave");
		PlayerPrefs.DeleteKey("cacheCureSkillSave");
		PlayerPrefs.DeleteKey("cacheHelpSkillSave");
		PlayerPrefs.DeleteKey("cacheAttackSkillSave");
		PlayerPrefs.DeleteKey("cacheFriendstipSave");
		PlayerPrefs.DeleteKey("cachetasksSave");
	}
	




	public void SaveingGame(){
        PlayerPrefs.SetString("ScanSave", SceneManager.GetActiveScene().path.Split(new string[] {"scan/"},StringSplitOptions.RemoveEmptyEntries)[1].Split('.')[0]);
		PlayerPrefs.SetString("charactorSave",SumVariable.charactor);
		PlayerPrefsX.SetBoolArray("banSave",SumVariable.teamban);
		PlayerPrefsX.SetIntArray("battleteamSave",SumVariable.battleteam);
		PlayerPrefs.SetInt("keySave",SumVariable.key);
		PlayerPrefsX.SetVector3("nextadSave",Player.transform.position);
		PlayerPrefsX.SetIntArray("charactorlv1",SumVariable.charactorlv[1]);
		PlayerPrefsX.SetIntArray("charactorlv2",SumVariable.charactorlv[2]);
		PlayerPrefsX.SetIntArray("charactorlv3",SumVariable.charactorlv[3]);
		PlayerPrefs.SetString("address",GameObject.Find("MapName").transform.GetChild(0).GetComponent<Text>().text);
		PlayerPrefs.SetString("Day",DateTime.Now.ToShortDateString().ToString());
		PlayerPrefs.SetString("Time",DateTime.Now.ToLongTimeString().ToString());
		cachearticlereadOnDisable(itemSave,itemUI,"itemSave","itemList");//道具
		cachearticlereadOnDisable(friendsSave,friendsUI,"friendsSave","friendsList");//禮物
		cachearticlereadOnDisable(mainSave,mainUI,"mainSave","mainList");//重要
		cachearticlereadOnDisable(CureSkillSave,CureSkill,"CureSkillSave","CureList");//凱斯伏
		cachearticlereadOnDisable(HelpSkillSave,HelpSkill,"HelpSkillSave","HelpList");//薩雷諾
		cachearticlereadOnDisable(AttackSkillSave,AttackSkill,"AttackSkillSave","AttackList");//艾憐娜
		cachearticlereadOnDisable(FriendstipSave,Friendstip,"FriendstipSave","FriendList");//友誼
		tasksOnDisable(tasksSave,"tasksSave");//任務
		Debug.Log(PlayerPrefs.GetString("itemSave"));
		PlayerPrefs.Save();
		Savedimage();
	}
	public void loadingGame(){
		if(PlayerPrefs.GetString("ScanSave")!=""){
			loadingtime=true;
			SumVariable.nextlevel=PlayerPrefs.GetString("ScanSave");
			SumVariable.nextad=PlayerPrefsX.GetVector3("nextadSave");
			SumVariable.nextdt="down";
			SumVariable.teamban=PlayerPrefsX.GetBoolArray("banSave");
			SumVariable.battleteam=PlayerPrefsX.GetIntArray("battleteamSave");
			SumVariable.charactor=PlayerPrefs.GetString("charactorSave");
			SumVariable.key=PlayerPrefs.GetInt("keySave");
			SumVariable.charactorlv[1]=PlayerPrefsX.GetIntArray("charactorlv1");
			SumVariable.charactorlv[2]=PlayerPrefsX.GetIntArray("charactorlv2");
			SumVariable.charactorlv[3]=PlayerPrefsX.GetIntArray("charactorlv3");
			PlayerPrefs.SetString("cacheitemSave",PlayerPrefs.GetString("itemSave"));
			PlayerPrefs.SetString("cachefriendsSave",PlayerPrefs.GetString("friendsSave"));
			PlayerPrefs.SetString("cachemainSave",PlayerPrefs.GetString("mainSave"));
			PlayerPrefs.SetString("cacheCureSkillSave",PlayerPrefs.GetString("CureSkillSave"));
			PlayerPrefs.SetString("cacheHelpSkillSave",PlayerPrefs.GetString("HelpSkillSave"));
			PlayerPrefs.SetString("cacheAttackSkill",PlayerPrefs.GetString("AttackSkillSave"));
			PlayerPrefs.SetString("cacheFriendstipSave",PlayerPrefs.GetString("FriendstipSave"));
			PlayerPrefs.SetString("cachetasksSave",PlayerPrefs.GetString("tasksSave"));
			PlayerPrefs.Save();
			SceneManager.LoadScene ("scan/loading1");
		}
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
							ites[j]=Instantiate(Resources.Load ("prefabs/"+prefabsname),new Vector3(0,2400.3300f-137.5072f*j,0),Quaternion.Euler(0,0,0)) as GameObject;
							ites[j].transform.SetParent (UI[i].gameObject.transform,false);//526.5784f
							ites[j].name = buttonnames + (j + 1);//-----itemitem----變數3
                            if (prefabsname != "Frienditemitem")
                            {
                                ites[j].transform.GetChild(1).GetComponent<Text>().text = cacheMyjsonSQL.main[j].number.ToString();
                            }
							ites[j].transform.GetChild(0).GetComponent<Text>().text=cacheMyjsonSQL.main[j].Name;
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
                            if (cachesave != "cacheFriendstipSave" && cachesave != "FriendstipSave")
                             {
                                cacheMyjsonSQL.main[q].number = Int32.Parse(UI[i].transform.GetChild(q).transform.GetChild(1).GetComponent<Text>().text);
								Debug.Log(Int32.Parse(UI[i].transform.GetChild(q).transform.GetChild(1).GetComponent<Text>().text));
                            }
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
								GameObject ites=Instantiate(Resources.Load("prefabs/"+prefabsname), new Vector3(0,2400.3300f-137.5072f*(cacheMyjsonSQL.gift-1),0),Quaternion.Euler(0,0,0)) as GameObject;
								ites.transform.SetParent (UI[i].gameObject.transform,false);//new Vector3(0.0004882813f,526.5784f-137.5072f*(cacheMyjsonSQL.gift-1),0)
								ites.name = buttonnames+(cacheMyjsonSQL.gift);
								ites.transform.GetChild(0).GetComponent<Text>().text=addthings;
								if(prefabsname!="Frienditemitem"){
									ites.transform.GetChild(1).GetComponent<Text>().text=addnum.ToString();
								}
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
							}else if(UI[i].transform.GetChild(j).transform.GetChild(0).GetComponent<Text>().text==addthings ){
								if(prefabsname != "skllskill" && prefabsname != "Frienditemitem")
                                  {
									UI[i].transform.GetChild(j).transform.GetChild(1).GetComponent<Text>().text=(Int32.Parse(UI[i].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text)+addnum).ToString();
								}
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
				leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("Image/0") as Sprite;
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
				q[j]=Instantiate(Resources.Load ("prefabs/team/icon" + SumVariable.team[i]),new Vector3(-850f,(-200f-270f*j),0),Quaternion.Euler(0,0,180)) as GameObject;
				q[j].transform.SetParent (Icon.gameObject.transform,false);
				q[j].name = "icon"+SumVariable.team[i];
				if(j>0){
					SetSelectedGameObjects("du",q[j-1].gameObject.GetComponent<Button>(),q[j].gameObject.GetComponent<Button>());
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
			upcharactor[i].transform.GetChild(4).transform.GetChild(0).GetComponent<RectTransform>().localPosition=new Vector3((500-500*(float)SumVariable.charactorlv[Int32.Parse(a)][6]/(float)SumVariable.charactorlv[Int32.Parse(a)][5]),0,0);
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
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/ListLight") as Sprite;
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
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/ListLight") as Sprite;
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
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/ListLight") as Sprite;
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
				Items[i].GetComponent<Canvas>().sortingOrder=4;
			}
			if(Items[i].name=="mainUI"){
				Items[i].GetComponent<Canvas>().sortingOrder=4;
			}
		}

	}
	public void ItemsitemUI(){
		mode=4;
		for(int i=0;i<Items.Length;i++){
					if(Items[i].name=="friendsUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=4;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
					if(Items[i].name=="mainUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=4;
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
						Items[i].GetComponent<Canvas>().sortingOrder=4;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
					if(Items[i].name=="mainUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("mainitem1")!=null){
							SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),GameObject.Find("mainitem1").GetComponent<Button>());
						}
					}
					if(Items[i].name=="itemUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=4;
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
						Items[i].GetComponent<Canvas>().sortingOrder=4;
						SetSelectedGameObjects("du",Items[i].GetComponent<Button>(),Items[i].GetComponent<Button>());
					}
					if(Items[i].name=="itemUI"){
						Items[i].GetComponent<Canvas>().sortingOrder=4;
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
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/ListLight") as Sprite;
				}
		}
		arraygameobjectbutton(Skills,true,1);
		arraygameobjectbutton(upcharactor,true,1);
		loadselected(upcharactor[0]);
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
						SetSelectedGameObjects("rl",GameObject.Find("icon1").GetComponent<Button>(),GameObject.Find("icon1").GetComponent<Button>());
					}
					if(Skills[i].name=="HelpSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("HelpSkill1")!=null){
							SetSelectedGameObjects("rl",es.currentSelectedGameObject.gameObject.GetComponent<Button>(),GameObject.Find("HelpSkill1").GetComponent<Button>());
						}
					}
					if(Skills[i].name=="CureSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("rl",GameObject.Find("icon3").GetComponent<Button>(),GameObject.Find("icon3").GetComponent<Button>());
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
						SetSelectedGameObjects("rl",GameObject.Find("icon1").GetComponent<Button>(),GameObject.Find("icon1").GetComponent<Button>());
					}
					if(Skills[i].name=="HelpSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("rl",GameObject.Find("icon2").GetComponent<Button>(),GameObject.Find("icon2").GetComponent<Button>());
						
					}
					if(Skills[i].name=="CureSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=102;
						if(GameObject.Find("CureSkill1")!=null){
							SetSelectedGameObjects("rl",es.currentSelectedGameObject.gameObject.GetComponent<Button>(),GameObject.Find("CureSkill1").GetComponent<Button>());
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
							SetSelectedGameObjects("rl",es.currentSelectedGameObject.gameObject.GetComponent<Button>(),GameObject.Find("AttackSkill1").GetComponent<Button>());
						}
					}
					if(Skills[i].name=="HelpSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("rl",GameObject.Find("icon2").GetComponent<Button>(),GameObject.Find("icon2").GetComponent<Button>());
					}
					if(Skills[i].name=="CureSkill"){
						Skills[i].GetComponent<Canvas>().sortingOrder=2;
						SetSelectedGameObjects("rl",GameObject.Find("icon3").GetComponent<Button>(),GameObject.Find("icon3").GetComponent<Button>());
					}
				}
		arraygameobjectbutton(AttackSkill,true,1);
		arraygameobjectbutton(HelpSkill,false,0);
		arraygameobjectbutton(CureSkill,false,0);
	}
	public void friendstipmode(){
		mode=7;
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Friends"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/ListLight") as Sprite;
				}
		}
		statusUI.GetComponentInParent<CanvasGroup>().alpha=0;
		arraygameobjectbutton(Friendstip,true,1);
		Friendstip=GameObject.FindGameObjectsWithTag("Friendstip");
		for(int i=0;i<Friendstip.Length;i++){
			if(Friendstip[i].name.Substring(0,5)=="ListF"){
				loadselected(Friendstip[i]);
			}
		}
	
	}
	public void systemsetting(){
		mode=8;
		for(int i=0;i<leftlist.Length;i++){
			leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="System"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/ListLight") as Sprite;
				}
		}
		arraygameobjectbutton(Systemset,true,1);
		statusUI.GetComponentInParent<CanvasGroup>().alpha=0;
		for(int i=0;i<Systemset.Length;i++){
			if(Systemset[i].GetComponent<Canvas>()!=null){
				Systemset[i].GetComponent<Canvas>().sortingOrder=50;
			}
			if(Systemset[i].name=="Save"){
				Systemset[i].GetComponent<Canvas>().sortingOrder=51;
				loadselected(Systemset[i]);
			}
		}
		arraygameobjectbutton(SystemExit,false,0);
		arraygameobjectbutton(Systemvoise,false,0);
		GameObject.Find("SaveMaskUI").GetComponent<CanvasGroup>().alpha=1;
		GameObject.Find("SaveMaskUI").GetComponent<CanvasGroup>().interactable=true;
		if(PlayerPrefs.GetString("Day")!=""){
			Savedimage();
		}
	}
	private void Savedimage(){//-------------------------------------------------------------------------------
		GameObject.Find("SaveDaytext").GetComponent<Text>().text=PlayerPrefs.GetString("Day");
		GameObject.Find("SaveTimetext").GetComponent<Text>().text=PlayerPrefs.GetString("Time");
		GameObject.Find("SaveMaptext").GetComponent<Text>().text=PlayerPrefs.GetString("address");
		for(int i=1;i<=3;i++){
			GameObject.Find("SaveTeam"+i).transform.GetChild(0).GetComponent<Image>().sprite=ass[PlayerPrefsX.GetIntArray("battleteamSave")[i-1]];
		}
		for(int i=1;i<=3;i++){
			Saveicons(i);
		}
	}
	private void Saveicons(int i){
		GameObject tmpsaveicon=GameObject.Find("Saveicon"+i);//i=1
		int [] tmparray=PlayerPrefsX.GetIntArray("charactorlv"+i);
		tmpsaveicon.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text=tmparray[0].ToString();
		tmpsaveicon.transform.GetChild(2).transform.GetChild(0).GetComponent<RectTransform>().localPosition=new Vector3((500-500*(float)tmparray[2]/(float)tmparray[1]),0,0);
		tmpsaveicon.transform.GetChild(3).transform.GetChild(0).GetComponent<RectTransform>().localPosition=new Vector3((500-500*(float)tmparray[4]/(float)tmparray[3]),0,0);
		tmpsaveicon.transform.GetChild(4).transform.GetChild(0).GetComponent<RectTransform>().localPosition=new Vector3((500-500*(float)tmparray[6]/(float)tmparray[5]),0,0);
	}
	public void systemquit(){
		GameObject.Find("SaveMaskUI").GetComponent<CanvasGroup>().alpha=0;
		GameObject.Find("SaveMaskUI").GetComponent<CanvasGroup>().interactable=false;
		arraygameobjectbutton(SystemExit,true,1);
		arraygameobjectbutton(Systemvoise,false,0);
		for(int i=0;i<Systemset.Length;i++){
			if(Systemset[i].GetComponent<Canvas>()!=null){
				Systemset[i].GetComponent<Canvas>().sortingOrder=49;
			}
			if(Systemset[i].name=="Exit"){
				Systemset[i].GetComponent<Canvas>().sortingOrder=51;
			}
		}
		for(int i=0;i<SystemExit.Length;i++){
			if(SystemExit[i].GetComponent<Canvas>()!=null){
				SystemExit[i].GetComponent<Canvas>().sortingOrder=51;
			}
		}
		loadselected(GameObject.Find("ExitNo"));

	}
	public void systemquitcheck(bool check){
		if(check){
			Application.Quit();
		}else{
			GameObject.Find("SaveMaskUI").GetComponent<CanvasGroup>().alpha=1;
			GameObject.Find("SaveMaskUI").GetComponent<CanvasGroup>().interactable=true;
			arraygameobjectbutton(SystemExit,false,0);
			for(int i=0;i<Systemset.Length;i++){
				Systemset[i].GetComponent<Canvas>().sortingOrder=49;
				if(Systemset[i].name=="Save"){
					loadselected(Systemset[i]);
					Systemset[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/ListDark") as Sprite;
				}
			}
			for(int i=0;i<SystemExit.Length;i++){
			if(SystemExit[i].GetComponent<Canvas>()!=null){
				SystemExit[i].GetComponent<Canvas>().sortingOrder=49;
			}
		}
		}
	}
	public void systemvoise(){
		GameObject.Find("SaveMaskUI").GetComponent<CanvasGroup>().alpha=0;
		GameObject.Find("SaveMaskUI").GetComponent<CanvasGroup>().interactable=false;
		arraygameobjectbutton(Systemvoise,true,1);
		arraygameobjectbutton(SystemExit,false,0);
		for(int i=0;i<Systemset.Length;i++){
			if(Systemset[i].GetComponent<Canvas>()!=null){
				Systemset[i].GetComponent<Canvas>().sortingOrder=49;
			}
			if(Systemset[i].name=="System" && Systemset[i].GetComponent<Canvas>()!=null){
				Systemset[i].GetComponent<Canvas>().sortingOrder=51;
			}
		}
		for(int i=0;i<SystemExit.Length;i++){
			if(SystemExit[i].GetComponent<Canvas>()!=null){
				SystemExit[i].GetComponent<Canvas>().sortingOrder=49;
			}
		}
	}
	public void taskmode(){
		mode=6;
		statusUI.GetComponentInParent<CanvasGroup>().alpha=0;
		for(int i=0;i<leftlist.Length;i++){
				leftlist[i].GetComponent<Button> ().interactable = false;
				if(leftlist[i].name=="Task"){
					leftlist[i].GetComponent<Image>().sprite= Resources.Load<Sprite>("chatboxpicture/ListLight") as Sprite;
				}
		}
		arraygameobjectbutton(tasks,true,1);
		arraygameobjectbutton(taskmains,true,1);
		arraygameobjectbutton(tasksecs,false,0);
		
		for(int i=0;i<tasks.Length;i++){
			if(tasks[i].name=="TaskUI"){
				tasks[i].GetComponent<Canvas>().sortingOrder=1;
			}
			if(tasks[i].name=="TaskMain"){
				tasks[i].GetComponent<Canvas>().sortingOrder=50;
				loadselected(tasks[i]);
			}
			if(tasks[i].name=="TaskSecondary"){
				tasks[i].GetComponent<Canvas>().sortingOrder=4;
			}
		}
	}
	public void taskmainmode(){
		for(int i=0;i<tasks.Length;i++){
			if(tasks[i].name=="TaskMain"){
				tasks[i].GetComponent<Canvas>().sortingOrder=50;
			}
			if(tasks[i].name=="TaskSecondary"){
				tasks[i].GetComponent<Canvas>().sortingOrder=4;
			}
		}
		arraygameobjectbutton(taskmains,true,1);
		arraygameobjectbutton(tasksecs,false,0);
	}
	public void tasksecmode(){
		for(int i=0;i<tasks.Length;i++){
			if(tasks[i].name=="TaskMain"){
				tasks[i].GetComponent<Canvas>().sortingOrder=4;
			}
			if(tasks[i].name=="TaskSecondary"){
				tasks[i].GetComponent<Canvas>().sortingOrder=50;
			}
		}
		arraygameobjectbutton(taskmains,false,0);
		arraygameobjectbutton(tasksecs,true,1);
	}
	//--------------------------------------------------------------
	//搜索
	public static bool taskexist(string list,string searchnumber){
		string listname="";
		switch(list){
			case "main":
			listname="TaskMainName";
			break;
			case "second":
			listname="TaskSecondaryName";
			break;
		}
		for(int i=1;i<9;i++){
			if(GameObject.Find(listname+i).GetComponent<tasknumbercachesave>().number==searchnumber){
				return true;
			}
		}
        return false;
	}
	//增加
	public static void taskwords(string list,string searchnumber,string maintest){
		string listname="";
		switch(list){
			case "main":
			listname="TaskMainName";
			break;
			case "second":
			listname="TaskSecondaryName";
			break;
		}
		for(int i=1;1<9;i++){
			if(GameObject.Find(listname+i).GetComponent<tasknumbercachesave>().number==""){
				GameObject.Find(listname+i).GetComponent<tasknumbercachesave>().number=searchnumber;
				GameObject.Find(listname+i).GetComponent<Text>().text=maintest;
                break;
			}
		}
	}
	//減少
	public static void deltask(string list,string number){
		string listname="";
		switch(list){
			case "main":
			listname="TaskMainName";
			break;
			case "second":
			listname="TaskSecondaryName";
			break;
		}
		for(int i=1;1<9;i++){
			if(GameObject.Find(listname+i).GetComponent<tasknumbercachesave>().number==number){
				GameObject.Find(listname+i).GetComponent<tasknumbercachesave>().number="";
				GameObject.Find(listname+i).GetComponent<Text>().text="";
				break;
			}
		}
	}
	//儲存
	void tasksOnDisable(MyjsonSQL cacheMyjsonSQL,string cachesave){
			cacheMyjsonSQL.main=new MyjsonSQL.Mains[17];
				for(int i=0;i<taskmains.Length;i++){
					if(taskmains[i].name.Length>=12){
						if(taskmains[i].name.Substring(0,12)=="TaskMainName"){		
            				if (taskmains[i].GetComponent<tasknumbercachesave>().number != "")
           					 {
               					 cacheMyjsonSQL.main[Int32.Parse(taskmains[i].name.Substring(12))].number = Int32.Parse(taskmains[i].GetComponent<tasknumbercachesave>().number);
               					 cacheMyjsonSQL.main[Int32.Parse(taskmains[i].name.Substring(12))].Name = taskmains[i].GetComponent<Text>().text;
           					 }else{
               					 cacheMyjsonSQL.main[Int32.Parse(taskmains[i].name.Substring(12))].number = 0;
                				 cacheMyjsonSQL.main[Int32.Parse(taskmains[i].name.Substring(12))].Name = "";
            				}
						}
					}
			}
            for(int i=0;i<tasksecs.Length;i++){
			  if(taskmains[i].name.Length>=17){
             	 if (tasksecs[i].name.Substring(0,17)=="TaskSecondaryName"){
			 		 if(tasksecs[i].GetComponent<tasknumbercachesave>().number != "") { 
                		cacheMyjsonSQL.main[Int32.Parse(tasksecs[i].name.Substring(17))+8].number = Int32.Parse(tasksecs[i].GetComponent<tasknumbercachesave>().number);
                		cacheMyjsonSQL.main[Int32.Parse(tasksecs[i].name.Substring(17))+8].Name = tasksecs[i].GetComponent<Text>().text;
              		}else{
                		cacheMyjsonSQL.main[Int32.Parse(tasksecs[i].name.Substring(17))+8].number = 0;
               			cacheMyjsonSQL.main[Int32.Parse(tasksecs[i].name.Substring(17))+8].Name = "";
             		}
				  }
			  }
        }
		var cacheitemSave=JsonUtility.ToJson(cacheMyjsonSQL);
		PlayerPrefs.SetString(cachesave,cacheitemSave);
	}

	//讀取
	MyjsonSQL taskstartread(string cachesave){
		MyjsonSQL cacheMyjsonSQL;
		if(PlayerPrefs.GetString(cachesave)!=""){//--cacheitemSave--變數1
			var cacheitemSave=PlayerPrefs.GetString(cachesave);//->-cacheitemSave--變數1
			cacheMyjsonSQL=JsonUtility.FromJson<MyjsonSQL>(cacheitemSave);//--------itemSave 變數0
			for(int i=1;i<=8;i++){
                if (cacheMyjsonSQL.main[i].number != 0)
                {
                    GameObject.Find("TaskMainName" + i).GetComponent<tasknumbercachesave>().number = cacheMyjsonSQL.main[i].number.ToString();
                    GameObject.Find("TaskMainName" + i).GetComponent<Text>().text = cacheMyjsonSQL.main[i].Name;
                }else{
                    GameObject.Find("TaskMainName" + i).GetComponent<tasknumbercachesave>().number = "";
                    GameObject.Find("TaskMainName" + i).GetComponent<Text>().text = "";
                }
			}
			for(int i=1;i<=8;i++){
                if (cacheMyjsonSQL.main[i].number != 0) { 
                    GameObject.Find("TaskSecondaryName"+i).GetComponent<tasknumbercachesave>().number=cacheMyjsonSQL.main[(8+i)].number.ToString();
			    	GameObject.Find("TaskSecondaryName"+i).GetComponent<Text>().text=cacheMyjsonSQL.main[(8+i)].Name;
                }else{
                    GameObject.Find("TaskSecondaryName" + i).GetComponent<tasknumbercachesave>().number = "";
                    GameObject.Find("TaskSecondaryName" + i).GetComponent<Text>().text = "";
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
	//--------------------------------------------------------------
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
