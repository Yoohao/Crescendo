using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour {

	private bool Rank = false;

	public Text[] options;
	public GameObject[] options_g;
	public Text anykey;
	public GameObject anykey_g;
	public Text arrow;
	public GameObject arrow_g; 

	public float[] offset;

	private int timer;

	public AudioClip select, enter, BGM;

	// Use this for initialization
	void Start () {
		anykey_g.SetActive (!MainManager.Press);
		arrow_g.SetActive (MainManager.Press);
		for (int i = 0; i < options_g.Length; i++)
			options_g [i].SetActive (MainManager.Press);
		
		if (!Rank)
			options [1].color = new Color (0.5f, 0.5f, 0.5f, 0.5f);

		if (MainManager.Index_BGM == null) {
			MainManager.Index_BGM = BGM;
			SoundManager.instance.playBGM (MainManager.Index_BGM);
		}
		if (MainManager.BGM_Change || MainManager.First) {
			SoundManager.instance.playBGM (MainManager.Index_BGM);
			MainManager.First = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!MainManager.Press) {
			timer++;
			timer %= 60;
			anykey_g.SetActive (timer > 30);
		}
		if (!MainManager.Press && Input.anyKey) {
			MainManager.Press = true;
			anykey_g.SetActive (!MainManager.Press);
			arrow_g.SetActive (MainManager.Press);
			for (int i = 0; i < options_g.Length; i++)
				options_g [i].SetActive (MainManager.Press);
		}
		else if (MainManager.Press) {
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				MainManager.Index_ID++;
				SoundManager.instance.playSingle (select);
				if (!Rank && MainManager.Index_ID == 1)
					MainManager.Index_ID++;
			}
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				MainManager.Index_ID--;
				SoundManager.instance.playSingle (select);
				if (!Rank && MainManager.Index_ID == 1)
					MainManager.Index_ID--;
			}
			if (Input.GetKeyDown (KeyCode.Return)) {
				//load
				SoundManager.instance.playSingle (enter);
				switch (MainManager.Index_ID) {
				case 0:
					SceneManager.LoadSceneAsync ("NewGame");
					break;
				case 1:
					break;
				case 2:
					SceneManager.LoadSceneAsync ("Setting");
					break;
				case 3:
					Application.Quit ();
					break;
				default:
					break;
				}
			}
			MainManager.Index_ID = Mathf.Clamp (MainManager.Index_ID, 0, 3);
			int id = MainManager.Index_ID;
			arrow.rectTransform.localPosition = new Vector3 (offset [id], options [id].transform.localPosition.y, 0);
		}
	}
};