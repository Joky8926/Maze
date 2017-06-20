using UnityEngine;
using System.Collections;

public class TlLine : MonoBehaviour {
	private UISprite uiSprite;
	private const int MAX_SIZE = 50;
	private Vector3 orgPos;
	private bool bMove = false;
	private float fSize = 0f;
	private float fMoveStartPos;
	private EMoveDir eMoveDir = EMoveDir.eMdNone;
	private ELineDir eLineDir = ELineDir.eLdNone;

	void Start () {
		uiSprite = this.GetComponent<UISprite>();
		orgPos = this.transform.localPosition;
		this.gameObject.SetActive(false);
	}

	public void Init(ELineDir _eLineDir) {
		eLineDir = _eLineDir;
	}

	public ELineDir f_eLineDir {
		get {
			return eLineDir;
		}
	}

	public void InitPosX(float x) {
		fMoveStartPos = x;
		Vector3 pos = new Vector3(x, orgPos.y);
		this.gameObject.SetActive(true);
		this.transform.localPosition = pos;
		uiSprite.width = (int)fSize;
	}

	public void InitPosY(float y) {
		fMoveStartPos = y;
		Vector3 pos = new Vector3(orgPos.x, y);
		this.gameObject.SetActive(true);
		this.transform.localPosition = pos;
		uiSprite.height = (int)fSize;
	}

	public void MoveLeft(float x) {
		switch (eMoveDir) {
			case EMoveDir.eMdNone:
				fSize = x;
				eMoveDir = EMoveDir.eMdLeft;
				break;
			case EMoveDir.eMdLeft:
				fSize += x;
				break;
			case EMoveDir.eMdRight:
				fSize -= x;
				if (fSize < 0) {
					eMoveDir = EMoveDir.eMdLeft;
					fSize = -fSize;
				}
				break;
		}
		DrawHorLine();
	}

	public void MoveRight(float x) {
		switch (eMoveDir) {
			case EMoveDir.eMdNone:
				fSize = x;
				eMoveDir = EMoveDir.eMdLeft;
				break;
			case EMoveDir.eMdLeft:
				fSize -= x;
				if (fSize < 0) {
					eMoveDir = EMoveDir.eMdRight;
					fSize = -fSize;
				}
				break;
			case EMoveDir.eMdRight:
				fSize += x;
				break;
		}
		DrawHorLine();
	}

	public void MoveUp(float y) {
		switch (eMoveDir) {
			case EMoveDir.eMdNone:
				fSize = y;
				eMoveDir = EMoveDir.eMdLeft;
				break;
			case EMoveDir.eMdUp:
				fSize += y;
				break;
			case EMoveDir.eMdDown:
				fSize -= y;
				if (fSize < 0) {
					eMoveDir = EMoveDir.eMdUp;
					fSize = -fSize;
				}
				break;
		}
		DrawVerLine();
	}

	public void MoveDown(float y) {
		switch (eMoveDir) {
			case EMoveDir.eMdNone:
				fSize = y;
				eMoveDir = EMoveDir.eMdLeft;
				break;
			case EMoveDir.eMdUp:
				fSize -= y;
				if (fSize < 0) {
					eMoveDir = EMoveDir.eMdDown;
					fSize = -fSize;
				}
				break;
			case EMoveDir.eMdDown:
				fSize += y;
				break;
		}
		DrawVerLine();
	}

	private void DrawHorLine() {
		if (eMoveDir == EMoveDir.eMdLeft) {
			Vector3 pos = new Vector3(fMoveStartPos - fSize, orgPos.y);
			this.transform.localPosition = pos;
		}
		uiSprite.width = (int)fSize;
	}

	private void DrawVerLine() {
		if (eMoveDir == EMoveDir.eMdDown) {
			Vector3 pos = new Vector3(orgPos.x, fMoveStartPos - fSize);
			this.transform.localPosition = pos;
		}
		uiSprite.height = (int)fSize;
	}

	public void HorizontalMove() {

	}

	public void VerticalMove() {

	}
}

public enum ELineDir {
	eLdNone,
	eLdHorizontal,
	eLdVertical
};

public enum EMoveDir {
	eMdNone,
	eMdLeft,
	eMdRight,
	eMdUp,
	eMdDown
};
