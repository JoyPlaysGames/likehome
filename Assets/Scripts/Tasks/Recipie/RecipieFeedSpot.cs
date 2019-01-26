using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieFeedSpot : Item
{

    public void ConsumeRecipie(Dictionary<IngredientKind, int> ingredients)
    {
        The.taskManager.ConsumeRecipie(ingredients);
    }
}
