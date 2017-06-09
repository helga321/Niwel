using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.A)) {
			transform.localPosition += Vector3.left * speed * Time.deltaTime;
			transform.localScale = new Vector3 (-0.25f, 0.25f, 1);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.localPosition += Vector3.right * speed * Time.deltaTime;
			transform.localScale = new Vector3 (0.25f, 0.25f, 1);
		}
	}
}
