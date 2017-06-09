using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	private GameObject playerObj;
	private Vector3 playerPos;
	private Vector3 targetPos;
	private float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerObj == null) {
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}

		playerPos = playerObj.transform.localPosition;
		targetPos = new Vector3(playerPos.x,5.5f,-10);
		transform.position = Vector3.SmoothDamp(transform.position,targetPos,ref velocity,smoothTime);
	}
}
