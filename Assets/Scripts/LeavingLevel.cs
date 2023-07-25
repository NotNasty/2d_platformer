using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeavingLevel : MonoBehaviour
{
	public bool loadMainMenu;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (loadMainMenu)
			{
				SceneManager.LoadScene(0);
			}
			else
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
	}
}
