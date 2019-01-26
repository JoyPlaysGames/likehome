using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	public GameObject itemSlot = null;
	public GameObject pot = null;

	public Item item = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		CheckItem();
	}

	private void CheckItem()
	{
		if (!GetComponentInChildren<Item>())
		{
			item = null;
		}
		else
		{
			item = GetComponentInChildren<Item>();
		}
	}

	public virtual void TakeItem(Item newItem)
    {
        item = newItem;
    }
}
