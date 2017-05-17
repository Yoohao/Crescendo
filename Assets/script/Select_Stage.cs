using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select_Stage : MonoBehaviour {

	//var
	private int id = -1, id_max=9;
	private int timer = 0, f = 0;
	private bool animate = false;
	private bool left = false;
	private bool OnChangeID = false;
	private bool select = false;


	//GameObject
	public GameObject ring, bg;

	//UI
	public Text song_name, time, require, speed, spd;
	public Button next, pre, spdu, spdd;
	public Image game_bg, panel, spdu_i, spdd_i, next_i, pre_i, exit_i, topbar, play;
	public Image[] stars;
	public GameObject next_g, pre_g, play_g, exit_g;
	public GameObject[] stars_g;
	public GameObject selectUI, settingUI;

	//Stage preview
/*	public AudioClip[] stage_clip;
	public Sprite[] stage_img;
	public AudioClip[] stage_gameclip;
	public Sprite[] stage_gameimg;
	public string[] stage_name;
	public int[] stage_time;
	public int[] stage_diff;
*/

	public Sprite bg_img;

	// Use this for initialization
	void Start () {

		id = MainManager.Stage_ID;
		
		next_i.CrossFadeAlpha (0f, 0f, false);
		pre_i.CrossFadeAlpha (0f, 0f, false);
		exit_i.CrossFadeAlpha (0f, 0f, false);
		topbar.CrossFadeAlpha (0f, 0f, false);
		play.CrossFadeAlpha (0f, 0f, false);
		next_i.CrossFadeAlpha (1f, 1f, false);
		pre_i.CrossFadeAlpha (1f, 1f, false);
		exit_i.CrossFadeAlpha (1f, 1f, false);
		topbar.CrossFadeAlpha (1f, 1f, false);
		play.CrossFadeAlpha (1f, 1f, false);

		fadeoutSetting (0f);
		fadeinSetting (1f);

		//StartCoroutine (updateInfo (true));
	}
	
	// Update is called once per frame
	void Update () {
		f++;
		f %= 60;
		next.transform.localPosition += new Vector3 (f >= 30 ? 0.5f : -0.5f, 0f);
		pre.transform.localPosition -= new Vector3 (f >= 30 ? 0.5f : -0.5f, 0f);

		next_g.SetActive (id < id_max);
		pre_g.SetActive (id > -1);
		selectUI.SetActive (id >= 0);
		settingUI.SetActive (id == -1);

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
		if (!select && !animate) {
			if (Input.GetKeyDown (KeyCode.Escape))
				ClickExit ();
			else if (Input.GetKeyDown (KeyCode.LeftArrow) && !animate && id >= 0)
				ClickPrevious ();
			else if (Input.GetKeyDown (KeyCode.RightArrow) && !animate && id < id_max)
				ClickNext ();
			else if (Input.GetKeyDown (KeyCode.Return))
				ClickPlay ();
		}
		int tmp_id = Mathf.Clamp (id, -1, id_max);
		if (tmp_id != id) {
			OnChangeID = false;
			id = tmp_id;
		}
		if (OnChangeID && id >= 0) {
			StartCoroutine (updateInfo ());
			OnChangeID = false;
		}
	}
		
	void fadeinSetting (float dur){
		Graphic[] obj = settingUI.GetComponentsInChildren<Graphic> ();
		for (int i = 0; i < obj.Length; i++)
			obj [i].CrossFadeAlpha (1f, dur, false);
	}

	void fadeoutSetting (float dur){
		Graphic[] obj = settingUI.GetComponentsInChildren<Graphic> ();
		for (int i = 0; i < obj.Length; i++)
			obj [i].CrossFadeAlpha (0f, dur, false);
	}

	void fadeinSelectUI (float dur){
		Graphic[] obj = selectUI.GetComponentsInChildren<Graphic> ();
		for (int i = 0; i < obj.Length; i++)
			obj [i].CrossFadeAlpha (1f, dur, false);
	}

	void fadeoutSelectUI (float dur){
		Graphic[] obj = selectUI.GetComponentsInChildren<Graphic> ();
		for (int i = 0; i < obj.Length; i++)
			obj [i].CrossFadeAlpha (0f, dur, false);
	}

	/*
	void fadeout(float duration = 0f){
		song_name.CrossFadeAlpha (0f, duration, false);
		time.CrossFadeAlpha (0f, duration, false);
		speed.CrossFadeAlpha (0f, duration, false);
		spd.CrossFadeAlpha (0f, duration, false);
		require.CrossFadeAlpha (0f, duration, false);

		spdu_i.CrossFadeAlpha (0f, duration, false);
		spdd_i.CrossFadeAlpha (0f, duration, false);
		spdd.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, duration, false);
		spdu.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, duration, false);
		panel.CrossFadeAlpha (0f, duration, false);
		game_bg.CrossFadeAlpha (0f, duration, false);
		for (int i = 0; i < stars.Length; i++)
			stars [i].CrossFadeAlpha (0f, duration, false);
	}

	void fadein(float duration = 0f){
		song_name.CrossFadeAlpha(1f, duration, false);
		time.CrossFadeAlpha(1f, duration, false);
		speed.CrossFadeAlpha(1f, duration, false);
		spd.CrossFadeAlpha(1f, duration, false);
		require.CrossFadeAlpha(1f, duration, false);

		spdu_i.CrossFadeAlpha(1f, duration, false);
		spdd_i.CrossFadeAlpha(1f, duration, false);
		spdd.GetComponentInChildren<Text>().CrossFadeAlpha(1f, duration, false);
		spdu.GetComponentInChildren<Text>().CrossFadeAlpha(1f, duration, false);
		panel.CrossFadeAlpha(1f, duration, false);
		game_bg.CrossFadeAlpha(1f, duration, false);
		for (int i = 0; i < stars.Length; i++)
			stars[i].CrossFadeAlpha(1f, duration, false);
	}
*/

	IEnumerator updateInfo(bool flag=false){
		//fade out
		if (!flag) {
			//fadeout (0.5f);
			fadeoutSelectUI(0.5f);
			yield return new WaitForSeconds (0.6f);
		} else {
			fadeoutSelectUI (0f);
			yield return new WaitForSeconds (0.1f);
		}
		//Get info
		string t = "";
		float sec = Mathf.RoundToInt (GameManger.instance.Music_Clip [id].length);
		t += (sec / 60).ToString ();
		t += ":";
		t += (sec % 60).ToString();

		song_name.text = GameManger.instance.Music_Name[id];
		time.text = t;
		for (int i = 0; i < stars.Length; i++)
			stars_g [i].SetActive (false);
		for (int i = 0; i < GameManger.instance.stars [id]; i++)
			stars_g [i].SetActive (true);
		MainManager.Game_Speed = 1;
		spd.text = MainManager.Game_Speed.ToString() + "x";
		game_bg.sprite = GameManger.instance.ResultImage [id];
		SoundManager.instance.playBGM (GameManger.instance.Music_Demo[id], true);

		//fade in
		fadeinSelectUI(0.5f);
	}

	public void ClickExit(){
		MainManager.Index_BGM = null;
		SceneManager.LoadSceneAsync ("HomeScreen");		
	}

	public void ClickPlay(){
		//Set info for next scene
		StartCoroutine(PlayWrapper());
		//bg.GetComponentInChildren<SpriteRenderer> ().sprite = bg_img;
	}

	public IEnumerator PlayWrapper()
	{
		MainManager.Stage_ID = id;
		MainManager.Game_Music = GameManger.instance.Music_Demo [id];
		MainManager.Game_Name = GameManger.instance.Music_Name [id];
		MainManager.Game_time = Mathf.RoundToInt (GameManger.instance.Music_Clip [id].length);
		MainManager.Game_BackGround = GameManger.instance.ResultImage [id];

		fadeoutSelectUI (0.3f);
		next_i.CrossFadeAlpha (0f, 0.3f, false);
		pre_i.CrossFadeAlpha (0f, 0.3f, false);
		exit_i.CrossFadeAlpha (0f, 0.3f, false);
		topbar.CrossFadeAlpha (0f, 0.3f, false);
		play.CrossFadeAlpha (0f, 0.3f, false);
		yield return new WaitForSeconds (0.4f);
		exit_g.SetActive (false);
		play_g.SetActive (false);
		select = animate = true;
		timer = 0;
		Debug.Log (id);
		MainManager.instance.SetInfo (id);
	}

	public void ClickNext(){
		if (animate)
			return;
		id++;
		timer = 0;
		left = false;
		animate = true;
		OnChangeID = true;
	}
	public void ClickPrevious(){
		if (animate)
			return;
		id--;
		timer = 0;
		left = true;
		animate = true;
		OnChangeID = true;
	}
	public void ClickUP(){
		if (MainManager.Game_Speed == 3)
			return;
		else {
			MainManager.Game_Speed += 1;
			spd.text = MainManager.Game_Speed.ToString() + "x";
		}
	}
	public void ClickDown(){
		if (MainManager.Game_Speed == 1)
			return;
		else {
			MainManager.Game_Speed -= 1;
			spd.text = MainManager.Game_Speed.ToString() + "x";
		}
	}
}
