using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	public bool mixtureTable = false;

	public GameObject itemSlot = null;
	public Item item = null;

	public int tableItemSlots = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!GetComponentInChildren<Item>())
		{
			item = null;
		}
		else
		{
			item = GetComponentInChildren<Item>();
		}
	}

	private void CreateItemSlots()
	{
		
	}
}
