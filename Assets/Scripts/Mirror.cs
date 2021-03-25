using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private Transform raySource;

    [SerializeField] private bool isApplyMagic;
    [SerializeField] private GameObject mirror;
    Rigidbody2D rb;
    Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>(); 
    }

    private void Start()
    {
        rb = collider.attachedRigidbody;
    }


    void Update()
    {
        if (isApplyMagic)
        {
            RotateUnderMagnet();
            MovingUnderMagnet(new Vector2(raySource.position.x + 1f, raySource.position.y));
        }

    }

    private void RotateUnderMagnet()
    {
        mirror.transform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * 20f);
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

        rb.velocity = direction.normalized * 2f;
    }

}
