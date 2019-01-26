using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] GameObject playerModel;
	[SerializeField] Animator playerAnimator;

	public static bool actionButtonPressed = false;
	public static bool hasItem = false;
	private bool animationPick = false;
	private bool animationPlace = false;

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

		if (movement != Vector3.zero)
		{
			playerAnimator.SetFloat("Move", 1f);
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, Quaternion.LookRotation(movement), 0.15F);
		}
		else
		{
			playerAnimator.SetFloat("Move", 0f);
		}

		if (hands.GetComponentInChildren<Item>())
		{
			hasItem = true;
		}
		
	}

	private void OnTriggerStay(Collider other)
	{
		Table table = other.gameObject.GetComponent<Table>();

		if (Input.GetKeyDown(KeyCode.F))
		{
			if (table && !table.slotIsEmpty && !hasItem)
			{
				item = other.gameObject.GetComponentInChildren<Item>();
				item.transform.SetParent(hands.transform);
				item.transform.position = new Vector3(hands.transform.position.x, hands.transform.position.y, hands.transform.position.z);
				table.slotIsEmpty = true;
				hasItem = true;
				playerAnimator.SetTrigger("PickItem");
				Debug.Log("Item Picked");
			}
			else if (table && table.slotIsEmpty && hasItem)
			{
				item.transform.SetParent(table.itemSlot.transform);
				item.transform.rotation = Quaternion.identity;
				float itemSize = item.transform.localScale.x / 2;
				item.transform.position = new Vector3(table.itemSlot.transform.position.x, table.itemSlot.transform.position.y + itemSize, table.itemSlot.transform.position.z);
				table.slotIsEmpty = false;
				hasItem = false;
				playerAnimator.SetTrigger("PlaceItem");
				Debug.Log("Item Placed");
			}
		}
	}
}
