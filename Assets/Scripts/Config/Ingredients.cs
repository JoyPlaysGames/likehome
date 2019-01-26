using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IngredientKind
{
    None = 0,
    SpiderLeg = 1,
    SpiderEye = 2,
    Snake = 3,
	Branch = 4,
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
    public Sprite icon;
    public GameObject prefab;
}