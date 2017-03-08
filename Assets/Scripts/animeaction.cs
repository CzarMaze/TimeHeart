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
		if (SumVariable.key == SumVariable.add.Length) {
			SumVariable.key = 0;
		}
		if (!SumVariable.ban [SumVariable.key]) {
			charactoranime (SumVariable.add [SumVariable.key]);
			SumVariable.charactor = SumVariable.add [SumVariable.key];
			SumVariable.key++;
		} else {
			SumVariable.key++;
			reselection ();
		}
	}

	public void charactoranime (string k)
	{
		if (k == "aleana") {
			this.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("anime/Aleana")as Sprite;
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animator Controller/Aleana/charactor") as RuntimeAnimatorController;
		} else if (k == "sharenold") {
			this.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("anime/Sharenold")as Sprite;
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animator Controller/Sharenold/Sharenold charactor") as RuntimeAnimatorController;
		} else if (k == "kaisvo") {
			this.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("anime/Kaisvo")as Sprite;
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animator Controller/Kaisvo/Kaisvo charactor") as RuntimeAnimatorController;

		}
	}
    public void PrintEvent(string s)
    {
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }
}
