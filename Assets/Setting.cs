using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour {

	public Slider BGM, Effect;

	// Use this for initialization
	void Start () {
		BGM.value = MainManager.BGM_Volume;
		Effect.value = MainManager.Effect_Volume;
	}

	public void OnChangeBGMVol(){
		SoundManager.instance.SetBGMVolume (BGM.value);
	}
	public void OnChangeEffVol(){
		SoundManager.instance.SetEffVolume (Effect.value);
	}
}
