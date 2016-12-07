using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

	public Transform seeker, target;

	Grid grid;

	void Awake() {
		grid = GetComponent<Grid> ();
	}

	void Update() {
		FindPath (seeker.position, target.position);
	}

	void FindPath(Vector3 startPos, Vector3 targetPos) {
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		List<Node> unvisitedSet = new List<Node> ();
		HashSet<Node> visitedSet = new HashSet<Node> ();
		unvisitedSet.Add (startNode);

		while (unvisitedSet.Count > 0) {
			Node currNode = unvisitedSet [0];
			for (int i = 1; i < unvisitedSet.Count; i++) {
				if (unvisitedSet [i].fCost < currNode.fCost || unvisitedSet[i].fCost == currNode.fCost && unvisitedSet[i].hCost < currNode.hCost) {
					currNode = unvisitedSet [i];
				}
			}

			unvisitedSet.Remove (currNode);
			visitedSet.Add (currNode);

			if (currNode == targetNode) {
				RetracePath (startNode, targetNode);
				return;
			}

			foreach (Node neighbor in grid.GetNeighbors(currNode)) {
				if (!neighbor.walkable || visitedSet.Contains (neighbor)) {
					continue;
				}

				int newMovCostToNeighbor = currNode.gCost + GetDistance (currNode, neighbor);
				if (newMovCostToNeighbor < neighbor.gCost || !unvisitedSet.Contains (neighbor)) {
					neighbor.gCost = newMovCostToNeighbor;
					neighbor.hCost = GetDistance (neighbor, targetNode);
					neighbor.parent = currNode;

					if (!unvisitedSet.Contains (neighbor)) {
						unvisitedSet.Add (neighbor);
					}
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node> ();
		Node currNode = endNode;

		while (currNode != startNode) {
			path.Add (currNode);
			currNode = currNode.parent;
		}
		path.Reverse ();

		grid.path = path;
	}

	int GetDistance(Node nodeA, Node nodeB) {
		int distX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int distY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		if (distX > distY)
			return 14 * distY + 10 * (distX - distY);
		return 14 * distX + 10 * (distY - distX);
	}
}
