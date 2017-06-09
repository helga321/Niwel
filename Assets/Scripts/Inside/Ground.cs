using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
	public Sprite[] groundSprites = new Sprite[3];
	public GameObject groundPrefab;
	public GameObject groundRootPrefab;

	float groundDistance = 2.2f;

	public void GenerateGround (float distanceXFromPivot, float distanceYFromPivot, GameObject roomRootObj)
	{
		int rand = 0;
		int count = Mathf.CeilToInt (distanceXFromPivot * 2 / groundDistance) - 1;
		GameObject rootObj = Instantiate (groundRootPrefab) as GameObject;

		for (int i = 0; i < count; i++) {
			GameObject obj = Instantiate (groundPrefab) as GameObject;
			rand = Random.Range (0, groundSprites.Length);
			obj.GetComponent<SpriteRenderer> ().sprite = groundSprites [rand];
			obj.transform.localPosition = new Vector3((groundDistance*i),0,0);
			obj.transform.SetParent(rootObj.transform,false);
		}
		rootObj.transform.SetParent(roomRootObj.transform,false);
	}
}
