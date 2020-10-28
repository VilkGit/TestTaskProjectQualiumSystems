using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//A class that implements the logic of the behavior of the point
// to which the triangles move
public class Point : MonoBehaviour
{
    //delegate that is called when creating a new point
    public delegate void AddedPoint();
    static public AddedPoint SIGNALAddedPoint;

    //A delegate that is called when a point is destroyed,
    // in particular when a triangle collides with a point
    public delegate void DestroyPoint();
    static public DestroyPoint SIGNALDestroyPoint;
    
    //stores previously created points
    static public List<GameObject> points = new List<GameObject>();

    // stores the transformation of the point 
    //that follows the triangles
    private Transform transofrmToMoveTheLine = null;
    
    // create a new point on the stage
    static public void AddPoint(Vector2 positionToWorld
        , GameObject prefPoint)
    {
        GameObject point = Instantiate<GameObject>(prefPoint
            , positionToWorld
            , Quaternion.Euler(0, 0, 0));

        AddLine(point);
        points.Add(point);     
        

        if (SIGNALAddedPoint != null)
        {
            SIGNALAddedPoint();
        }
    }

    //create a new LineRenderer for new point
    static private void AddLine(GameObject point)
    {
        LineRenderer line = point.AddComponent<LineRenderer>();
        line.material = Resources.Load<Admin>("Admin").lineMaterial;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.textureMode = LineTextureMode.Tile;
        line.sortingOrder = -1;

        line.SetPosition(1, point.transform.position);
        if (points.Count != 0)
        {
            line.SetPosition(0, points[points.Count - 1]
                .transform.position);
        }
        else
        {
            line.SetPosition(0, point.transform.position);
        }
        
    }

    private void Update()
    {
        if(transofrmToMoveTheLine != null)
        {
            MoveLinePoint();
        }
    }

    // Deleted the point when triangle touch the point
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Arrow>() != null)
        {
            if (collision.GetComponent<Arrow>()
                .GetPoint() == this.gameObject)
            {
                Destroy(gameObject);               
            }
        }
    }

    // Overrides the point for the line and removes 
    //from points the entry also removes the signature 
    //to the delegate
    private void OnDestroy()
    {
        for(int i = 1; i < points.Count - 1; i++)
        {
            if (points[i] == gameObject)
            {
                LineRenderer line = points[i + 1]
                    .GetComponent<LineRenderer>();
                
                line.SetPosition(0
                    , points[i - 1].transform.position);
                break;
            }
        }
        points.Remove(gameObject);
        
        if (SIGNALDestroyPoint != null)
        {
            SIGNALDestroyPoint();
        }
    }

    // moves line by triangle 
    private void MoveLinePoint()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        if (line != null)
        {
            line.SetPosition(0, transofrmToMoveTheLine.position);
        }
    }

    public void SetTransformToMoveTheLine(Transform transform)
    {
        transofrmToMoveTheLine = transform;
    }
}
