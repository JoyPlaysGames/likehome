using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] GameObject player = null;

	private Vector3 offset;
	public Vector3 extraOffset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 p = player.transform.position + offset + extraOffset;
        if (player.transform.gameObject.GetComponent<Player>().inside)
        {
            float cPos = p.x;
            if (cPos < -1.5f) p.x = -1.5f;
            if (cPos > 1.5f) p.x = 1.5f;
        }

        transform.position = p; 
	}
}
