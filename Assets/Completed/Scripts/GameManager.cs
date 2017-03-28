using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;		//Allows us to use SceneManager


namespace Completed
{
	using System.Collections.Generic;		//Allows us to use Lists.
	using UnityEngine.UI;					//Allows us to use UI.


	public class GameManager : MonoBehaviour
	{
		public float levelStartDelay = 2f;
		public float turnDelay = 0.1f;
		public int playerFoodPoints = 15;
		public static GameManager instance = null;
		[HideInInspector] public bool playersTurn = true;


		public static Text levelText;
		public static GameObject levelImage;
		private BoardManager boardScript;
		public static int level = 0;
		private List<Enemy> enemies;
		private bool enemiesMoving;
		private bool doingSetup = true;


		void Awake()
		{
			if (instance == null)

				instance = this;

			else if (instance != this)

				Destroy(gameObject);

			DontDestroyOnLoad(gameObject);

			enemies = new List<Enemy>();

			boardScript = GetComponent<BoardManager>();

			//InitGame();
		}

		void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
		{
			level++;
			InitGame();
		}

		void OnEnable()
		{
			SceneManager.sceneLoaded += OnLevelFinishedLoading;
		}

		void OnDisable()
		{
			//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change event as soon as this script is disabled.
			//Remember to always have an unsubscription for every delegate you subscribe to!
			SceneManager.sceneLoaded -= OnLevelFinishedLoading;
		}

		//Initializes the game for each level.
		void InitGame()
		{
			if (level == 1) {
				playerFoodPoints = 30;
			} else if (level == 2) {
				playerFoodPoints = 30;
			}
			doingSetup = true;

			levelImage = GameObject.Find("LevelImage");

			levelText = GameObject.Find("LevelText").GetComponent<Text>();

			levelText.text = "Stage " + level;
			levelImage.SetActive(true);

			Invoke("HideLevelImage", levelStartDelay);

			enemies.Clear();

			boardScript.SetupScene(level);

		}


		void HideLevelImage()
		{
			levelImage.SetActive(false);

			doingSetup = false;
		}

		//Update is called every frame.
		void Update()
		{
			if(playersTurn || enemiesMoving || doingSetup)

				return;

			StartCoroutine (MoveEnemies ());
		}

		public void AddEnemyToList(Enemy script)
		{
			enemies.Add(script);
		}


		public void GameOver()
		{
			//levelText.text = "You Died on stage " + level + " :(";
			//levelImage.SetActive(true);

			//level -= 1;

			enabled = false;
		}
		private void Restart ()
		{
			SceneManager.LoadScene (0);
		}
		IEnumerator MoveEnemies()
		{
			enemiesMoving = true;

			yield return new WaitForSeconds(turnDelay);

			if (enemies.Count == 0)
			{
				yield return new WaitForSeconds(turnDelay);
			}

			for (int i = 0; i < enemies.Count; i++)
			{
				enemies[i].MoveEnemy ();

				yield return new WaitForSeconds(enemies[i].moveTime);
			}
			playersTurn = true;

			enemiesMoving = false;
		}
	}
}
