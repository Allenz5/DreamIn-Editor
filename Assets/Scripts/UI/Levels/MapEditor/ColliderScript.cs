using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Collider script
/// </summary>
public class ColliderScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public GameObject CurrentObject;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && MapInteractions.Instance.ObjectType == 2)
        {
            EditMap.Instance.RemoveCollider(CurrentObject);
            Destroy(CurrentObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && MapInteractions.Instance.ObjectType == 2)
        {
            EditMap.Instance.RemoveCollider(CurrentObject);
            Destroy(CurrentObject);
        }
    }
}
