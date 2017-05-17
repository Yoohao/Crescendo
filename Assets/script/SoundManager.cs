using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource effect;
	public AudioSource BGM;
	public static SoundManager instance = null;

	public AudioClip[] GameClip;
	public AudioClip[] stage_gameclip;

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
		MainManager.BGM_Volume = vol;
	}
	public void SetEffVolume(float vol){
		effect.volume = vol;
		MainManager.Effect_Volume = vol;
	}
	public void playBGM(AudioClip clip, bool fade=false){
		if (fade)
			BGM.volume = 0;
		else
			BGM.volume = MainManager.BGM_Volume;
		BGM.clip = clip;
		BGM.Play ();
		if (fade)
			StartCoroutine (fadein ());
	}

	IEnumerator fadein(){
		for (float i = 0f; i < MainManager.BGM_Volume; i += 0.005f) {
			BGM.volume = i;
			yield return new WaitForSeconds (0.01f);
		}
		BGM.volume = MainManager.BGM_Volume;
	}
	public void Stop()
	{
		BGM.Stop ();
	}

	public float timeProgress()
	{
		return BGM.time / BGM.clip.length;
	}

	public float songStop()
	{
		return BGM.clip.length - BGM.time;
	}
}