using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tranpoline : MonoBehaviour
{
	public float jumpMultiplier;

	private const string PARAMETER_JUMPING = "IsJumping";

	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();	
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<Player>().JumpOnTrampoline(jumpMultiplier);
			animator.SetBool(PARAMETER_JUMPING, true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			animator.SetBool(PARAMETER_JUMPING, false);
		}
	}
}
