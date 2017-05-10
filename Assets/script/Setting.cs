using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Setting : MonoBehaviour {

	public Slider eff_vol, BGM_vol;
	public string[] device;
	public AudioClip demo_eff;

	// Use this for initialization
	void Start () {
		eff_vol.value = MainManager.Effect_Volume;
		BGM_vol.value = MainManager.BGM_Volume;
	}

	public void ClickCancel(){
		SoundManager.instance.SetBGMVolume (MainManager.BGM_Volume);
		SceneManager.LoadSceneAsync ("Index");
	}

	public void ClickOK(){
		MainManager.Effect_Volume = eff_vol.value;
		MainManager.BGM_Volume = BGM_vol.value;
		SceneManager.LoadSceneAsync ("Index");
	}

	public void EffectVolOnChange(){
		SoundManager.instance.playdemo (demo_eff, eff_vol.value);
	}
	public void BGMVolOnChange(){
		SoundManager.instance.SetBGMVolume (BGM_vol.value);
	}


}
