using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Transform source;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatIsEnemy;


    [SerializeField] private Texture [] textures;
    [SerializeField] private LineRenderer lineMagnet;
    [SerializeField] private float timeChangeSprite;
    private float timeCounter;
    private int texturesCounter;
    [SerializeField] private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        lineMagnet.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionMouse.z = 0;
            Vector3 direction = positionMouse - source.position;
            RaycastHit2D hit = Physics2D.Raycast(source.position, direction, distance, whatIsEnemy);
            if (hit)
            {
                rb = hit.collider.attachedRigidbody;
                rb.GetComponent<Enemy>().ApplyMagic(true);
            }
        }

        if (Input.GetKey(KeyCode.R) && rb)
        {
            rb.GetComponent<Enemy>().MovingUnderMagnet(new Vector2 (source.position.x + 2f, source.position.y));
            print(rb.velocity);

            lineMagnet.enabled = true;

            lineMagnet.SetPosition(0, source.position);
            lineMagnet.SetPosition(1, rb.transform.position);

            MagnetAnimation();
        }

        if (Input.GetKeyUp(KeyCode.R) && rb)
        {
            rb.GetComponent<Enemy>().ApplyMagic(false);

            rb = null;
            lineMagnet.enabled = false;
        }

    }


    void MagnetAnimation()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= timeChangeSprite)
        {
            lineMagnet.material.SetTexture("_MainTex", textures[texturesCounter]);
            texturesCounter++;
            if (texturesCounter == textures.Length)
            {
                texturesCounter = 0;
            }
            timeCounter = 0f;
        }

    }
}
