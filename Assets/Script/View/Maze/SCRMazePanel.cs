using System.IO;
using UnityEngine;

public class SCRMazePanel : MonoBehaviour {
	private CSPrefabFactory preFactory;
	private SCRGrid scrGrid;
	private SCRPlayer scrPlayer;

	void Awake() {
		preFactory = CSPrefabFactory.Instance();
		AddChild();
	}

	public void DoMove() {
		MoveNext();
	}

	public void OnMoveEnd() {
		MoveNext();
	}

	private void MoveNext() {
		if (scrGrid.hasNextPos) {
			scrPlayer.DoMove(scrGrid.nextPos);
		}
	}

	private void AddChild() {
		AddMazeGrid();
		AddMazePlayer();
	}

	private void AddMazeGrid() {
		GameObject goMazeGrid = preFactory.GetMazeGrid();
		CSGameObject.AddChildResetPos(goMazeGrid, this.gameObject);
		scrGrid = goMazeGrid.GetComponent<SCRGrid>();
		LoadMap();
	}

	private void AddMazePlayer() {
		GameObject goMazePlayer = preFactory.GetMazePlayer();
		CSGameObject.AddChildGO(goMazePlayer, this.gameObject);
		scrPlayer = goMazePlayer.GetComponent<SCRPlayer>();
		scrPlayer.Init(this, scrGrid.firstPos);
	}

	private void LoadMap() {
		StreamReader sr = File.OpenText(Application.dataPath + "//map1");
		string str = sr.ReadLine();
		sr.Close();
		InitMap(str);
	}

	private void InitMap(string str) {
		string[] arrStr = str.Split('|');
		string[] arrGridStr = arrStr[0].Split(':');
		int uRow = int.Parse(arrGridStr[0]);
		int uCol = int.Parse(arrGridStr[1]);
		int[] arrValue = new int[arrStr.Length - 2];
		for (int i = 1; i < arrStr.Length - 1; i++) {
			arrValue[i - 1] = int.Parse(arrStr[i]);
		}
		scrGrid.Init(this, uRow, uCol, arrValue);
	}
}
