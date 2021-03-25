using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float speed;

	[SerializeField] private Transform raySource;
	[SerializeField] private float distanceRayForEdge;
	[SerializeField] private float distanceRayForGround;
	[SerializeField] private LayerMask whatIsGround;
	private Rigidbody2D rb;
	private int direction = 1;
	[SerializeField] private bool isApplyMagic;

	Collider2D collider;





	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		collider = GetComponent<Collider2D>();
	}
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (isApplyMagic)
		{
			return;
		}

		Moving();
	}

	private void Moving()
	{
		RaycastHit2D isEdge = Physics2D.Raycast(raySource.position, -transform.up, distanceRayForEdge, whatIsGround);
		RaycastHit2D isGrounded = Physics2D.Raycast(transform.position, -transform.up, distanceRayForGround, whatIsGround);

		if (!isEdge && isGrounded)
		{
			direction = -direction;
			Flip(direction);
		}

		rb.velocity = new Vector2(direction * speed, rb.velocity.y);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawRay(raySource.position, -transform.up * distanceRayForEdge);
	}

	void Flip(int scaleX)
	{
		transform.localScale = new Vector3(scaleX, 1, 1);
	}

	public void ApplyMagic(bool isApplyMagic)
	{
		this.isApplyMagic = isApplyMagic;


		if (isApplyMagic)
		{
			rb.gravityScale = 0;
			collider.enabled = false;
		}
		else
		{

			rb.gravityScale = 1;
			collider.enabled = true;
		}


	}

	public void MovingUnderMagnet(Vector2 positionTarget)
	{
		Vector2 direction = positionTarget - (Vector2)transform.position;

		if (Vector2.Distance(positionTarget, (Vector2)transform.position) < 0.1f)
		{
			rb.velocity = Vector2.zero;
			return;
		}

		rb.velocity = direction.normalized  * 2f;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawRay(transform.position, -transform.up * distanceRayForGround);
	}

}
