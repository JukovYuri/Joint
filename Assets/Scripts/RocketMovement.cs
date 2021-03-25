using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketMovement : MonoBehaviour
{
    public int pointsNumbers;

    List<Vector2> points = new List<Vector2>();

    List<GameObject> pointsAim = new List<GameObject>();

    int pointIndex = 0;

    Vector2 currentPoint;
    Vector2 nextPoint;

    bool isCanStarted;

    Rigidbody2D rb;
    Vector2 dir;

    public GameObject prefabAimRocket;
    public GameObject prefabBoom;
    private bool isCanMove;
    private bool isCanFinished;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCanStarted)
        {
            print("начал");
            print("изменил курсор");
            // изменить курсор
        }


        if (Input.GetKey(KeyCode.R) && !isCanStarted)
        {

            if (Input.GetMouseButtonDown(0))
            {

                if (pointIndex == pointsNumbers - 1)
                {

                    points.Insert(0, transform.position);
                    isCanStarted = true;
                    print("все собрал");
                    print(points.Count);
                    return;
                }


                points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                GameObject clonText = Instantiate(prefabAimRocket, points[pointIndex], Quaternion.identity);
                pointsAim.Add(clonText);

                clonText.GetComponent<Text>().text = (pointIndex + 1).ToString();

                if (pointIndex == pointsNumbers - 2)
                {
                    clonText.GetComponent<Text>().color = Color.red;
                    clonText.GetComponentInChildren<Image>().color = Color.red;
                }
                else
                {
                    clonText.GetComponent<Text>().color = Color.cyan;
                    clonText.GetComponentInChildren<Image>().color = Color.cyan;
                }

                pointIndex++;

            }
        }

        if (Input.GetKeyUp(KeyCode.R) && !isCanStarted)
        {

            if (isCanMove  &&  !isCanFinished)
            {
                // вернуть курсор
                print("вернуть курсор");
                return;
            }



            foreach (var item in pointsAim)
            {
                Destroy(item);
            }
            points.Clear();
            pointsAim.Clear();

            isCanStarted = false;

            pointIndex = 0;
            print("очистил прервалось");
        }

        if (isCanStarted)
        {
            pointIndex = 0;
            print("запустил");

            isCanStarted = false;

            isCanMove = true;

        }

        if (isCanMove)
        {
            Move();
        }

        if (isCanFinished)
        {
            points.Clear();
            pointsAim.Clear();
            pointIndex = 0;
            Destroy(gameObject);

            isCanStarted = false;
            isCanMove = false;
            isCanFinished = false;

            print("все закончилось и очистилось");
        }


    }

    void Move()
    {
        SetPath();

        if (isCanFinished)
        {
            return;
        }


        float distanceCurrent = Vector2.Distance(transform.position, nextPoint);

        dir = nextPoint - currentPoint;
        transform.up = dir;
        rb.velocity = dir.normalized * 8f;


        if (distanceCurrent < 1f)
        {

              Destroy(pointsAim[pointIndex]);

        }

        if (distanceCurrent < 0.1f)
        {
            pointIndex++;
        }





        //float distance = Vector2.Distance(currentPoint, nextPoint);
        //float koeff = distanceCurrent / distance * distance;


    }

    void SetPath()
    {
        if (pointIndex == pointsNumbers-1)
        {
            rb.velocity = Vector2.zero;
            isCanMove = false;
            isCanFinished = true;
            print("бабах!");
            return;
        }

        currentPoint = points[pointIndex];
        nextPoint = points[pointIndex + 1];

    }

}
