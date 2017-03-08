using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
public class blackdown1 : Story {
	int i=1;//這邊先宣告資料表最小數值
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Player") {
			StartCoroutine (meet ("A",this.gameObject.name+i));//你可以看到左邊 ( ) 內的 this.gameObject.name  +  i 這個格式
			/*
			 * this.gameObject.name 為你的((物件名字)) 你原本打  黑地下室1  後面的1是多打的 我已經全部改掉 以後請務必記得不要打沒必要的數字
			 * 
			 * i 為數字 1'2'3 .....看你資料表設這個有多少,詳細設定請看下面
			 * 
			 * 從上面兩個組合起來後就是  黑地下室+1     黑地下室+2    依此類推
			 * 就變成你資料表的 黑地下室1 黑地下室2
			*/
			if (i < 3) {//這邊的2是指你資料庫內的資料表名子最大一個數字
				i++;
			}else {//當對話到最大數字時
				i = 3;//變回到第一個
			}
		}
	}
	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.name == "Player") {
			StopAllCoroutines ();
		}
	}
	protected override void otherthing(){

	}

}
