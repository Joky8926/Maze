using UnityEngine;
using System.Collections;

public class TlLine : MonoBehaviour {
	private UISprite uiSprite;
	private const int MAX_SIZE = 50;
	private const int LINE_WIDTH = 2;
	private Vector3 orgPos;
	private bool bMove = false;
	private float fSize = 0f;
	private float fMoveStartPos;
	private float fOrgPos;
	private ELineDir eLineDir = ELineDir.eLdNone;

	void Awake() {
		uiSprite = this.GetComponent<UISprite>();
	}

	public void Init(ELineDir _eLineDir, Vector3 vec3Pos) {
		eLineDir = _eLineDir;
		if (eLineDir == ELineDir.eLdHorizontal) {
			uiSprite.pivot = UIWidget.Pivot.Left;
			uiSprite.height = LINE_WIDTH;
		} else {
			uiSprite.pivot = UIWidget.Pivot.Bottom;
			uiSprite.width = LINE_WIDTH;
		}
		orgPos = vec3Pos;
		this.transform.localPosition = vec3Pos;
		this.gameObject.SetActive(false);
	}

	public void InitMoveStartPos(float fPos) {
		fMoveStartPos = fPos;
		this.gameObject.SetActive(true);
		Vector3 vec3Pos;
		if (eLineDir == ELineDir.eLdHorizontal) {
			fOrgPos = orgPos.x;
			vec3Pos = new Vector3(fMoveStartPos, orgPos.y);
			uiSprite.width = (int)fSize;
		} else {
			fOrgPos = orgPos.y;
			vec3Pos = new Vector3(orgPos.x, fMoveStartPos);
			uiSprite.height = (int)fSize;
		}
		this.transform.localPosition = vec3Pos;
	}

	public void Move(float fDist) {
		fSize += fDist;
		DrawLine();
	}

	private void DrawLine() {
		int uLineLength;
		if (fSize < 0) {
			Vector3 pos;
			if (eLineDir == ELineDir.eLdHorizontal) {
				pos = new Vector3 (fMoveStartPos + fSize, orgPos.y);
			} else {
				pos = new Vector3 (orgPos.x, fMoveStartPos + fSize);
			}
			this.transform.localPosition = pos;
			if (f_bIsEnd) {
				uLineLength = (int)(fMoveStartPos - fOrgPos);
			} else {
				uLineLength = (int)(-fSize);
			}
		} else {
			if (f_bIsEnd) {
				uLineLength = (int)(fOrgPos + MAX_SIZE - fMoveStartPos);
			} else {
				uLineLength = (int)fSize;
			}
		}
		if (eLineDir == ELineDir.eLdHorizontal) {
			uiSprite.width = uLineLength;
		} else {
			uiSprite.height = uLineLength;
		}
	}

	public bool f_bIsEnd {
		get {
			return fMoveStartPos + fSize - fOrgPos >= MAX_SIZE || fMoveStartPos + fSize <= fOrgPos;
		}
	}

	public ELineDir f_eLineDir {
		get {
			return eLineDir;
		}
	}
}

public enum ELineDir {
	eLdNone,
	eLdHorizontal,
	eLdVertical
};
