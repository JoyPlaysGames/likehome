using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCheck : MonoBehaviour
{
	public AudioClip insideClip;
	public AudioClip outsideClip;

	public GameObject inside;
	public GameObject outside;

	private void Start()
	{
			Debug.Log("Play Sound");
			The.soundManager.PlayLoop(insideClip);
	}

	private void LateUpdate()
	{
		if (The.soundManager.SourceLoop != null)
		{
			BackgroundMusic();
		}
		
	}

	private void BackgroundMusic()
	{

		Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{

			if (hit.collider.tag.Equals("Outside"))
			{
				if (The.soundManager.SourceLoop.clip.length > 40)
				{
					The.soundManager.PlayLoop(outsideClip);
				}
			};
			if (hit.collider.tag.Equals("Inside"))
			{
				if (The.soundManager.SourceLoop.clip.length < 40)
				{
					The.soundManager.PlayLoop(insideClip);
				}
			}
		}
	}   
}
