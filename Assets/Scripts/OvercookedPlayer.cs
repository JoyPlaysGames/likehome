using UnityEngine;

public class OvercookedPlayer : MonoBehaviour {

	[SerializeField] GameObject playerModel;
	[SerializeField] Animator playerAnimator;

	[SerializeField] GameObject hands = null;
 
	[SerializeField] float speed = 10f;

	private bool buttonPressed = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");

		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

		float realTimeSpeed = Time.deltaTime * speed;
		transform.Translate(movement * realTimeSpeed);

		
		

		if (Input.GetKey(KeyCode.F))
		{
			buttonPressed = true;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Item") && buttonPressed)
		{
			buttonPressed = false;
			Destroy(other.gameObject);
		}
	}
}
