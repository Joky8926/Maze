using UnityEngine;
using System.Collections;

public class TlDrawLine : MonoBehaviour {
	public GameObject _goPreDownLine;
	public GameObject _goPresRightLine;
	private TlLine[,] arrDownLine = new TlLine[5, 10];
	private TlLine[,] arrRightLine = new TlLine[5, 10];
	private Vector2 vec2LastPos;
	private bool bMove = false;
	private Vector2 vec2Offset = new Vector2(35, 35);
	private TlLine lastLine;

	void Start() {
		InitLine();
	}

	void Update() {
		if (Input.GetMouseButton(0)) {
			if (bMove) {
				DrawLine();
			}
			vec2LastPos = UICamera.lastEventPosition;
			bMove = true;
		}
		if (Input.GetMouseButtonUp(0)) {
			bMove = false;
			lastLine = null;
		}
	}

	private GameObject CreateLine(GameObject goPre) {
		GameObject go = Instantiate(goPre);
		go.transform.parent = this.transform;
		go.transform.localScale = Vector3.one;
		return go;
	}

	private void InitLine() {
		for (int i = 0; i < arrDownLine.GetLength(0); i ++) {
			for (int j = 0; j < arrDownLine.GetLength(1); j++) {
				DrawDownLine(i, j);
				DrawRightLine(i, j);
			}
		}
	}

	private void DrawDownLine(int x, int y) {
		if (arrDownLine[x, y] != null) {
			return;
		}
		GameObject go = CreateLine(_goPreDownLine);
		Vector2 vec2Pos = new Vector2(x * 50 - Screen.width / 2, (y + 1) * 50 - Screen.height / 2) + vec2Offset;
		Vector3 pos = new Vector3(vec2Pos.x, vec2Pos.y);
		go.transform.localPosition = pos;
		go.name = _goPreDownLine.name + "_" + x + "_" + y;
		TlLine scrLine = go.GetComponent<TlLine>();
		arrDownLine[x, y] = scrLine;
	}

	private void DrawRightLine(int x, int y) {
		if (arrRightLine[x, y] != null) {
			return;
		}
		GameObject go = CreateLine(_goPresRightLine);
		Vector2 vec2Pos = new Vector2((x + 1) * 50 - Screen.width / 2, (y + 1) * 50 - Screen.height / 2) + vec2Offset;
		Vector3 pos = new Vector3(vec2Pos.x, vec2Pos.y);
		go.transform.localPosition = pos;
		go.name = _goPresRightLine.name + "_" + x + "_" + y;
		TlLine scrLine = go.GetComponent<TlLine>();
		arrRightLine[x, y] = scrLine;
	}

	private void DrawLine() {
		float X = vec2LastPos.x - UICamera.lastEventPosition.x;
		float Y = vec2LastPos.y - UICamera.lastEventPosition.y;
		if (X == 0 || Y == 0) {
			return;
		}
		TlLine scrLine;
		if (Mathf.Abs(X) > Mathf.Abs(Y)) {
			scrLine = GetLine(ELineDir.eldDown);
			if (scrLine == null) {
				return;
			}
			if (lastLine != null && lastLine != scrLine) {
				return;
			}
			if (lastLine == null) {
				lastLine = scrLine;
				float posX = vec2LastPos.x - Screen.width / 2;
				lastLine.InitPosX(posX);
			}
			if (X > 0) {
				lastLine.MoveRight(X);
			} else {
				lastLine.MoveLeft(-X);
			}
		} else {
			scrLine = GetLine(ELineDir.eldRight);
			if (scrLine == null) {
				return;
			}
			if (lastLine != null && lastLine != scrLine) {
				return;
			}
			if (lastLine == null) {
				lastLine = scrLine;
				float poxY = vec2LastPos.y - Screen.height / 2;
				lastLine.InitPosY(poxY);
			}
			if (Y > 0) {
//				lastLine.MoveUp(Y);
			} else {
//				lastLine.MoveDown(-Y);
			}
		}
	}

	private TlLine GetLine(ELineDir eLineDir) {
		Vector2 vec2 = UICamera.lastEventPosition - vec2Offset;
		int X = Mathf.FloorToInt(vec2.x / 50);
		int Y = Mathf.FloorToInt(vec2.y / 50);
		if (X < 0 || Y < 0) {
			return null;
		}
		if (arrDownLine.GetLength(0) <= X || arrDownLine.GetLength(1) <= Y) {
			return null;
		}
		if (eLineDir == ELineDir.eldDown) {
			Debug.Log("Down:" + X + "|" + Y);
			return arrDownLine[X, Y];
		} else {
			Debug.Log("Right:" + X + "|" + Y);
			return arrRightLine[X, Y];
		}
	}
}
