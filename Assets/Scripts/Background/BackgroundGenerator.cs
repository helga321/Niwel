using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {
	public InitManager initManagerInstance;

	public GameObject bgRoot;
	public GameObject skyRootPrefab;
	public GameObject ambientRootPrefab;
	public GameObject buildingBGRootPrefab;
	public GameObject skyPrefab;
	public GameObject ambientPrefab;
	public GameObject buildingBGPrefab;

	public Sprite skySprite;
	public Sprite[] ambientSprites;
	public Sprite[] groundBuildingSprites;
	public Sprite[] floatingBuildingSprites;

	private float yPos = -5f;
	
	public void GenerateBackgrounds (float platformLength){
		GameObject bgRootObj = Instantiate(bgRoot) as GameObject;
		GenerateSkyBackground(platformLength,bgRootObj);
		GenerateAmbientBackground(platformLength,bgRootObj);
		GenerateBuildingBackground(platformLength,bgRootObj);

		initManagerInstance.ChangeState();
	}

	public void GenerateSkyBackground (float platformLength,GameObject bgRootObj)
	{
		GameObject skyRootObj = Instantiate (skyRootPrefab) as GameObject;

		float skySpriteWidth = skySprite.bounds.size.x;
		int spriteCount = Mathf.CeilToInt (platformLength / skySpriteWidth);

		for (int i = -1; i < spriteCount; i++) {
			GameObject skyObj = Instantiate(skyPrefab) as GameObject;
			skyObj.transform.localPosition = new Vector3(i*skySpriteWidth,yPos,0);
			skyObj.transform.SetParent(skyRootObj.transform,false);
		}
		skyRootObj.transform.SetParent(bgRootObj.transform,false);
	}

	public void GenerateAmbientBackground (float platformLength,GameObject bgRootObj)
	{
		int spriteCount = CalculateSpriteCount(platformLength,ambientSprites);
		float currXPos = -40;
		int randomSprite = 0;

		GameObject ambientRootObj = Instantiate(ambientRootPrefab) as GameObject;

		for (int i = 0; i < spriteCount; i++) {
			GameObject ambientObj = Instantiate(ambientPrefab) as GameObject;
			randomSprite = Random.Range(0,ambientSprites.Length);
			ambientObj.GetComponent<SpriteRenderer>().sprite=ambientSprites[randomSprite];
			float spriteWidth = ambientObj.GetComponent<SpriteRenderer>().bounds.size.x;
			ambientObj.transform.localPosition = new Vector3(currXPos,yPos,0);
			currXPos+=spriteWidth;
			ambientObj.transform.SetParent(ambientRootObj.transform,false); 
		}
		ambientRootObj.transform.SetParent(bgRootObj.transform,false);
	}

	public void GenerateBuildingBackground (float platformLength,GameObject bgRootObj)
	{
		float currXPos = -40;
		int randomSprite = 0;
		int spriteCount = CalculateSpriteCount (platformLength, groundBuildingSprites);
		GameObject buildingRootObj = Instantiate (buildingBGRootPrefab) as GameObject;

		for (int i = 0; i < spriteCount; i++) {
			GameObject buildingObj = Instantiate(buildingBGPrefab) as GameObject;
			randomSprite = Random.Range(0,groundBuildingSprites.Length);
			buildingObj.GetComponent<SpriteRenderer>().sprite = groundBuildingSprites[randomSprite];
			float spriteWidth = buildingObj.GetComponent<SpriteRenderer>().bounds.size.x;
			buildingObj.transform.localPosition = new Vector3(currXPos,yPos,0);
			currXPos+=spriteWidth;
			buildingObj.transform.SetParent(buildingRootObj.transform,false);
		}
		buildingRootObj.transform.SetParent(bgRootObj.transform,false);
	}

	int CalculateSpriteCount (float platformLength, Sprite[] spriteArray)
	{
		float totalWidth=0;
		float avgWidth=0;
		for (int i = 0; i < spriteArray.Length; i++) {
			totalWidth += spriteArray [i].bounds.size.x;
		}
		avgWidth = totalWidth / groundBuildingSprites.Length;

		return Mathf.CeilToInt (platformLength / avgWidth);
	}
}
