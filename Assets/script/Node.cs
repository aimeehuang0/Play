using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{
	public bool walkable;
	public int x,y;

	public int gCost;
	public int hCost;
	public int fCost;
	public Node parent;
	public Node(bool _walkable, int _x, int _y){
		walkable = _walkable;
		x=_x;
		y=_y;
	}

	public int fcost{
		get{
			return gCost+hCost;

		}

	}
}
