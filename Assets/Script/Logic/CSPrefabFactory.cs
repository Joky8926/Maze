using UnityEngine;
using System.Collections.Generic;

public class CSPrefabFactory {
	private static CSPrefabFactory _instance;
	private Dictionary<string, GameObject> dictPrefab = new Dictionary<string, GameObject>();

	private readonly string PREFAB_PATH = "Prefab/";
	private const string PRE_MAZE_GRID = "Pre_MazeGrid";
	private const string PER_MAZE_BLOCK = "Pre_MazeBlock";
	private const string PRE_MAZE_PLAYER = "Pre_MazePlayer";

	private CSPrefabFactory() {

	}

	public static CSPrefabFactory Instance() {
		if (_instance == null) {
			_instance = new CSPrefabFactory();
        }
        return _instance;
	}

	public GameObject GetMazeGrid() {
		GameObject goMazeGrid = GetCommonGO(PRE_MAZE_GRID, false);
		return goMazeGrid;
	}

	public GameObject CreateMazeBlock() {
		GameObject goMazeBlock = GetCommonGO(PER_MAZE_BLOCK, true);
		return goMazeBlock;
	}

	public GameObject GetMazePlayer() {
		GameObject goMazePlayer = GetCommonGO(PRE_MAZE_PLAYER, false);
		return goMazePlayer;
	}

	private GameObject GetCommonGO(string sName, bool bSave) {
		GameObject goPre = GetPrefab(sName, false);
		GameObject goCommon = GameObject.Instantiate(goPre);
		goCommon.name = goPre.name;
		return goCommon;
	}

	private GameObject GetPrefab(string sName, bool bSave) {	// TODO: unit test
		string sPath = PREFAB_PATH + sName;
//		Debug.Log(sPath);
		GameObject go = null;
		if (bSave) {
			if (dictPrefab.ContainsKey(sPath)) {
				go = dictPrefab[sPath];
			} else {
				go = Resources.Load(sPath) as GameObject;
				dictPrefab[sPath] = go;
			}
		} else {
			go = Resources.Load(sPath) as GameObject;
		}
		return go;
	}
}
