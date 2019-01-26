using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{

	public bool IsFire = false;
	public float duration = 1.0f;
	public float intensityCoefficient;
	private Light FirePlace;
	public float lightIntensifier = 1;

	private UnityEngine.AI.NavMeshAgent what;

	void Awake()
	{
		FirePlace = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update()
	{
		if (IsFire)
		{
			//intensityCoefficient = Mathf.Lerp(intensityCoefficient, 20, 10f * Time.deltaTime);
			float phi = Time.time / duration * 2 * Mathf.PI;
			float amplitude = Mathf.Cos(phi) * 0.5f + intensityCoefficient;
			amplitude = amplitude / lightIntensifier;
			FirePlace.intensity = amplitude;
		}
		else
		{
			float phi = Time.time / duration * 2 * Mathf.PI;
			float amplitude = Mathf.Cos(phi) * 0.5f + intensityCoefficient;
			FirePlace.intensity = amplitude;
		}

	}


}