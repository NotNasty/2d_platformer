using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed;
	public float deadZone;

	private void Start()
	{
		if (!IsInDeadZone())
		{
			transform.position = GetTargetPosition();
		}
	}

	private void Update()
	{
		if (!IsInDeadZone())
		{
			Vector3 targetPosition = GetTargetPosition();
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
		}
	}

	private bool IsInDeadZone()
	{
		return Vector3.Distance(transform.position, target.position) <= deadZone;
	}

	private Vector3 GetTargetPosition()
	{
		return new Vector3(target.position.x, transform.position.y, transform.position.z);
	}
}
