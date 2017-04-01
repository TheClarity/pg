using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;		//Allows us to use SceneManager
namespace Completed
{
	public class PushWall : MonoBehaviour
	{
		public float restartLevelDelay = .1f;	

		private SpriteRenderer spriteRenderer;


		void Awake ()
		{
			spriteRenderer = GetComponent<SpriteRenderer> ();
		}
		public static void MoveWall ()
		{
			
		}
		private void OnTriggerEnter2D (Collider2D other)
		{
			if(other.tag == "Exit")
			{
				Invoke ("Restart", 0);

				enabled = false;
			}
		}
		private void Restart ()
		{
			SceneManager.LoadScene (0);
		}
	}
}
