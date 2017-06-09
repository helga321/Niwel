using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour {
	public delegate void ParallaxCameraTranslate(float deltaMovement);
	public ParallaxCameraTranslate onCameraTranslate;
	private float oldPosition;
	// Use this for initialization
	void Start () {
		oldPosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x != oldPosition) {
			if (onCameraTranslate != null) {
				float delta = oldPosition - transform.position.x;
				onCameraTranslate(delta);
			}
			oldPosition = transform.position.x;
		}
	}
}
