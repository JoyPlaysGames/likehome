using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieMixPot : Table
{
    public Dictionary<IngredientKind, int> ingredients = new Dictionary<IngredientKind, int>();

	void Start()
	{
		if (mixtureTable)
		{
			pot.SetActive(true);
		}	
	}

	public void TakeIngredient(Item item)
    {
        if(ingredients.ContainsKey(item.kind))
        {
            ingredients[item.kind] += 1;
        }
        else
        {
            ingredients.Add(item.kind, 1);
        }
		Debug.Log(ingredients[item.kind]);
		Destroy(item.transform.gameObject);
    }

    public override void TakeItem(Item item)
    {
        TakeIngredient(item);
    }


}
