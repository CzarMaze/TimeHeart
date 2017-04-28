using UnityEngine;

public class musicaction : MonoBehaviour {

	void Start () {
		if (SumVariable.nextmuc != GetComponent<AudioSource> ().clip||GetComponent<AudioSource>().pitch!=SumVariable.nextmucpth) {
			if (SumVariable.nextmuc != null) {
				Destroy (GameObject.Find ("MUSIC"));
			}
			DontDestroyOnLoad (this);
			SumVariable.nextmuc = GetComponent<AudioSource> ().clip;
			SumVariable.nextmucpth = GetComponent<AudioSource> ().pitch;
		} else{
			Destroy (this.gameObject);
		}
	}

}
