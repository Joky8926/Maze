using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPlayer : MonoBehaviour {
	public static SCRPlayer instance;
	private Vector3 vec3NextPos = Vector3.zero;
	private Vector3 vec3Dict;
	private float fTime = 0f;

	void Awake() {
		instance = this;
	}

	public void ResetPos(Vector3 vec3Pos) {
		Debug.Log(vec3Pos);
		vec3NextPos = vec3Pos;
		GetNextPos();
	}

	void Update () {
		if (vec3NextPos == Vector3.zero) {
			return;
		}
		fTime += Time.deltaTime;
		if (fTime >= 1f) {
			GetNextPos();
		} else {
			this.transform.position += vec3Dict * Time.deltaTime;
		}
	}

	private void GetNextPos() {
		fTime = 0f;
		this.transform.position = vec3NextPos;
		vec3NextPos = SCRGrid.instance.GetNextPos();
		if (vec3NextPos != Vector3.zero) {
			vec3Dict = vec3NextPos - this.transform.position;
		}
	}
}
