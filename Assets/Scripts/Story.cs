using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

public abstract class Story : MonoBehaviour
{
    node Z = new node();
    //*
    node Z1 = new node();
    //串列連結
    node Z2 = new node();
    //*
    protected IDbConnection dbcon;
    //*
    protected IDbCommand dbcmd;
    //資料庫讀取
    protected IDataReader Read;
    //*
	protected CanvasGroup box;
    protected Text n;
    protected Text s;
    protected GameObject L, M, R;
    protected String na = null,say1 = null,say2 = null,iml = null,imm = null,imr = null,nowtk = null,muc = null;
    protected GameObject buttons,buttonText1,buttonText2 =null;
    protected Button button1,button2;
    protected int road;
	protected abstract void otherthing();
	protected AudioClip notalkmusic=null;
	protected AudioSource MUSIC;
	protected AudioSource talkbox;

    //---------------------------------------------------------------------------------
    protected void Start()
    {
		box = GameObject.Find("talkbox").GetComponent<CanvasGroup> ();
        n = GameObject.Find("name").GetComponent<Text>();
        s = GameObject.Find("Story").GetComponent<Text>();
        L = GameObject.Find("LImage");
        M = GameObject.Find("MImage");
        R = GameObject.Find("RImage");
        buttons = GameObject.Find("buttons");
        buttonText1=GameObject.Find("Text1");
        buttonText2=GameObject.Find("Text2");
        button1 = GameObject.Find("Button1").GetComponent<Button>();
        button2 = GameObject.Find("Button2").GetComponent<Button>();
		talkbox = GameObject.Find ("talkbox").GetComponent<AudioSource> ();
        MUSIC = GameObject.Find("MUSIC").GetComponent<AudioSource>();
        button1.onClick.AddListener(OnClick1);
		button2.onClick.AddListener(OnClick2);
        L.GetComponent<Image>().sprite = Resources.Load<Sprite>("0") as Sprite;
        M.GetComponent<Image>().sprite = Resources.Load<Sprite>("0") as Sprite;
        R.GetComponent<Image>().sprite = Resources.Load<Sprite>("0") as Sprite;
    }

    protected void open(string a,string name)
    {
        bool x = true;
        road = 1;
        string cons = "URI=file:" + Application.dataPath + "/Plugins/"+a+".sqlite";
        dbcon = (IDbConnection)new SqliteConnection(cons);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string sql = "SELECT name,say1,say2,imageL,imageM,imageR,nowtalk,music FROM " + name; //人物名稱載入
        dbcmd.CommandText = sql;
        Read = dbcmd.ExecuteReader();

        while (Read.Read())
        {
            na = say1 = say2 = iml = imm = imr = nowtk = muc = "";
            if (!Read.IsDBNull(0))
            {
                na = Read.GetString(0);
            }
            if (!Read.IsDBNull(1))
            {
                say1 = Read.GetString(1);
            }//------------讀取資料庫
            if (!Read.IsDBNull(2))
            {
                say2 = Read.GetString(2);
            }
            if (!Read.IsDBNull(3))
            {
                iml = Read.GetString(3);
            }
            if (!Read.IsDBNull(4))
            {
                imm = Read.GetString(4);
            }
            if (!Read.IsDBNull(5))
            {
                imr = Read.GetString(5);
            }
            if (!Read.IsDBNull(6))
            {
                nowtk = Read.GetString(6);
            }
            if (!Read.IsDBNull(7))
            {
                muc = Read.GetString(7);
            }
            Z1.add(na, say1, say2, iml, imm, imr, nowtk, muc);
            Z2 = Z1;
            Z1 = new node();
            Z2.setn(Z1);
            if (x)
            {
                Z = Z2;
                x = false;
            }
        }
        Read.Close();
        Read = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }
    //---------------------------------------------------------------------------------
	protected IEnumerator meet(string a,string s)
	{
		yield return new WaitForSeconds(0.01f);
		if (Input.GetKeyDown(KeyCode.Space)||Input.GetKey(KeyCode.LeftControl))
		{
			StopAllCoroutines();
			open(a,s);
			SumVariable.keyboardopen = false;
			StartCoroutine(Sumthing.view(box,0, 1, 0.0625,0.005f));
			StartCoroutine(word());
			yield return null;
		}
		StartCoroutine(meet(a,s));
		yield return null;
	}

