using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareGrid : MonoBehaviour {
	[SerializeField]
	public int GridSize;
	public Vector2 gridWorldSize;
	Node[,] grid;
	[SerializeField]
	int gridSizeX, gridSizeY;
	[SerializeField]
	IsometricManger isometricmanger;

	void CreatGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				bool walkable = isometricmanger.unitys[x * GridSize + y].walkable;
				grid[x, y] = new Node(walkable, x, y);

				if (walkable)
				{
					//  Debug.Log(unityRef.unity[6 * i + j].name);
					isometricmanger.unitys[x * GridSize + y].GetComponent<SpriteRenderer>().color = Color.white;

				}
				else {
					isometricmanger.unitys[x * GridSize + y].GetComponent<SpriteRenderer>().color = Color.red;
				}

				bool triggerable = isometricmanger.unitys[x * GridSize + y].trigger;
				if (triggerable)
				{
					isometricmanger.unitys[x * GridSize + y].GetComponent<SpriteRenderer>().color = Color.blue;
				}

			}
		}
	}


	public List<Node> GetNeightbours(Node _node) {
		List<Node> neighbours = new List<Node>();
		for (int x = -1; x <=1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;
				if (x == -1 && y == 1)
					continue;
				if (x == 1 && y == 1)
					continue;
				if (x == -1 && y == -1)
					continue;
				if (x == 1 && y == -1)
					continue;
				int checkX = _node.x + x;
				int checkY = _node.y + y;
				Debug.Log(checkX+","+checkY);
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					// Debug.Log(_node.x + "," + _node.y);
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}

	public Node NodeFromWorldPoint(int _x, int _y) {
		int x = _x;
		int y = _y;
		return grid[x,y];
		}

	void Start(){

		CreatGrid();

	}

	void Update(){
		CreatGrid();
	}
}