using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieFeedSpot : MonoBehaviour
{

    public void ConsumeRecipie(Dictionary<IngredientKind, int> ingredients)
    {
		Player._instance.potInHand = false;
        The.taskManager.ConsumeRecipie(ingredients);
    }
}
