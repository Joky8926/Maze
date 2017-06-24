using UnityEngine;
using System.Collections;

public class TlLine : MonoBehaviour {
	private UISprite uiSprite;
	private const int MAX_SIZE = 50;
	private const int LINE_WIDTH = 2;
	private Vector3 orgPos;
	private float fSize;
	private float fMoveStartPos;
	private float fOrgPos;
	private ELineDir eLineDir = ELineDir.eLdNone;
	private SIntPos2 curPos;
	private SIntPos2 nextPos;

	void Awake() {
		uiSprite = this.GetComponent<UISprite>();
	}

	public void Init(ELineDir _eLineDir, Vector3 vec3Pos, SIntPos2 pos) {
		eLineDir = _eLineDir;
		curPos = pos;
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

	public void InitMoveStartPos(bool bOpp) {
		float fPos;
		if (eLineDir == ELineDir.eLdHorizontal) {
			fPos = bOpp ? orgPos.x + MAX_SIZE : orgPos.x;
		} else {
			fPos = bOpp ? orgPos.y + MAX_SIZE :orgPos.y;
		}
		InitMoveStartPos(fPos);
	}

	public void InitMoveStartPos(float fPos) {
		fSize = 0f;
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
				pos = new Vector3(fMoveStartPos + fSize, orgPos.y);
			} else {
				pos = new Vector3(orgPos.x, fMoveStartPos + fSize);
			}
			this.transform.localPosition = pos;
			if (f_bIsEnd) {
				nextPos = curPos;
				uLineLength = (int)(fMoveStartPos - fOrgPos);
			} else {
				uLineLength = (int)(-fSize);
			}
		} else {
			if (f_bIsEnd) {
				nextPos = curPos;
				if (eLineDir == ELineDir.eLdHorizontal) {
					nextPos.x++;
				} else {
					nextPos.y++;
				}
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
			return fMoveStartPos + fSize - fOrgPos > MAX_SIZE || fMoveStartPos + fSize < fOrgPos;
		}
	}

	public ELineDir f_eLineDir {
		get {
			return eLineDir;
		}
	}

	public SIntPos2 f_nextPos {
		get {
			return nextPos;
		}
	}
}
