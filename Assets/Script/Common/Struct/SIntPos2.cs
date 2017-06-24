
public struct SIntPos2 {
	private int _x;
	private int _y;

	public SIntPos2(int posX, int posY) {
		_x = posX;
		_y = posY;
	}

	public int x {
		get {
			return _x;
		}
		set {
			_x = value;
		}
	}

	public int y {
		get {
			return _y;
		}
		set {
			_y = value;
		}
	}

	public static SIntPos2 operator + (SIntPos2 posA, SIntPos2 posB) {
		return new SIntPos2(posA.x + posB.x, posA.y + posB.y);
	}
}
