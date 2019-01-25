using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecipieKind
{
    None = 0,
    SpiderLegSoup = 1,
    SpiderEyeSushi = 2,
    SnakeTailBBQ = 3,
}

public class Recipies : MonoBehaviour
{
    public List<RecipieConfig> recipieConfigs;

    private void Awake()
    {
        The.recipies = this;
    }

    public RecipieConfig GetRecipie(RecipieKind kind)
    {

        for(int i = 0; i < recipieConfigs.Count; i++)
        {
            if(recipieConfigs[i].kind == kind)
            {
                return recipieConfigs[i];
            }
        }
        return null;
    }
}


[Serializable]
public class RecipieConfig
{
    public RecipieKind kind = RecipieKind.None;
    public List<RecipieRequirements> reqirements;
}

[Serializable]
public class RecipieRequirements
{
    public IngredientKind ingredient = IngredientKind.None;
    public int count = 1;
}


