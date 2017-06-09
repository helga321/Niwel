using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partition : MonoBehaviour {
	public Sprite[] partitionHorz;
	public Sprite[] partitionVert;
	public GameObject partitionRootPrefab;
	public GameObject partitionPrefab;
	public GameObject ladderRootPrefab;
	public GameObject ladderPrefab;

	float spriteDistance = 2.2f;
	float ladderDistance = 1.6f;
	int spriteCount = 0;
	int minPartHorzGap = 2;
	int maxPartHorzGap = 3;

	public void InitPartition (float distanceXFromPivot, float distanceYFromPivot, GameObject roomRootObj, bool createHorzGap,bool createVertGap){
		GeneratePartitionHorz(-distanceXFromPivot,distanceYFromPivot,partitionHorz,false,roomRootObj,createHorzGap);
		GeneratePartitionVert(distanceXFromPivot*2,distanceYFromPivot*2,partitionVert,true,roomRootObj,createVertGap);
		GeneratePartitionVert(0,distanceYFromPivot*2,partitionVert,false,roomRootObj,createVertGap);
		GeneratePartitionHorz(-distanceXFromPivot,-distanceYFromPivot,partitionHorz,true,roomRootObj,createHorzGap);
	}

	void GeneratePartitionHorz (float distanceXFromPivot, float distanceYFromPivot, Sprite[] currPartSprite, bool groundFloor, 
	GameObject roomRootObj,bool createHorzGap)
	{
		int randSprite = 0;
		int randPartStart = 0;
		int randPartDistance = 0;
		GameObject rootObj = Instantiate (partitionRootPrefab) as GameObject;
		spriteCount = Mathf.Abs (Mathf.CeilToInt (distanceXFromPivot * 2 / spriteDistance)) + 1;


		if (!groundFloor && createHorzGap) {
			randPartStart = Random.Range (2, spriteCount - maxPartHorzGap);
			randPartDistance = Random.Range (minPartHorzGap, maxPartHorzGap + 1);
		}

		for (int i = 0; i < spriteCount; i++) {
			if ((i < randPartStart) || (i >= (randPartStart + randPartDistance))) {
				GameObject partObj = Instantiate (partitionPrefab) as GameObject;
				randSprite = Random.Range (0, currPartSprite.Length);
				if (groundFloor) {
					partObj.transform.localPosition = new Vector3 ((spriteDistance * i), 0, 0f);
				} else {
					partObj.transform.localPosition = new Vector3 ((spriteDistance * i), distanceYFromPivot * 2, 0f);
				}
				partObj.GetComponent<SpriteRenderer> ().sprite = currPartSprite [randSprite];
				partObj.transform.SetParent (rootObj.transform, false);
			}
			if (i == randPartStart && !groundFloor && createHorzGap) {
				GameObject ladderRootObj = Instantiate(ladderRootPrefab) as GameObject;
				ladderRootObj.transform.SetParent(roomRootObj.transform,false);
				GenerateLadder((spriteDistance*i),distanceYFromPivot*2,ladderRootObj);
			}
		}
		createHorzGap=false;
		rootObj.transform.SetParent(roomRootObj.transform,false);
	}

	void GeneratePartitionVert 
	(float distanceXFromPivot, float distanceYFromPivot, Sprite[] currPartSprite, bool flipPartition,
	 GameObject roomRootObj, bool createVertGap)
	{
		int rand = 0;
		GameObject rootObj = Instantiate (partitionRootPrefab) as GameObject;
		spriteCount = Mathf.Abs (Mathf.CeilToInt (distanceYFromPivot / spriteDistance))-1;

		for (int i = 0; i < spriteCount; i++) {

			if (!createVertGap) {
				GameObject partObj = Instantiate (partitionPrefab) as GameObject;
				rand = Random.Range (0, currPartSprite.Length);
				partObj.transform.localPosition = new Vector3 (distanceXFromPivot, (spriteDistance * i), 0f);
				if (flipPartition) {
					partObj.transform.localScale = new Vector3 (-1, 1, 1);
				}
				partObj.GetComponent<SpriteRenderer> ().sprite = currPartSprite [rand];
				partObj.transform.SetParent (rootObj.transform, false);
			}
			createVertGap=false;
		}

		rootObj.transform.SetParent(roomRootObj.transform,false);
	}

	void GenerateLadder (float startXPos, float distanceYFromPivot, GameObject ladderRoot)
	{
		int spriteCount = Mathf.Abs (Mathf.CeilToInt (distanceYFromPivot / ladderDistance)) - 1;
		for (int i = 0; i < spriteCount; i++) {
			GameObject ladderObj = Instantiate (ladderPrefab) as GameObject;
			ladderObj.transform.localPosition = new Vector3(startXPos,(distanceYFromPivot-ladderDistance*i),0);
			ladderObj.transform.SetParent(ladderRoot.transform,false);
		}
	}
}
