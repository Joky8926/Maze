using UnityEngine;
using System.Collections;

public class TlLine : MonoBehaviour {
	private const int MAX_SIZE = 50;
	private Vector3 orgPos;
	private bool bMove = false;

	void Start () {
		orgPos = this.transform.localPosition;
		this.gameObject.SetActive(false);
	}

	public void InitHorizontal(Vector2 vec2Pos) {
		Vector3 pos = new Vector3(vec2Pos.x, orgPos.y);
		this.gameObject.SetActive(true);
		this.transform.localPosition = pos;
	}

	public void InitVertical(Vector2 vec2Pos) {
		Vector3 pos = new Vector3(orgPos.x, vec2Pos.y);
		this.gameObject.SetActive(true);
		this.transform.localPosition = pos;
	}

	public void HorizontalMove() {

	}

	public void VerticalMove() {

	}
}

public enum ELineDir {
	eldRight,
	eldDown
};
