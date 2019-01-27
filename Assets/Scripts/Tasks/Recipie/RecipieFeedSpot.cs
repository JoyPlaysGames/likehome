using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieFeedSpot : MonoBehaviour
{
	public Pot pot;

    public void ConsumeRecipie(Dictionary<IngredientKind, int> ingredients)
    {
		Player._instance.potInHand = false;
		Player._instance.playerAnimator.SetTrigger("PlaceItem");
		The.taskManager.ConsumeRecipie(ingredients);
		if(ingredients.Count != 0)
		{
			pot.ingredients = new Dictionary<IngredientKind, int>();
		}
    }
}
