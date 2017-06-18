using UnityEngine;
using System.Collections;

public class TlDrawLine : MonoBehaviour {
	public GameObject _goPreDownLine;
	public GameObject _goPresRightLine;
	private TlLine[,] arrDownLine = new TlLine[5, 10];
	private TlLine[,] arrRightLine = new TlLine[5, 10];
	private Vector2 vec2LastPos;
	private bool bMove = false;

	void Start() {
	
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			DrawLine();
		}
		if (Input.GetMouseButton(0)) {
			if (bMove) {
				CheckDir(vec2LastPos, UICamera.lastEventPosition);
			}
			vec2LastPos = UICamera.lastEventPosition;
			bMove = true;
		}
		if (Input.GetMouseButtonUp(0)) {
			bMove = false;
		}
	}

	private GameObject CreateLine(GameObject goPre) {
		GameObject go = Instantiate(goPre);
		go.transform.parent = this.transform;
		go.transform.localScale = Vector3.one;
		return go;
	}

	private void DrawLine() {
		Vector2 vec2 = UICamera.lastEventPosition;
		int X = Mathf.FloorToInt(vec2.x / 50);
		int Y = Mathf.FloorToInt(vec2.y / 50);
		if (arrDownLine.GetLength(0) <= X || arrDownLine.GetLength(1) <= Y) {
			return;
		}
		DrawDownLine(X, Y);
		DrawRightLine(X, Y);
	}

	private void DrawDownLine(int x, int y) {
		if (arrDownLine[x, y] != null) {
			return;
		}
		GameObject go = CreateLine(_goPreDownLine);
		Vector3 pos = new Vector3(x * 50 - Screen.width / 2, (y + 1) * 50 - Screen.height * 0.5f);
		go.transform.localPosition = pos;
		TlLine scrLine = go.GetComponent<TlLine>();
		arrDownLine[x, y] = scrLine;
	}

	private void DrawRightLine(int x, int y) {
		if (arrRightLine[x, y] != null) {
			return;
		}
		GameObject go = CreateLine(_goPresRightLine);
		Vector3 pos = new Vector3((x + 1) * 50 - Screen.width / 2, (y + 1) * 50 - Screen.height * 0.5f);
		go.transform.localPosition = pos;
		TlLine scrLine = go.GetComponent<TlLine>();
		arrDownLine[x, y] = scrLine;
	}

	private Vector2 GetMousePos() {
		Vector2 vec2 = UICamera.lastEventPosition - new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
		return vec2;
	}

	private void CheckDir(Vector2 lastPos, Vector2 nowPos) {
		float X = lastPos.x - nowPos.x;
		float Y = lastPos.y - nowPos.y;
		if (Mathf.Abs(X) > Mathf.Abs(Y)) {
			if (X > 0) {
				Debug.Log("左");
			} else if (X < 0) {
				Debug.Log("右");
			}
		} else {
			if (Y > 0) {
				Debug.Log("下");
			} else if (Y < 0) {
				Debug.Log("上");
			}
		}
	}
}
