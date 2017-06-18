using UnityEngine;
using System.Collections.Generic;

public class Test : MonoBehaviour {
	public GameObject _goPreRow;
	public GameObject _goPresCol;
	private GameObject[,] arrRow = new GameObject[5, 10];
	private GameObject[,] arrCol = new GameObject[5, 10];
	private Vector2 vec2LastPos;
	private bool bMove = false;

	void Start () {
//		Debug.Log("row:\t" + _goRow.transform.position.ToString());
//		Debug.Log("col:\t" + _goCol.transform.position.ToString());
//		Debug.Log("width:\t" + Screen.width + "\thight:\t" + Screen.height);
//		GameObject go = Instantiate(_goPreRow);
//		go.transform.parent = this.transform;
//		go.transform.localScale = Vector3.one;
//		UISprite uiSprite = go.GetComponent<UISprite>();
//		Debug.Log("width:\t" + uiSprite.width + "\thight:\t" + uiSprite.height);
//		uiSprite.width = 123;
//		lstRow.Add(go);
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Vector2 vec2 = UICamera.lastEventPosition;
			Debug.Log("MouseDown:\t" + vec2.ToString());
			Debug.Log("Index X:" + Mathf.CeilToInt(vec2.x / 50) + "\tY:" + Mathf.CeilToInt(vec2.y / 50));
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
