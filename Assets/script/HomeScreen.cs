using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class HomeScreen : MonoBehaviour {

	public AudioClip clip;
	public Text press;
	public Text percentage;
	public GameObject per;

	private bool flag = true;


	// Use this for initialization
	void Start () {
		if (MainManager.Index_BGM == null) {
			MainManager.Index_BGM = clip;
			SoundManager.instance.playBGM (MainManager.Index_BGM);
		}
		StartCoroutine (loop ());
		per.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			flag = false;
			per.SetActive (true);
			StartCoroutine (load ());
		}
	}

	IEnumerator load(){
		AsyncOperation async = SceneManager.LoadSceneAsync ("Newgame");
		while (!async.isDone) {
			percentage.text = Mathf.RoundToInt(async.progress * 100).ToString() + "%";
			yield return null;
		}
	}

	IEnumerator loop(){
		while (flag) {
			press.CrossFadeAlpha (0f, 1f, false);
			yield return new WaitForSeconds (1f);
			press.CrossFadeAlpha (1f, 1f, false);
			yield return new WaitForSeconds (1f);
		}
		press.CrossFadeAlpha (0f, 0.01f, false);
	}
}
