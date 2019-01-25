﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] GameObject player = null;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = player.transform.position + offset;
	}
}