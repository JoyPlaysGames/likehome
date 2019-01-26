using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieFeedSpot : MonoBehaviour
{

    List<IngredientKind> ingredients;


    // Start is called before the first frame update
    public void ConsumeRecipie(List<IngredientKind> ingredients)
    {
        if(The.recipies.DoesRecipieExistByIngredients(ingredients))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
