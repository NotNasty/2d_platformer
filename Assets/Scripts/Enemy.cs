using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] points;
	public float speed;

	private int currentIndex = 0;
	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards(transform.position, points[currentIndex].position, speed);
		if (transform.position == points[currentIndex].position)
		{
			currentIndex++;
			if (currentIndex == points.Length)
			{
				currentIndex = 0;
			}
		}

		if (transform.position.x < points[currentIndex].position.x)
		{
			spriteRenderer.flipX = true;
		}
		else if (transform.position.x > points[currentIndex].position.x)
		{
			spriteRenderer.flipX = false;
		}
	}
}
