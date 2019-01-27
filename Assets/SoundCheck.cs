using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundCheck : MonoBehaviour
{
	public AudioClip insideClip;
	public AudioClip outsideClip;
	public AudioClip mainMenuSound;

	public GameObject inside;
	public GameObject outside;

	private void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			Debug.Log("Play Sound");
			The.soundManager.PlayLoop(insideClip);
		}
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			Debug.Log("Play Sound");
			The.soundManager.PlayLoop(mainMenuSound);
		}

	}

	private void LateUpdate()
	{
		if (The.soundManager.SourceLoop != null && SceneManager.GetActiveScene().buildIndex == 1)
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
