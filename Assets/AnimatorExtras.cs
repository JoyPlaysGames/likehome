using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorExtras : MonoBehaviour
{
	public GameObject gameObject;
	void EnableGameObject()
	{
		gameObject.SetActive(true);
	}
	void DisableGameObject()
	{
		gameObject.SetActive(false);
	}
}
