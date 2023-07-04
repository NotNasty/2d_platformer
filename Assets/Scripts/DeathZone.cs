using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
	public Player player;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		player.IsInDeathZone = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		player.IsInDeathZone = false;
	}
}
