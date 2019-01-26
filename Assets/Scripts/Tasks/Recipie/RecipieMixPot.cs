using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieMixPot : Table
{
    public Dictionary<IngredientKind, int> ingredients;


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
        Destroy(item.transform.gameObject);
    }

    public override void TakeItem(Item item)
    {
        TakeIngredient(item);
    }


}
