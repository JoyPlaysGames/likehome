﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
	public Dictionary<IngredientKind, int> ingredients; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		RecipieFeedSpot rFS = other.gameObject.GetComponent<RecipieFeedSpot>();
		if (rFS)
		{
			Destroy(gameObject);
			rFS.ConsumeRecipie(ingredients);
		}
	}
}
