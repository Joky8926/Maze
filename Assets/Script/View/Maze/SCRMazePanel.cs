using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRMazePanel : MonoBehaviour {
	private CSPrefabFactory preFactory;
	private GameObject goMazeGrid;
	private GameObject goMazePlayer;

	void Awake() {
		preFactory = CSPrefabFactory.Instance();
		CreateMazeGrid();
		CreateMazePlayer();
	}

	private void CreateMazeGrid() {
		goMazeGrid = preFactory.GetMazeGrid();
		Vector3 localPos = goMazeGrid.transform.localPosition;
		goMazeGrid.transform.parent = this.transform;
		goMazeGrid.transform.localScale = Vector3.one;
		goMazeGrid.transform.localPosition = localPos;
	}

	private void CreateMazePlayer() {
		goMazePlayer = preFactory.GetMazePlayer();
		Vector3 localPos = goMazePlayer.transform.localPosition;
		goMazePlayer.transform.parent = this.transform;
		goMazePlayer.transform.localScale = Vector3.one;
		goMazePlayer.transform.localPosition = localPos;
	}
}
