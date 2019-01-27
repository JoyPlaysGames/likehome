using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player _instance;

	public Animator playerAnimator;
	public Animator houseAnimator;

	[SerializeField] GameObject playerModel;
	[SerializeField] GameObject playerBroom;
	[SerializeField] float speed = 10f;

	public GameObject hittuPointu;
	public GameObject usabilityFignja;
	public GameObject hands = null;
	public Item item = null;

	public bool potInHand = false;

	private bool animationPick = false;
	private bool animationPlace = false;
	private bool iAmAttacking;
	private bool doorClosed = true;
	private bool performingAction = false;

	private GameObject visibleObject;

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this; 
		}
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
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (visibleObject != null && visibleObject.tag != "Door")
			{
				Item visibleItem = visibleObject.GetComponent<Item>();
				Table visibleTable = visibleObject.GetComponent<Table>();
				TaskEnviromentSpot visibleSpot = visibleObject.GetComponent<TaskEnviromentSpot>();
				Pot visiblePot = visibleObject.GetComponentInChildren<Pot>();
				RecipieMixPot rMP = visibleObject.GetComponent<RecipieMixPot>();

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
                    visibleTable.TakeItem(item);
                    item = null;
                    return;
				}

				if (visibleTable != null && item == null && visiblePot != null)
				{
					visiblePot.gameObject.transform.SetParent(hands.transform);
					visiblePot.gameObject.transform.position = visiblePot.startingLocation;
					visiblePot.ingredients = visibleTable.GetComponent<RecipieMixPot>().ingredients;
					playerAnimator.SetTrigger("PickItem");
					potInHand = true;
					return;
				}

				if (potInHand && item == null && rMP != null)
				{
					Pot newPot = hands.GetComponentInChildren<Pot>();
					newPot.gameObject.transform.SetParent(rMP.itemSlot.transform);
					newPot.gameObject.transform.position = newPot.startingLocation;
					newPot.gameObject.transform.rotation = newPot.startingRotation;
					newPot.ingredients = rMP.ingredients;
					playerAnimator.SetTrigger("PlaceItem");
					potInHand = false;
					
				}

				if (item == null && visibleSpot != null && !potInHand)
				{
                    if (visibleSpot.taskId < 0) return;
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
        yield return new WaitForSeconds(0.4f);
        The.enemyManager.PlayerAtksEnemy(hittuPointu.transform.position);
        yield return new WaitForSeconds(0.4f);
		playerBroom.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		iAmAttacking = false;
	}

   

    IEnumerator PerformASpotTimer(TaskEnviromentSpot spot)
    {
		if(spot.kind == TaskKind.FloorMop)
		{
			playerBroom.SetActive(true);
		}
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
			else
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
