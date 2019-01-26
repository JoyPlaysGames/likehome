using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieFeedSpot : MonoBehaviour
{

    public void ConsumeRecipie(Dictionary<IngredientKind, int> ingredients)
    {
		Player._instance.potInHand = false;
		Player._instance.playerAnimator.SetTrigger("PlaceItem");
		Player._instance.houseAnimator.SetTrigger("MonsterFeed");
		The.taskManager.ConsumeRecipie(ingredients);
    }
}
