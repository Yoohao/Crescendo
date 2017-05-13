using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select_stage : MonoBehaviour {

	//var
	private int id = -1, id_max;
	private int timer = 0, f = 0;
	private bool animate = false;
	private bool left = false;
	private bool OnChangeID = false;


	//GameObject
	public GameObject ring;

	//UI
	public Text song_name, time, require, speed, spd;
	public Button exit, play, next, pre, spdu, spdd;
	public Image game_bg, panel, spdu_i, spdd_i;
	public Image[] stars;
	public GameObject next_g, pre_g;
	public GameObject[] stars_g;

	//Stage preview
	public AudioClip[] stage_clip;
	public Sprite[] stage_img;
	public string[] stage_name;
	public int[] stage_time;
	public int[] stage_diff;

	// Use this for initialization
	void Start () {
		if (MainManager.Stage_ID != -1)
			id = MainManager.Stage_ID;
		else
			id = 0;
		id_max = stage_name.Length - 1;

		StartCoroutine (updateInfo (true));
	}
	
	// Update is called once per frame
	void Update () {
	
		f++;
		f %= 60;
		next.transform.localPosition += new Vector3 (f >= 30 ? 0.5f : -0.5f, 0f);
		pre.transform.localPosition -= new Vector3 (f >= 30 ? 0.5f : -0.5f, 0f);

		next_g.SetActive (id < id_max);
		pre_g.SetActive (id > 0);

		if (animate && timer<45) {
			timer++;
			ring.transform.Rotate (new Vector3 (0f, 0f, left ? -1f : 1f), Space.Self);
			if (timer == 45)
				animate = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape) && !animate) {
			ClickExit ();
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && !animate && id > 0) {

			id--;
			timer = 0;
			left = true;
			animate = true;
			OnChangeID = true;
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && !animate && id < id_max) {
			id++;
			timer = 0;
			left = false;
			animate = true;
			OnChangeID = true;
		}
		int tmp_id = Mathf.Clamp (id, 0, id_max);
		if (tmp_id != id) {
			OnChangeID = false;
			id = tmp_id;
		}
		if (OnChangeID) {
			StartCoroutine (updateInfo ());
			OnChangeID = false;
		}

	}

	IEnumerator updateInfo(bool flag=false){
		//fade out
		if (!flag) {
			song_name.CrossFadeAlpha (0f, 0.5f, false);
			time.CrossFadeAlpha (0f, 0.5f, false);
			speed.CrossFadeAlpha (0f, 0.5f, false);
			spd.CrossFadeAlpha (0f, 0.5f, false);
			require.CrossFadeAlpha (0f, 0.5f, false);

			spdu_i.CrossFadeAlpha (0f, 0.5f, false);
			spdd_i.CrossFadeAlpha (0f, 0.5f, false);
			spdd.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 0.5f, false);
			spdu.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 0.5f, false);
			panel.CrossFadeAlpha (0f, 0.5f, false);
			game_bg.CrossFadeAlpha (0f, 0.5f, false);
			for (int i = 0; i < stars.Length; i++)
				stars [i].CrossFadeAlpha (0f, 0.5f, false);
			yield return new WaitForSeconds (0.6f);
		} else {
			song_name.CrossFadeAlpha (0f, 0f, false);
			time.CrossFadeAlpha (0f, 0f, false);
			speed.CrossFadeAlpha (0f, 0f, false);
			spd.CrossFadeAlpha (0f, 0f, false);
			require.CrossFadeAlpha (0f, 0f, false);

			spdu_i.CrossFadeAlpha (0f, 0f, false);
			spdd_i.CrossFadeAlpha (0f, 0f, false);
			spdd.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 0f, false);
			spdu.GetComponentInChildren<Text> ().CrossFadeAlpha (0f, 0f, false);
			panel.CrossFadeAlpha (0f, 0f, false);
			game_bg.CrossFadeAlpha (0f, 0f, false);
			for (int i = 0; i < stars.Length; i++)
				stars [i].CrossFadeAlpha (0f, 0f, false);
			yield return new WaitForSeconds (0.1f);
		}
		//Get info
		string t = "";
		t += (stage_time [id] / 60).ToString ();
		t += ":";
		t += (stage_time [id] % 60).ToString();

		song_name.text = stage_name[id];
		time.text = t;
		for (int i = 0; i < stars.Length; i++)
			stars_g [i].SetActive (false);
		for (int i = 0; i < stage_diff [id]; i++)
			stars_g [i].SetActive (true);
		MainManager.Game_Speed = 1;
		spd.text = MainManager.Game_Speed.ToString() + "x";
		game_bg.sprite = stage_img [id];
		SoundManager.instance.playBGM (stage_clip [id], true);

		//fade in
		song_name.CrossFadeAlpha(1f, 0.5f, false);
		time.CrossFadeAlpha(1f, 0.5f, false);
		speed.CrossFadeAlpha(1f, 0.5f, false);
		spd.CrossFadeAlpha(1f, 0.5f, false);
		require.CrossFadeAlpha(1f, 0.5f, false);

		spdu_i.CrossFadeAlpha(1f, 0.5f, false);
		spdd_i.CrossFadeAlpha(1f, 0.5f, false);
		spdd.GetComponentInChildren<Text>().CrossFadeAlpha(1f, 0.5f, false);
		spdu.GetComponentInChildren<Text>().CrossFadeAlpha(1f, 0.5f, false);
		panel.CrossFadeAlpha(1f, 0.5f, false);
		game_bg.CrossFadeAlpha(1f, 0.5f, false);
		for (int i = 0; i < stars.Length; i++)
			stars[i].CrossFadeAlpha(1f, 0.5f, false);
	}

	public void ClickExit(){
		MainManager.Index_BGM = null;
		SceneManager.LoadSceneAsync ("HomeScreen");		
	}

	public void ClickPlay(){}
	public void ClickNext(){}
	public void ClickPrevious(){}
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
