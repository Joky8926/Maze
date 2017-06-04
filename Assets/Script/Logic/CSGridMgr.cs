using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSGridMgr {
	private static CSGridMgr _instance;

	private CSGridMgr() {

	}

	public static CSGridMgr Instance() {
		if (_instance == null) {
			_instance = new CSGridMgr();
        }
        return _instance;
	}
}
