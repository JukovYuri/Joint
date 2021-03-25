using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    public Transform source;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatIsMirror;


    [SerializeField] private Texture[] textures;
    [SerializeField] private LineRenderer lineMagnet;
    [SerializeField] private float timeChangeSprite = 0.1f;
    private float timeCounter;
    private int texturesCounter;
    [SerializeField] private Rigidbody2D rbMirror;



    void Start()
    {
        lineMagnet.enabled = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionMouse.z = 0;
            Vector3 direction = positionMouse - source.position;
            RaycastHit2D hit = Physics2D.Raycast(source.position, direction, distance, whatIsMirror);
            if (hit)
            {
                rbMirror = hit.collider.attachedRigidbody;
                rbMirror.GetComponent<Mirror>().ApplyMagic(true);         
            }
        }

        if (Input.GetKey(KeyCode.R) && rbMirror)
        {
            lineMagnet.enabled = true;

            lineMagnet.SetPosition(0, source.position);
            lineMagnet.SetPosition(1, rbMirror.transform.position);

            MagnetAnimation();
        }

        if (Input.GetKeyUp(KeyCode.R) && rbMirror)
        {
            rbMirror.GetComponent<Mirror>().ApplyMagic(false);
            rbMirror = null;
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
