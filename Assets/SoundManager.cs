using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource Source;
	public AudioSource SourceLoop;

	public static SoundManager instance;

	[Header("Overall Music")]
	public AudioClip mainMenu;
	public AudioClip gameInHouse;
	public AudioClip gameOutsideHouse;

	[Header("Effects")]
	public AudioClip firePlace;
	public AudioClip birds;

	void Awake()
	{
		The.soundManager = this;
		DontDestroyOnLoad(gameObject);
	}

	public void PlayOnce(AudioClip clip)
	{
		Source.PlayOneShot(clip);
	}

	public void PlayOnce(AudioClip clip, AudioSource source)
	{
		source.PlayOneShot(clip);
	}

	public void PlayLoop(AudioClip clip)
	{
		SourceLoop.Stop();
		SourceLoop.loop = true;
		SourceLoop.clip = clip;
		SourceLoop.Play(0);
		

	}


}
