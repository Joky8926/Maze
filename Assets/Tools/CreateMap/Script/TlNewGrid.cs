using UnityEngine;
using System.Collections;

public class TlNewGrid : MonoBehaviour {
	public UIInput _lblCol;
	public UIInput _lblRow;
	public GameObject _goGridPanel;
	private CSPrefabFactory preFactory;
	private SCRGrid scrGrid;
	private int uCol;
	private int uRow;

	void Awake() {
		preFactory = CSPrefabFactory.Instance();
		AddMazeGrid();
		_lblRow.value = "1";
		_lblCol.value = "1";
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("MouseDown");
		}
		if (Input.GetMouseButton(0)) {
			Debug.Log("Mouse");
		}
		if (Input.GetMouseButtonUp(0)) {
			Debug.Log("MouseUp");
		}
	}

	public void OnChangeGrid() {
		if (_lblCol.value == "" || _lblRow.value == "") {
			return;
		}
		uCol = int.Parse(_lblCol.value);
		uRow = int.Parse(_lblRow.value);
		scrGrid.InitGrid(uRow, uCol);
	}

	private void AddMazeGrid() {
		GameObject goMazeGrid = preFactory.GetMazeGrid();
		CSGameObject.AddChildResetPos(goMazeGrid, _goGridPanel);
		scrGrid = goMazeGrid.GetComponent<SCRGrid>();
	}
}
