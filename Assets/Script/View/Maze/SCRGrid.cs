using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRGrid : MonoBehaviour {
	public static SCRGrid instance;
	public GameObject _preBlock;
	public SCRBlock.DelegateOnClickBlock onClickBlock;

	private List<SCRBlock> lstBlock = new List<SCRBlock>();
	private UIGrid uiGrid;
	private int uMaxCol;
	private int uMaxRow;
	private int uLastIndex = 0;
	private List<int> lstPath = new List<int>();
	private int uMoveIndex = 1;

	void Awake() {
		uiGrid = this.GetComponent<UIGrid>();
		uMaxCol = uiGrid.maxPerLine;
		uMaxRow = 4;
		instance = this;
	}

	public void ResetGrid(int uRow, int uCol) {
		uMaxRow = uRow;
		uMaxCol = uCol;
		uiGrid.maxPerLine = uMaxCol;
		CreateAllBlock(uMaxRow * uMaxCol);
		uiGrid.Reposition();
		lstPath.Add(0);
	}

	private void CreateAllBlock(int count) {
		ShowAllBlock(count);
		for (int i = lstBlock.Count; i < count; i++) {
			SCRBlock scr = CreateBlock(i);
			lstBlock.Add(scr);
		}
		for (int i = count; i < lstBlock.Count; i++) {
			lstBlock[i].SetShow(false);
		}
	}

	private void ShowAllBlock(int count) {
		for (int i = 0; i < count && i < lstBlock.Count; i++) {
			lstBlock[i].SetShow(true);
		}
	}

	private SCRBlock CreateBlock(int index) {
		GameObject block = Instantiate(_preBlock);
		block.transform.parent = this.transform;
		block.transform.localScale = Vector3.one;
		block.name = _preBlock.name + "_" + index;
		SCRBlock scr = block.GetComponent<SCRBlock>();
		scr.index = index;
		if (onClickBlock != null) {
			scr.onClickBlock += onClickBlock;
		}
		return scr;
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

	public void SetValue(int[] arrValue) {
		for (int i = 0; i < arrValue.Length; i++) {
			lstBlock[i].SetValue(arrValue[i]);
		}
	}

	public Vector3 GetNextPos() {
		if (uMoveIndex >= lstPath.Count) {
			return Vector3.zero;
		}
		int uCurIndex = lstPath[uMoveIndex++];
		return lstBlock[uCurIndex].transform.position;
	}

	public void ResetPlayerPos() {
		SCRPlayer.instance.ResetPos(lstBlock[0].transform.position);
	}
}
