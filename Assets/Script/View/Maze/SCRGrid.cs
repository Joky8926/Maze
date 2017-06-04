using System.Collections.Generic;
using UnityEngine;

public class SCRGrid : MonoBehaviour {
	public DelegateOnClickBlock onClickBlock;
	private SCRMazePanel scrMazePanel;
	private CSPrefabFactory preFactory;
	private UIGrid uiGrid;
	private List<SCRBlock> lstBlock = new List<SCRBlock>();
	private int uMaxCol;
	private int uMaxRow;
	private int uLastIndex = 0;
	private List<int> lstPath = new List<int>();
	private int uMoveIndex = 1;

	public void Init(SCRMazePanel scrMazePanel, int uRow, int uCol, int[] arrValue) {
		this.scrMazePanel = scrMazePanel;
		uiGrid = this.GetComponent<UIGrid>();
		InitGrid(uRow, uCol);
		SetBlockValue(arrValue);
	}

	public void InitGrid(int uRow, int uCol) {
		uMaxRow = uRow;
		uMaxCol = uCol;
		uiGrid.maxPerLine = uMaxCol;
		CreateAllBlock(uMaxRow * uMaxCol);
		uiGrid.Reposition();
		lstPath.Add(0);
	}

	private void CreateAllBlock(int count) {
		for (int i = 0; i < count && i < lstBlock.Count; i++) {
			lstBlock[i].SetShow(true);
		}
		preFactory = CSPrefabFactory.Instance();
		for (int i = lstBlock.Count; i < count; i++) {
			SCRBlock scr = CreateOneBlock(i);
			lstBlock.Add(scr);
		}
		for (int i = count; i < lstBlock.Count; i++) {
			lstBlock[i].SetShow(false);
		}
	}

	private SCRBlock CreateOneBlock(int index) {
		GameObject goBlock = preFactory.CreateMazeBlock();
		CSGameObject.AddChildGO(goBlock, this.gameObject);
		goBlock.name += "_" + index;
		SCRBlock scr = goBlock.GetComponent<SCRBlock>();
		scr.Init(this, index);
		if (onClickBlock != null) {
			scr.onClickBlock += onClickBlock;
		}
		return scr;
	}

	private void SetBlockValue(int[] arrValue) {
		for (int i = 0; i < arrValue.Length; i++) {
			lstBlock[i].SetValue(arrValue[i]);
		}
	}

	private SCRBlock GetLeft(int index) {
		if (index % uMaxCol == 0) {
			return null;
		}
		return lstBlock[index - 1];
	}

	private SCRBlock GetRight(int index) {
		if ((index + 1) % uMaxCol == 0) {
			return null;
		}
		return lstBlock[index + 1];
	}

	private SCRBlock GetUp(int index) {
		if (index / uMaxCol == 0) {
			return null;
		}
		return lstBlock[index - uMaxCol];
	}

	private SCRBlock GetDown(int index) {
		if (index / uMaxCol == uMaxRow - 1) {
			return null;
		}
		return lstBlock[index + uMaxCol];
	}

	public void SetLeftNon(int uIndex, bool bNon) {
		SCRBlock scr = GetLeft(uIndex);
		if (scr != null) {
			scr.SetNonRight(bNon);
		}
	}

	public void SetRightNon(int uIndex, bool bNon) {
		SCRBlock scr = GetRight(uIndex);
		if (scr != null) {
			lstBlock[uIndex].SetNonRight(bNon);
		}
	}

	public void SetUpNon(int uIndex, bool bNon) {
		SCRBlock scr = GetUp(uIndex);
		if (scr != null) {
			scr.SetNonDown(bNon);
		}
	}

	public void SetDownNon(int uIndex, bool bNon) {
		SCRBlock scr = GetDown(uIndex);
		if (scr != null) {
			lstBlock[uIndex].SetNonDown(bNon);
		}
	}

	public void DoLine(int nowIndex) {
		bool isChange = false;
		if (uLastIndex - 1 == nowIndex) {
			if (uLastIndex % uMaxCol != 0) {
				isChange = lstBlock[nowIndex].SetRightLine();
			}
		} else if (uLastIndex + 1 == nowIndex) {
			if (nowIndex % uMaxCol != 0) {
				isChange = lstBlock[uLastIndex].SetRightLine();
			}
		} else if (uLastIndex + uMaxCol == nowIndex) {
			isChange = lstBlock[uLastIndex].SetDownLine();
		} else if (uLastIndex - uMaxCol == nowIndex) {
			isChange = lstBlock[nowIndex].SetDownLine();
		}
		if (isChange) {
			if (lstPath.Count > 1 && lstPath[lstPath.Count - 2] == nowIndex) {
				lstPath.RemoveAt(lstPath.Count - 1);
			} else {
				lstPath.Add(nowIndex);
			}
			uLastIndex = nowIndex;
		}
	}

	public void SetLastIndex(int index) {
		uLastIndex = index;
	}

	public void CleanAllLine() {
		for (int i = 0; i < lstBlock.Count; i++) {
			lstBlock[i].CleanAllLine();
		}
	}

	public string GetValue() {
		string str = "" + uMaxRow + ":" + uMaxCol + "|";
		for (int i = 0; i < uMaxCol * uMaxRow; i++) {
			str += lstBlock[i].GetValue() + "|";
		}
		return str;
	}

	public void OnBlockPress() {
		scrMazePanel.DoMove();
	}

	public bool hasNextPos {
		get {
			return uMoveIndex < lstPath.Count;
		}
	}

	public Vector3 firstPos {
		get {
			return lstBlock[0].transform.position;
		}
	}

	public Vector3 nextPos {
		get {
			int uCurIndex = lstPath[uMoveIndex++];
			return lstBlock[uCurIndex].transform.position;
		}
	}
}
