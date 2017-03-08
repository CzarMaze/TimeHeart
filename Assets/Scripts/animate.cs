using UnityEngine;
using System.Collections;

public class animate : Story {
    Animator [] Animateraction;
    void Awake()
    {
        Animateraction = GameObject.Find("Animateraction").GetComponentsInChildren<Animator>();
    }
    public void Print()
    {
        open("AnimationHome1", "AnimationHomeA1");
        SumVariable.keyboardopen = false;
        StartCoroutine(Sumthing.view(box, 0, 1, 0.0625, 0.005f));
        StartCoroutine(word());
        this.GetComponent<Animator>().enabled = false;
    }
    public void StopAnimate()
    {
        this.GetComponent<Animator>().enabled = false;
    }
    public void musicanimate(string s){
        soundmuc(MUSIC, s);
    }
    public void talknoxmusicanimate(string s)
    {
        soundmuc(talkbox,s);
    }
    protected override void otherthing()
    {
        for (int i= 0;i < Animateraction.Length; i++) {
            Animateraction[i].enabled = true;
        }
    }
}
