using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Stage : MonoBehaviour {

	public GameObject ring, arrow;

	//UI
	public Text Stage_Name, Stage_Time, Stage_Info;
	public Image Stage_Cover, panel;
	public GameObject btnR, btnL;
	public Image[] star;
	public GameObject[] star_g;

	//Stage Info
	public string[] Songname, time, info;
	public int[] difficulty;
	public Sprite[] cover;
	public AudioClip[] clip;

	//var
	private int id = MainManager.Stage_ID;
	private int count = 0;
	private bool onChangeID = false;
	private bool animate = false;
	private bool once = true;
	private bool right;
	private int timer = 0;

	// Use this for initialization
	void Start () {
		SoundManager.instance.playBGM (clip [id]);
		Stage_Name.text = Songname [id];
		Stage_Time.text = time [id];
		Stage_Info.text = info [id];
		Stage_Cover.sprite = cover [id];

		for (int i = 0; i < star.Length; i++)
			star_g [i].SetActive (false);
		for (int i = 0; i < difficulty[id]; i++)
			star_g [i].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

		timer++;
		timer %= 60;

		btnR.SetActive (id < Songname.Length - 1 && once);
		btnL.SetActive (id > 0 && once);

		Vector3 v = new Vector3 (.5f * (timer > 30 ? -1f : 1f), 0f, 0f);
		btnR.transform.localPosition += v;
		btnL.transform.localPosition -= v;

		if (animate) {
			ring.transform.Rotate (new Vector3 (0f, 0f, right ? -1f : 1f));
			count++;
			if (count == 45) {
				animate = false;
				count = 0;
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow) && !animate && id < Songname.Length - 1) {
			animate = true;
			right = true;
			id++;
			onChangeID = true;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && !animate && id > 0) {
			animate = true;
			right = false;
			id--;
			onChangeID = true;
		} else if (Input.GetKeyDown (KeyCode.Return) && once) {
			MainManager.Stage_ID = id;
			once = false;
			StartCoroutine (load ());
			btnL.SetActive (false);
			btnR.SetActive (false);
			Stage_Name.CrossFadeAlpha (0f, 0.5f, false);
			Stage_Time.CrossFadeAlpha (0f, 0.5f, false);
			Stage_Info.CrossFadeAlpha (0f, 0.5f, false);
			Stage_Cover.CrossFadeAlpha (0f, 0.5f, false);
			panel.CrossFadeAlpha (0f, 0.5f, false);

			for (int i = 0; i < star.Length; i++) {
				star [i].CrossFadeAlpha (0f, 0.5f, false);
			}
		}

		int tmp_id = Mathf.Clamp (id, 0, Songname.Length - 1);
		if (id != tmp_id)
			onChangeID = false;
		id = tmp_id;
		if (onChangeID) {
			StartCoroutine (UpdateUI (id));
			onChangeID = false;
		}
	}

	IEnumerator UpdateUI(int idx){

		Stage_Name.CrossFadeAlpha (0f, 0.5f, false);
		Stage_Time.CrossFadeAlpha (0f, 0.5f, false);
		Stage_Info.CrossFadeAlpha (0f, 0.5f, false);
		Stage_Cover.CrossFadeAlpha (0f, 0.5f, false);
		panel.CrossFadeAlpha (0f, 0.5f, false);
		for (int i = 0; i < star.Length; i++) {
			star [i].CrossFadeAlpha (0f, 0.5f, false);
		}
		yield return new WaitForSeconds (0.6f);

		Stage_Name.text = Songname [idx];
		Stage_Time.text = time [idx];
		Stage_Info.text = info [idx];
		Stage_Cover.sprite = cover [idx];
		SoundManager.instance.playBGM (clip [idx]);	

		for (int i = 0; i < star.Length; i++)
			star_g [i].SetActive (false);
		for (int i = 0; i < difficulty[id]; i++)
			star_g [i].SetActive (true);
		
		Stage_Name.CrossFadeAlpha (1.0f, 0.5f, false);
		Stage_Time.CrossFadeAlpha (1f, 0.5f, false);
		Stage_Info.CrossFadeAlpha (1.0f, 0.5f, false);
		Stage_Cover.CrossFadeAlpha (1f, 0.5f, false);
		print (difficulty [idx]);
		for (int i = 0; i < difficulty[idx]; i++) {
			star [i].CrossFadeAlpha (1f, 0.5f, false);
		}
		panel.CrossFadeAlpha (1.0f, 0.5f, false);
	}

	IEnumerator load(){
		for (int i = 0; i < 100; i++) {
			ring.transform.localScale += new Vector3 (0.03f, 0.03f, 0f);
			yield return new WaitForSeconds (0.01f);
		}
	}

	public void onclickR (){
		if (!(id < Songname.Length - 1))
			return;
		animate = true;
		right = true;
		id++;
		onChangeID = true;
	}
	public void onclickL (){
		if (!(id > 0))
			return;
		animate = true;
		right = false;
		id--;
		onChangeID = true;
	}
}
