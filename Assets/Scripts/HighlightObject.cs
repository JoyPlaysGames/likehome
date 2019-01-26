using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
	public GameObject highlight;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			highlight.SetActive(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			highlight.SetActive(false);
		}
	}

}
