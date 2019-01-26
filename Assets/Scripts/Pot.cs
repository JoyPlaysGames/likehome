using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
	public Dictionary<IngredientKind, int> ingredients = new Dictionary<IngredientKind, int>();

	public Transform ItemSlot = null;

	private Quaternion startingRotation;
	public Vector3 startingLocation;

    // Start is called before the first frame update
    void Start()
    {
		startingLocation = transform.position;
		startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay(Collider other)
	{
		RecipieFeedSpot rFS = other.gameObject.GetComponent<RecipieFeedSpot>();
		if (rFS && Input.GetKeyDown(KeyCode.F))
		{
			
			rFS.ConsumeRecipie(ingredients);
			transform.parent = ItemSlot;
			transform.position = ItemSlot.position;
			transform.position = startingLocation;
			transform.rotation = startingRotation;
			ingredients.Clear();
        }
	}
}
