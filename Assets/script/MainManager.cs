using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MainManager : MonoBehaviour {

	//config

	public static MainManager instance = null;

	//Index
	private static AudioClip index_BGM = null;
	private static bool press = false;
	private static bool first = true;
	private static int select_id = 0;

	//Setting
	private static float effect_vol = .5f;
	private static float BGM_vol = .5f;
	private static bool BGM_change = false;
	private static string[] device = {"string1", "string2", "string3", "string4"};

	//Stage
	private static int stage_id = 0;

	//Game
	private static AudioClip game_music;

	//Mag calibration
	private static float MoffsetX, MoffsetY, MoffsetZ;

	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	public static AudioClip Index_BGM{
		get{ return index_BGM; }
		set{ index_BGM = value; }
	}

	public static bool Press{
		get{ return press; }
		set{ press = value; }
	}

	public static bool First{
		get{ return first; }
		set{ first = value; }
	}

	public static int Index_ID{
		get{ return select_id; }
		set{ select_id = value; }
	}

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

	public static int Stage_ID{
		get{ return stage_id; }
		set{ stage_id = value; }
	}

	public static AudioClip Game_Music{
		get{ return game_music; }
		set{ game_music = value; }
	}

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

}
