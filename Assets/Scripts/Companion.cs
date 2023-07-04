using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
	public Player myMaster;
	public float speed;
	public float xDistanceFromPlayer;
	public float yDistanceFromPlayer;

	private Transform playerTransform;

	private void Start()
	{
		playerTransform = myMaster.gameObject.GetComponent<Transform>();
		transform.position = new Vector3(playerTransform.position.x + xDistanceFromPlayer,
			playerTransform.position.y + yDistanceFromPlayer, 0);
	}

	private void FixedUpdate()
	{
		Vector3 targetPosition = new Vector3(playerTransform.position.x + xDistanceFromPlayer,
			playerTransform.position.y + yDistanceFromPlayer, 0);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
	}
}
