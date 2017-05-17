using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	//var
	public const int SETTING = -1;
	public const bool FADE_IN = true, FADE_OUT = false;
	public int id = -1, max_id, timer = 0, f;
	private bool ChangeID = false, animate = false, left, changed, select = false;

	//UI Canvas
	public GameObject DefaultUI, SettingUI, SelectUI;
	public GameObject Next, Previous, ring;
	public Text Stage_name, Stage_time, spd;
	public Image Stage_img, panel;
	public GameObject[] Stars;

	// Use this for initialization
	void Start(){

		id = MainManager.Stage_ID;
		max_id = GameManger.instance.Music_Name.Length-1;
		changed = (id != SETTING);

		DefaultUI.SetActive (true);
		SettingUI.SetActive (id==SETTING);
		SelectUI.SetActive (id!=SETTING);

		StartCoroutine (FadeUI (DefaultUI, 1f, FADE_IN));
		if (id != SETTING)
			UpdateInfo (true);
	}
	
	// Update is called once per frame
	void Update () {
		print (id);
		SettingUI.SetActive (id==SETTING);
		SelectUI.SetActive (id!=SETTING);
		Next.SetActive (id < max_id);
		Previous.SetActive (id > -1);

		f++;
		f %= 60;
		Next.transform.localPosition += new Vector3 (f > 29 ? 0.5f : -0.5f, 0);
		Previous.transform.localPosition += new Vector3 (f > 29 ? -0.5f : 0.5f, 0);

		if (animate && select && timer < 60) {
			ring.transform.localScale += new Vector3 (0.1f, 0.1f);
			if (ring.transform.localScale.x > 2.5f) {
				animate = false;
				SoundManager.instance.Stop ();
				SceneManager.LoadSceneAsync("GameVer1");
			}
		} else if (animate && timer < 30) {
			timer++;
			ring.transform.Rotate (new Vector3 (0f, 0f, left ? -1.5f : 1.5f), Space.Self);
			if (timer == 30)
				animate = false;
		}


		if (Input.GetKeyDown (KeyCode.LeftArrow) && !animate && id > -1) {
			ClickPre ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && !animate && id < max_id) {
			ClickNext ();
		} else if (Input.GetKeyDown (KeyCode.Return) && !animate && !select && id != SETTING) {
			ClickPlay ();
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			ClickExit ();
		}

		int tmp_id = Mathf.Clamp (id, -1, GameManger.instance.Music_Name.Length-1);
		if (tmp_id != id) {
			ChangeID = false;
			id = tmp_id;
		}

		if (ChangeID) {
			if (id == SETTING) {
				//show setting UI
				StartCoroutine (FadeUI (id == SETTING ? SettingUI : SelectUI, 1f, FADE_IN));
			} else {
				//show Select UI & update info
				StartCoroutine (FadeUI (id == SETTING ? SettingUI : SelectUI, 1f, FADE_IN));
				UpdateInfo ();
			}
			ChangeID = false;
		}

	}

	IEnumerator FadeUI(GameObject UI, float duration, bool FadeIn = true){
		Graphic[] obj = UI.GetComponentsInChildren<Graphic> ();
		for (int i = 0; i < obj.Length; i++)
			obj [i].CrossFadeAlpha (FadeIn ? 0f : 1f, 0f, true);
		yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < obj.Length; i++)
			obj [i].CrossFadeAlpha (FadeIn ? 1f : 0f, duration, true);
		yield return new WaitForSeconds (duration + 0.1f);
	}

	IEnumerator FadeImage (Image obj, float duration, bool FadeIn = true){
		obj.CrossFadeAlpha (FadeIn ? 0f : 1f, 0f, true);
		yield return new WaitForSeconds (0.1f);
		obj.CrossFadeAlpha (FadeIn ? 1f : 0f, duration, true);
		yield return new WaitForSeconds (duration + 0.1f);
	}

	void UpdateInfo(bool flag = false){
		StartCoroutine (FadeUI (SelectUI, 0.5f, FADE_OUT));
		StartCoroutine (FadeImage (panel, 0.5f, FADE_OUT));
		StartCoroutine (FadeImage (Stage_img, 0.5f, FADE_OUT));

		string str = "";
		int t = Mathf.RoundToInt(GameManger.instance.Music_Clip[id].length);
		str += (t / 60).ToString();
		str += ":";
		str += (t % 60).ToString();

		Stage_name.text = GameManger.instance.Music_Name [id];
		Stage_time.text = str;
		Stage_img.sprite = GameManger.instance.ResultImage[id];
		for (int i = 0; i < 5; i++)
			Stars [i].SetActive (false);
		for (int i = 0; i < GameManger.instance.stars [id]; i++)
			Stars [i].SetActive (true);
		if (!(id == 0 && changed && !left) || flag)
			SoundManager.instance.playBGM (GameManger.instance.Music_Demo [id], true);

		StartCoroutine (FadeUI (SelectUI, 0.5f, FADE_IN));
		StartCoroutine (FadeImage (panel, 0.5f, FADE_IN));
		StartCoroutine (FadeImage (Stage_img, 0.5f, FADE_IN));

		changed = true;
	}

	public void ClickExit(){
		MainManager.Index_BGM = null;
		SceneManager.LoadSceneAsync ("HomeScreen");
	}

	public void ClickPlay()
	{
		MainManager.Stage_ID = id;
		MainManager.Game_Music = GameManger.instance.Music_Demo [id];
		MainManager.Game_Name = GameManger.instance.Music_Name [id];
		MainManager.Game_time = Mathf.RoundToInt (GameManger.instance.Music_Clip [id].length);
		MainManager.Game_BackGround = GameManger.instance.ResultImage [id];

		StartCoroutine (FadeUI (DefaultUI, 0.3f, FADE_OUT));
		StartCoroutine (FadeUI (SelectUI, 0.3f, FADE_OUT));

		select = animate = true;
		timer = 0;
		Debug.Log (id);
		MainManager.instance.SetInfo (id);
	}
	public void ClickNext(){
		if (animate)
			return;
		id++;
		ChangeID = true;
		left = false;
		timer = 0;
		animate = true;
	}
	public void ClickPre(){
		if (animate)
			return;
		id--;
		ChangeID = true;
		left = true;
		timer = 0;
		animate = true;
	}
}
