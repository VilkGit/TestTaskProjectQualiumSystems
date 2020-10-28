using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventSystemUI : StandaloneInputModule
{
    public PointerEventData GetLastPointerEventDataPublic(int id)
    {
        return GetLastPointerEventData(id);
    }
}