using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] GameObject playerModel;
	[SerializeField] Animator playerAnimator;

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
        if (performingAction) return;

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

		Interaction();
	}

	private void Interaction()
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
			if (visibleObject != null)
			{
				Item visibleItem = visibleObject.GetComponent<Item>();
				Table visibleTable = visibleObject.GetComponent<Table>();
				if (visibleItem == null)
				{
					visibleItem = visibleTable.item;
				}

				if (item == null && visibleItem != null)
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

				if (item != null && visibleTable != null && visibleTable.item == null)
				{
					item.transform.SetParent(visibleTable.itemSlot.transform);
					item.transform.position = visibleTable.itemSlot.transform.position;
					playerAnimator.SetTrigger("PlaceItem");
					visibleTable.item = item;
					item = null;
				}
			}else if(item == null)
			{
				CheckInteractableObjects();
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
    private void Update()
    {
        if (performingAction) return; 

        //CheckInteractableObjects();
    }

    void CheckInteractableObjects()
    {
        TaskEnviromentSpot spot = null;
        float d = 7f;
        for (int i = 0; i < The.taskManager.taskSpots.Count; i++)
        {
            TaskEnviromentSpot s = The.taskManager.taskSpots[i];
            Debug.Log(Vector3.Distance(transform.localPosition, s.transform.localPosition));
            Debug.Log(transform.localPosition);
            Debug.Log(s.transform.localPosition);
            if (Vector3.Distance(transform.localPosition, s.transform.localPosition) < d && s.taskId >= 0)
            {
                spot = s; break;
            }
        }

        if (spot != null)
        {
            performingAction = true;
            StartCoroutine(PerformASpotTimer(spot));
            return;
        }
    }

    

    IEnumerator PerformASpotTimer(TaskEnviromentSpot spot)
    {
        yield return new WaitForSeconds(3f);
        performingAction = false;
        spot.FinishTask();
    }
}
