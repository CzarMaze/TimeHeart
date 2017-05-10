using UnityEngine;
using System.Collections;
public class X : Story {
	bool x=false;
	int ta=1;
	void OnCollisionEnter2D(Collision2D other){
		x=true;
	}
	void LateUpdate(){
		if(Input.GetKeyUp(KeyCode.Space)&&x){
			x=false;
			if(menu.taskexist("main","C")){//-------------"main"=>主線	"123456"=>編號123456 目標提示確認
				menu.deltask("main","C");//-------------"main"=>主線	"123456"=>編號123456 刪除目標提示
				Debug.Log("task message");
				//meet("A",this.gameObject.name);----------------------當擁有符合上述的目標提示 此對話改為另一個對話
			}else{
				meet("A",this.gameObject.name);
				if (ta <1) {
					ta++;
				}
				/*此為範例
						meet("A",this.gameObject.name+ta);
						if(ta<"該對話資料表最大數值"){
							ta++;
						}
					}
				 */
			}
			
		}
	}
	void OnCollisionExit2D(Collision2D other){
		x=false;
	}
	protected override void otherthing(){	
	}
}
