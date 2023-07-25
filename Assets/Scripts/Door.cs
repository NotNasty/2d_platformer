using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	public GameObject blackSquare;
	public float speedOfBlackSquare = 0.1f;

	private Animator _animator;
	private SpriteRenderer _blackSquareSprt;
	private bool hideBlackSquare;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		if (blackSquare != null )
		{
			_blackSquareSprt = blackSquare.GetComponent<SpriteRenderer>();
		}
		
	}

	private void Update()
	{
		if (_blackSquareSprt != null)
		{
			if (hideBlackSquare && _blackSquareSprt.color != Color.clear)
			{
				_blackSquareSprt.color = Color.Lerp(_blackSquareSprt.color, Color.clear, speedOfBlackSquare);
			}
			else if (!hideBlackSquare && _blackSquareSprt.color != Color.black)
			{
				_blackSquareSprt.color = Color.Lerp(_blackSquareSprt.color, Color.black, speedOfBlackSquare);
			}
		}
	}

		private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !collision.isTrigger)
		{
			_animator.SetBool("PlayerNear", true);
			hideBlackSquare = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !collision.isTrigger)
		{
			_animator.SetBool("PlayerNear", false);
			hideBlackSquare = false;
		}
	}
}
