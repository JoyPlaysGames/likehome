using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
	public Dictionary<IngredientKind, int> ingredients = new Dictionary<IngredientKind, int>(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay(Collider other)
	{
		RecipieFeedSpot rFS = other.gameObject.GetComponent<RecipieFeedSpot>();
		if (rFS && Input.GetKeyDown(KeyCode.F))
		{
			
			rFS.ConsumeRecipie(ingredients);
            Destroy(gameObject);
        }
	}
}
