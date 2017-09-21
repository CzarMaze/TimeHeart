using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class cameralimit : MonoBehaviour {
	CanvasGroup wx;
	protected bool x,y;
	protected float xx, yy;
	GameObject player;
	Transform cameranow;
    float nx, ny;

	//bool windowmode;
	void Awake(){
		wx = GameObject.Find ("sendtoblack").GetComponent<CanvasGroup> ();
		player= GameObject.Find("Player");

	}
    void Start() {
        player.transform.position = SumVariable.nextad;
		Invoke("delayfuntion",0.5f);
		SumVariable.keyboardopen = true;
		x = true; y = true;
	}
	void delayfuntion(){
		StartCoroutine(Sumthing.notview (wx,1, 0, 0.0625,0.03f));
	}

	void Update()
	{		
		transform.position = new Vector3(xx, yy, -30);
		if (x)
		{
			xx = player.transform.position.x;

		}else
		{
			if (Math.Abs(player.transform.position.x) <= Math.Abs(xx))
			{
				xx = player.transform.position.x;
			}
		}
		if (y)
		{
			yy = player.transform.position.y;
		}else
		{
			if (Math.Abs(player.transform.position.y) <= Math.Abs(yy))
			{
				yy = player.transform.position.y;
			}
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "backgroundlimit" && (other.name== "limit_right"||other.name== "limit_left"))
        {
			x = false;
        }
        if (other.tag == "backgroundlimit" && (other.name == "limit_up" || other.name == "limit_down"))
        {
			y = false;
        }
    }
	void OnTriggerStay2D(Collider2D other){
		if(other.name == "limitup_right"){
			xx=(float)(xx-0.003);
		}
		if(other.name == "limitup_left"){
			xx=(float)(xx+0.003);
		}
		if(other.name == "limitup_up"){
			yy=(float)(yy-0.003);
		}
		if(other.name == "limitup_down"){
			yy=(float)(yy+0.003);
		}
	}
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "backgroundlimit" && (other.name == "limit_right" || other.name == "limit_left"))
        {
			x = true;
        }
        if (other.tag == "backgroundlimit" && (other.name == "limit_up" || other.name == "limit_down"))
        {
			y = true;
        }
	}
	public void LoadingGame(){
		if(PlayerPrefs.GetString("ScanSave")!=""){
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
	public void ExitGame(){
		Application.Quit();
	}
}
