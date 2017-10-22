using UnityEngine;
using System.Collections;

public class battleplay : MonoBehaviour {
	public static Animator EA;
	public void attrackchoice(){
		EA.Play(Animator.StringToHash("hurt"));
		battle.num++;
	}
}
