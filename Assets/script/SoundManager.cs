using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource effect;
	public AudioSource BGM;
	public static SoundManager instance = null;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

	}

	public void playSingle(AudioClip clip){
		effect.volume = MainManager.Effect_Volume;
		effect.clip = clip;
		effect.Play ();
	}

	public void playdemo(AudioClip clip, float vol){
		effect.volume = vol;
		effect.clip = clip;
		effect.Play ();
	}

	public void SetBGMVolume(float vol){
		BGM.volume = vol;
	}

	public void playBGM(AudioClip clip){
		BGM.volume = MainManager.BGM_Volume;
		BGM.clip = clip;
		BGM.Play ();
	}
}
