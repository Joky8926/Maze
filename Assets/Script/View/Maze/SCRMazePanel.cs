using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRMazePanel : MonoBehaviour {
	private CSPrefabFactory preFactory;
	private GameObject goMazeGrid;
	private GameObject goMazePlayer;

	void Awake() {
		preFactory = CSPrefabFactory.Instance();
		AddChild();
	}

	private void AddChild() {
		goMazeGrid = preFactory.GetMazeGrid();
		goMazePlayer = preFactory.GetMazePlayer();
		CSGameObject.AddGameObject(goMazeGrid, this.gameObject);
		CSGameObject.AddGameObject(goMazePlayer, this.gameObject);
	}
}
