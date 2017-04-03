using UnityEngine;
using System.Collections;

public class animeaction : MonoBehaviour
{
	protected Animator animator;
	private int keyup = Animator.StringToHash ("up");
	private int keydown = Animator.StringToHash ("down");
	private int keyright = Animator.StringToHash ("right");
	private int keyleft = Animator.StringToHash ("left");

	void Start ()
	{
		animator = GetComponent<Animator> ();
		charactoranime (SumVariable.charactor);
		animator.Play (Animator.StringToHash (SumVariable.nextdt));
	}

	void Update ()
	{
		if (SumVariable.keyboardopen) {
			if (Input.GetKey (KeyCode.RightArrow)) {
				animator.SetBool ("stop", false);
				animator.Play (keyright);
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				animator.SetBool ("stop", false);
				animator.Play (keyleft);
			} else if (Input.GetKey (KeyCode.UpArrow)) {
				animator.SetBool ("stop", false);
				animator.Play (keyup);
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				animator.SetBool ("stop", false);
				animator.Play (keydown);
			}
			if (!(Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.UpArrow))) {
				animator.SetBool ("stop", true);
			}
			if (Input.GetKeyUp (KeyCode.Tab)) {
				reselection ();
			}



		}
	}

	public void reselection ()
	{
		if (SumVariable.key == SumVariable.team.Length) {
			SumVariable.key = 0;
		}
		if (SumVariable.teamban [SumVariable.key]) {
			charactoranime (SumVariable.team [SumVariable.key].ToString());
			SumVariable.charactor = SumVariable.team [SumVariable.key].ToString();
			SumVariable.key++;
		} else {
			SumVariable.key++;
			reselection ();
		}
	}

	public void charactoranime (string k)
	{
		this.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("anime/"+k)as Sprite;
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animator Controller/"+k+"/"+k+"charactor") as RuntimeAnimatorController;
	}
}
