using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
	public float maxHealth;
	public float healthVisualSpeed;
	public float coins;
	public float speed;
	private float addForce;
	public float addForceMax;
	public float jumpForce;
	public float dashForce;
	public float sneakCoefficient;
	public float shiftCoefficient;
	public float animationSlowDown;
	public GameObject bulletPrefab;

	public TMP_Text coinsText;
	public Image healthImage;

	public bool IsInDeathZone { get; set; }

	private const string PARAMETER_IS_GROUNDED = "IsGrounded";
	private const string PARAMETER_MAGNITUDE = "Magnitude";
	private const string GROUND_TAG_NAME = "Ground";
	private const float defaultAnimationSpeed = 1;

	private float health;
	private float healthVisual;
	private bool isGrounded;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rb2d;
	private Animator animator;
	private Vector3 defaultPosition;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		defaultPosition = transform.position;
		health = maxHealth;
		healthVisual = health;
	}

	private void Update()
	{
		if (IsInDeathZone)
		{
			transform.position = defaultPosition;
			return;
		}

		if (Input.GetKeyDown(KeyCode.W) && isGrounded)
		{
			rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			addForce = addForceMax;
			StartCoroutine(addForceTimer());
			rb2d.AddForce(transform.up * dashForce, ForceMode2D.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			addForce = -addForceMax;
			rb2d.AddForce(transform.up * dashForce, ForceMode2D.Impulse);
			StartCoroutine(addForceTimer());
			
		}

		if (Input.GetMouseButtonDown(0))
		{
			GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
			newBullet.GetComponent<Bullet>().targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		if (Input.GetAxis("Horizontal") > 0)
		{
			spriteRenderer.flipX = false;
		}
		else if (Input.GetAxis("Horizontal") < 0)
		{
			spriteRenderer.flipX = true;
		}

		animator.SetBool(PARAMETER_IS_GROUNDED, isGrounded);
		animator.SetFloat(PARAMETER_MAGNITUDE, rb2d.velocity.magnitude);

		coinsText.text = coins.ToString();

		healthVisual = Mathf.Lerp(healthVisual, health, healthVisualSpeed);
		healthImage.fillAmount = healthVisual / maxHealth;
	}

	private void FixedUpdate()
	{
		float currentSpeed;

		if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
		{
			currentSpeed = speed * sneakCoefficient;
			animator.speed = defaultAnimationSpeed * animationSlowDown;

		}
		else if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
		{
			currentSpeed = speed * shiftCoefficient;
			animator.speed = defaultAnimationSpeed;
		}
		else
		{
			currentSpeed = speed;
			animator.speed = defaultAnimationSpeed;
		}

		rb2d.velocity = new Vector2((Input.GetAxis("Horizontal") + addForce) * currentSpeed,
			rb2d.velocity.y);
	}

	public void Damage(float damage)
	{
		health -= damage;

		if (health <= 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private IEnumerator addForceTimer()
	{
		yield return new WaitForSeconds(0.2f);
		addForce = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(GROUND_TAG_NAME))
		{
			isGrounded = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag(GROUND_TAG_NAME))
		{
			isGrounded = false;
		}
	}

	public void JumpOnTrampoline(float jumpMultiplier)
	{
		rb2d.velocity = Vector2.zero;
		rb2d.AddForce(jumpForce * jumpMultiplier * transform.up, ForceMode2D.Impulse);
	}
}