	//----------------------------------------------------
    
    //讀取圖片/圖片動畫效果
    protected void ploading(string s1, string s2, string s3)
    {
		L.GetComponent<Image>().color = new Color32(123, 123, 123, 255);
		M.GetComponent<Image>().color = new Color32(123, 123, 123, 255);
		R.GetComponent<Image>().color = new Color32(123, 123, 123, 255);
        if (Z.getnowtalk() == "L")
        {
			L.GetComponent<Image>().color = Color.white;
        }
        else if (Z.getnowtalk() == "M")
        {
			M.GetComponent<Image>().color = Color.white;
        }
        else if (Z.getnowtalk() == "R")
        {
			R.GetComponent<Image>().color = Color.white;
        }
		lmralpha(L,s1);
        lmralpha(R,s3);
        lmralpha(M,s2);
    }
	protected void lmralpha(GameObject lmr,string s1)
	{
		if (lmr.GetComponent<Image>().sprite != Resources.Load<Sprite>(s1) as Sprite)
		{
			lmr.GetComponent<Image>().sprite = Resources.Load<Sprite>(s1) as Sprite;
			StartCoroutine(Sumthing.view (lmr.GetComponent<CanvasGroup>() ,0, 1, 0.25,0.1f));
		}
	}
    //---------------------------------------------------------------------------------
	protected void soundmuc(AudioSource a,string s){
        if (s != "")
        {
            a.clip = Resources.Load<AudioClip>("music/"+s) as AudioClip;
			a.Play();
        }
    }
    //---------------------------------------------------------------------------------
    protected void OnClick1(){
        road = 1;
        button1.interactable = false;
        button2.interactable = false;
    }
    protected void OnClick2(){
        road = 2;
        button1.interactable = false;
        button2.interactable = false;
    }
    protected IEnumerator word()
    {
		soundmuc(talkbox,Z.getmusic());
        ploading(Z.getim("l"), Z.getim("m"), Z.getim("r"));
        n.text = Z.getname();
        s.text = "";
        if (Z.getsay(road).Substring(0, 1) == "(")
        {
            buttons.GetComponent<CanvasGroup>().alpha = 1;
            buttonText1.GetComponent<Text>().text = Z.getsay(1);
            buttonText2.GetComponent<Text>().text = Z.getsay(2);
            button1.interactable = true;
            button2.interactable = true;
        }
        else
        {
            for (int i = 0; i < Z.getsay(road).Length; i++)
            {
                s.text += Z.getsay(road).Substring(i, 1);
                yield return new WaitForSeconds(0.1f);//文字顯示速度
                if(Input.GetKeyDown(KeyCode.Space)||Input.GetKey(KeyCode.LeftControl)){
                    s.text=Z.getsay(road);
                    break;
                }
            }
        }
        //-------------------------------------------------------
        while (true)
        {//延遲判斷
            yield return new WaitForSeconds(0.01f);//延遲判斷速度
            if (Z.getsay(road).Substring(0, 1) == "(")
            {
                if (!(button1.interactable || button2.interactable))//需縮減
                {
                    buttons.GetComponent<CanvasGroup>().alpha = 0;
                    break;
                }
            }
            else
            {
				if (Input.GetKeyDown(KeyCode.Space)||Input.GetKey(KeyCode.LeftControl))//需縮減
                {
                    break;
                }
            }
        }
		Z = Z.n();
		if (Z.getsay (road) == "finish") {
			StartCoroutine (Sumthing.notview (box, 1, 0, 0.5, 0.07f));
			Invoke ("endthing", 0.2f);
			otherthing ();
		} else if (Z.getsay (road) == "" || Z.getsay (road) == null) {
			StartCoroutine (Sumthing.notview (box, 1, 0, 0.5, 0.07f));
			Invoke ("endthing", 0.2f);
            otherthing();
        } else {
			StartCoroutine (word ());
		}
        yield return null;
    }
	void endthing(){
		SumVariable.keyboardopen = true;
	}
}
