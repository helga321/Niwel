using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour{
	public RoomInside[] roomPrefab;
	public GameObject roomRootObj;

	private RoomInside[,] roomObj;
	private int minRoomHorz = 1;
	private int maxRoomHorz = 2;
	private int minRoomVert = 1;
	private int maxRoomVert = 2;
	private int activeWallCount = 0;
	private float buildingCount = 5;
	private float buildingTotalWidth = 0;

	public void GenerateBuilding (float platformLength)
	{
		float positionRange = platformLength / buildingCount;
		for (int i = 0; i < buildingCount; i++) {
			CreateRooms(i*positionRange);
		}
	}

	private void CreateRooms (float startingBuildingXPos)
	{
		float currXPos = startingBuildingXPos;
		float[] currYPos;
		float currRandXPos = 0;
		bool activeWall = false;
		int currRoom = 0;

		int horzCount = Random.Range (minRoomHorz, maxRoomHorz + 1);
		int vertCount = Random.Range (minRoomVert, maxRoomVert + 1);
		GameObject currRoomParent = Instantiate (roomRootObj) as GameObject;

		currYPos = new float[horzCount];
		roomObj = new RoomInside[vertCount, horzCount];

		for (int i = 0; i < vertCount; i++) {
			for (int j = 0; j < horzCount; j++) {
				currRoom = Random.Range (0, roomPrefab.Length);

				if (currRoom > -1) {
					roomObj [i, j] = Instantiate (roomPrefab [currRoom]) as RoomInside;	
					roomObj [i, j].SetDistanceFromPivot ();
					roomObj [i, j].SetRoomRowCol (i, j);

					if (j == 0) {
						buildingTotalWidth += roomObj [i, j].GetDistanceXFromPivot ();
					}

					if (i != 0) {
						currYPos [j] += (roomObj [i - 1, j].GetDistanceYFromPivot () * 2);
					}

//					if (i > 0) {
//						Debug.Log("i:"+i);
//						Debug.Log("j:"+j); 
//						float tempY1 = roomObj [i-1, j+1].GetDistanceYFromPivot ();
//						float tempY0 = roomObj [i-1, j].GetDistanceYFromPivot ();
//						float tempX1 = roomObj[i+1,j].GetDistanceXFromPivot();
//						float tempX0 = roomObj[i,j].GetDistanceXFromPivot();
//						if ((tempY1 > tempY0) && (tempX1>tempX0)) {
//							float deltaX = tempX1-tempX0;
//							currXPos = roomObj[i-1,j].GetDistanceXFromPivot()*2;
//							PlaceRoom(currRoomParent,roomObj[i-1,j+1],currXPos,currYPos[j]);
//						}
//					}

					PlaceRoom (currRoomParent, roomObj [i, j], currXPos, currYPos [j]);

					if (i == 0 && (j == 0 || j == (horzCount - 1))) {
						roomObj [i, j].SetCreateVertGapBool (true);
					}

					if (i < vertCount - 1) {
						roomObj [i, j].SetCreateHorzGapBool (true);
					}

					roomObj [i, j].InitRoom ();

					if (j > 0) {
						if (Random.value > 0.1f) {
							activeWall = true;
						} else {
							activeWall = false;
						}

						PlaceWallOrPassage (roomObj [i, j], roomObj [i, j - 1], activeWall);

						if (activeWallCount == vertCount) {
							CheckVertPartitions(horzCount,vertCount);
						}
					}

					if (i > 0) {
						CheckHorzPartitions (roomObj [i - 1, j], roomObj [i, j]);
					}

					currXPos += (roomObj[i,j].GetDistanceXFromPivot()*2);
				} 

			}
			currXPos = startingBuildingXPos;
		}
	}

	private void PlaceRoom (GameObject roomParent, RoomInside newRoom,float xPos,float yPos){
		newRoom.transform.localPosition = new Vector3(xPos,yPos,0);
		newRoom.transform.SetParent(roomParent.transform,true);
	}

	private void PlaceWallOrPassage (RoomInside currentRoom, RoomInside adjacentRoom, bool activeWall){
		if (activeWall) {
			currentRoom.transform.GetChild ((int)WallDirection.West).gameObject.SetActive (true);
			adjacentRoom.transform.GetChild ((int)WallDirection.East).gameObject.SetActive (true);
			activeWallCount++;
		} else {
			currentRoom.transform.GetChild ((int)WallDirection.West).gameObject.SetActive (false);
			adjacentRoom.transform.GetChild ((int)WallDirection.East).gameObject.SetActive (false);
		}
	}

	private void CheckHorzPartitions (RoomInside room1, RoomInside room2)
	{
		int room1ChildCount = room1.transform.childCount;
		int room2ChildCount = room2.transform.childCount;
		Transform part1=null;
		Transform part2=null;

		if (room1ChildCount == 5) {
			part1 = room1.transform.GetChild ((int)WallDirection.North);
			Debug.Log("part1childidx:"+(int)WallDirection.North);
		} else if (room1ChildCount == 6) {
			Debug.Log("room1");
			part1 = room1.transform.GetChild ((int)WallDirection.North+1);
			Debug.Log("part1childidx:"+((int)WallDirection.North+1));
		}

		if (room2ChildCount == 5) {
			part2 = room1.transform.GetChild ((int)WallDirection.South);
			Debug.Log("part2childidx:"+(int)WallDirection.South);
		} else if (room2ChildCount == 6) {
			Debug.Log("room2");
			part2 = room1.transform.GetChild ((int)WallDirection.South+1);
			Debug.Log("part2childidx:"+((int)WallDirection.South+1));
		}

		int room1SpriteCount = part1.childCount;
		int room2SpriteCount = part2.childCount;
		int dCount = room1SpriteCount - room2SpriteCount;

		if (dCount > 0 || dCount == 0) {
			part2.gameObject.SetActive (false);
		} else {
			for (int i = 0; i < (room2SpriteCount - Mathf.Abs(dCount)); i++) {
				part2.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	private void CheckVertPartitions (int horzCount, int vertCount)
	{
		int randRow = Random.Range (0, vertCount - 1);
		int randCol = Random.Range (0, horzCount - 1);
		if (roomObj [randRow,randCol] != null) {
			CreateVerticalGap(randRow,randCol,(int)WallDirection.East);
		}
		if (roomObj [randRow,randCol+1] != null) {
			CreateVerticalGap(randRow,randCol+1,(int)WallDirection.West);
		}
	}

	private void CreateVerticalGap (int row, int col, int idx){
		Transform obj = roomObj[row,col].transform.GetChild(idx);
		obj.GetChild(0).gameObject.SetActive(false);
	}
}