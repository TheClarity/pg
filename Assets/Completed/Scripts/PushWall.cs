using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;		//Allows us to use SceneManager
namespace Completed
{
	public class PushWall : MonoBehaviour
	{
		public float restartLevelDelay = .1f;	

		private SpriteRenderer spriteRenderer;		//Store a component reference to the attached SpriteRenderer.


		void Awake ()
		{
			//Get a component reference to the SpriteRenderer.
			spriteRenderer = GetComponent<SpriteRenderer> ();
		}


		//DamageWall is called when the player attacks a wall.
		public static void MoveWall ()
		{
			
		}
		private void OnTriggerEnter2D (Collider2D other)
		{
			//Check if the tag of the trigger collided with is Exit.
			if(other.tag == "Exit")
			{
				//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
				Invoke ("Restart", 0);

				//Disable the player object since level is over.
				enabled = false;
			}
		}
		private void Restart ()
		{
			//Load the last scene loaded, in this case Main, the only scene in the game.
			SceneManager.LoadScene (0);
		}
	}
}
