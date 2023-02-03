using System;
using System.Collections;
using System.Collections.Generic;
using EditorLogics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using TMPro;

/// <summary>
/// Map interactions
/// </summary>
public class MapInteractions : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public GameObject Background;
    public GameObject CollideMap;
    public GameObject TempImage;
    public GameObject ColliderImage;
    public Sprite EraserImage;
    public Sprite RotationImage;
    public Sprite DraggingImage;

    // Editing tools
    public ButtonGroup Tools;
    /**
     * -1 represent normal 
     * 0 represent object
     * 1 represent collider
     * 2 represent eraser
     * 3 represent rotation
     * 4 represent drag
     */
    public int ObjectType = -1;

    //Singleton
    public static MapInteractions Instance;

    //Map dragging
    private Vector3 MouseIniPos;
    void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    /// <summary>
    /// Handle dragging and temp image following
    /// </summary>
    void Update()
    {
        if (TempImage.GetComponent<Image>().sprite != null)
        {
            TempImage.transform.position = Input.mousePosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(1) || (Input.GetMouseButton(0) && ObjectType == 4))
        {
            Vector3 diff = Input.mousePosition - MouseIniPos;
            Background.transform.position += diff;
            CollideMap.transform.position += diff;
            MouseIniPos = Input.mousePosition;
        } else if (Input.GetMouseButton(0) && ObjectType == 1)
        {
            EditMap.Instance.AddCollider();
        }
    }

    /// <summary>
    /// Start dragging on pointer down & Add collider and object on pointer down
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(1))
        {
            MouseIniPos = Input.mousePosition;
        } else if (ObjectType == 0 && Input.GetMouseButtonDown(0) && TempImage.GetComponent<Image>().sprite != null)
        {
            EditMap.Instance.AddObject();
        } else if (ObjectType == 4 && Input.GetMouseButtonDown(0)){
            MouseIniPos = Input.mousePosition;
        }
    }

    #region Button Group
    /// <summary>
    /// Clear temp image(exit object or collider adding)
    /// </summary>
    public void ClearTempImage()
    {
        TempImage.GetComponent<Image>().sprite = null;
        TempImage.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }
    /// <summary>
    /// Switch to nothing
    /// </summary>
    public void SetToNormal(){
        ObjectType = -1;
        ClearTempImage();
        CollideMap.SetActive(false);
    }

    /// <summary>
    /// Switch to collider adding tool
    /// </summary>
    public void SetToCollider()
    {
        ObjectType = 1;
        TempImage.GetComponent<Image>().sprite = ColliderImage.GetComponent<Image>().sprite;
        TempImage.GetComponent<Image>().color = ColliderImage.GetComponent<Image>().color;
        TempImage.GetComponent<Image>().rectTransform.sizeDelta = ColliderImage.GetComponent<Image>().rectTransform.sizeDelta;
        CollideMap.SetActive(true);
    }

    /// <summary>
    /// Switch to eraser tools
    /// </summary>
    public void SetToEraser(){
        ObjectType = 2;
        ClearTempImage();
        TempImage.GetComponent<Image>().sprite = EraserImage;
        TempImage.GetComponent<Image>().color = Color.white;
        TempImage.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
        CollideMap.SetActive(true);

    }

    public void SetToRotate(){
        ObjectType = 3;
        TempImage.GetComponent<Image>().sprite = RotationImage;
        TempImage.GetComponent<Image>().color = Color.white;
        TempImage.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
        CollideMap.SetActive(false);
    }

    public void SetToDrag(){
        ObjectType = 4;
        TempImage.GetComponent<Image>().sprite = DraggingImage;
        TempImage.GetComponent<Image>().color = Color.white;
        TempImage.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
        CollideMap.SetActive(false);
    }

    #endregion
}
