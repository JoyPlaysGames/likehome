using UnityEngine;

public class Player : MonoBehaviour {

	public static bool actionButtonPressed = false;
	public static bool hasItem = false;

	[SerializeField] GameObject hands = null;
 
	[SerializeField] float speed = 10f;

	private Item item = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

		float realTimeSpeed = Time.deltaTime * speed;
		transform.Translate(movement * realTimeSpeed);
		hands.transform.position = transform.position;

		if(hands.GetComponentInChildren<Item>())
		{
			hasItem = true;
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
			actionButtonPressed = true;
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		Table table = collision.gameObject.GetComponent<Table>();

		if (table && actionButtonPressed && !table.slotIsEmpty && !hasItem)
		{
			item = collision.gameObject.GetComponentInChildren<Item>();
			item.transform.SetParent(hands.transform);
			item.transform.position = new Vector3 (hands.transform.position.x + 1f, hands.transform.position.y + 0.1f, hands.transform.position.z);
			table.slotIsEmpty = true;
			hasItem = true;
			actionButtonPressed = false;
		}
		else if (table && table.slotIsEmpty && actionButtonPressed && hasItem)
		{
			item.transform.SetParent(table.itemSlot.transform);
			float itemSize = item.transform.localScale.x / 2;
			item.transform.position = new Vector3(table.itemSlot.transform.position.x, table.itemSlot.transform.position.y + itemSize, table.itemSlot.transform.position.z);
			table.slotIsEmpty = false;
			hasItem = false;
			actionButtonPressed = false;
		}
	}
}
