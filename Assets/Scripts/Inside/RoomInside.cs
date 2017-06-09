using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallDirection{
	North=1,
	East,
	West,
	South
}

public class RoomInside : MonoBehaviour {
	//public Sprite[] roomBG;
	Partition partition;
	Ground ground;

	float distanceXFromPivot;
	float distanceYFromPivot;
	int randSprite=0;
	int roomRow = 0;
	int roomCol = 0; 
	bool createHorzGap = false;
	bool createVertGap = false;
	Sprite currSprite;

	public void InitRoom(){
		partition = GetComponent<Partition>();
		ground = GetComponent<Ground>();

		ground.GenerateGround(distanceXFromPivot,distanceYFromPivot,this.gameObject);
		partition.InitPartition(distanceXFromPivot,distanceYFromPivot,this.gameObject,createHorzGap,createVertGap);
	}

	public void SetDistanceFromPivot (){
		currSprite = GetComponent<SpriteRenderer>().sprite;
		distanceXFromPivot = currSprite.bounds.extents.x;
		distanceYFromPivot = currSprite.bounds.extents.y;
	}

	public void SetRoomRowCol (int row, int col){
		roomRow = row;
		roomCol = col;
	}

	public void SetCreateHorzGapBool (bool createHorzGap){
		this.createHorzGap = createHorzGap;
	}

	public void SetCreateVertGapBool (bool createVertGap){
		this.createVertGap = createVertGap;
	}

	public int GetRoomRow (){
		return roomRow;
	}

	public int GetRoomCol(){
		return roomCol;
	}

	public float GetDistanceXFromPivot(){
		return this.distanceXFromPivot;
	}

	public float GetDistanceYFromPivot(){
		return this.distanceYFromPivot;
	}
}
