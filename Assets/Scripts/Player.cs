using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] GameObject playerModel;
	[SerializeField] Animator playerAnimator;
	[SerializeField] Animator houseAnimator;
	[SerializeField] GameObject playerBroom;
	public GameObject usabilityFignja;
	private bool iAmAttacking;
	public bool potInHand = false;
	private bool doorClosed = true;

	public static bool actionButtonPressed = false;
	private bool animationPick = false;
	private bool animationPlace = false;

	public GameObject hands = null;

	[SerializeField] float speed = 10f;

	public Item item = null;

	private GameObject visibleObject;

	public static Player _instance;

    bool performingAction = false;

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this; 
		}
	}

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void FixedUpdate()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!iAmAttacking && item == null)
			{
				StartCoroutine("Attack");
			}
			
		}

        if (performingAction) return;

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

		float realTimeSpeed = Time.deltaTime * speed;
		transform.Translate(movement * realTimeSpeed);

		if (movement != Vector3.zero)
		{
			playerAnimator.SetFloat("Move", 1f);
			playerModel.transform.rotation = Quaternion.Slerp(new Quaternion (0f, playerModel.transform.rotation.y, playerModel.transform.rotation.z, playerModel.transform.rotation.w), Quaternion.LookRotation(movement), 0.15F);
		}
		else
		{
			playerAnimator.SetFloat("Move", 0f);
		}

		Interaction();
	}

	private void Interaction()
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
			if (visibleObject != null && visibleObject.tag != "Door")
			{
				Item visibleItem = visibleObject.GetComponent<Item>();
				Table visibleTable = visibleObject.GetComponent<Table>();
				TaskEnviromentSpot visibleSpot = visibleObject.GetComponent<TaskEnviromentSpot>();
				Pot visiblePot = visibleObject.GetComponentInChildren<Pot>();

				if (visibleItem == null && visibleSpot == null && visiblePot == null && visibleTable != null)
				{
					visibleItem = visibleTable.item;
				}

				if (item == null && visibleItem != null && !potInHand)
				{
					item = visibleItem;
					item.transform.SetParent(hands.transform);
					item.transform.position = hands.transform.position;
					playerAnimator.SetTrigger("PickItem");
					visibleItem = null;
					if (visibleTable != null)
					{
						visibleTable.item = null;
					}
					return;
				}

				if (item != null && visibleTable != null && visibleTable.item == null && !potInHand)
				{
					item.transform.SetParent(visibleTable.itemSlot.transform);
					item.transform.position = visibleTable.itemSlot.transform.position;
					playerAnimator.SetTrigger("PlaceItem");
                    //visibleTable.item = item;
                    visibleTable.TakeItem(item);
                    item = null;
				}

				if(visibleTable != null && item == null && visiblePot != null)
				{
					visiblePot.gameObject.transform.SetParent(hands.transform);
					visiblePot.gameObject.transform.position = hands.transform.position;
					visibleTable.GetComponent<RecipieMixPot>().ingredients = visiblePot.ingredients;
					potInHand = true;
				}

				if (item == null && visibleSpot != null && !potInHand)
				{
					performingAction = true;
					StartCoroutine(PerformASpotTimer(visibleSpot));
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		visibleObject = other.gameObject;
	}

	private void OnTriggerExit(Collider other)
	{
		visibleObject = null;
	}

	IEnumerator Attack()
	{
		playerBroom.SetActive(true);
		playerAnimator.SetTrigger("Attack");
		yield return new WaitForSeconds(0.8f);
		playerBroom.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		iAmAttacking = false;
	}

    IEnumerator PerformASpotTimer(TaskEnviromentSpot spot)
    {
		playerBroom.SetActive(true);
		playerAnimator.SetTrigger("SweepFloor");
		yield return new WaitForSeconds(3f);
        performingAction = false;
		playerBroom.SetActive(false);
        spot.FinishTask();
    }

	private void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyDown(KeyCode.F) && other.CompareTag("Door"))
		{
			if (doorClosed)
			{
				StartCoroutine(DoorMechanism(true));
			}
			if (doorClosed)
			{
				StartCoroutine(DoorMechanism(false));
			}
		}
	}

	IEnumerator DoorMechanism(bool check)
	{
		if (check)
		{
			houseAnimator.SetTrigger("OpenDoor");
			yield return new WaitForSeconds(1f);
			doorClosed = false;
		}
		
		else 
		{
			houseAnimator.SetTrigger("CloseDoor");
			yield return new WaitForSeconds(1f);
			doorClosed = true;
		}
	}
}
