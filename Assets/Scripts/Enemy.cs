using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform[] points;
	public float speedAttack;
	public float speedIdle;
	public float cooldownTime;
	public float cooldownDistance;

	private float speed;
	private int currentIndex = 0;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rb2d;
	private Vector3 target;
	private Transform player;
	private bool cooldown;
	private Animator animator;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	public void FixedUpdate()
	{
		if (player != null)
		{
			if (cooldown)
			{
				target = player.transform.position + Vector3.up * cooldownDistance;
				speed = speedIdle;
			}
			else
			{
				target = player.transform.position;
				speed = speedAttack;
			}
		}
		else
		{
			target = points[currentIndex].position;
			if (transform.position == target)
			{
				currentIndex++;
				if (currentIndex == points.Length)
				{
					currentIndex = 0;
				}
			}
			speed = speedIdle;
		}

		transform.position = Vector3.MoveTowards(transform.position, target, speed);
		

		if (transform.position.x < target.x)
		{
			spriteRenderer.flipX = true;
		}
		else if (transform.position.x > target.x)
		{
			spriteRenderer.flipX = false;
		}

		rb2d.velocity = Vector2.zero;
	}

	public void Update()
	{
		animator.SetBool("IsAttacking", player != null);
	}

	private IEnumerator CooldownTimer()
	{
		yield return new WaitForSeconds(cooldownTime);
		cooldown = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			//damage
			if (!cooldown) 
			{
				cooldown = true;
				StartCoroutine(CooldownTimer());
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			player = collision.transform;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			player = null;
		}
	}
}
