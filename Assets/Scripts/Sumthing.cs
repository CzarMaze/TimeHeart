using UnityEngine;
using System;
using System.Collections;

public static class Sumthing{
	public static IEnumerator view(CanvasGroup a,int i, int j, double x,float n)
	{
		for (double q = i; q <= j; q = q + x)
		{
			a.alpha = (float)q; //顯示視窗
			yield return new WaitForSeconds(n);
		}
		yield return null;
	}
	//---------------------------------------------------------------------------------
	public static IEnumerator notview(CanvasGroup a,int i, int j, double x,float n)
	{
		for (double q = i; q >= j; q = q - x)
		{
			a.alpha = (float)q; //隱藏視窗
			yield return new WaitForSeconds(n);
		}
		yield return null;
	}
    
}
internal class node
{
    String name = null;
    String say1 = null, say2 = null;
    String ima = null, imb = null, imc = null;
    String tk = null, mc = null;
    node next;

    public void add(String n, String s1, String s2, String l, String m, String r, String tk, String mc)
    {
        this.name = n;
        this.say1 = s1;
        this.say2 = s2;
        this.ima = l;
        this.imb = m;
        this.imc = r;
        this.tk = tk;
        this.mc = mc;
    }

    public void setn(node n)
    {
        this.next = n;
    }

    public String getname()
    {
        return name;
    }

    public String getsay(int s)
    {
        if (s == 1)
        {
            return say1;
        }
        else
        {
            return say2;
        }
    }

    public String getim(String s)
    {
        switch (s)
        {
            case "l":
                return ima;
            case "m":
                return imb;
            case "r":
                return imc;
            default:
                return "";
        }
    }

    public String getnowtalk()
    {
        return tk;
    }

    public String getmusic()
    {
        return mc;
    }

    public node n()
    {
        return next;
    }

}


