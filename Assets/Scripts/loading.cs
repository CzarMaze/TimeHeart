using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class loading : MonoBehaviour {
	public Text loadText;
	void Start(){
		StartCoroutine (LoadingScreen (SumVariable.nextlevel));
	}
	protected IEnumerator LoadingScreen (string nextlevel){
		int load = 0;
		AsyncOperation async = SceneManager.LoadSceneAsync("scan/"+nextlevel);
		async.allowSceneActivation = false;
		while (async.progress<0.9f) {
			while (load < (int)async.progress * 100000) {
				load++;
				loadText.text = load.ToString();
				yield return new WaitForEndOfFrame ();
			}
			yield return new WaitForEndOfFrame ();
		}
		loadText.text = "100";
		async.allowSceneActivation = true;
	}
}
