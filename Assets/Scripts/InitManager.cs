using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InitStates{
	Platform,Background,Building
}

public class InitManager : MonoBehaviour {
	public MainPlatformGenerator platformInstance;
	public Building buildingInstance;
	public BackgroundGenerator backgroundInstance;

	private int initState=-1;
	private float platformLength=20;
	private float platformSingleSize=20.48f;

	public void ExecuteNextState ()
	{
		float platformTotalSize = platformLength * platformSingleSize;
		if (initState == (int)InitStates.Platform) {
			platformInstance.GeneratePlatform (platformLength,platformSingleSize);
		} else if (initState == (int)InitStates.Background) {
			backgroundInstance.GenerateBackgrounds(platformTotalSize);
		} else if (initState == (int)InitStates.Building) {
			buildingInstance.GenerateBuilding (platformTotalSize);
		} 
	}

	public void ChangeState(){
		initState++;
		ExecuteNextState();
	}

}
