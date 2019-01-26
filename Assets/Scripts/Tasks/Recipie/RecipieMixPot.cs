using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieMixPot : Item
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
    }

    
}
