using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private GameObject point = null;

    private Admin admin = null;

    // Start is called before the first frame update
    private void Awake()
    {
        Point.SIGNALAddedPoint += SubscibeSignalAddPoint;
        Point.SIGNALDestroyPoint += SubscibeSignalDestroyPoint;
    }

    private void OnDestroy()
    {
        Point.SIGNALAddedPoint -= SubscibeSignalAddPoint;
        Point.SIGNALDestroyPoint -= SubscibeSignalDestroyPoint;
    }

    void Start()
    {
        admin = Resources.Load<Admin>("Admin");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(point != null)
        {
            
            MoveTo(point.transform.position, admin.speed);
            LookAt2D(point.transform.position, Vector2.up);

        }
    }

    private void LookAt2D(Vector2 at, Vector2 see)
    {
        Vector2 position = transform.position;
        Vector2 startVector =  at - position;
        float angle = Vector2.SignedAngle(startVector, see) * -1;
        transform.rotation = Quaternion.Euler(0, 0, angle);   
    }

    private void MoveTo(Vector3 to, float speed)
    {
        speed /= 1000;
        Vector3 direction = (transform.position - to).normalized;
        if((to - transform.position).magnitude < speed)
        {
            transform.position = Vector2.Lerp(transform.position
                , to, speed);
        }
        else
        {
            transform.position -= direction * speed;
        }


    }

    private void SubscibeSignalAddPoint()
    {
        if(point == null)
        {
            SetPoint(Point.points[0]);
            
        }
    }
    private void SubscibeSignalDestroyPoint()
    {
        if(Point.points.Count != 0)
        {
            SetPoint(Point.points[0]);

        }
    }

    private void SetPoint(GameObject point)
    {
        this.point = point;
        point.GetComponent<Point>()
                .SetTransformToMoveTheLine(transform);

    }
    public GameObject GetPoint()
    {
        return this.point;
    }

    
}