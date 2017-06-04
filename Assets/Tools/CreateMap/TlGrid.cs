using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TlGrid : MonoBehaviour {
	public UILabel _lblCol;
	public UILabel _lblRow;
	public UIToggle _tgUp;
	public UIToggle _tgDown;
	public UIToggle _tgLeft;
	public UIToggle _tgRight;
	private SCRGrid scrGrid;
	private int uCol;
	private int uRow;

	void Awake() {
		scrGrid = this.GetComponent<SCRGrid>();
		scrGrid.onClickBlock = OnClickBlock;
	}

	public void OnChangeGrid() {
		if (_lblCol.text == "" || _lblRow.text == "") {
			return;
		}
		uCol = int.Parse(_lblCol.text);
		uRow = int.Parse(_lblRow.text);
		scrGrid.InitGrid(uRow, uCol);
	}

	private void OnClickBlock(int uIndex) {
		Debug.Log("OnClickBlock:" + uIndex);
		scrGrid.SetUpNon(uIndex, _tgUp.value);
		scrGrid.SetDownNon(uIndex, _tgDown.value);
		scrGrid.SetLeftNon(uIndex, _tgLeft.value);
		scrGrid.SetRightNon(uIndex, _tgRight.value);
	}

	public void OnClickSave() {
		Debug.Log("OnClickSave");
		string str = scrGrid.GetValue();
		WriterFile(Application.dataPath, "map1", str);
	}

	private void WriterFile(string path, string name, string info) {
		StreamWriter sw;
		FileInfo fi = new FileInfo (path + "//" + name);
		sw = fi.CreateText ();
		sw.WriteLine (info);
		sw.Close ();
	}
}
