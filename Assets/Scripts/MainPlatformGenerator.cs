using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlatformGenerator : MonoBehaviour {
	public GameObject platformPrefab;
	public GameObject platformParentPrefab;
	public InitManager initManagerInstance;
	
	public void GeneratePlatform (float platformLength, float platformSingleSize)
	{
		GameObject parentObj = Instantiate (platformParentPrefab) as GameObject;

		for (int i = -1; i < platformLength; i++) {
			GameObject platformObj = Instantiate(platformPrefab) as GameObject;
			platformObj.transform.localPosition = new Vector3(i*platformSingleSize,0,0);
			platformObj.transform.SetParent(parentObj.transform,false);
		}

		initManagerInstance.ChangeState();
	}

}
