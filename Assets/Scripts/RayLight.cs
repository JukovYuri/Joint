using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLight : MonoBehaviour
{
	[SerializeField] LayerMask whatIsMirror;

	Vector2 startPoint, direction;
	List<Vector3> points = new List<Vector3>();
	int distance = 100;
	[SerializeField] private LineRenderer lr;


	private float timeCounter;
	private float timeChangeSprite = 0.1f;
	public Texture[] textures;
	private int animationStep;

	void Start()
	{
		lr.enabled = true;
		startPoint = transform.position;
		direction = -transform.up;
	}

	private void Update()
	{
		RayAnimation();

		var hitData = Physics2D.Raycast(startPoint, direction, distance, whatIsMirror);

		points.Clear();
		points.Add(startPoint);

		if (hitData)
		{
			ReflectFurther(startPoint, hitData);
		}
		else
		{
			points.Add(startPoint + direction * distance);
		}

		lr.positionCount = points.Count;
		lr.SetPositions(points.ToArray());
	}

	private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
	{

		points.Add(hitData.point);

		Vector2 inDirection = (hitData.point - origin).normalized;
		Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

		var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.001f), newDirection, distance, whatIsMirror);
		if (newHitData)
		{
			if (newHitData.collider.CompareTag("MirrorEnd"))
			{
				points.Add(newHitData.point);

				newHitData.collider.GetComponent<SpriteRenderer>().color = Color.red;
				return;
			}

			ReflectFurther(hitData.point, newHitData);
			
		}
		else
		{
			points.Add(hitData.point + newDirection * distance);
		}
	}

	private void RayAnimation()
	{
		timeCounter += Time.deltaTime;

		if (timeCounter >= timeChangeSprite)
		{
			animationStep++;
			if (animationStep == textures.Length)
			{
				animationStep = 0;
			}

			lr.material.SetTexture("_MainTex", textures[animationStep]);

			timeCounter = 0f;
		}
	}
}



