using UnityEngine;
using System.Collections;
using System;
public class cameralimit : MonoBehaviour {
	CanvasGroup wx;
	protected bool x,y;
	protected float xx, yy;
	GameObject player;
	Transform cameranow;
    float nx, ny;
	void Awake(){
		wx = GameObject.Find ("sendtoblack").GetComponent<CanvasGroup> ();
		cameranow = GameObject.Find ("Camera").GetComponent<Transform> ();
		player= GameObject.Find("Player");
	}
    void Start() {
        player.transform.position = SumVariable.nextad;
        StartCoroutine(Sumthing.notview (wx,1, 0, 0.0625,0.03f));
		SumVariable.keyboardopen = true;
		x = true; y = true;
	}

	void Update()
	{
		cameranow.transform.position = transform.position;
		transform.position = new Vector3(xx, yy, -20);
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
}
