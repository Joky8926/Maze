using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SCRLoadMap : MonoBehaviour {
	public SCRGrid scrGrid;

	void Start () {
		StreamReader sr = File.OpenText(Application.dataPath + "//map1");
		string str = sr.ReadLine();
		sr.Close();
		Debug.Log(str);
		Init(str);
	}

	private void Init(string str) {
		string[] arrStr = str.Split('|');
		Debug.Log("len:" + arrStr.Length);
		string[] arrGridStr = arrStr[0].Split(':');
		scrGrid.ResetGrid(int.Parse(arrGridStr[0]), int.Parse(arrGridStr[1]));
		int[] arrValue = new int[arrStr.Length - 2];
		for (int i = 1; i < arrStr.Length - 1; i++) {
			arrValue[i - 1] = int.Parse(arrStr[i]);
		}
		scrGrid.SetValue(arrValue);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
