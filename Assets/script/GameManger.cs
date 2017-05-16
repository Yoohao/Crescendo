using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour {

	public static GameManger instance = null;
	public AudioClip[] Music_Clip;
	public AudioClip[] Music_Demo;
	public string[] Music_Name;
	public int[] stars;
	public Sprite[] Background;
	public Sprite[] ResultImage;
	public int[] Sensors;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
