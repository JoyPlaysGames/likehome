using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCheck : MonoBehaviour
{
	public bool outside;
	public bool inside;

	public AudioClip insideClip;
	public AudioClip outsideClip;

	private void Start()
	{
			Debug.Log("Play Sound");
			The.soundManager.PlayLoop(insideClip);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (outside)
			{
				Debug.Log("I am outside");
				Debug.Log("Start outside audio");
				The.soundManager.PlayLoop(outsideClip);
			}
			if (inside)
			{
				Debug.Log("Start inside audio");
				The.soundManager.PlayLoop(insideClip);					
			}
		}
	}



}
