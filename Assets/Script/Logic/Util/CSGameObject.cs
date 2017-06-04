using UnityEngine;

public class CSGameObject {
	public static void AddGameObject(GameObject goChild, GameObject goParent) {
		Vector3 localPos = goChild.transform.localPosition;
		goChild.transform.parent = goParent.transform;
		goChild.transform.localScale = Vector3.one;
		goChild.transform.localPosition = localPos;
	}
}
