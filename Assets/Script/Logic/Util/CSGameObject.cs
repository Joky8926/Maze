using UnityEngine;

public class CSGameObject {
	public static void AddChildResetPos(GameObject goChild, GameObject goParent) {
		Vector3 localPos = goChild.transform.localPosition;
		goChild.transform.parent = goParent.transform;
		goChild.transform.localScale = Vector3.one;
		goChild.transform.localPosition = localPos;
	}

	public static void AddChildGO(GameObject goChild, GameObject goParent) {
		goChild.transform.parent = goParent.transform;
		goChild.transform.localScale = Vector3.one;
	}
}
