using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	public bool slotIsEmpty = true;

	public GameObject itemSlot = null;

	// Use this for initialization
	void Start () {
		if (GetComponentInChildren<Item>())
		{
			slotIsEmpty = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	//private void OnCollisionStay(Collision collision)
	//{
	//	if (Player.actionButtonPressed && collision.gameObject.GetComponent<Player>() && Player.hasItem && slotIsEmpty)
	//	{
	//		Item item = collision.gameObject.GetComponentInChildren<Item>();
	//		item.transform.parent = itemSlot.transform;
	//		item.transform.position = itemSlot.transform.position;
	//		slotIsEmpty = false;
	//	}
	//}
}
