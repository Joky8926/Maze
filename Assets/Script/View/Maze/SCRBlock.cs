using UnityEngine;

public class SCRBlock : MonoBehaviour {
	public DelegateOnClickBlock onClickBlock;

	public GameObject _goDown;
	public GameObject _goRight;
	public GameObject _goNonDown;
	public GameObject _goNonRight;
	private SCRGrid scrGrid;
	private int uIndex;

	public void Init(SCRGrid scrGrid, int uIndex) {
		this.scrGrid = scrGrid;
		this.uIndex = uIndex;
		CleanAllLine();
		_goNonDown.SetActive(false);
		_goNonRight.SetActive(false);
	}

	void OnClick() {
		if (onClickBlock != null) {
			onClickBlock(uIndex);
		}
	}

	void OnDragOver() {
		scrGrid.DoLine(uIndex);
	}

	void OnPress(bool isPressed) {
		if (!isPressed) {
			scrGrid.OnBlockPress();
		}
	}

	public bool SetDownLine() {
		if (isNonDown) {
			return false;
		}
		_goDown.SetActive(!_goDown.activeSelf);
		return true;
	}

	public bool SetRightLine() {
		if (isNonRight) {
			return false;
		}
		_goRight.SetActive(!_goRight.activeSelf);
		return true;
	}

	public void SetNonDown(bool bNone) {
		_goNonDown.SetActive(bNone);
	}

	public void SetNonRight(bool bNone) {
		_goNonRight.SetActive(bNone);
	}

	public void CleanAllLine() {
		_goDown.SetActive(false);
		_goRight.SetActive(false);
	}

	public void SetShow(bool bShow) {
		this.gameObject.SetActive(bShow);
	}

	private bool isNonDown {
		get {
			return _goNonDown.activeSelf;
		}
	}

	private bool isNonRight {
		get {
			return _goNonRight.activeSelf;
		}
	}

	public int GetValue() {
		int num = 0;
		if (isNonRight) {
			num |= 1;
		}
		if (isNonDown) {
			num |= 1 << 1;
		}
		return num;
	}

	public void SetValue(int value) {
		if ((value & 1) != 0) {
			_goNonRight.SetActive(true);
		}
		if ((value & 1 << 1) != 0) {
			_goNonDown.SetActive(true);
		}
	}
}
