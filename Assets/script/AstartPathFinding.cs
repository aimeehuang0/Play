using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstartPathFinding : MonoBehaviour {
	[SerializeField]
	SquareGrid squaregrid;

	[SerializeField]
		Character mycharacter;
	// Use this for initialization
	void Start () {
		
	}
	
	public  void FindPath(int _startX, int _startY, int _EndX, int _EndY) {
		Debug.Log("startPathFinding");
		Node startNode = squaregrid.NodeFromWorldPoint(_startX, _startY);
		Node targetNode  = squaregrid.NodeFromWorldPoint(_EndX, _EndY);
		List<Node> openSet = new List<Node>();
		HashSet<Node> closeSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Debug.Log("openset");
			Node currentNode = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].fCost < currentNode.fCost||openSet[i].fcost == currentNode.fcost && openSet[i].hCost<currentNode.hCost) {
					currentNode = openSet[i];
				}
			}
			openSet.Remove(currentNode);
			closeSet.Add(currentNode);

			if (currentNode == targetNode) {
				Debug.Log("end PathFind");
				RetracePath(startNode, targetNode);
				return;
			}

			foreach (Node neighbour in squaregrid.GetNeightbours(currentNode)) {

				if (!neighbour.walkable || closeSet.Contains(neighbour)) {
					Debug.Log("Unwalkable");
					continue;
				}

				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = currentNode;
					Debug.Log("addneighbout to openset");
					if (!openSet.Contains(neighbour)) {
						Debug.Log("addneighbout to openset");
						openSet.Add(neighbour);
					}
				}
			}
		}
	}

	void RetracePath(Node _startNode ,Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != _startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		mycharacter.path = path;

	}

	int GetDistance(Node A, Node B) {
		int dstX = Mathf.Abs(A.x - B.x);
		int dstY = Mathf.Abs(A.y - B.y);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}

}
