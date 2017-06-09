using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public InitManager initManagerInstance;
	public GameObject playerPrefab;
	public Transform startPosSpawner;

	private GameObject playerObj; //->replace obj type with Player class
	private float platformLength=20;
	private float platformSingleSize=20.48f;

	// Use this for initialization
	void Start () {
		SpawnPlayer();
	}

	void SpawnPlayer (){
		playerObj = Instantiate(playerPrefab) as GameObject; //->replace obj type with Player class
		playerObj.transform.localPosition = startPosSpawner.localPosition;
		initManagerInstance.ChangeState();
	}
}
