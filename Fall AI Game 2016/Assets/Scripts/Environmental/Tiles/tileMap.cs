using UnityEngine;
using System.Collections;

public class tileMap : MonoBehaviour {

	[SerializeField] private GameObject tiles;		// Holds the tile prefab

	[SerializeField] private float height, width;	// Holds the tile's height and width to properly adjust each tile's position

	[SerializeField] private int row, column;		// Holds the number of rows and columns the map should be

	// Use this for initialization
	void Start () {
		// Initialize the number of rows and columns
		row = 25;
		column = 25;

		// Initialize the height and width of each tile
		height = tiles.transform.localScale.y * 10;
		width = tiles.transform.localScale.x * 10;

		//print ("Width: " + width + " Height: " + height);

		// Initialize the board
		for (int i = 0; i < column; i++) {
			for (int j = 0; j < row; j++) {
				Instantiate(tiles, new Vector3 ((float) i * width + gameObject.transform.localPosition.x + (width / 2), 1f, (float) j * height + gameObject.transform.localPosition.y + (height / 2)), Quaternion.identity);
				//print ("i: " + i + " j: " + j + " spawn coordinate x: " + ((float) i * width + gameObject.transform.localPosition.x + (width / 2)) + " spawn coordinate y: " + ((float) j * height + gameObject.transform.localPosition.y + (height / 2)));
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
