using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelControl : MonoBehaviour
{
    private GameObject prefPoint = null;
    private Admin admin = null;

    void Start()
    {
        admin = Resources.Load<Admin>("Admin");
        prefPoint = admin.prefPoint;
    }

    void Update()
    {
        TouchScreen();
    }
    private void TouchScreen()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                PointerEventData eventData = GetPED(t.fingerId);
                if (t.phase == TouchPhase.Began 
                    && eventData.pointerEnter == gameObject)
                {
                    RaycastHit2D hit = DropRay(t.position);
                    if (hit.collider == null)
                    {
                        Point.AddPoint(
                            PositionToWorldFromCamera(
                                Camera.main, t.position)
                            , prefPoint);
                    }

                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {

            PointerEventData eventData = GetPED(-1);
            if (eventData.pointerEnter != this.gameObject) return;

            RaycastHit2D hit = DropRay(Input.mousePosition);
            if (hit.collider == null)
            {
                Point.AddPoint(
                    PositionToWorldFromCamera(
                        Camera.main, Input.mousePosition)
                    , prefPoint);
            }
        }

    }

    private RaycastHit2D DropRay(Vector2 positionScreen)
    {
        Ray ray = Camera.main.ScreenPointToRay(positionScreen);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        return hit;
    }

    private Vector3 PositionToWorldFromCamera(
        Camera camera,
        Vector3 vecPos
        )
    {
        Vector3 localVecPos = vecPos;
        localVecPos.z -= camera.transform.position.z;
        Vector3 posInWorld = camera.ScreenToWorldPoint(localVecPos);

        return posInWorld;
    }
    private PointerEventData GetPED(int id)
    {
        return EventSystem.current.gameObject.
            GetComponent<EventSystemUI>().
            GetLastPointerEventDataPublic(id);
    }
}


