using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Object Script
/// </summary>
public class ObjectScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public string message = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && MapInteractions.Instance.ObjectType == 2)
        {
            Remove();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && MapInteractions.Instance.ObjectType == 2)
        {
            Remove();
        }
    }

    /// <summary>
    /// Destroy this object
    /// </summary>
    private void Remove(){
        EditMap.Instance.RemoveObject(gameObject);
        Destroy(gameObject, 0);
    }

    private void ShowInfo(){

    }

    private void Rotate(){

    }
}
