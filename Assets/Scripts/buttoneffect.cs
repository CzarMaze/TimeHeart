using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttoneffect : MonoBehaviour
{
    protected EventSystem es;
    protected GameObject YN;
    public GameObject tmp, cache;

    protected GameObject [] upcharactor,Items; 
    void Start()
    {
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        YN = GameObject.Find("Selectitem");
        Items=GameObject.FindGameObjectsWithTag("Itemss");
    }
    public void buttoneffects()
    {
        YN.GetComponent<buttoneffect>().tmp = es.currentSelectedGameObject.gameObject;
        YN.GetComponent<CanvasGroup>().alpha = 1;
        YN.GetComponent<Canvas>().overrideSorting = true;
        YN.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = true;
        YN.transform.GetChild(2).GetComponent<Canvas>().overrideSorting = true;
        YN.transform.GetChild(1).GetComponent<Button>().interactable = true;
        YN.transform.GetChild(2).GetComponent<Button>().interactable = true;
    }
    public void buttoneffectsYes()
    {
        if (tmp.transform.GetChild(1).GetComponent<Text>().text != 0.ToString())
        {
            switch (tmp.transform.GetChild(0).GetComponent<Text>().text)
            {
                case "人之初":
                    Debug.Log("SA");
                    buttonthingmathdel(tmp, 1);
                    break;
                case "單人補血":
                    StartCoroutine(choicechr("血量+50"));
                    break;
                default:
                    StartCoroutine(errbox());
                    break;
            }
        }
        closemesgebox();
    }
    IEnumerator errbox()
    {
        GameObject.Find("Erroritem").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Erroritem").GetComponent<Canvas>().overrideSorting = true;
        yield return new WaitForSeconds(1);
        GameObject.Find("Erroritem").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Erroritem").GetComponent<Canvas>().overrideSorting = false;
        yield return null;
    }
    IEnumerator choicechr(string q){
        string s;
        upcharactor=GameObject.FindGameObjectsWithTag("menuupcharactor");
        arraygameobjectbutton(upcharactor,true,1);
        yield return new WaitForSeconds(0.02f);
        loadselected(upcharactor[0]);
        while(true){
            yield return new WaitForSeconds(0.02f);
            if(Input.GetKeyDown(KeyCode.Space)){
                s=es.currentSelectedGameObject.name.Substring(4);
                break;
            }
        }
        arraygameobjectbutton(upcharactor,false,1);
        for(int i=0; i<Items.Length;i++){
            if(Items[i].name=="itemUI"){
               loadselected(Items[0]);
            }
        }
        Debug.Log(s+q);
        buttonthingmathdel(tmp, 1);
        yield return null;
    }
    public void buttoneffectsNo()
    {
        closemesgebox();
    }
    void closemesgebox()
    {
        YN.GetComponent<CanvasGroup>().alpha = 0;
        YN.GetComponent<Canvas>().overrideSorting = false;
        YN.transform.GetChild(1).GetComponent<Canvas>().overrideSorting = false;
        YN.transform.GetChild(2).GetComponent<Canvas>().overrideSorting = false;
        YN.transform.GetChild(1).GetComponent<Button>().interactable = false;
        YN.transform.GetChild(2).GetComponent<Button>().interactable = false;
    }
    void buttonthingmathdel(GameObject tmp, int x)
    {
        tmp.transform.GetChild(1).GetComponent<Text>().text = (Int32.Parse(tmp.transform.GetChild(1).GetComponent<Text>().text) - x).ToString();
    }

    void loadselected(GameObject a){// -----------------------------------設定第一選項
		es.firstSelectedGameObject=a;
		es.SetSelectedGameObject(null);
 		es.SetSelectedGameObject(es.firstSelectedGameObject);
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
}
