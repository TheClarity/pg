using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;		//Allows us to use SceneManager


namespace Completed
{
	public class Player : MovingObject
	{
		public float restartLevelDelay = .1f;
		public int pointsPerFood = 10;
		public int pointsPerSoda = 20;
		public int wallDamage = 1;
		public Text foodText;	
		private Animator animator;
		private int food;

		protected override void Start ()
		{
			animator = GetComponent<Animator>();
			food = GameManager.instance.playerFoodPoints;
			foodText.text = "Steps: " + food;
			base.Start ();
		}

		private void OnDisable ()
		{
			GameManager.instance.playerFoodPoints = food;
		}


		private void Update ()
		{
			if(!GameManager.instance.playersTurn) return;

			int horizontal = 0;
			int vertical = 0;

			#if UNITY_STANDALONE || UNITY_WEBPLAYER

			horizontal = (int) (Input.GetAxisRaw ("Horizontal"));

			vertical = (int) (Input.GetAxisRaw ("Vertical"));

			if(horizontal != 0)
			{
				vertical = 0;
			}
			#endif
			if(horizontal != 0 || vertical != 0)
			{
				AttemptMove<PushWall> (horizontal, vertical);
			}
			if (Input.GetKeyUp(KeyCode.R))
			{
				GameManager.level -= 1;
				Invoke ("Restart", 0);
			}
			else if (Input.GetKeyUp(KeyCode.Alpha1))
			{
				GameManager.level = 0;
				Invoke ("Restart", 0);
			}
			else if (Input.GetKeyUp(KeyCode.Alpha2))
			{
				GameManager.level = 1;
				Invoke ("Restart", 0);
			}
			else if (Input.GetKeyUp(KeyCode.Alpha3))
			{
				GameManager.level = 2;
				Invoke ("Restart", 0);
			}
		}

		protected override void AttemptMove <T> (int xDir, int yDir)
		{
			if (GameManager.instance.playersTurn && GameManager.levelImage.active == false) {
				
				food--;

				foodText.text = "Steps: " + food;
				base.AttemptMove <T> (xDir, yDir);

				RaycastHit2D hit;

				if (Move (xDir, yDir, out hit)) {
				}

				CheckIfGameOver ();


				GameManager.instance.playersTurn = false;
				Invoke ("setTrue", .2f);

			}
		}
		private void setTrue(){
			GameManager.instance.playersTurn = true;
		}

		protected override void OnCantMove <T> (T component)
		{
			
		}


		private void OnTriggerEnter2D (Collider2D other)
		{

			 if(other.tag == "Food")
			{
				food += pointsPerFood;
				foodText.text = "+" + pointsPerFood + " Steps: " + food;

				other.gameObject.SetActive (false);
			}

			else if(other.tag == "Soda")
			{
				food = pointsPerSoda;

				foodText.text = "=" + pointsPerSoda + " Steps: " + food;

				other.gameObject.SetActive (false);
			}

			else if(other.tag == "PushWall"){

				food = 100;
				foodText.text =  "Steps: " + food;
			}
		}
		private void Restart ()
		{
			GameManager.instance.playersTurn = false;
			SceneManager.LoadScene (0);

		}
		public void LoseFood (int loss)
		{
			animator.SetTrigger ("playerHit");
			food -= loss;
			foodText.text = "-"+ loss + " Steps: " + food;
			CheckIfGameOver ();
		}
		private void CheckIfGameOver ()
		{
			if (food <= 0)
			{
				//GameManager.canMove = false;
				GameManager.levelText.text = "You Died On Level " + GameManager.level + " :(";
				GameManager.levelImage.SetActive(true);
				GameManager.level -= 1;
				Invoke ("Restart", 1);
			}
		}
	}
}
