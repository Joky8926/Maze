using UnityEngine;

public class SCRPlayer : MonoBehaviour {
	private SCRMazePanel scrMazePanel;
	private Vector3 vec3NextPos;
	private Vector3 vec3Dict;
	private float fTime = 0f;
	private bool bMove = false;

	void Update() {
		if (!bMove) {
			return;
		}
		fTime += Time.deltaTime;
		if (fTime >= 1f) {
			OnMoveEnd();
		} else {
			this.transform.position += vec3Dict * Time.deltaTime;
		}
	}

	public void Init(SCRMazePanel scrMazePanel, Vector3 vec3Pos) {
		this.scrMazePanel = scrMazePanel;
		this.transform.position = vec3Pos;
	}

	public void DoMove(Vector3 vec3Pos) {
		bMove = true;
		vec3NextPos = vec3Pos;
		vec3Dict = vec3NextPos - this.transform.position;
	}

	private void OnMoveEnd() {
		fTime = 0f;
		bMove = false;
		this.transform.position = vec3NextPos;
		scrMazePanel.OnMoveEnd();
	}
}
