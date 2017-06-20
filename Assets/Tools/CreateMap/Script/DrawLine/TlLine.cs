using UnityEngine;
using System.Collections;

public class TlLine : MonoBehaviour {
	private UISprite uiSprite;
	private const int MAX_SIZE = 50;
	private Vector3 orgPos;
	private bool bMove = false;
	private float fSize = 0f;
	private float fMoveStartPos;
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

	public void InitPos(float fPos) {
		fMoveStartPos = fPos;
		this.gameObject.SetActive(true);
		Vector3 vec3Pos;
		if (eLineDir == ELineDir.eLdHorizontal) {
			vec3Pos = new Vector3(fMoveStartPos, orgPos.y);
			uiSprite.width = (int)fSize;
		} else {
			vec3Pos = new Vector3(orgPos.x, fMoveStartPos);
			uiSprite.height = (int)fSize;
		}
		this.transform.localPosition = vec3Pos;
	}

	public void Move(float fDist) {
		fSize += fDist;
		if (eLineDir == ELineDir.eLdHorizontal) {
			DrawHorLine();
		} else {
			DrawVerLine();
		}
	}

	private void DrawHorLine() {
		if (fSize < 0) {
			Vector3 pos = new Vector3(fMoveStartPos + fSize, orgPos.y);
			this.transform.localPosition = pos;
		}
		uiSprite.width = (int)Mathf.Abs(fSize);
	}

	private void DrawVerLine() {
		if (fSize > 0) {
			Vector3 pos = new Vector3(orgPos.x, fMoveStartPos + fSize);
			this.transform.localPosition = pos;
		}
		uiSprite.height = (int)Mathf.Abs(fSize);
	}
}

public enum ELineDir {
	eLdNone,
	eLdHorizontal,
	eLdVertical
};
