using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed

{

	public class BoardManager : MonoBehaviour
	{
		[Serializable]
		public class Count
		{
			public int minimum;
			public int maximum;


			//Assignment constructor.
			public Count (int min, int max)
			{
				minimum = min;
				maximum = max;
			}
		}


		public int columns = 8;
		public int rows = 8;
		public Count wallCount = new Count (5, 9);
		public Count foodCount = new Count (1, 5);
		public GameObject exit;
		public GameObject[] floorTiles;
		public GameObject[] wallTiles;
		public GameObject[] foodTiles;
		public GameObject[] sodaTiles;
		public GameObject[] enemyTiles;
		public GameObject[] outerWallTiles;
		public GameObject[] pushWallTiles;

		private Transform boardHolder;
		private List <Vector3> gridPositions = new List <Vector3> ();


		void InitialiseList ()
		{
			gridPositions.Clear ();

			for(int x = 1; x < columns-1; x++)
			{
				for(int y = 1; y < rows-1; y++)
				{
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}


		void BoardSetup ()
		{
			boardHolder = new GameObject ("Board").transform;

			for(int x = -1; x < columns + 1; x++)
			{
				for(int y = -1; y < rows + 1; y++)
				{
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

					if(x == -1 || x == columns || y == -1 || y == rows)
						toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];

					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

					instance.transform.SetParent (boardHolder);
				}
			}
		}


		Vector3 RandomPosition ()
		{
			Vector3 v = new Vector3 (1,0,0);
			gridPositions.RemoveAt (1);
			return v;
		}
    void walls(GameObject[] tileArray){
			GameObject tileChoice;
			Vector3 pos;
			if (GameManager.level == 1) {
				
				makeRow (2, 9, 1, tileArray);
				makeRow (0, 4, 7, tileArray);
				makeCol (2, 4, 8, tileArray);
				makeCol (1, 2, 12, tileArray);
				makeCol (1, 5, 11, tileArray);
				makeRow (2, 5, 4, tileArray);
				makeRow (6, 9, 4, tileArray);
				makeCol (6, 8, 5, tileArray);

			}
			else if (GameManager.level == 2) {
				makeRow (0, 3, 6, tileArray);
				makeCol (1, 6, 2, tileArray);
				makeRow (3, 5, 1, tileArray);
				makeCol (1, 3, 4, tileArray);
				makeCol (4, 7, 4, tileArray);
				makeCol (1, 4, 6, tileArray);
				makeCol (5, 7, 6, tileArray);
				makeCol (4, 6, 8, tileArray);
				makeRow (9, 11, 5, tileArray);
				makeCol (2, 6, 10, tileArray);
			}
		}
		void makeCol(int start, int end,int col,  GameObject[] tileArray){
			GameObject tileChoice;
			Vector3 pos;
			for(int i = start; i < end; i++){
				tileChoice = tileArray [Random.Range (0, tileArray.Length)];
				pos = new Vector3 (col, i, 1);
				Instantiate (tileChoice, pos, Quaternion.identity);
			}
		}
		void makeRow(int start, int end,int row,  GameObject[] tileArray){
			GameObject tileChoice;
			Vector3 pos;
			for(int i = start; i < end; i++){
				tileChoice = tileArray [Random.Range (0, tileArray.Length)];
				pos = new Vector3 (i, row, 1);
				Instantiate (tileChoice, pos, Quaternion.identity);
			}
		}
		void plus5(GameObject[] tileArray){
			if (GameManager.level == 1) {
				makeCol (3, 4, 7, tileArray);
				makeCol (7, 8, 6, tileArray);
				makeCol (4, 5, 9, tileArray);
				makeCol (5, 6, 0, tileArray);
			}else if (GameManager.level == 2) {
				makeCol (7, 8, 6, tileArray);
				makeCol (4, 5, 9, tileArray);
				makeCol (2, 3, 3, tileArray);
				makeCol (3, 4, 5, tileArray);
			}
		}
		void set5(GameObject[] tileArray){
			if (GameManager.level == 1) {
				makeCol (7, 8, 4, tileArray);
			}else if (GameManager.level == 2) {
				makeCol (7, 8, 3, tileArray);
			}
		}
		void pushWall(GameObject[] tileArray){
			
			if (GameManager.level == 1) {
				GameObject tileChoice = tileArray [0];
				Vector3 pos = new Vector3 (5, 2, 2);
				Instantiate (tileChoice, pos, Quaternion.identity);
			} else if (GameManager.level == 2) {
				GameObject tileChoice = tileArray [0];
				Vector3 pos = new Vector3 (6, 4, 2);
				Instantiate (tileChoice, pos, Quaternion.identity);
			}
		}
		void exitPos(GameObject tile){
			if (GameManager.level == 1) {
				GameObject tileChoice = tile;
				Vector3 pos = new Vector3 (12, 7, 2);
				Instantiate (tileChoice, pos, Quaternion.identity);
			}else if (GameManager.level == 2) {
				GameObject tileChoice = tile;
				Vector3 pos = new Vector3 (12, 7, 2);
				Instantiate (tileChoice, pos, Quaternion.identity);
			}
		}
		public void SetupScene (int level)
		{
			BoardSetup ();
			InitialiseList ();
			//LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
			walls(wallTiles);
			plus5(foodTiles);
			set5(sodaTiles);
			pushWall(pushWallTiles);
			exitPos(exit);
			//LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);

			//Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
		}
	}
}
