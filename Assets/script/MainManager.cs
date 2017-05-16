using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MainManager : MonoBehaviour {

	public static MainManager instance = null;

	//HomeScreen
	private static AudioClip index_BGM = null;

	//Setting
	private static float effect_vol = .5f;
	private static float BGM_vol = .5f;
	private static bool BGM_change = false;
	private static string[] device = {"string1", "string2", "string3", "string4"};

	//Stage
	private static int stage_id = -1;

	//Game
	private static AudioClip game_music = null;
	private static Sprite game_bg = null;
	private static Sprite result_bg=null;
	private static string game_name = "";
	private static int game_time = -1;
	private static int game_speed = 1;
	public static int stars=0;
	public static int sensor=0;
	//Mag calibration
	private static float MoffsetX = 0 , MoffsetY = 0, MoffsetZ = 0;

	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	//HomeScreen
	public static AudioClip Index_BGM{
		get{ return index_BGM; }
		set{ index_BGM = value; }
	}

	//Setting
	public static float Effect_Volume{
		get{ return effect_vol; }
		set{ effect_vol = value; }
	}

	public static float BGM_Volume{
		get{ return BGM_vol; }
		set{ BGM_vol = value; }
	}

	public static bool BGM_Change{
		get{ return BGM_change; }
		set{ BGM_change = value; }
	}

	public static string[] Device{
		get{ return device; }
	}

	//Stage
	public static int Stage_ID{
		get{ return stage_id; }
		set{ stage_id = value; }
	}


	public static Sprite Result_img{
		get{ return result_bg; }
		set{ result_bg = value; }
	}
	//Game

	public static AudioClip Game_Music{
		get{ return game_music; }
		set{ game_music = value; }
	}

	public static Sprite Game_BackGround{
		get{ return game_bg; }
		set{ game_bg = value; }
	}

	public static string Game_Name {
		get{ return game_name; }
		set{ game_name = value; }
	}

	public static int Game_time {
		get{ return game_time; }
		set{ game_time = value; }
	}

	public static int Game_Speed{
		get{ return game_speed; }
		set{ game_speed = value; }
	}

	//Mag Calibration
	public static float MagX{
		get{ return MoffsetX; }
		set{ MoffsetX = value; }
	}

	public static float MagY{
		get{ return MoffsetY; }
		set{ MoffsetY = value; }
	}

	public static float MagZ{
		get{ return MoffsetZ; }
		set{ MoffsetZ = value; }
	}

	private void wirteConfig(){
	}

	public void SetInfo(int id)
	{
		stage_id = id;
		game_music = GameManger.instance.Music_Clip[id];
		game_name = GameManger.instance.Music_Name[id];
		game_bg = GameManger.instance.Background [id];
		result_bg = GameManger.instance.ResultImage[id];
		stars = GameManger.instance.stars [id];
		sensor = GameManger.instance.Sensors [id];
	}
}
