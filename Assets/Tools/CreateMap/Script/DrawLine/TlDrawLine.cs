using UnityEngine;
using System.Collections;

public class TlDrawLine : MonoBehaviour {
	public GameObject _goPreLine;
	private const string HORIZONTAL_LINE = "HorizontalLine";
	private const string VERTICAL_LINE = "Vertical";
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
		for (int i = 0; i < arrDownLine.GetLength(0); i++) {
			for (int j = 0; j < arrDownLine.GetLength(1); j++) {
				DrawHorizontalLine(i, j);
				DrawVerticalLine(i, j);
			}
		}
	}

	private void DrawHorizontalLine(int x, int y) {
		GameObject go = CreateLine(_goPreLine);
		Vector2 vec2Pos = new Vector2(x * 50 - Screen.width / 2, y * 50 - Screen.height / 2) + vec2Offset;
		Vector3 pos = new Vector3(vec2Pos.x, vec2Pos.y);
		go.name = HORIZONTAL_LINE + "_" + x + "_" + y;
		TlLine scrLine = go.GetComponent<TlLine>();
		SIntPos2 intPos = new SIntPos2(x, y);
		scrLine.Init(ELineDir.eLdHorizontal, pos, intPos);
		arrDownLine[x, y] = scrLine;
	}

	private void DrawVerticalLine(int x, int y) {
		GameObject go = CreateLine(_goPreLine);
		Vector2 vec2Pos = new Vector2(x * 50 - Screen.width / 2, y * 50 - Screen.height / 2) + vec2Offset;
		Vector3 pos = new Vector3(vec2Pos.x, vec2Pos.y);
		go.name = VERTICAL_LINE + "_" + x + "_" + y;
		TlLine scrLine = go.GetComponent<TlLine>();
		SIntPos2 intPos = new SIntPos2(x, y);
		scrLine.Init(ELineDir.eLdVertical, pos, intPos);
		arrRightLine[x, y] = scrLine;
	}

	private void DrawLine() {
		float X = UICamera.lastEventPosition.x - vec2LastPos.x;
		float Y = UICamera.lastEventPosition.y - vec2LastPos.y;
		if (X == 0 && Y == 0) {
			return;
		}
		if (lastLine == null) {
			float fPos;
			if (Mathf.Abs(X) > Mathf.Abs(Y)) {
				lastLine = GetLine(ELineDir.eLdHorizontal);
				fPos = vec2LastPos.x - Screen.width / 2;
			} else {
				lastLine = GetLine(ELineDir.eLdVertical);
				fPos = vec2LastPos.y - Screen.height / 2;
			}
			lastLine.InitMoveStartPos(fPos);
		}
		float fDist;
		if (lastLine.f_eLineDir == ELineDir.eLdHorizontal) {
			fDist = X;
		} else {
			fDist = Y;
		}
		lastLine.Move(fDist);
		if (lastLine.f_bIsEnd) {
			SIntPos2 nextPos = lastLine.f_nextPos;
			TlLine nextLine;
			if (Mathf.Abs(X) > Mathf.Abs(Y)) {
				nextLine = GetHorizontalLine(X < 0, nextPos);
				fDist = X;
				if (nextLine == null) {
					nextLine = GetVerticalLine(Y < 0, nextPos);
					fDist = Y;
				}
			} else {
				nextLine = GetVerticalLine(Y < 0, nextPos);
				fDist = Y;
				if (nextLine == null) {
					nextLine = GetHorizontalLine(X < 0, nextPos);
					fDist = X;
				}
			}
			if (nextLine != null) {
				nextLine.InitMoveStartPos(fDist < 0);
				lastLine = nextLine;
			}
			lastLine.Move(fDist);
		}
	}

	private TlLine GetHorizontalLine(bool bLeft, SIntPos2 pos) {
		if (bLeft) {
			pos.x--;
		}
		return GetLine(ELineDir.eLdHorizontal, pos.x, pos.y);
	}

	private TlLine GetVerticalLine(bool bDown, SIntPos2 pos) {
		if (bDown) {
			pos.y--;
		}
		return GetLine(ELineDir.eLdVertical, pos.x, pos.y);
	}

	private TlLine GetLine(ELineDir eLineDir, int X, int Y) {
		if (X < 0 || X >= arrDownLine.GetLength(0) || Y < 0 || Y >= arrDownLine.GetLength(1)) {
			return null;
		}
		if (eLineDir == ELineDir.eLdHorizontal) {
			Debug.Log("Horizontal:" + X + "|" + Y);
			return arrDownLine[X, Y];
		} else {
			Debug.Log("Vertical:" + X + "|" + Y);
			return arrRightLine[X, Y];
		}
	}

	private TlLine GetLine(ELineDir eLineDir) {
		Vector2 vec2 = UICamera.lastEventPosition - vec2Offset;
		int X = Mathf.FloorToInt(vec2.x / 50);
		int Y = Mathf.FloorToInt(vec2.y / 50);
		X = Mathf.Clamp(X, 0, arrDownLine.GetLength(0) - 1);
		Y = Mathf.Clamp(Y, 0, arrDownLine.GetLength(1) - 1);
		if (eLineDir == ELineDir.eLdHorizontal) {
			Debug.Log("Horizontal:" + X + "|" + Y);
			return arrDownLine[X, Y];
		} else {
			Debug.Log("Vertical:" + X + "|" + Y);
			return arrRightLine[X, Y];
		}
	}
}
