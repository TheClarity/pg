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
		public float turnDelay = 1f;
		public int playerFoodPoints = 15;
		public static GameManager instance = null;
		[HideInInspector] public bool playersTurn = false;


		public static Text levelText;
		public static GameObject levelImage;
		private BoardManager boardScript;
		public static int level = 0;
		private bool enemiesMoving;
		public bool doingSetup = true;


		void Awake()
		{
			if (instance == null)

				instance = this;

			else if (instance != this)

				Destroy(gameObject);

			DontDestroyOnLoad(gameObject);


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
			playersTurn = false;
			if (level == 1) {
				playerFoodPoints = 30;
			} else if (level == 2) {
				playerFoodPoints = 30;
			}
			else if (level == 3) {
				playerFoodPoints = 35;
			}
			if (level < 4) {
				doingSetup = true;

				levelImage = GameObject.Find ("LevelImage");

				levelText = GameObject.Find ("LevelText").GetComponent<Text> ();

				levelText.text = "Stage " + level;
				levelImage.SetActive (true);

				Invoke ("HideLevelImage", levelStartDelay);

				boardScript.SetupScene (level);
			} else {

				levelImage = GameObject.Find ("LevelImage");

				levelText = GameObject.Find ("LevelText").GetComponent<Text> ();

				levelText.text = "That's all for now, Thanks for playing!";
				levelImage.SetActive (true);
				restartGame ();
			}
		}

		void restartGame(){
			level = 0;
			Invoke ("Restart", 5f);
		}
		void HideLevelImage()
		{
			levelImage.SetActive(false);
			doingSetup = false;
			playersTurn = true;
		}

		//Update is called every frame.
		void Update()
		{
			if(playersTurn || enemiesMoving || doingSetup)

				return;

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
	}
}
