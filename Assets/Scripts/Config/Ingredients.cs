using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientKind
{
    None = 0,
}


public class Ingredients : MonoBehaviour
{
    public List<IngredientConfig> ingredientConfigs;

    private void Awake()
    {
        The.ingredients = this;
    }

    public IngredientConfig GetIngredientConfig(IngredientKind kind)
    {

        for (int i = 0; i < ingredientConfigs.Count; i++)
        {
            if (ingredientConfigs[i].kind == kind)
            {
                return ingredientConfigs[i];
            }
        }
        return null;
    }
}

[Serializable]
public class IngredientConfig
{
    public IngredientKind kind = IngredientKind.None;

}